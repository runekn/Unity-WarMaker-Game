using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    public int WorldHeight; // in tiles
    public int WorldWidth; // in tiles
    public TileSet Tileset;
    public int TileSize = 6; // (height in pixels) / (units per pixel)

    private GameObject[,] _tiles; // The tile objects
    private GridMap _gridMap;

	// Use this for initialization
	void Start () {
        Tileset = TileSet.Load("test.ts");
        GenerateWorld();
        GridMap.Save(_gridMap, "test");
	}

    // Update is called once per frame
    void Update () {

    }

    public void GenerateWorld()
    {
        GenerateWorld(GridMap.Empty(WorldHeight, WorldWidth, TileSize));
    }

    public void GenerateWorld(GridMap gridmap)
    {
        // Purge existing tiles
        _gridMap = gridmap;
        _tiles = new GameObject[_gridMap.Width, _gridMap.Height];

        // Start placing tiles
        for(var x = 0; x < _gridMap.Width; x++)
        {
            for(var y = 0; y < _gridMap.Height; y++)
            {
                // If it is not specified by gridmap
                if (_gridMap.Tiles[x, y] == null)
                {
                    // Choose a random rotation
                    var rotation = UnityEngine.Random.Range(0, 4);

                    // If it should be a boundary tile
                    if (y == 0 || y == _gridMap.Height - 1 || x == 0 || x == _gridMap.Width - 1)
                    {
                        PlaceTile(Tileset.Tiles["boundary"][UnityEngine.Random.Range(0, Tileset.Tiles["boundary"].Count)], new Vector2(x, y), rotation);
                    }
                    // If it should be a common tile
                    else
                    {
                        PlaceTile(Tileset.Tiles["common"][UnityEngine.Random.Range(0, Tileset.Tiles["common"].Count)], new Vector2(x, y), rotation);
                    }
                }
                // If gridmap specifies this tile
                else
                {
                    var tile = _gridMap.Tiles[x, y];
                    PlaceTile(Tileset.Tiles[tile.Type.Category][tile.Type.Id], tile.Position, tile.Rotation);
                }
            }
        }
    }

    /*
     * Will Create a tile game object with the following scheme:
     *      Tile (TileController, BoxCollider2D) 
     *      {
     *          Sprite (SpriteRenderer)
     *          Colliders 
     *          {
     *              Collider 1 (BoxCollider2D)
     *              Collider 2 (BoxCollider2D)
     *              ...
     *              Collider x (BoxCollider2D)
     *          }
     *      }
     */
    public void PlaceTile(TileType tileType, Vector2 position, int rotation)
    {
        // Remove old tile object at position
        var oldTile = _tiles[(int)position.x, (int)position.y];
        if (oldTile != null) Destroy(oldTile);

        // Create tile gameobject
        var tile = new GameObject();
        tile.name = "Tile";

        // Calculate world position
        var worldPosition = Vector3.zero;
        worldPosition.y -= ((_gridMap.Height / 2) * _gridMap.TileSize) - (position.y * _gridMap.TileSize);
        worldPosition.x -= ((_gridMap.Width / 2) * _gridMap.TileSize) - (position.x * _gridMap.TileSize);
        worldPosition.z = 10;

        // Spawn new tile at position
        tile.transform.position = worldPosition;
        tile.transform.parent = transform;
        //Instantiate(tile, worldPosition, new Quaternion(), transform);

        // Add spriterenderer
        var spriteObject = new GameObject();
        spriteObject.transform.position = worldPosition;
        spriteObject.transform.parent = tile.transform;
        //var spriteObject = Instantiate(new GameObject(), transform, false);
        spriteObject.name = "Sprite";
        var sprite = spriteObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        sprite.sprite = tileType.Sprite;

        // Add sprite colliders
        var colliderParentObject = new GameObject();
        colliderParentObject.transform.parent = tile.transform;
        colliderParentObject.transform.localPosition = Vector2.zero;
        //var colliderParentObject = Instantiate(new GameObject(), transform, false);
        colliderParentObject.name = "Colliders";
        foreach (var spriteCollider in tileType.Colliders)
        {
            var colliderObject = new GameObject();
            colliderObject.transform.parent = colliderParentObject.transform;
            colliderObject.transform.localPosition = Vector2.zero;
            if (spriteCollider.Layer >= 32)
                Debug.Log("stop");
            colliderObject.layer = spriteCollider.Layer;
            colliderObject.name = "collider";
            var spriteColliderComponent = colliderObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
            spriteColliderComponent.size = spriteCollider.size;
            spriteColliderComponent.offset = spriteCollider.offset;
        }

        // Add Tile collider
        var collider = tile.AddComponent<BoxCollider2D>() as BoxCollider2D;
        collider.size = new Vector2(_gridMap.TileSize, _gridMap.TileSize);
        tile.layer = LayerMask.NameToLayer("Tile"); // Tile layer

        // Add Tile controller
        var controller = tile.AddComponent<TileController>() as TileController;
        controller.Tile = new Tile() { Type = tileType, Position = position, Rotation = rotation };

        // Add to collections
        _tiles[(int)position.x, (int)position.y] = tile;
        _gridMap.Tiles[(int)position.x, (int)position.y] = controller.Tile;
    }
}
