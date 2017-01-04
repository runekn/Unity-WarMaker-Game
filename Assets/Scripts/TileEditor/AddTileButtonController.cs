using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTileButtonController : MonoBehaviour {

    public TileEditorController EditorController;
    public string Category;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => EditorController.AddTile(Category));
    }
}
