using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform pointA; 
    public Transform pointB;
    public float platformSpeed = 2f;
    private bool movingToB = true; 

    void Update()
    {
        Transform targetPoint = movingToB ? pointB : pointA;

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, platformSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            movingToB = !movingToB;
        }
    }

}
