using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Curves;

public class ChangeColorFromGradientOverTime : MonoBehaviour
{
    [SerializeField] private SharedColor _sharedColor;
    [SerializeField] private SharedGradient _colorGrandient;
    [SerializeField] private Curve _curve;
    public float rate;

    private void Update()
    {
        _sharedColor.value = _colorGrandient.sharedGradient.Evaluate( Mathf.PingPong(Time.time *rate, 1));
    }
}
