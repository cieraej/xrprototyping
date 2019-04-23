using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Feedback/Calibration")]
public class FeedbackCalibrator : ScriptableObject
{
    [SerializeField] public FeedbackIdentifier _interactableA;
    [SerializeField] public FeedbackIdentifier _interactabbleB;
 
    [SerializeField] private SharedGameObjects oneShotPrefabs;
    [SerializeField] private AudioEvent _audio;

    public bool canPlay = true;
    float timeBetweenPlay = .25f;
    public void Feedback(AudioSource audioSource, HapticFeedback hapticFeedback, Vector3 position)
    {
        if (canPlay)
        {
            Debug.Log("Playing Feedback");
            _audio.Play(audioSource);
            if (hapticFeedback != null)
            {
                hapticFeedback.Vibrate();
            }
            oneShotPrefabs.InstantiateRandom(position);
        }
    }


    public IEnumerator WaitASec()
    {
        if (canPlay)
        {
            canPlay = false;
            yield return new WaitForSeconds(timeBetweenPlay);
            canPlay = true;
        }
    }
}
