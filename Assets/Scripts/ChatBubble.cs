using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    public GameObject childObject;
    public float activationRadius = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ActivateChildObject();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DeactivateChildObject();
        }
    }

    private void ActivateChildObject()
    {
        if (childObject != null)
        {
            childObject.SetActive(true);
        }
    }

    private void DeactivateChildObject()
    {
        if (childObject != null)
        {
            childObject.SetActive(false);
        }
    }
}