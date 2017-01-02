using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class GridMap {

    [NonSerialized]
    public static readonly string SAVEPATH = Application.persistentDataPath + "/Maps/";

    public Tile[,] Tiles;
    public int TileSize;
    public int Height;
    public int Width;
	
    public static void Save(GridMap map, string name)
    {
        var formatter = new BinaryFormatter();
        var file = File.Open(SAVEPATH + name, FileMode.OpenOrCreate);
        formatter.Serialize(file, map);
        file.Close();
    }

    public static GridMap Load(string name)
    {
        var formatter = new BinaryFormatter();
        var file = File.Open(SAVEPATH + name, FileMode.Open);
        return formatter.Deserialize(file) as GridMap;
    }

}
