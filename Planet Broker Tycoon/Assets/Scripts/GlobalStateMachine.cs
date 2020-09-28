using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStateMachine : MonoBehaviour
{
    // The amount of money the player have. Cannot be negative.
    public int money = 5000;
    // Scale from 0 to 100. Game over on 100.
    public float financialPressure = 0f;
    // Increase of the pressure per tick.
    public float financialPressureRate = 0.3f;
    // Increase of the pressure rate per tick.
    public float financialPressureRateIncrease = 0.2f;
    // Reduce the pressure when acquiring money.
    public float moneyToPressureReliefConversionRate = 0.0001f;
    // Scale from 0 to 100. Game over on 0.
    public float ecologialIntegrity = 100.0f;
    // Time in seconds before next tick.
    public float tickDelay = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("IncreasePressure", 0, tickDelay);
    }

    private void IncreasePressure()
    {
        financialPressure += financialPressureRate;
        financialPressureRate += financialPressureRateIncrease;
        // TODO Load game over scene or freeze game and display restart/quit modal
        if (ecologialIntegrity <= 0.0f)
        {
            Debug.Log("Partie perdue pour cause écologique.");
        }
        else if (financialPressure >= 100.0f)
        {
            Debug.Log("Partie perdu pour cause économique.");
        }
    }

    void Update()
    {
        
    }

    // Setters

    public void adjustMoney(int gain)
    {
        Debug.Log("money: " + money);
        money += gain;
        financialPressure -= (gain * moneyToPressureReliefConversionRate);
    }

    public void adjustEcoIntegrity(float damage)
    {
        ecologialIntegrity += damage;
    }
}
