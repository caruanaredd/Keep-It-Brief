using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitchScript : MonoBehaviour
{
    public CinemachineVirtualCamera firstCamera;
    public CinemachineVirtualCamera secondCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Switch camera priorities
            firstCamera.Priority = 0; // Lower priority
            secondCamera.Priority = 1; // Higher priority
        }
    }
}
