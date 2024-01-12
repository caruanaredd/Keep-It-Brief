using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fist"))
        {
            transform.SetParent(other.transform.parent);
            Interaction interaction = other.GetComponentInParent<Interaction>();
            interaction.Hold(transform);
        }
    }
}