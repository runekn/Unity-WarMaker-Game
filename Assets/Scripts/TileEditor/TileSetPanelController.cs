using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileSetPanelController : MonoBehaviour {
    
    public TileEditorController EditorController;
    public RectTransform DisplayPanel;
    public GameObject TilePanel;
    public GameObject CategoryPanel;

    private List<GameObject> _panels;

    void Awake()
    {
        _panels = new List<GameObject>();
    }

    public void Draw(TileSet Set)
    {
        if (Set != null)
        {
            foreach (var panel in _panels) Destroy(panel);
            _panels.Clear();

            foreach (var category in Set.Tiles)
            {
                // Create panel object
                var categoryPanel = Instantiate(CategoryPanel);
                var controlPanel = categoryPanel.transform.FindChild("ControlPanel");
                controlPanel.FindChild("CategoryName").GetComponent<Text>().text = category.Key;
                var addtilecontroller = controlPanel.FindChild("AddTileButton").GetComponent<AddTileButtonController>();
                addtilecontroller.Category = category.Key;
                addtilecontroller.EditorController = EditorController;
                categoryPanel.transform.SetParent(DisplayPanel, false);

                _panels.Add(categoryPanel);

                var tileImagesParent = categoryPanel.transform.FindChild("ImagePanel");

                foreach (var type in category.Value)
                {
                    // Create tile panel object
                    var tilePanel = Instantiate(TilePanel);
                    tilePanel.transform.SetParent(tileImagesParent.transform, false);

                    var controller = tilePanel.GetComponent<TileButtonController>();
                    controller.Type = type;
                    controller.EditorController = EditorController;
                }
            }
        }
    }

    private IEnumerator FixPanelsCoroutine()
    {
        foreach(var panel in _panels)
        {
            panel.SetActive(panel.activeSelf);
        }
        yield return 0;
        foreach (var panel in _panels)
        {
            panel.SetActive(panel.activeSelf);
        }
    }

    public void FixPanels()
    {
        StartCoroutine(FixPanelsCoroutine());
    }
}
