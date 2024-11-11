using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    [SerializeField] private Transform characterTransform;
    [SerializeField] private Vector3 offset;            
    [SerializeField] private RectTransform healthBarUI; 
    [SerializeField] private Camera mainCamera;         

    void Update()
    {
        if (characterTransform != null && healthBarUI != null)
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(characterTransform.position + offset);
            healthBarUI.position = screenPos;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
}
