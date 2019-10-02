using UnityEngine;
#if UNITY_EDITOR
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(AudioEvent), true)]
public class AudioEventEditor : Editor
{
    /// <summary>
    /// Audio source used to preview.
    /// </summary>
    [SerializeField] private AudioSource _previewer;

    /// <summary>
    /// Adds a audio source so it can be previewed in the unity editor. 
    /// </summary>
    public void OnEnable()
    {
        _previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
    }

    /// <summary>
    /// Destoys the editor audio source. 
    /// </summary>
    public void OnDisable()
    {
        DestroyImmediate(_previewer.gameObject);
    }

    /// <summary>
    /// What shows up in the editor. 
    /// </summary>
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
        if (GUILayout.Button("Preview"))
        {
            ((AudioEvent) target).Play(_previewer);
        }
        EditorGUI.EndDisabledGroup();
    }
}
#endif

public abstract class AudioEvent : ScriptableObject
{
    public abstract void Play(AudioSource source);
}