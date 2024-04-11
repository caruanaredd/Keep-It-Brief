using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
public class Interaction : MonoBehaviour
{
     public GameObject interaction;
    

    private Movement movement;
    private Transform heldObject;
    private Companion companion; // Reference to the Companion script
    private Animator myAnimation;

    void Awake()
    {
        movement = GetComponent<Movement>();
        companion = GetComponentInChildren<Companion>(); // Adjust this based on your hierarchy
        myAnimation = GetComponent<Animator>();
    }

    // Triggered directly by the control system when the "Interact" button is pressed
    void OnInteract(InputValue value)
    {
        bool isPressed = value.Get<float>() != 0;

        Debug.Log("Interact button pressed: " + isPressed);

        if (isPressed)
        {
            if (heldObject == null)
            {
                StartCoroutine(DisableInteraction());
            }
            else
            {
                Release();
            }
        }
        else
        {
            interaction.transform.localPosition = Vector2.zero;
        }

        if (isPressed)
        {
            interaction.SetActive(true);
            Debug.Log("Interaction object active: " + interaction.activeSelf);
            // Check if the interaction object is a door, then teleport if it is
            //if (interaction.CompareTag("Door"))
            //{
               // Debug.Log("Interacting with door");
               // Teleport();
           // }
        }
        else
        {
            interaction.SetActive(false);
        }
    }

    IEnumerator DisableInteraction()
    {
        interaction.transform.localPosition = movement.direction.ToVector2();
        yield return new WaitForSeconds(0.25f);
        interaction.transform.localPosition = Vector2.zero;
    }

    

    // The Hold method to be called by other scripts
    public void Hold(Transform obj)
    {
        if (heldObject != null)
            return;

        heldObject = obj;
        myAnimation.SetBool("isPushing", true);
    }

    public void Release()
    {
        heldObject.SetParent(null);
        heldObject = null;
        myAnimation.SetBool("isPushing", false);
    }
}