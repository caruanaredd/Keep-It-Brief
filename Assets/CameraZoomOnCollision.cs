using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomOnCollision : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float zoomFactor = 0.5f; // Amount to zoom in by
    public float zoomSpeed = 5f; // Speed at which the camera zooms in

    private float originalOrthographicSize;
    private float targetOrthographicSize;
    private bool isZooming = false;

    void Start()
    {
        originalOrthographicSize = virtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = originalOrthographicSize * (1 - zoomFactor); // Calculate target size
    }

    void Update()
    {
        if (isZooming)
        {
            float newSize = Mathf.MoveTowards(virtualCamera.m_Lens.OrthographicSize, targetOrthographicSize, zoomSpeed * Time.deltaTime);
            virtualCamera.m_Lens.OrthographicSize = newSize;

            if (Mathf.Approximately(newSize, targetOrthographicSize))
            {
                isZooming = false; // Zooming completed
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fist"))
        {
            Debug.Log("Triggered zoom");
            isZooming = true; // Start zooming in
        }
    }
}