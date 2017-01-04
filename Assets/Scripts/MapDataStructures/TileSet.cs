using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileSet {
    
    public Dictionary<string, List<TileType>> Tiles; // Category -> tiles

    private int _id = 0;

    public TileSet()
    {
        Tiles = new Dictionary<string, List<TileType>>();
    }

    public static void Save(TileSet tileset, string name)
    {
        var formatter = new BinaryFormatter();
        var file = File.Open(Constants.TILESETPATH + name, FileMode.OpenOrCreate);
        formatter.Serialize(file, tileset);
        file.Close();
    }

    public static TileSet Load(string name)
    {
        var formatter = new BinaryFormatter();
        var file = File.Open(Constants.TILESETPATH + name, FileMode.Open);
        var result = formatter.Deserialize(file) as TileSet;
        file.Close();
        return result;
    }

    public void Add(TileType tile)
    {
        AddCategory(tile.Category);

        Tiles[tile.Category].Add(tile);
        tile.Id = _id++;
    }

    public void AddCategory(string name)
    {
        if (!Tiles.ContainsKey(name))
            Tiles.Add(name, new List<TileType>());
    }

    public void Remove(TileType tile)
    {
        Tiles[tile.Category].Remove(tile);
    }
}
