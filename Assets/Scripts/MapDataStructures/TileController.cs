using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

    public GameObject ParentObject;
    public Tile Tile
    {
        get
        {
            return _tile;
        }
        set
        {
            _tile = value;
            Rotate(_tile.Rotation);
        }
    }
    private Tile _tile;

    private static readonly int[] ANGLES = new int[] { 0, 90, 180, 270 };

    public void Rotate(int rotation)
    {
        _tile.Rotation = rotation;
        transform.rotation = Quaternion.Euler(0, 0, ANGLES[_tile.Rotation]);
    }

    public void RelativeRotate(int rotation)
    {
        _tile.Rotation = ((_tile.Rotation + rotation) % 4 + 4) % 4; ; //(Rotation + rotation) % 4. Done this way since % doesn't work in negatives.
        transform.rotation = Quaternion.Euler(0, 0, ANGLES[_tile.Rotation]);
    }
}
