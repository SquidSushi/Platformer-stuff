using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float size = 1f;
    public float frequency = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, Mathf.Sin(Time.time * frequency) * Time.deltaTime * size, 0);
    }
}
