using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileEditorController : MonoBehaviour {

    public Sprite DefaultTileSprite;
    public TileType Tile;
    public Transform TileViewerTransform;
    public TileViewerController TileViewer;
    public TileSetPanelController TileSetViewController;
    public InputField PixelPerUnitField;
    public InputField CategoryNameField;
    public Dropdown LayerDropDown;

    private TileButtonController _tileButtonController;
    private TileSet _currentSet;

    private void Start()
    {
        NewTileSet();
    }

    private void Draw()
    {
        TileViewer.Draw(Tile);
        if (_tileButtonController != null) _tileButtonController.Draw();
    }

    public void ChangeTileTexture()
    {
        var pngData = ImportWindow.ShowImageImportWindow();
        Tile.Sprite = TileType.GetSpritePNG(pngData, float.Parse(PixelPerUnitField.text));
        Draw();
    }

    public void ChangePixelPerUnit()
    {
        Tile.Sprite = TileType.GetSpritePNG(Tile.Sprite.texture.EncodeToPNG(), float.Parse(PixelPerUnitField.text));
        Draw();
    }

    public void ChangeTile(TileType tile, TileButtonController tileButtonController)
    {
        _tileButtonController = tileButtonController;
        Tile = tile;
        PixelPerUnitField.text = tile.Sprite.pixelsPerUnit.ToString();
        Draw();
    }

    public void Load()
    {
        ChangeTileSet(TileSet.Load("test.ts"));
    }

    public void Save()
    {
        TileSet.Save(_currentSet, "test.ts");
    }

    
    public void AddTile(string category)
    {
        _currentSet.Add(new TileType() { Category = category, Sprite = DefaultTileSprite, Colliders = new List<TileType.SpriteCollider>() });
        TileSetViewController.Draw(_currentSet);
    }

    public void AddCategory()
    {
        _currentSet.AddCategory(CategoryNameField.text);
        TileSetViewController.Draw(_currentSet);
    }

    public void ChangeTileSet(TileSet set)
    {
        _currentSet = set;
        TileSetViewController.Draw(_currentSet);
    }

    public void NewTileSet()
    {
        _currentSet = new TileSet();
        TileSetViewController.Draw(_currentSet);
    }

    public void RemoveTile()
    {
        _currentSet.Remove(Tile);
        Tile = null;
        Draw();
        TileSetViewController.Draw(_currentSet);
    }

    public void AddCollider()
    {
        StartCoroutine(AddColliderCoroutine());
    }

    private IEnumerator AddColliderCoroutine()
    {
        if (Tile == null) yield break;

        // Wait for user to click and hold
        while (!Input.GetButtonDown("Fire1"))
            yield return 0;

        yield return 0;

        // Create collider
        var collider = new TileType.SpriteCollider() { Layer = LayerMask.NameToLayer(LayerDropDown.options[LayerDropDown.value].text), offset = Vector2.zero, size = Vector2.zero };
        Tile.Colliders.Add(collider);

        // Record where the user pressed relative to tile
        Vector2 startPosition = TileViewerTransform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        // Move collider center and position with mouse
        while (!Input.GetButtonUp("Fire1"))
        {
            // Allow user to cancel
            if(Input.GetButton("Fire2"))
            {
                Tile.Colliders.Remove(collider);
                Draw();
                break;
            }

            // Get new mouse position relative to tile
            Vector2 newPosition = TileViewerTransform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            // Set collider size
            collider.size = new Vector2(Mathf.Abs(newPosition.x - startPosition.x), Mathf.Abs(newPosition.y - startPosition.y));

            // Set collider center
            collider.offset = new Vector2((startPosition.x + newPosition.x) / 2, (startPosition.y + newPosition.y) / 2);

            Draw();

            yield return 0;
        }
    }
}
