using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private GlobalStateMachine gsm;
    // All child of the "Grid" GameObject that have a "Mineable" script attached.
    private Mineable[] tiles;

    // Start is called before the first frame update
    void Start()
    {
        gsm = GameObject.Find("GSM").GetComponent<GlobalStateMachine>();
        InvokeRepeating("MineAllTiles", 0, gsm.tickDelay);
        tiles = GetComponentsInChildren<Mineable>();
    }

    void MineAllTiles()
    {
        int profit = 0;
        float ecoDrain = 0f;


        foreach(Mineable tile in tiles)
        {
            KeyValuePair<int, float> tileGains = tile.Mine();

            profit += tileGains.Key;
            ecoDrain += tileGains.Value;
        }
        // Debug.Log("Got a total of " + profit + " and " + ecoDrain);
        UpdateGlobalState(profit, ecoDrain);
    }

    public void UpdateGlobalState(int profit, float ecoDrain)
    {
        gsm.adjustMoney(profit);
        gsm.adjustEcoIntegrity(ecoDrain);
    }

    public bool PopulateRandomTileWith(Building building)
    {
        foreach(Mineable tile in tiles)
        {
            if (tile.IsAvailable())
            {
                tile.Register(building);
                return true;
            }
        }
        return false;
    }
}
