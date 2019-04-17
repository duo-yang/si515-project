using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmpBehavior : MonoBehaviour {
  private AudioAnalyzer _audioNode;
  private Transform _trans;
  private Renderer _rend;
  private Vector3 _originalScale;
  public Light lightSource;
  public Color colorOn;
  public Color colorOff;

  private bool _lightOn = true;

	// Use this for initialization
	void Start () {
    _trans = this.transform;
    _originalScale = _trans.localScale;
    _rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
    _trans.localScale = (AudioAnalyzer.Amplitude[(int)AudioAnalyzer.Channel.Stereo] + 1) * _originalScale;
	}

  public void toggleLight() {
    if (_lightOn) {
      _rend.material.SetColor("_Color", colorOff);
      _rend.material.SetColor("_EmissionColor", colorOff);
      lightSource.color = colorOff;
    } else {
      _rend.material.SetColor("_Color", colorOn);
      _rend.material.SetColor("_EmissionColor", colorOn);
      lightSource.color = colorOn;
    }
    _lightOn = !_lightOn;
  }
}
