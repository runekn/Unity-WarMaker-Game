using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tile {

    public int Id; // Id in the tileset
    public int Rotation;
    public SerializableVector2 Position;
    public Sprite Sprite;
    public List<BoxCollider2D> Colliders;

}