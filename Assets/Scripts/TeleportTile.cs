using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TeleportTile : MonoBehaviour
{

public GridObject teleportDestination;
public GridObject player;
public Canvas videoCanvas; // Assign this in the Inspector

private VideoPlayer videoPlayer;
private bool isTeleporting = false;
private bool isGamePaused = false;

void Start() 
{
    // Check if videoCanvas is null
    if (videoCanvas == null)
    {
        Debug.LogError("Canvas not assigned! Assign 'videoCanvas' in the Inspector.");
        return;
    }

    // Get the VideoPlayer component
    videoPlayer = videoCanvas.GetComponentInChildren<VideoPlayer>();

    // Check if videoPlayer is null
    if (videoPlayer == null)
    {
        Debug.LogError("VideoPlayer component not found within the assigned Canvas.");
        return;
    }

    // Set the sorting order to ensure the video is displayed on top of everything
    videoCanvas.sortingOrder = 100;

    // Subscribe to the video preparation completed event
    videoPlayer.prepareCompleted += OnVideoPrepared;
    videoPlayer.loopPointReached += OnVideoFinished;

    // Disable the canvas initially
    videoCanvas.enabled = false; 

    // Start preparing the video immediately
    videoPlayer.Prepare();
}

void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Fist"))
    {
        isTeleporting = true;
        Debug.Log("Teleporting...");

        // Activate the video Canvas
        videoCanvas.enabled = true;

        // Start playing the video
        videoPlayer.Play();

        // Pause game logic (optional)
        if (Time.timeScale > 0f)
        {
            Time.timeScale = 0f;
            isGamePaused = true;
        }
    }
}

public void OnVideoPrepared(VideoPlayer vp)
{
    // Play the video when preparation is complete (you might not need this if the video plays automatically)
    //vp.Play(); 
}


public void OnVideoFinished(VideoPlayer vp)
{
    // Stop the video when it reaches the end
    vp.Stop();

    // Hide the video Canvas after it has finished playing
    videoCanvas.enabled = false;
}

void Update()
{
    if (isTeleporting && !videoPlayer.isPlaying)
    {
        // Teleport the player when the video stops playing
        player.Teleport(teleportDestination.Cell);
        isTeleporting = false;

        // Resume game logic (if paused)
        if (isGamePaused)
        {
            Time.timeScale = 1f;
            isGamePaused = false;
        }
    }
}
}