using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Mineable : MonoBehaviour
{
    public int resources = 50000;
    public int maxResources = 75000;
    public Building building = null;
    private Text infoContainerUI;
    private bool canDestroy = false;

    public GameObject valueText;
    public GameObject CanvasContainer;
    public Color32 bad;
    public Color32 good;
    public Color32 money;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = GetComponent<LinearColorGradient>().GetColorAt((float)resources / maxResources);
        infoContainerUI = GameObject.Find("TileInfoLabel").GetComponent<Text>();
    }

    // Sell building on mouse hovering and right click.
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && canDestroy && building.canBeSold)
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
        StartCoroutine(launchText());
        
        return new KeyValuePair<int, float>(building.tickProfit, building.tickEcoDamage * -1);
    }

    private IEnumerator launchText()
    {
        var profit = Instantiate(valueText, building.transform.position, Quaternion.identity);
        profit.transform.SetParent(CanvasContainer.transform);
        profit.transform.localScale = new Vector3(.5f, .5f, .5f);
        profit.GetComponent<Text>().text = building.tickProfit.ToString() + "$";
        profit.GetComponent<Text>().color = (building.tickProfit > 0 ? money : bad);
        profit.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 150));
        Destroy(profit, 1f);
        yield return new WaitForSeconds(.1f);
        var damages = Instantiate(valueText, building.transform.position, Quaternion.identity);
        damages.transform.SetParent(CanvasContainer.transform);
        damages.transform.localScale = new Vector3(.5f, .5f, .5f);
        damages.GetComponent<Text>().text = (building.tickEcoDamage * -1).ToString() + "eco";
        damages.GetComponent<Text>().color = (building.tickEcoDamage <= 0 ? good : bad);
        damages.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 150));
        Destroy(damages, 1f);
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

    void OnMouseOver()
    {
        infoContainerUI.text = string.Format("Ressources: {1}{0}Bâtiment : {2}{0}Valeur de vente : {3}",
            Environment.NewLine, 
            resources, 
            (building == null ? "--" : building.name), 
            (building == null ? "--" : (building.canBeSold ? building.sellValue.ToString() : "Impossible")));
    }
}
