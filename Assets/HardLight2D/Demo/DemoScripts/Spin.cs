using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float minSpeed = 0.1f;
    public float maxSpeed = 1f;

    void Update ()
    {
        transform.Rotate (Vector3.forward, Random.Range(minSpeed,maxSpeed) * Time.deltaTime * 100f);
    }
}