using System;
using System.Threading;
using UnityEngine;

public class PitchTracker : MonoBehaviour
{
    #region Variables
    public Note CurrentNote;
    public delegate void NoteDelegate(string noteName);
    public event NoteDelegate NoteDetected;
    private static PitchTracker _instance;
    private static readonly int DB_THRESHOLD = -40;
    private static readonly int MAX_BUFFER_SIZE = 4096;
    private static readonly int SAMPLE_RATE = 44100;
    private static readonly double TIME_THRESHOLD = 0.25;
    private bool hasMicrophone;
    private Thread nativeThread;
    private AudioClip buffer;
    private float[] sampleBuffer;
    private double[] doubleSampleBuffer;
    private DyWavePitchTracker tracker;
    private int lastPosition;
    private int currentPosition;
    private double lastPitch;
    private string lastNoteCalculated;
    private string currrentDetectedNote;
    private float noteDeltaTime;
    #endregion
    #region Monobehaviors
    void Awake()
    {
        //Check if any instances of the
        //pitch tracker have been initialized;
        // quit if this is the case 

        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            enabled = false;
            return;
        }
    }

    void Start()
    {
        hasMicrophone = (Microphone.devices != null && Microphone.devices.Length > 0);

        if (!hasMicrophone)
        {
            Debug.LogWarning("This device does not appear to have a microphone. Disabling script!");
            this.gameObject.SetActive(false);
            enabled = false;
            return;
        }
        

        sampleBuffer = new float[MAX_BUFFER_SIZE];
        doubleSampleBuffer = new double[MAX_BUFFER_SIZE];
        lastPosition = currentPosition = 0;
        tracker = new DyWavePitchTracker();

        Debug.Log("Using Unity Audio; latency may be higher.");
        buffer = Microphone.Start("", true, 1, SAMPLE_RATE);


        lastNoteCalculated = currrentDetectedNote = "";
        noteDeltaTime = 0;
        lastPitch = 0;
    }

    public double GetCurrentPitch()
    {


        currentPosition = Microphone.GetPosition("");
        
        if (lastPosition == currentPosition)
        {
            return lastPitch;
        }

        var samplesRead = (currentPosition - lastPosition);
        
        if (samplesRead < 0)
        {
            samplesRead += SAMPLE_RATE;
        }

        buffer.GetData(sampleBuffer, lastPosition);
        Array.Copy(sampleBuffer, doubleSampleBuffer, Math.Min(samplesRead, sampleBuffer.Length));

        lastPosition = currentPosition;
        
        var sum = 0.0;
        for (int i = 0; i < samplesRead && i < sampleBuffer.Length; i++)
        {
            sum += sampleBuffer[i] * sampleBuffer[i];
        }

        var rms = Math.Sqrt(sum / samplesRead);
        var decibel = 20 * Math.Log10(rms);
        
        if (decibel < DB_THRESHOLD)
        {
            lastPitch = 0;
        }
        else
        {
            lastPitch = tracker.computePitch(doubleSampleBuffer, 0, Math.Min(samplesRead, doubleSampleBuffer.Length));
        }

        return lastPitch;
    }
    
    void Update()
    {
        var currentPitch = GetCurrentPitch();
        var currentAccuracy = Notation.GetNoteAccuracy((float)currentPitch);
        var note = Notation.GetNoteName((float)currentPitch);

        CurrentNote = CurrentNote ?? new Note();
        CurrentNote.Pitch = currentPitch;
        CurrentNote.Name = note;
        CurrentNote.Accuracy = currentAccuracy;


        currrentDetectedNote = currentPitch > 0 ? currrentDetectedNote : "";

        if (currentPitch > 0)// && lastNoteCalculated == note)
        {
            noteDeltaTime += Time.deltaTime;

            if (noteDeltaTime >= TIME_THRESHOLD
               && currrentDetectedNote != note
               && NoteDetected != null)
            {
                NoteDetected(note);
                currrentDetectedNote = note;
            }
        }
        else
        {
            noteDeltaTime = 0;
            lastNoteCalculated = note;
        }
    }

    #endregion
    #region Functions
    private float getSmoothAverage(float[] vals)
    {
        float minVal = float.MaxValue;
        float maxVal = 0;
        float sum = 0;

        foreach (var val in vals)
        {
            minVal = Mathf.Min(val, minVal);
            maxVal = Mathf.Max(val, maxVal);
            sum += val;
        }

        return (sum - minVal - maxVal) / (vals.Length - 2);
    }
   
    public static PitchTracker Tracker
    {
        get
        {
            return _instance;
        }
    }
    #endregion
}
