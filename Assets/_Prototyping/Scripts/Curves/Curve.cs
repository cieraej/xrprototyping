using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Curves
{
    [CreateAssetMenu(menuName = "Curve/Simple")]
    public class Curve : ScriptableObject
    {
        [SerializeField] private AnimationCurve animationCurve = new AnimationCurve(new Keyframe(0, 0, 1, 1), new Keyframe(1, 1, 1, 1));

        public float Evaluate(float percent)
        {
            return animationCurve.Evaluate(percent);
        }
    }
}
