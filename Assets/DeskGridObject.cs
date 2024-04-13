using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeviceType { Computer, Laptop }

public class DeskGridObject : GridObject
{
    [SerializeField] private DeviceType type;
    [SerializeField] private Sprite newSprite; // Assign the new sprite in the Unity Editor

    protected override void OnStopMoving()
    {

        var targetLayer = 1 << LayerMask.NameToLayer("Targets");
        var collider = Physics2D.OverlapCircle(transform.position, 0.25f, targetLayer);
        if (collider == null)
        {
            // just to keep the code clean
            return;
        }

        switch (type)
        {
            case DeviceType.Computer:
                if (!collider.CompareTag("Computer"))
                    return;
                break;
            case DeviceType.Laptop:
                if (!collider.CompareTag("Laptop"))
                    return;
                break;
        }

        AudioManager.instance.PlaySoundEffect(0);
        BoxSceneManager.correctSlots++;
        ChangeObjectSprite(newSprite);
        Destroy(collider.gameObject);
        Destroy(this); // destroys just the script but not the object
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
