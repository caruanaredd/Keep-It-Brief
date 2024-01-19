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
        }
        else
        {
            interaction.SetActive(false);
        }
    }

    // Triggered directly by the control system when the "InteractionComp" button is pressed
    void OnInteractionComp(InputValue value)
    {
        bool isPressed = value.Get<float>() != 0;

        // Call the ToggleCanvasAndFreezeGame method from the Companion script
        //companion.ToggleCanvasAndFreezeGame(isPressed);
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