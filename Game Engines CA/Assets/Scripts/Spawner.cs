using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public int width = 20;
    public int height = 20;

    void Start()
    {
        int halfWidth = width / 2;

        for (int j = 0; j < height; j++)
        {
            for (int i = -halfWidth; i < halfWidth; i++)
            {
                Vector3 pos = transform.TransformPoint(new Vector3(0, i, j+1));


                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = pos;
                cube.transform.rotation = transform.rotation;

                cube.GetComponent<Renderer>().material.color =
                    Color.HSVToRGB(Random.value, 1, 1);

                cube.transform.parent = this.transform; // Parenting

                

            }
        }
    }





}
