using UnityEngine;

public class NormalisedFloatToRendererColor : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private int _materialIndex;
    [SerializeField] private string _colorProperty = "_Color";
    [SerializeField] private Gradient _gradient;
    [SerializeField] private SharedNormalisedFloat _sharedNormalisedFloat;

    private float currentNormalisedFloat;
    private float previousNormalisedFloat;

    private void Start()
    {
        _renderer.materials[_materialIndex].EnableKeyword(_colorProperty);
    }

    void Update()
    {
        currentNormalisedFloat = _sharedNormalisedFloat.NormaliseClamped();
        if (currentNormalisedFloat != previousNormalisedFloat)
        {
            _renderer.materials[_materialIndex].SetColor(_colorProperty, _gradient.Evaluate(currentNormalisedFloat));
        }
        previousNormalisedFloat = currentNormalisedFloat;
    }
}
