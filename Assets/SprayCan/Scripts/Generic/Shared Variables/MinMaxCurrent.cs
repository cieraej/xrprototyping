using UnityEngine;
[CreateAssetMenu(menuName = "Float/MinMaxCurrent")]
public class MinMaxCurrent: ScriptableObject
{
    [SerializeField] private float min;
    [SerializeField] private float max;
    public float current;

    public float Normalise()
    {
        return (current - min) / (max - min);
    }
    public float NormaliseClamped()
    {
        return Mathf.Clamp01((current - min) / (max - min));
    }
}