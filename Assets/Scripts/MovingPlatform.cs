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

    private void OnDrawGizmosSelected()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.green;
        Vector3 moveOffset = new Vector3(0, size, 0);
        moveOffset += transform.position; 
        Gizmos.DrawLine(transform.position, moveOffset);
        Gizmos.DrawWireCube(moveOffset, new(transform.localScale.x * collider.size.x, transform.localScale.y * collider.size.y, 1));
    }
}
