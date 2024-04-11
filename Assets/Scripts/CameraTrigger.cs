using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{

    public CinemachineVirtualCamera firstCamera;
    public CinemachineVirtualCamera secondCamera;
    public CinemachineVirtualCamera boxCamera;

    private bool isFirstCameraActive = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Toggle between cameras
            isFirstCameraActive = !isFirstCameraActive;

            if (isFirstCameraActive)
            {
                // Switch to the first camera
                ActivateFirstCamera();
            }
            else
            {
                // Switch to the second camera
                ActivateSecondCamera();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Transitioning to the next area, switch to boxCamera
            ActivateBoxCamera();
        }
    }

    private void ActivateFirstCamera()
    {
        firstCamera.Priority = 1;
        secondCamera.Priority = 0;
        boxCamera.Priority = 0; // Ensure boxCamera is not active
    }

    private void ActivateSecondCamera()
    {
        firstCamera.Priority = 0;
        secondCamera.Priority = 1;
        boxCamera.Priority = 0; // Ensure boxCamera is not active
    }

    private void ActivateBoxCamera()
    {
        firstCamera.Priority = 0;
        secondCamera.Priority = 0;
        boxCamera.Priority = 1;
    }
}