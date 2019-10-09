using UnityEngine;

public class NormalisedFloatToRendererColor : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private int _materialIndex;
    [SerializeField] private string _colorProperty = "_Color";
    [SerializeField] private Gradient _gradient;
    [SerializeField] private SharedNormalisedFloat _sharedNormalisedFloat;
    [SerializeField] private float _sensitivity = .5f; 

    private float currentNormalisedFloat;
    private float previousNormalisedFloat;

    private void Start()
    {
        _renderer.materials[_materialIndex].EnableKeyword(_colorProperty);
    }

    void Update()
    {
        currentNormalisedFloat = _sharedNormalisedFloat.NormaliseClamped();
     
            _renderer.materials[_materialIndex].SetColor(_colorProperty, Color.Lerp (_renderer.materials[_materialIndex].GetColor(_colorProperty),_gradient.Evaluate(currentNormalisedFloat), Time.deltaTime*_sensitivity));
        
        previousNormalisedFloat = currentNormalisedFloat;
    }
}
