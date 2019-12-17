using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundVisual : MonoBehaviour
{
    public float rmsValue;
    public float dbValue;
    public float pitchValue;
    public float visualModefier = 50;
    public float smoothSpeed = 10.00f;
    public float keepPerc = 0.6f;
    public float MaxScale = 9;

    private const int SAMPLE_SIZE = 1024;
    private AudioSource source;
    private float[] samples;
    private float[] spectrum;
    private float sampleRate;
    private Transform[] visualList;
    private float[] visualScale;
    private int amnVisual = 50;


    // Start is called before the first frame update
    void Start()
    {

        source = GetComponent<AudioSource>();
        samples = new float[SAMPLE_SIZE];
        spectrum = new float[SAMPLE_SIZE];
        sampleRate = AudioSettings.outputSampleRate;
        //spamLine();
        spawnCircle();
    }

    private void spamLine()
    {

        visualScale = new float[amnVisual];
        visualList = new Transform[amnVisual];

        for (int i = 0; i < amnVisual; i++)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
            visualList[i] = go.transform;
            visualList[i].position = Vector3.right * i;
            go.GetComponent<Renderer>().material.color =
                 Color.HSVToRGB(Random.value, 1, 1);
        }
    }

    private void spawnCircle()
    {
        visualScale = new float[amnVisual];
        visualList = new Transform[amnVisual];

        Vector3 center = Vector3.zero;
        float radius = 10f;
        for (int i = 0; i < amnVisual; i++)
        {
            float ang = i * 1f / amnVisual;
            ang = ang * Mathf.PI * 2;

            float x = center.x + Mathf.Cos(ang) * radius;
            float y = center.y + Mathf.Sin(ang) * radius;

            Vector3 pos = center + new Vector3(x, y, 0);
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
            go.GetComponent<Renderer>().material.color =
                 Color.HSVToRGB(Random.value, 1, 1);
            go.transform.position = pos;
            go.transform.rotation = Quaternion.LookRotation(Vector3.forward, pos);
            visualList[i] = go.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

        AnalyzeSound();
        updateVisual();

    }

    private void updateVisual()
    {
        int visualIndex = 10;
        int spectrumIndex = 10;
        int averageSize = (int)(SAMPLE_SIZE * keepPerc) / amnVisual;

        while (visualIndex < amnVisual)
        {
            int j = 0;
            float sum = 0;
            while (j < averageSize)
            {
                sum += spectrum[spectrumIndex];
                spectrumIndex++;
                j++;
            }

            float scaleY = sum / averageSize * visualModefier;
            visualScale[visualIndex] -= Time.deltaTime * smoothSpeed;
            if (visualScale[visualIndex] < scaleY)
                visualScale[visualIndex] = scaleY;

            if (visualScale[visualIndex] > MaxScale)
                visualScale[visualIndex] = MaxScale;


            visualList[visualIndex].localScale = Vector3.one + Vector3.up * visualScale[visualIndex];
            visualIndex++;

        }

    }

    void AnalyzeSound()
    {

        source.GetOutputData(samples, 0);
        int i = 0;
        float sum = 0;
        for (; i < SAMPLE_SIZE; i++)
        {
            sum += samples[i] * samples[i];
        }

        rmsValue = Mathf.Sqrt(sum / SAMPLE_SIZE);
        dbValue = 20 * Mathf.Log10(rmsValue / .1f);

        source.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
    }

}
