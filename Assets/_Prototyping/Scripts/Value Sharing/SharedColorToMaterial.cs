using Curves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedColorToMaterial : MonoBehaviour
{
    [SerializeField] private SharedColor _sharedColor;
    [SerializeField] private Material _materialToChange;
    [SerializeField] private string _propertyToChange = "_Color";
    [SerializeField] private float _sensitivity = .5f; 
    private Color _startColor;

    /// <summary>
    /// Enable the keywords for the material.
    /// </summary>
    private void Awake()
    {

        _materialToChange.EnableKeyword(_propertyToChange);
        _startColor = _materialToChange.GetColor(_propertyToChange);
    }

    private void Update()
    {
        _materialToChange.SetColor(_propertyToChange, Color.Lerp(_materialToChange.GetColor(_propertyToChange), _sharedColor.value, Time.time * _sensitivity));

    }
    private void OnApplicationQuit()
    {
        _materialToChange.SetColor(_propertyToChange, _startColor);
    }

}
