using System;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private Single parallaxEffect;

    private Single startPos;
    private Single length;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        Parallax();
    }

    private void Parallax()
    {
        Single dist = cam.transform.position.x * parallaxEffect;
        Single movement = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startPos + dist, cam.transform.position.y, transform.position.z);

        if (movement > startPos + length)
        {
            startPos += length;
        }
        else if (movement < startPos - length)
        {
            startPos -= length;
        }
    }
}
