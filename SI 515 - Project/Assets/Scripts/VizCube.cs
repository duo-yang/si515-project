using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VizCube : MonoBehaviour {
  private Light _pointLight;
  private Renderer _rend;

  void Awake() {
  }

  // Use this for initialization
  void Start () {
    _rend = GetComponent<Renderer>();
    _pointLight = this.transform.Find("Point Light").gameObject.GetComponent<Light>();
  }
	
	// Update is called once per frame
	void Update () {
	}

  public void setColor(float hue) {
    _rend.material.SetColor("_EmissionColor", Color.HSVToRGB(hue, 0.7F, 1.0F));
    _pointLight.color = Color.HSVToRGB(hue, 0.7F, 1.0F);
  }

  public void setLight(float light) {
    _pointLight.intensity = light;
  }
}
