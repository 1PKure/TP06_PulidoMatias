using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 prevCameraPosition;
    private float spriteWidth, startPosition;
    [SerializeField] private float parallaxMultiplier;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        prevCameraPosition = cameraTransform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        startPosition = transform.position.x;
    }


    void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - prevCameraPosition.x) * parallaxMultiplier * Time.deltaTime * 1000;
        float moveAmount = cameraTransform.position.x * (1 - parallaxMultiplier);
        transform.Translate(new Vector3 (deltaX, 0, 0));
        prevCameraPosition = cameraTransform.position;

        if (moveAmount > (startPosition + spriteWidth))
        {
            transform.Translate(new Vector3(spriteWidth, 0, 0));
            startPosition += spriteWidth;
        }
        else if (moveAmount < (startPosition - spriteWidth))
        {
            transform.Translate(new Vector3(-spriteWidth,0, 0));
            startPosition -= spriteWidth;
        }
    }
}
