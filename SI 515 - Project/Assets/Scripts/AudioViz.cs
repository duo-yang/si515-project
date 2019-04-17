using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioViz : MonoBehaviour {
  private AudioSource _audioSource;
  public float[] samples = new float[512];

	// Use this for initialization
	void Start () {
    _audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
    getSpectrumfromAudioSource();
	}

  public void getSpectrumfromAudioSource (AudioSource source) {
    if (source != null) source.GetSpectrumData(samples, 0, FFTWindow.Blackman);
  }

  public void getSpectrumfromAudioSource () {
    getSpectrumfromAudioSource(_audioSource);
  }

}
