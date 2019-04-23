using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Feedback/Calibration")]
public class FeedbackCalibratorWithContext : FeedbackCalibrator
{
    [SerializeField] private FeedbackContext context;
}
