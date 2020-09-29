using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Factory : MonoBehaviour
{
    public GameObject building;
    public Text infoContainerUI;
    private Building bldComponent;
    private GlobalStateMachine gsm;
    private bool canCreate = false;

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
            canCreate = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f); // Fully visible sprite;
            canCreate = true;
        }
    }

    void OnMouseDown()
    {
        if (canCreate)
        {
            GameObject instance = Instantiate(building, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);

            instance.GetComponent<DragAndSnap>().isBeingHeld = true;
        }
    }

    void OnMouseOver()
    {
        infoContainerUI.text = string.Format(
            "{1}{0}{0}Coût en argent : ${2}{0}Gain en argent : ${3}{0}{0}Coût en ressources : {4:0.0}{0}Dégât écologique : {5:0.0}{0}{0}Notes : {0}{6}",
            Environment.NewLine, bldComponent.name, bldComponent.moneyCost, bldComponent.tickProfit, bldComponent.ecoCost, bldComponent.tickEcoDamage, bldComponent.notes);
    }
}
