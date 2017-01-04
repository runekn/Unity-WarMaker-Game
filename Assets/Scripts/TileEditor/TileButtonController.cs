using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class TileButtonController : MonoBehaviour {

    public TileEditorController EditorController;
    public ToggleGroup Togglegroup;
    public TileType Type
    {
        get { return _type; }
        set { _type = value; Draw(); }
    }

    private TileType _type;

    private void Start()
    {
        var toggle = GetComponent<Toggle>();
        toggle.group = Togglegroup;
        toggle.onValueChanged.AddListener((selected) => { if (selected) EditorController.ChangeTile(Type, this); });
    }

    public void Draw()
    {
        GetComponent<Image>().sprite = _type.Sprite;
    }
}
