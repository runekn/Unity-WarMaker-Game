using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    public int WorldHeight; // in tiles
    public int WorldWidth; // in tiles
    public GameObject[] Tiles;
    public GameObject[] BoundaryTiles;
    public int TileSize = 6; // (height in pixels) / (units per pixel)

    private GameObject[,] _tiles;
    private GridMap _gridMap;

	// Use this for initialization
	void Start () {
        GenerateWorld();
        GridMap.Save(_gridMap, "test");
	}

    // Update is called once per frame
    void Update () {

    }

    private void GenerateWorld()
    {
        GenerateWorld(new GridMap() { Tiles = new Tile[WorldWidth,WorldHeight], Height = WorldHeight, Width = WorldWidth, TileSize = TileSize });
    }

    private void GenerateWorld(GridMap gridmap)
    {
        // Purge existing tiles
        _gridMap = gridmap;
        _tiles = new GameObject[_gridMap.Width, _gridMap.Height];

        // Start placing tiles
        for(var x = 0; x < _gridMap.Width; x++)
        {
            for(var y = 0; y < _gridMap.Height; y++)
            {
                if (_gridMap.Tiles[x, y] == null)
                {
                    // Choose a random rotation
                    var rotation = UnityEngine.Random.Range(0, 4);

                    // If it should be a boundary tile
                    if (y == 0 || y == _gridMap.Height - 1 || x == 0 || x == _gridMap.Width - 1)
                    {
                        PlaceTile(BoundaryTiles[UnityEngine.Random.Range(0, BoundaryTiles.Length)], new Vector2(x, y), rotation);
                    }
                    else
                    {
                        PlaceTile(Tiles[UnityEngine.Random.Range(0, Tiles.Length)], new Vector2(x, y), rotation);
                    }
                }
                else
                {
                    var tile = _gridMap.Tiles[x, y];
                    PlaceTile(Tiles[tile.Id], tile.Position, tile.Rotation);
                }
            }
        }
    }

    public void PlaceTile(GameObject tileType, Vector2 position, int rotation)
    {
        // Remove old tile at position
        var oldTile = _tiles[(int)position.x, (int)position.y];
        if (oldTile != null) Destroy(oldTile);

        // Calculate world position
        var worldPosition = Vector3.zero;
        worldPosition.y -= ((_gridMap.Height / 2) * _gridMap.TileSize) - (position.y * _gridMap.TileSize);
        worldPosition.x -= ((_gridMap.Width / 2) * _gridMap.TileSize) - (position.x * _gridMap.TileSize);
        worldPosition.z = 10;
        // Spawn new tile at position
        var tile = Instantiate(tileType, worldPosition, new Quaternion(), transform);

        // Add Tile collider
        var collider = tile.AddComponent<BoxCollider2D>() as BoxCollider2D;
        collider.size = new Vector2(_gridMap.TileSize, _gridMap.TileSize);
        tile.layer = LayerMask.NameToLayer("Tile"); // Tile layer

        // Add Tile controller
        var controller = tile.AddComponent<TileController>() as TileController;
        controller.Tile = new Tile() { Position = position, Rotation = rotation };
        
        // Add to collections
        _tiles[(int)position.x, (int)position.y] = tile;
        _gridMap.Tiles[(int)position.x, (int)position.y] = controller.Tile;
    }
}
