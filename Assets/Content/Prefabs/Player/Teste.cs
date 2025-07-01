using System;
using UnityEngine;

public class Teste : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float msecs = Time.realtimeSinceStartup * 1000f;

        transform.position = new Vector3(
            Mathf.Sin(msecs * 0.01f) * 1.0f,
            Mathf.Cos(msecs * 0.01f) * 1.0f,
            transform.position.z
        );
    }
}
