using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollapseButtonController : MonoBehaviour {

    public TileSetPanelController PanelController;

    void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener((bool that) => PanelController.FixPanels());
    }
}
