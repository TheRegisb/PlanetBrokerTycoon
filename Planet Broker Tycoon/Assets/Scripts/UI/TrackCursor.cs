using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }
}
