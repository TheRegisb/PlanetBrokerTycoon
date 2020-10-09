using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearColorGradient : MonoBehaviour
{
    public Color AtZero = Color.red;
    public Color AtOne = Color.green;

    private Gradient gradient;
    // Start is called before the first frame update
    void Start()
    {
        GradientColorKey[] colorKey = new GradientColorKey[2];
        GradientAlphaKey[] alphaKey;

        gradient = new Gradient();
        colorKey[0].color = AtZero;
        colorKey[0].time = 0.0f;
        colorKey[1].color = AtOne;
        colorKey[1].time = 1.0f;
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
    }

    public Color GetColorAt(float percent)
    {
        return gradient.Evaluate(percent);
    }
}
