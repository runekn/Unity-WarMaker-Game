using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineOfSightController : MonoBehaviour {

    public Transform StartPosition;

    private LineRenderer _line;

	// Use this for initialization
	void Start () {
        _line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        var startPosition = StartPosition.position;
        var mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(startPosition);
        mouseDirection.z = 0;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        var distance = Vector2.Distance(startPosition, mousePosition);

        var groundLayerhitInfo = Physics2D.Raycast(startPosition, mouseDirection, distance, 8);
        var turretLayerhitInfo = Physics2D.Raycast(startPosition, mouseDirection, distance, 9);
        var defaultLayerhitInfo = Physics2D.Raycast(startPosition, mouseDirection, distance, 1);

        startPosition.z = -1;
        mousePosition.z = -1;
        _line.SetPositions(new Vector3[] { startPosition, mousePosition });
    }
}
