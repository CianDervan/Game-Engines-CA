using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulse : MonoBehaviour
{
    public GameObject _sampleCubePrefab;
    GameObject[] _sampleCube = new GameObject[128];
    public float _maxScale;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < 128; i++)
        {
            GameObject _instanceSampleCube = (GameObject)Instantiate(_sampleCubePrefab);
            _instanceSampleCube.transform.position = this.transform.position;
            _instanceSampleCube.transform.parent = this.transform;
            _instanceSampleCube.name = "sampleCube" + i;
            this.transform.eulerAngles = new Vector3(0, -2.8125f * i, 0);
            _sampleCube[i] = _instanceSampleCube;
        }

    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < 128; i++)
        {

            if (_sampleCube != null)
            {

                _sampleCube[1].transform.localScale = new Vector3(10, (pulseSetUp._samples[i] * _maxScale) + 2, 10);

            }

        }

    }
}
