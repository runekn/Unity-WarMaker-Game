using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileSet {

    [NonSerialized]
    public static readonly string SAVEPATH = Application.persistentDataPath + "/TileSets/";

    public Dictionary<string, List<Tile>> Tiles; // Category -> tiles

    public TileSet()
    {
        Tiles = new Dictionary<string, List<Tile>>();
    }

    public static void Save(TileSet tileset, string name)
    {
        var formatter = new BinaryFormatter();
        var file = File.Open(SAVEPATH + name, FileMode.OpenOrCreate);
        formatter.Serialize(file, tileset);
        file.Close();
    }

    public static GridMap Load(string name)
    {
        var formatter = new BinaryFormatter();
        var file = File.Open(SAVEPATH + name, FileMode.Open);
        return formatter.Deserialize(file) as GridMap;
    }

    public void Add(string category, Tile tile)
    {
        Tiles[category].Add(tile);
    }
}
