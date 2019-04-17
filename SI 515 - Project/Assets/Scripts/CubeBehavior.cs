using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour {

  private Light _pointLight;
  private Renderer _rendCube;

  // Use this for initialization
  void Start () {
    _pointLight = this.transform.Find("Point Light").GetComponent<Light>();
    _rendCube = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {

  }

  public void updateLight(float intensity) {
    if (_pointLight != null) {
      _pointLight.intensity = intensity;
    }
  }

  public void updateEmit(float alpha) {
    if (_rendCube != null) {
      _rendCube.material.SetColor("_EmissionColor", new Color(0.08088237F, 0.5055777F, 1.0F, alpha));
    }
  }

}
