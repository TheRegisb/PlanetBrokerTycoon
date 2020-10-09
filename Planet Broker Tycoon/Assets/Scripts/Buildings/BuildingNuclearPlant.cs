using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingNuclearPlant : Building
{
    public int ticksUntilWaste = 30;
    public GameObject wastePrefab;
    private int tickCount = 0;

    public override void TickSpecial()
    {
        tickCount += 1;

        if (tickCount == ticksUntilWaste)
        {
            GridManager gm = GameObject.Find("Grid").GetComponent<GridManager>();
            Building b = Instantiate(wastePrefab, gameObject.transform.localPosition, Quaternion.identity).GetComponent<Building>();
            if (!gm.PopulateRandomTileWith(b)) // Try to add waste building to grid.
            {
                gm.UpdateGlobalState(0, b.ecoCost * -1); // Impact eco integrity if no tile free.
                Destroy(b.gameObject);
            }
            tickCount = 0;
        }
    }
}
