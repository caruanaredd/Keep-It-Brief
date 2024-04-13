using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomOnCollision : MonoBehaviour
{
   
     public GameObject dark;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fist"))
        {
            // Turn on the dark GameObject
            dark.SetActive(true);
        }
    }
}