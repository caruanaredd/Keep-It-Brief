using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BoxTarget : MonoBehaviour
{
    public Collider2D target;
    public Sprite newSprite; // Assign the new sprite in the Unity Editor

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == target)
        {
            other.transform.SetParent(transform);
            other.transform.localPosition = Vector3.zero;
            AudioManager.instance.PlaySoundEffect(0);
            Destroy(target.gameObject);
            // Change the sprite of the object when triggered
            ChangeObjectSprite(newSprite);
        }
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
