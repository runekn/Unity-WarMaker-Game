using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileType {

    public int Id; // Id in the tileset
    public string Category;
    public List<SpriteCollider> Colliders;
    public Sprite Sprite
    {
        get
        {
            if (_sprite != null) return _sprite;
            _sprite = GetSpritePNG(_textureData, _ppu);
            return _sprite;
        }
        set
        {
            _ppu = value.pixelsPerUnit;
            _textureData = value.texture.EncodeToPNG();
            _sprite = value;
        }
    }
    [NonSerialized]
    private Sprite _sprite;

    private float _ppu;
    private byte[] _textureData;

    public static Sprite GetSprite(byte[] textureData, float ppu)
    {
        var texture = new Texture2D(0, 0);
        texture.filterMode = FilterMode.Point;
        texture.LoadRawTextureData(textureData);

        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), ppu);
    }

    public static Sprite GetSpritePNG(byte[] textureData, float ppu)
    {
        var texture = new Texture2D(0, 0);
        texture.filterMode = FilterMode.Point;
        texture.LoadImage(textureData);

        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), ppu);
    }

    [Serializable]
    public class SpriteCollider
    {
        public int Layer;
        public SerializableVector2 offset;
        public SerializableVector2 size;
    }
}
