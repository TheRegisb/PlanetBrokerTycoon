using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public GameObject building;
    private Building bldComponent;
    private GlobalStateMachine gsm;

    // Start is called before the first frame update
    void Start()
    {
        gsm = GameObject.Find("GSM").GetComponent<GlobalStateMachine>();
        bldComponent = building.GetComponent<Building>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gsm.money < bldComponent.moneyCost)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f); // Half-visible sprite;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f); // Fully visible sprite;
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }

    void OnMouseDown()
    {
        GameObject instance = Instantiate(building, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);

        instance.GetComponent<DragAndSnap>().isBeingHeld = true;
    }
}
