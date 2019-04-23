using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(PreviewFeedback))]
public class InvokeUnityEventEditor : Editor
{

    /// <summary>
    /// Buttons to be pressed on the inspector GUI 
    /// </summary> ]
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PreviewFeedback myScript = (PreviewFeedback)target;

        if (GUILayout.Button("Test Feedback from : " + myScript._calibrator.name))
        {
            myScript.TestFeedback();
        }
    }
}
#endif

public class PreviewFeedback : MonoBehaviour
{
    /// <summary>
    /// The calibrator to test.
    /// </summary>
    public FeedbackCalibrator _calibrator;
    /// <summary>
    /// The feedback manager that we feed the calibrator into to test. 
    /// </summary>
    public FeedbackManager _feedbackManager;

    /// <summary>
    /// Tests the feedback. Called from the editor. 
    /// </summary>
    public void TestFeedback(){
        _feedbackManager.PlayFeedBack(_calibrator, null, this.transform.position);
    }
}
