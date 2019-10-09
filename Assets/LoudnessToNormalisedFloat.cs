using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoudnessToNormalisedFloat : MonoBehaviour
{

    public float MicLoudness;
    public float minVolume = 0;
    public float maxVolume; 
    private string _device;

    [SerializeField] private SharedNormalisedFloat _sharedNormalisedVolume;


    bool _isInitialized;

    AudioClip _clipRecord = null;

    int _sampleWindow = 128;

    void OnEnable()
    {
        InitMic();
        _isInitialized = true;
    }

    void Update()
    {
        float loudness = 0; 


        float[] waveData = new float[_sampleWindow];

        int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1); // null means the first microphone

        if (micPosition < 0)
        {
            return;
        }

        _clipRecord.GetData(waveData, micPosition);

        // Getting a peak on the last 128 samples
        for (int i = 0; i < _sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];

            if (loudness < wavePeak)
            {
                loudness = wavePeak;
            }

            if (maxVolume < loudness)
            {
                maxVolume = loudness;
            }
        }

        _sharedNormalisedVolume.value = loudness;
        _sharedNormalisedVolume.NormaliseClamped();
        MicLoudness = loudness; 
    }

    void InitMic()
    {
        if (_device == null)
        {
            _device = Microphone.devices[0];
        }

        _clipRecord = Microphone.Start(_device, true, 999, 44100);
    }

    void StopMicrophone()
    {
        Microphone.End(_device);
        _sharedNormalisedVolume.value = 0; 
    }

    void OnDisable()
    { 
        StopMicrophone();
    }

    void OnDestroy()
    {
        StopMicrophone();
    }

    void OnApplicationFocus(bool focused)
    {
        if (focused)
        {
            if (!_isInitialized)
            {
                InitMic();
                _isInitialized = true;
            }
        }
        else
        {
            StopMicrophone();
            _isInitialized = false;

        }
    }
}
