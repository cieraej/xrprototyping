using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Curves;
public class ChangeSharedColorFromMinMaxCurrent : MonoBehaviour
{
    [SerializeField] private SharedColor _sharedColor;
    [SerializeField] private SharedGradient _colorGrandient;
    [SerializeField] private Curve _curve;
    [SerializeField] private MinMaxCurrent value; 

    void Update()
    {
        print("Clamped Normal " + value.NormaliseClamped() + " current " + value.current);
            _sharedColor.value = _colorGrandient.sharedGradient.Evaluate(_curve.Evaluate(value.NormaliseClamped()));
      

    }
}
