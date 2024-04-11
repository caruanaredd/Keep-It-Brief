using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TeleportCamera : MonoBehaviour
{
    public CinemachineVirtualCamera destinationCamera; // Camera to switch to after teleportation

    


    private void SwitchCamera()
    {
        // Disable all cameras except the destination camera
        CinemachineVirtualCamera[] allCameras = FindObjectsOfType<CinemachineVirtualCamera>();
        foreach (CinemachineVirtualCamera camera in allCameras)
        {
            if (camera == destinationCamera)
            {
                // Activate the destination camera
                camera.Priority = 1;
            }
            else
            {
                // Deactivate other cameras
                camera.Priority = 0;
            }
        }
    }

    
}