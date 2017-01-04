using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class GridMap {

    public Tile[,] Tiles;
    public int TileSize;
    public int Height;
    public int Width;
	
    public static void Save(GridMap map, string name)
    {
        var formatter = new BinaryFormatter();
        var file = File.Open(Constants.MAPSPATH + name, FileMode.OpenOrCreate);
        formatter.Serialize(file, map);
        file.Close();
    }

    public static GridMap Load(string name)
    {
        var formatter = new BinaryFormatter();
        var file = File.Open(Constants.MAPSPATH + name, FileMode.Open);
        return formatter.Deserialize(file) as GridMap;
    }

    /*
     * Returns a new empty gridmap with specified size.
     */
    public static GridMap Empty(int height, int width, int tileSize)
    {
        return new GridMap() { Tiles = new Tile[width, height], Height = height, Width = width, TileSize = tileSize };
    }

}
