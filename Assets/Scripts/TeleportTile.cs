using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TeleportTile : MonoBehaviour
{

public GridObject teleportDestination;
public GridObject player;
private bool isTeleporting = false;
private bool isGamePaused = false;

void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Fist"))
    {
        isTeleporting = true;
        VideoManager.instance.PlayLoadingVideo();
        Debug.Log("Teleporting...");

        // Pause game logic (optional) CHECK ME
        // if (Time.timeScale > 0f)
        // {
        //     Time.timeScale = 0f;
        //     isGamePaused = true;
        // }
    }
}

void Update()
{
    if (isTeleporting)
    {
        // Teleport the player when the video stops playing
        player.Teleport(teleportDestination.Cell);
        isTeleporting = false;

        // Resume game logic (if paused) CHECK ME
        if (isGamePaused)
        {
            Time.timeScale = 1f;
            isGamePaused = false;
        }
    }
}
}