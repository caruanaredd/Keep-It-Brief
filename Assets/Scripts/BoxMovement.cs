using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{

    public float moveDistance = 0.5f;
    private Vector3 originalPosition;
    private bool isInteracted = false;
    private Vector3 hitDirection;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (isInteracted)
        {
            // Move the box in the direction it was hit from
            transform.position += hitDirection * moveDistance;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isInteracted)
            {
                transform.SetParent(other.transform.parent);
            }
            else
            {
                transform.SetParent(null);
            }
            
            isInteracted = !isInteracted;
        }
    }
}