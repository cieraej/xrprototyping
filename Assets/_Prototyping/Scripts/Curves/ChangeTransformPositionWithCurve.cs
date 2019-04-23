using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Curves
{
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
        public void MatchPosition()
        {
            StartCoroutine(CalculateCurve.AnimatePosition(_curve, _toMove, _toMove.position, _moveTo.position, _duration, true));
        }

        /// <summary>
        /// Matches Rotation.
        /// </summary>
        [ContextMenu("Animate Rotation")]
        public void MatchRotation()
        {
            StartCoroutine(CalculateCurve.AnimateRotation(_curve, _toMove, _toMove.rotation, _moveTo.rotation, _duration, true));
        }

        /// <summary>
        /// Matches Scale.
        /// </summary>
        [ContextMenu("Animate Scale")]
        public void MatchScale()
        {
            StartCoroutine(CalculateCurve.AnimateScale(_curve, _toMove, _toMove.localScale, _moveTo.localScale, _duration, true));
        }
    }
}
