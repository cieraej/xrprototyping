using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormaliseFloatToColorFromGradient : MonoBehaviour
{
    public SharedGradient colorGradient;
    public SharedColor sharedColor;
    public SharedFloat sharedFloat;
    float colorValue;
    public float min, max;
    public float changeSensitity = 100f;

    private void Update()
    {
        LerpColor();
    }

    public void LerpColor()
    {
        sharedColor.value = Color.Lerp(sharedColor.value, colorGradient.sharedGradient.Evaluate(NormalizeValue(sharedFloat.value, min, max)), Time.deltaTime * changeSensitity);
    }

    private float NormalizeValue(float value, float min, float max)
    {
        return (value - min) / (max - min);
    }
}
