using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Curves;

public class AnimatePositionWithCurve : MonoBehaviour
{
    [SerializeField] Curve _curve;
    [SerializeField] Transform _toMove;
    [SerializeField] float _speed;
    [SerializeField] Vector3 _localOffsetToMoveTo;

    private Vector3 startLocalPosition;
    private Vector3 goalPosition;
    private float timer; 

    private void Start()
    {
        startLocalPosition = _toMove.localPosition;
        goalPosition = startLocalPosition + _localOffsetToMoveTo; 
        timer = 0; 
    }

    void Update()
    {
        timer += Time.deltaTime; 
        _toMove.localPosition = Vector3.Lerp(startLocalPosition, goalPosition, _curve.Evaluate(Mathf.PingPong(timer * _speed, 1)));
    }
}
