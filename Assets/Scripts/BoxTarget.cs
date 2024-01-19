using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BoxTarget : MonoBehaviour
{
    public Sprite newSprite; // Assign the new sprite in the Unity Editor
    public int boxType;
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (boxType)
        {
            case 1:
                if (!other.CompareTag("Computer"))
                    return;
                break;
            case 2:
                if (!other.CompareTag("Laptop"))
                    return;
                break;
        }

        other.transform.SetParent(transform);
        other.transform.localPosition = Vector3.zero;
        AudioManager.instance.PlaySoundEffect(0);
        // Change the sprite of the object when triggered
        ChangeObjectSprite(newSprite);

        BoxMovement bm = other.GetComponent<BoxMovement>();
        bm?.Release();
        
        Destroy(other.gameObject);

        BoxSceneManager.correctSlots++;
    }

    // Function to change the sprite of the object
    void ChangeObjectSprite(Sprite sprite)
    {
        // Get the SpriteRenderer component attached to the GameObject
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if the SpriteRenderer component exists
        if (spriteRenderer != null)
        {
            // Assign the new sprite to the SpriteRenderer component
            spriteRenderer.sprite = sprite;
        }
        else
        {
            // Print a warning if the SpriteRenderer component is not found
            Debug.LogWarning("SpriteRenderer component not found on this GameObject.");
        }
    }
}
