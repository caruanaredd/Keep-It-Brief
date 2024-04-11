using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTesting : MonoBehaviour
{
 public CinemachineVirtualCamera generalCamera;

    void Start()
    {
        generalCamera.Priority = 0;
    }

    void OnTriggerEnter2D(Collider2D other){

        if (other.CompareTag("Player")) {
            generalCamera.Priority= 1;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            generalCamera.Priority= 0;
        }
    }
}