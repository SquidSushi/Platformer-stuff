using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public PlayerController target;
    public float minSpeed = 1;
    public float percentSpeed = 0.9f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        Vector2 fullWay = (target.state.CamOffset() + target.transform.position) - transform.position;
        float distance = fullWay.magnitude;
        float percentageWay = (fullWay * (1 - Mathf.Pow(percentSpeed, Time.deltaTime))).magnitude;
        float fixedway = minSpeed * Time.deltaTime;
        float z = transform.position.z;
        if (distance < fixedway)
        {
            transform.position = target.state.CamOffset() + target.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
            return;
        }
        transform.Translate(fullWay.normalized * Mathf.Max(percentageWay, fixedway));
        return;
    }
}
