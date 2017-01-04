using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileViewerController : MonoBehaviour {

    public SpriteRenderer Renderer;
    public Transform ColliderParent;

    private List<GameObject> _colliderObjects = new List<GameObject>();

    public void Draw(TileType tile)
    {
        if (tile == null)
        {
            Renderer.sprite = null;
        }
        else
        {
            // Draw sprite
            Renderer.sprite = tile.Sprite;

            // Destroy old lines
            foreach (var colliderObject in _colliderObjects) Destroy(colliderObject);
            _colliderObjects.Clear();

            // Draw colliders
            foreach (var collider in tile.Colliders)
            {
                var lineOject = new GameObject();
                lineOject.transform.parent = ColliderParent;
                _colliderObjects.Add(lineOject);
                var line = lineOject.AddComponent<LineRenderer>();
                Vector3[] positions = new Vector3[5];

                Vector3 colliderCenter = collider.offset;
                colliderCenter.z = -1;

                // Create object at collider center for debugging purposes
                var centerObject = new GameObject();
                centerObject.name = "Center";
                centerObject.transform.parent = lineOject.transform;
                centerObject.transform.localPosition = collider.offset;

                // Upper left corner
                var upperleft = colliderCenter;
                upperleft.x -= collider.size.x / 2;
                upperleft.y -= collider.size.y / 2;
                positions[0] = upperleft;

                // Upper right corner
                var upperright = colliderCenter;
                upperright.x += collider.size.x / 2;
                upperright.y -= collider.size.y / 2;
                positions[1] = upperright;

                // Lower right corner
                var lowerright = colliderCenter;
                lowerright.x += collider.size.x / 2;
                lowerright.y += collider.size.y / 2;
                positions[2] = lowerright;

                // Lower left corner
                var lowerleft = colliderCenter;
                lowerleft.x -= collider.size.x / 2;
                lowerleft.y += collider.size.y / 2;
                positions[3] = lowerleft;

                // Make sure it makes a full rectangle
                positions[4] = positions[0];

                line.numPositions = 5;
                line.SetPositions(positions);
                line.startWidth = 0.1f;
                line.endWidth = 0.1f;
                line.useWorldSpace = false;
            }
        }
    }
}
