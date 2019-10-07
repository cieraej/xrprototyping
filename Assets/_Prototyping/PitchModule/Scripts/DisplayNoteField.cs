using UnityEngine;

public class DisplayNoteField : MonoBehaviour
{
    /// <summary>
    /// What the current note is
    /// </summary>
    [SerializeField] private Note currentNote;
    [SerializeField] private SharedString stringVariable;
    [SerializeField] private SharedFloat floatVariableAccuracy;
    [SerializeField] private SharedFloat floatVariablePitch;

    [SerializeField] private float sampleRate = 4;

    private void Update()
    {
        if (Time.frameCount % sampleRate == 0)
        {
            ShowNoteProperty();
        }
    }

    public void ShowNoteProperty()
    {
        if (stringVariable != null)
        {
            stringVariable.value = currentNote.Name;
        }
        if (floatVariablePitch != null)
        {
            floatVariablePitch.value = (float)currentNote.Pitch;
        }
        if (floatVariableAccuracy != null)
        {
            floatVariableAccuracy.value = (float)currentNote.Accuracy;
        }

    }
}
