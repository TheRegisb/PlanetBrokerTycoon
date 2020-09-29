using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Mineable : MonoBehaviour
{
    public int resources = 50000;
    public int maxResources = 75000;
    public Building building = null;
    private bool canDestroy = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = GetComponent<LinearColorGradient>().GetColorAt((float)resources / maxResources);
    }

    // Sell building on mouse hovering and right click.
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && canDestroy)
        {
            Unregister();
        }
    }

    // Extract resources according to the buiding's rate,
    // update the tile color
    // and return a {monetary gain, ecological damage} pair.
    public KeyValuePair<int, float> Mine()
    {
        if (building == null                               // Nothing to exploit the resources
            || resources - building.tickResourceDrain < 0) // or unable to produce
        {
            return new KeyValuePair<int, float>(0, 0f); 
        }
        resources = Mathf.Clamp(resources - building.tickResourceDrain, 0, maxResources);
        building.TickSpecial();
        GetComponent<SpriteRenderer>().color = GetComponent<LinearColorGradient>().GetColorAt((float)resources / maxResources);
        return new KeyValuePair<int, float>(building.tickProfit, building.tickEcoDamage * -1);
    }

    // Get a reference to the building, reposition it at the center of the tile
    // remove physical interaction and apply construction cost.
    public void Register(Building building)
    {
        this.building = building;
        this.building.gameObject.transform.localPosition = gameObject.transform.localPosition;
        this.building.gameObject.GetComponent<Collider2D>().enabled = false;
        this.building.PlayDropSound();
        Destroy(this.building.gameObject.GetComponent<Rigidbody2D>());
        GetComponentInParent<GridManager>().UpdateGlobalState(building.moneyCost * -1, building.ecoCost * -1);
    }

    // Delete the building for a set monetary gain.
    public void Unregister()
    {
        float soundDuration = this.building.PlaySellSound();
        this.building.GetComponent<SpriteRenderer>().enabled = false;
        GetComponentInParent<GridManager>().UpdateGlobalState(building.sellValue, 0);
        Destroy(building.gameObject, soundDuration);
        this.building = null;
    }

    public bool IsAvailable()
    {
        return building == null;
    }

    // Building can be sold only if the mouse hover the tile.

    void OnMouseEnter()
    {
        canDestroy = true;
    }

    void OnMouseExit()
    {
        canDestroy = false;
    }
}
