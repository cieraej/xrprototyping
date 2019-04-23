using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Curves;

public class ChangeMaterialColor : MonoBehaviour {

    [SerializeField] private Material _materialToChange;
    [SerializeField] private string _propertyToChange = "_Color";
    [SerializeField] private Gradient _colorGradient;
    [SerializeField] private float _min;
    [SerializeField] private float _max;
    [SerializeField] private float _changeSensitity = 100f; 
    [SerializeField] private float _duration = 2f;
    [SerializeField] private Curve _curve; 
    private Color _startColor; 

    /// <summary>
    /// Enable the keywords for the material.
    /// </summary>
    private void Awake () {

        _materialToChange.EnableKeyword(_propertyToChange);
        _startColor = _materialToChange.GetColor(_propertyToChange);

    }

    private void OnApplicationQuit()
    {
        _materialToChange.SetColor(_propertyToChange, _startColor);
    }

    [ContextMenu("Animate Color")]
    public void AnimateColor()
    {
        StartCoroutine(CalculateCurve.AnimateColor(_curve, _materialToChange, _propertyToChange, _colorGradient, _duration));
    }
}
