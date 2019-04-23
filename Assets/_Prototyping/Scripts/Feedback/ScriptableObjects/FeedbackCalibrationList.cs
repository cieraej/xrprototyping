using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Feedback/Calibration List")]
public class FeedbackCalibrationList : ScriptableObject{

    [SerializeField] public FeedbackCalibrator[] _feedbackCalibrators;


}
