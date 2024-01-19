using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Companion : MonoBehaviour
{
    public Canvas roboCanvas;
    public GameObject canvas;

    private bool isCanvasEnabled = false; // Track the state of the canvas

    void Start()
    {
        // Ensure the canvas is initially turned off
      
    }

    // OnTriggerEnter2D is called when a collider enters the trigger zone
    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fist"))
        {
            // Call the method when the player enters the trigger zone
            ToggleCanvasAndFreezeGame(!isCanvasEnabled); // Toggle the state
        }
    }

    // OnTriggerExit2D is called when a collider exits the trigger zone
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Fist"))
        {
            // Call the method when the player exits the trigger zone
            ToggleCanvasAndFreezeGame(!isCanvasEnabled); // Toggle the state
        }
    }

    public void ToggleCanvasAndFreezeGame(bool enableCanvas)
    {
        // Toggle the canvas based on the provided parameter
        roboCanvas.enabled = enableCanvas;
        isCanvasEnabled = enableCanvas;

        // Freeze or unfreeze the game based on the canvas state
        Time.timeScale = (roboCanvas.enabled) ? 0f : 1f;

        // Log statements for debugging
        Debug.Log("Canvas State: " + roboCanvas.enabled);
        Debug.Log("Time Scale: " + Time.timeScale);
    }*/
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fist"))
        {
            StartCoroutine(Popup());
        }
    }

    public IEnumerator Popup()
   {
       canvas.SetActive(true);
       yield return new WaitForSeconds(5f);
       canvas.SetActive(false);
   }
}