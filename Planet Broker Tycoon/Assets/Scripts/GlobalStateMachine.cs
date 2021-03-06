﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalStateMachine : MonoBehaviour
{
    // The amount of money the player have. Cannot be negative.
    public int money = 10000;
    // Scale from 0 to 100. Game over on 100.
    public float financialPressure = 0f;
    // Increase of the pressure per tick.
    public float financialPressureRate = 1f;
    // Increase of the pressure rate per tick.
    public float financialPressureRateIncrease = 0.1f;
    // Reduce the pressure when acquiring money.
    public float moneyToPressureReliefConversionRate = 0.0004f;
    // Scale from 0 to 100. Game over on 0.
    public float ecologialIntegrity = 100.0f;
    // Time in seconds before next tick.
    public float tickDelay = 1.0f;

    public GameObject gameOverModal;
    public string ecologicalGameOverMsg;
    public string financialGameOverMsg;

    public Text moneyLabel;
    public Slider financialPressureSlider;
    public Slider ecologicalIntegritySlider;
    

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("WorldTick", tickDelay, tickDelay); // Start the ticking mecanism after 'tickDelay' seconds elapsed.
        moneyLabel.text = money.ToString();
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
        if (ecologialIntegrity <= 0.0f || financialPressure >= 100.0f)
        {
            gameOverModal.SetActive(true);
            GameObject.Find("GameOverText").GetComponent<Text>().text = (ecologialIntegrity <= 0.0f ? ecologicalGameOverMsg : financialGameOverMsg);
            gameOverModal.SetActive(true);
            GameObject.Find("BackgroundAudioPlayer").GetComponent<AudioFadeOut>().FadeOut();
            gameObject.GetComponent<AudioSource>().Play();
            Time.timeScale = 0;
            
        }
    }

    void Update()
    {
        
    }

    // Setters

    public void adjustMoney(int gain)
    {
        money += gain;
        if (gain > 0) // Making money relief the pressure, spending does nothing about it.
        {
            financialPressure = Mathf.Clamp(financialPressure - gain * moneyToPressureReliefConversionRate, 0.0f, 100.0f);
        }
        moneyLabel.text = money.ToString();
    }

    public void adjustEcoIntegrity(float damage)
    {
        ecologialIntegrity = Mathf.Clamp(ecologialIntegrity + damage, 0.0f, 100.0f);
        ecologicalIntegritySlider.value = ecologialIntegrity;
        SetSliderTextTo(ecologicalIntegritySlider, string.Format("{0:00.00}%", ecologialIntegrity));
    }

    // Utils
    private void SetSliderTextTo(Slider slider, string text)
    {
        slider.gameObject.transform.Find("PercentageLabel").GetComponent<Text>().text = text;
    }
}
