using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorController : MonoBehaviour {

    public WorldGenerator WorldGenerator;
    public Camera Camera;

    public GameObject _chosenTile;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            PlaceTile(DetectTile());
        }

        if (Input.GetButtonDown("Fire2"))
        {
            RotateTile(DetectTile());
        }

        if (Input.GetButtonDown("Fire3"))
        {
            StartCoroutine(MoveCamera());
        }

        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Zoom(-(Input.GetAxis("Mouse ScrollWheel") * 3));
        }
	}

    private void PlaceTile(GameObject tile)
    {
        if (tile == null)
        {
            Debug.Log("No tile");
            return;
        }

        var tileController = tile.GetComponent<TileController>();
        Debug.Log(tileController.Tile.Position);
        //WorldGenerator.PlaceTile(_chosenTile, tileController.Tile.Position, 0);
        WorldGenerator.PlaceTile(tileController.Tile.Type, tileController.Tile.Position, 0);
    }

    private void RotateTile(GameObject tile)
    {
        if (tile == null)
        {
            Debug.Log("No tile");
            return;
        }

        tile.GetComponent<TileController>().RelativeRotate(-1);
    }

    private GameObject DetectTile()
    {
        int layerMask = LayerMask.GetMask("Tile");
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, layerMask);

        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        else return null;
    }

    private IEnumerator MoveCamera()
    {
        while (Input.GetButton("Fire3"))
        {
            // Get mouse movement
            var oldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            yield return 0;
            var newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var movement = newPosition - oldPosition;

            // Apply movement to camera
            Camera.transform.Translate(movement * -1);
        }
    }

    private void Zoom(float value)
    {
        Camera.orthographicSize += value;
    }
}
