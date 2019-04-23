using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Audio Events/Simple")]
public class SimpleAudioEvent : AudioEvent
{
    /// <summary>
    /// Audio clips to randomly play. 
    /// </summary>
    [SerializeField] private AudioClip[] clips;
    /// <summary>
    /// Volume.  From 0-1
    /// </summary>
    [SerializeField] private RangedFloat volume;
    /// <summary>
    /// Pitch from 0-2.
    /// </summary>
    [MinMaxRange(0, 2)]
    [SerializeField] private RangedFloat pitch;
    /// <summary>
    /// Plays the audio at the given audio source. 
    /// </summary>
    /// <param name="source"></param>
    public override void Play(AudioSource source)
    {
        // no clips, nothing to play
        if (clips.Length == 0) return;
        // choose random clip
        source.clip = clips[Random.Range(0, clips.Length)];
        // choose random volume
        source.volume = Random.Range(volume.minValue, volume.maxValue);
        // choose random pitch
        source.pitch = Random.Range(pitch.minValue, pitch.maxValue);
        // play!
        source.Play();
    }
}