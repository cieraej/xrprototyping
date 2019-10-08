using UnityEngine;

public class NormalisedFloatToColor : MonoBehaviour
{
    [SerializeField] private SharedNormalisedFloat _sharedNormalisedFloat;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private SharedColor _color;
    [SerializeField] private float _speed = .5f; 
    void Update()
    {
        _color.value = Color.Lerp(_color.value, _gradient.Evaluate(_sharedNormalisedFloat.NormaliseClamped()), Time.deltaTime *_speed);
    }
}
