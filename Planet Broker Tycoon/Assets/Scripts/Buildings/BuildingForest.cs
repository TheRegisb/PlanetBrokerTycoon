using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingForest : Building
{
    public int ticksUntilSaleIncrease = 20;
    public int sellValueIncreaseRate = 100;
    private int tickCount = 0;

    public override void TickSpecial()
    {
        tickCount += 1;

        if (tickCount == ticksUntilSaleIncrease)
        {
            sellValue += sellValueIncreaseRate;
            tickCount = 0;
        }
    }
}
