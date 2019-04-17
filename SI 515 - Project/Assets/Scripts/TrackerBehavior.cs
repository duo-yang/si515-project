using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerBehavior : MonoBehaviour {
  public TextMesh posText;
  private Transform _trans;

	// Use this for initialization
	void Start () {
    _trans = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
  }
}
