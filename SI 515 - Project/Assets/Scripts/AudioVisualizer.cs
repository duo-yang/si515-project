using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizer : MonoBehaviour {
  public GameObject cubePrefab;

  public AudioAnalyzer.Channel channel;
  public float maxScale = 1.0F;
  public float minSize = 1.0F;
  public float barSize = 10.0F;
  public float barMargin = 2.0F;
  public float barRotation = 6.0F;

  public bool useLight = false;
  public bool useEmit = false;

  private int _sampleSize = 0;
  private float[,] _sampleSource;

  private GameObject[] _sampleCubes;

  // Use this for initialization
  void Start () {
    _sampleSize = AudioAnalyzer._bandCounts;
    _sampleCubes = new GameObject[_sampleSize];
    for (int i = 0; i < _sampleSize; i++) {
      GameObject _instCube = (GameObject)Instantiate(cubePrefab, this.transform);
      _instCube.transform.localPosition = Vector3.right * (barSize + barMargin) * i;
      _instCube.transform.localEulerAngles = new Vector3(barRotation * i, 0, 0);
      _instCube.name = "SampleCube" + i;
      _sampleCubes[i] = _instCube;
    }
	}
	
	// Update is called once per frame
	void Update () {
    for (int i = 0; i < _sampleSize; i++) {
      if (_sampleCubes != null) {
        _sampleCubes[i].transform.localScale = new Vector3(barSize, (AudioAnalyzer.audioBandBuffer[i, (int) channel] * maxScale) + minSize, barSize);

        if (useEmit) _sampleCubes[i].GetComponent<VizCube>().setColor(AudioAnalyzer.audioBandBuffer[i, (int)channel]);
        if (useLight) _sampleCubes[i].GetComponent<VizCube>().setLight(AudioAnalyzer.audioBandBuffer[i, (int)channel]);
      }
    }
  }

}
