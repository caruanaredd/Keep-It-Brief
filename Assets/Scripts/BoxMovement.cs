using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    private Interaction _interaction;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fist"))
        {
            transform.SetParent(other.transform.parent);
            _interaction = other.GetComponentInParent<Interaction>();
            _interaction.Hold(transform);
        }
    }

    public void Release()
    {
        _interaction.Release();
    }
}