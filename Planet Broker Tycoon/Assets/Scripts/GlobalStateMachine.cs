using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text moneyLabel;
    public Slider financialPressureSlider;
    public Slider ecologicalIntegritySlider;
    

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("WorldTick", tickDelay, tickDelay); // Start the ticking mecanism after 'tickDelay' seconds elapsed.
        moneyLabel.text = "$" + money;
        financialPressureSlider.value = financialPressure;
        SetSliderTextTo(financialPressureSlider, string.Format("{0:00.00}%", financialPressure));
        SetSliderTextTo(ecologicalIntegritySlider, string.Format("{0:00.00}%", ecologialIntegrity));
    }

    // Increase finanial pressure and update its label,
    // the check for game over conditions.
    private void WorldTick()
    {
        financialPressure = Mathf.Clamp(financialPressure + financialPressureRate, 0.0f, 100.0f);
        financialPressureRate += financialPressureRateIncrease;
        financialPressureSlider.value = financialPressure;
        SetSliderTextTo(financialPressureSlider, string.Format("{0:00.00}%", financialPressure));
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
        money += gain;
        financialPressure -= (gain * moneyToPressureReliefConversionRate);
        moneyLabel.text = "$" + money;
    }

    public void adjustEcoIntegrity(float damage)
    {
        ecologialIntegrity += damage;
        SetSliderTextTo(ecologicalIntegritySlider, string.Format("{0:00.00}%", ecologialIntegrity));
    }

    // Utils
    private void SetSliderTextTo(Slider slider, string text)
    {
        slider.gameObject.transform.Find("PercentageLabel").GetComponent<Text>().text = text;
    }
}
