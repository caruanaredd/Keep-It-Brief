using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
   public GameObject childObject;
    public float activationRadius = 5f;
    public float activeDuration = 5f; // Duration in seconds for which the child object stays active

    private Coroutine activationCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateChildObject();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DeactivateChildObjectAfterDelay();
        }
    }

    private void ActivateChildObject()
    {
        if (childObject != null)
        {
            childObject.SetActive(true);
            // Start the coroutine to deactivate the child object after a delay
            if (activationCoroutine != null)
                StopCoroutine(activationCoroutine);
            activationCoroutine = StartCoroutine(DeactivateChildObjectCoroutine());
        }
    }

    private IEnumerator DeactivateChildObjectCoroutine()
    {
        yield return new WaitForSeconds(activeDuration);
        DeactivateChildObject();
    }

    private void DeactivateChildObjectAfterDelay()
    {
        // If the player exits the trigger, cancel the deactivation coroutine
        if (activationCoroutine != null)
            StopCoroutine(activationCoroutine);
        activationCoroutine = null;
        // Start a new coroutine with delay to deactivate the child object
        activationCoroutine = StartCoroutine(DeactivateChildObjectCoroutine());
    }

    private void DeactivateChildObject()
    {
        if (childObject != null)
        {
            childObject.SetActive(false);
            // Reset the activation coroutine reference
            activationCoroutine = null;
        }
    }
}