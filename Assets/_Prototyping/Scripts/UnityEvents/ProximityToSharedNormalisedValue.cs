using UnityEngine;
using UnityEngine.Events;

public class ProximityToSharedNormalisedValue : MonoBehaviour
{
    [SerializeField] private Transform _origin;
    [SerializeField] private Transform _target;

    [SerializeField] private float _inRangeDistance = .5f;

    [SerializeField] private SharedNormalisedFloat _normalisedFloat;


    private void Update()
    {
        _normalisedFloat.value = Vector3.Distance(_origin.position, _target.position) - _inRangeDistance;
        _normalisedFloat.NormaliseClamped(); 
    }
}
