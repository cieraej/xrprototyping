using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FeedbackManager : MonoBehaviour
{
    /// <summary>
    /// List of feedback calibators. Which is a list of all the calibration events and the identifiers that correspond to it. 
    /// </summary>
    [SerializeField] private FeedbackCalibrationList _feedbackCalibrationList;
    /// <summary>
    /// Audio source that will or will not be added dependeding on the current audio source status.
    /// </summary>
    private AudioSource _audioSource;

    /// <summary>
    /// Awake this instance. Just calibrates the audio source. In the future when localized audio becomes a bigger deal (which it should be). This won't be nessarcy.
    /// </summary>
	private void Awake()
	{
        _audioSource = this.GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.Log("No audio source on " + this.gameObject + " adding it in Feedback Manager!");
            this.gameObject.AddComponent<AudioSource>();
            _audioSource = this.GetComponent<AudioSource>();
        }


	}
    /// <summary>
    /// Gets the feedback. Called from collisions between two feedback objects. 
    /// </summary>
    /// <param name="feedbackA">Feedback a.</param>
    /// <param name="feedbackB">Feedback b.</param>
    /// <param name="hapticFeedback">Haptic feedback.</param>
    /// <param name="parent">Parent.</param>
	public void GetFeedback(FeedbackIdentifier feedbackA, FeedbackIdentifier feedbackB, HapticFeedback hapticFeedback, Vector3 parent)
    {
        // go through the calibration list
        for (int i = 0; i < _feedbackCalibrationList._feedbackCalibrators.Length; i++)
        {
            // if theres a match between feedback A and feedback B
            if ((_feedbackCalibrationList._feedbackCalibrators[i]._interactableA == feedbackA && _feedbackCalibrationList._feedbackCalibrators[i]._interactabbleB == feedbackB)
                || (_feedbackCalibrationList._feedbackCalibrators[i]._interactableA == feedbackB && _feedbackCalibrationList._feedbackCalibrators[i]._interactabbleB == feedbackA))
            {
                PlayFeedBack(_feedbackCalibrationList._feedbackCalibrators[i], hapticFeedback, parent);
            }
        }
    }

    public void PlayFeedBack (FeedbackCalibrator toCalibrate, HapticFeedback hapticFeedback,Vector3 parent ){
        // get that feedback!!
       //Debug.Log("Playing Feedbaac")
        toCalibrate.Feedback(_audioSource,hapticFeedback, parent);

        // haptic feedback is optional ! so it is allowed to be null!
        if (hapticFeedback != null)
        {
            StartCoroutine(hapticFeedback.WaitASec());
        }
    }
 
}
