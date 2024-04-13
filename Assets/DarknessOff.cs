using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessOff : MonoBehaviour
{   
    public GameObject dark;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the triggering object has the "Fist" tag
        if (other.gameObject.CompareTag("Fist"))
        {
            // Turn off the dark GameObject
            dark.SetActive(false);
        }
    }
}