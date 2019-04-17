using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioAnalyzer : MonoBehaviour {
  public static int _bandCounts = 8;
  public static int _sampleCounts = 512;

  public bool useBuffer = true;
  public float bufferDecreaseSpeed = 0.005f;
  public float audioProfile = 5.0f;

  [HideInInspector]
  public static float[,] audioBand, audioBandBuffer;

  public static float[] Amplitude = new float[3];
  public static float[] AmplitudeBuffer = new float[3];

  public enum Channel { Left = 0, Right = 1, Stereo = 2 };

  private AudioSource _audioSource;

  private float[] samplesLeft, samplesRight;

  private float[,] freqBands;
  private float[,] bandBuffer;

  private float[,] _bufferDecrease;
  private float[,] _freqBandHighest;
  private float[] _ampHighest = new float[3];

  // Use this for initialization
  void Start () {
    _audioSource = GetComponent<AudioSource>();

    audioBand = new float[_bandCounts, 3];
    audioBandBuffer = new float[_bandCounts, 3];

    samplesLeft = new float[_sampleCounts];
    samplesRight = new float[_sampleCounts];

    freqBands = new float[_bandCounts, 3];
    bandBuffer = new float[_bandCounts, 3];

    _bufferDecrease = new float[_bandCounts, 3];

    _freqBandHighest = new float[_bandCounts, 3];

    useAudioProfile(audioProfile);
	}
	
	// Update is called once per frame
	void Update () {
    getSpectrumfromAudioSource();
    getFreqBands();
	}

  private void getSpectrumfromAudioSource (AudioSource source) {
    if (source != null) {
      source.GetSpectrumData(samplesLeft, (int) Channel.Left, FFTWindow.Blackman);
      source.GetSpectrumData(samplesRight, (int) Channel.Right, FFTWindow.Blackman);
    }
  }

  private void getSpectrumfromAudioSource () {
    getSpectrumfromAudioSource(_audioSource);
  }

  private void getFreqBands () {
    int count = 0;
    float[] curAmp = new float[3];
    float[] curAmpBuffer = new float[3];

    for (int i = 0; i < _bandCounts; i++) {
      float avgLeft = 0;
      float avgRight = 0;
      int sampleCount = (int)Mathf.Pow(2, i + 1);
      if (i == _bandCounts - 1) {
        sampleCount += 2;
      }
      for (int j = 0; j < sampleCount; j++) {
        avgLeft += samplesLeft[count] * (count + 1);
        avgRight += samplesRight[count] * (count + 1);
        count++;
      }
      avgLeft /= count;
      avgRight /= count;
      freqBands[i, (int) Channel.Left] = avgLeft * 10;
      freqBands[i, (int) Channel.Right] = avgRight * 10;
      freqBands[i, (int) Channel.Stereo] = (freqBands[i, (int) Channel.Left] + freqBands[i, (int) Channel.Right]) / 2;

      for (int k = 0; k < 3; k++) {
        if (useBuffer) {
          if (bandBuffer[i, k] <= freqBands[i, k]) {
            _freqBandHighest[i, k] = (_freqBandHighest[i, k] < freqBands[i, k]) ? freqBands[i, k] : _freqBandHighest[i, k];
            bandBuffer[i, k] = freqBands[i, k];
            _bufferDecrease[i, k] = bufferDecreaseSpeed;
          } else {
            bandBuffer[i, k] -= _bufferDecrease[i, k];
            _bufferDecrease[i, k] *= 1.2f;
          }

          audioBandBuffer[i, k] = bandBuffer[i, k] / _freqBandHighest[i, k];
          curAmpBuffer[k] += audioBandBuffer[i, k];

        }

        audioBand[i, k] = freqBands[i, k] / _freqBandHighest[i, k];
        curAmp[k] += audioBand[i, k];

      }
    }

    for (int k = 0; k < 3; k++) {
      if (curAmp[k] > _ampHighest[k]) _ampHighest[k] = curAmp[k];

      Amplitude[k] = curAmp[k] / _ampHighest[k];
      AmplitudeBuffer[k] = curAmpBuffer[k] / _ampHighest[k];
    }
  }

  private void useAudioProfile(float profile) {
    for (int i = 0; i < _bandCounts; i++) {
      for (int k = 0; k < 3; k++) {
        _freqBandHighest[i, k] = profile;
      }
    }
  }

}
