using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndSnap : MonoBehaviour
{
    public bool isBeingHeld = false;
    private float startX, startY;
    private Collider2D target = null;
    private bool snapped = false;

    // Set the location to the mouse cursor.
    void Start()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        startX = mousePos.x - gameObject.transform.localPosition.x;
        startY = mousePos.y - gameObject.transform.localPosition.y;
    }

    // Destroy or register the building to some tile on left mouse button release.
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !snapped) // Left mouse button released when not already snapped.
        {
            if (target == null)
            {
                Destroy(this.gameObject);
            } else
            {
                snapped = true;
                target.gameObject.GetComponent<Mineable>().Register(GetComponent<Building>());
            }
        }
    }

    // Make the building follow the mouse cursor until the left mouse button is released.
    void FixedUpdate()
    {
        if (isBeingHeld && !snapped)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            gameObject.transform.localPosition = new Vector3(mousePos.x - startX, mousePos.y - startY, 0);
        }
    }

    // Manual start, only applicable for instances that were not spawned at runtime.
    void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        isBeingHeld = true;
        startX = mousePos.x - gameObject.transform.localPosition.x;
        startY = mousePos.y - gameObject.transform.localPosition.y;
    }

    // Check if the building is over an empty tile
    // and set the target to the last hovered tile.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Snapable") && other.gameObject.GetComponent<Mineable>().IsAvailable())
        {
            target = other;
        }
    }

    // Remove target if out of the bounds of a tile without overlapping a new one.
    void OnTriggerExit2D(Collider2D other)
    {
        if (target == other) // Leaving the tile without a new target already.
        {
            target = null;
        }
    }
}
