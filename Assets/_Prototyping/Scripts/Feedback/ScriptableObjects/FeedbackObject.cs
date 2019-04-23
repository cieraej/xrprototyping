using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FeedbackObject : MonoBehaviour {

    [SerializeField] private FeedbackIdentifier _identity;
    [SerializeField] public HapticFeedback _hapticFeedback;
    [SerializeField] private Collider[] _collidersToIgnore;
    [SerializeField] private UnityEvent _OnCollision;
    private Collider _collider;
    [SerializeField] private FeedbackManager _feedbackManager;
	private void Awake()
	{
        if(_feedbackManager == null)
        {
            _feedbackManager = FindObjectOfType<FeedbackManager>(); 
        }

        try
        {
            _collider = this.GetComponent<Collider>();

            for (int i = 0; i < _collidersToIgnore.Length; i++)
            {
                Physics.IgnoreCollision(_collider, _collidersToIgnore[i], true);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Collider on feedback object is null. Cannot ignore collision " + e); 
        }
	}
	private void OnCollisionEnter(Collision collision)
	{
        if(collision.gameObject.GetComponent<FeedbackObject>() != null)
        {
            _OnCollision.Invoke(); 
            _feedbackManager.GetFeedback(_identity, collision.gameObject.GetComponent<FeedbackObject>()._identity, _hapticFeedback, collision.contacts[0].point);
        }
	}
}
