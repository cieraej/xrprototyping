using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnProximityAndOrientationInvokeUnityEvent : MonoBehaviour
{

    [SerializeField] private Transform _origin;
    [SerializeField] private Transform _target;

    [SerializeField] private float _inRangeDistance = .5f; 

    [MinMaxRange(-1,1)]
    [SerializeField] private RangedFloat _orientationPercision;
    
    /// <summary>
    /// Unity Events to invoke
    /// </summary>
    [SerializeField] private UnityEvent _onProximityEnterAndOrientedTowards;
    [SerializeField] private UnityEvent _onProximityExitAndOrientedAway;

    [SerializeField] private UnityEvent _onProximityEnter;
    [SerializeField] private UnityEvent _onProximityExit;

    [SerializeField] private UnityEvent _onOrientedTowards;
    [SerializeField] private UnityEvent _onOrientedAway;
    
    private float currentOrientation;
    private float currentDistance;

    private bool isInRange = false;
    private bool isOriented = false;

    private bool previousIsInRange = false;
    private bool previousIsOriented = false;

    private void Update()
    {
        // calculations
        currentOrientation = Vector3.Dot(_origin.forward, _target.forward);
        currentDistance = Vector3.Distance(_origin.position, _target.position);

        // assiging bool values from this
        if(currentDistance < _inRangeDistance)
        {
            isInRange = true; 
        }
        else
        {
            isInRange = false; 
        }

        if( currentOrientation < _orientationPercision.maxValue && currentOrientation > _orientationPercision.minValue)
        {
            isOriented = true; 
        }
        else
        {
            isOriented = false; 
        }

        // check orientation
        if(isOriented && !previousIsOriented)
        {
            _onOrientedTowards.Invoke();
        }
        else if (!isOriented && previousIsOriented)
        {
            _onOrientedAway.Invoke(); 
        }

        // check proximity
        if(isInRange && !previousIsOriented)
        {
            _onProximityEnter.Invoke();
        }
        else if(!isInRange && previousIsInRange)
        {
            _onProximityExit.Invoke(); 
        }

        //for both orientation and distance
        if( (isOriented & isInRange) && !(previousIsInRange & previousIsOriented))
        {
            _onProximityEnterAndOrientedTowards.Invoke();
        }
        else if ((!isOriented | !isInRange) && (previousIsInRange & previousIsOriented))
        {
            _onProximityExitAndOrientedAway.Invoke();

        }

        // storing previous values
        previousIsInRange = isInRange;
        previousIsOriented = isOriented;
    }


}
