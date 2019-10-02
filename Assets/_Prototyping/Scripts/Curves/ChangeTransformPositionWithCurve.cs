using UnityEngine;
using Curves;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(ChangeTransformPositionWithCurve))]
public class ChangeTransformPositionWithCurveEditor : Editor
{

    /// <summary>
    /// Buttons to be pressed on the inspector GUI 
    /// </summary> ]
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ChangeTransformPositionWithCurve myScript = (ChangeTransformPositionWithCurve)target;

        if (GUILayout.Button("Animate Position"))
        {
            myScript.AnimatePosition();
        }

        if (GUILayout.Button("Animate Rotation"))
        {
            myScript.AnimateRotation();
        }

        if (GUILayout.Button("Animate Scale"))
        {
            myScript.AnimateScale();
        }
    }
}
#endif

public class ChangeTransformPositionWithCurve : MonoBehaviour
{
    /// <summary>
    /// Transform to move.
    /// </summary>
    [SerializeField] private Transform _toMove;
    /// <summary>
    /// Transform to move to.
    /// </summary>
    [SerializeField] private Transform _moveTo;
    /// <summary>
    /// Animation curve it will follow.
    /// </summary>
    [SerializeField] private Curve _curve;
    /// <summary>
    /// Duration of the change.
    /// </summary>
    [SerializeField] private float _duration = 1f;

    /// <summary>
    /// Matches position.
    /// </summary>
    [ContextMenu("Animate Position")]
    public void AnimatePosition()
    {
        StartCoroutine(CalculateCurve.AnimatePosition(_curve, _toMove, _toMove.position, _moveTo.position, _duration, true));
    }

    /// <summary>
    /// Matches Rotation.
    /// </summary>
    [ContextMenu("Animate Rotation")]
    public void AnimateRotation()
    {
        StartCoroutine(CalculateCurve.AnimateRotation(_curve, _toMove, _toMove.rotation, _moveTo.rotation, _duration, true));
    }

    /// <summary>
    /// Matches Scale.
    /// </summary>
    [ContextMenu("Animate Scale")]
    public void AnimateScale()
    {
        StartCoroutine(CalculateCurve.AnimateScale(_curve, _toMove, _toMove.localScale, _moveTo.localScale, _duration, true));
    }
}

