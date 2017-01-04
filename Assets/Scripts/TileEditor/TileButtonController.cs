using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileButtonController : MonoBehaviour {

    public TileEditorController EditorController;
    public TileType Type
    {
        get { return _type; }
        set { _type = value; Draw(); }
    }

    private TileType _type;

    private void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => EditorController.ChangeTile(Type, this));
    }

    public void Draw()
    {
        GetComponent<Image>().sprite = _type.Sprite;
    }
}
