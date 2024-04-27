using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public static VideoManager instance; // Singleton instance

    public Canvas videoCanvas; // Assign this in the Inspector
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        // Check if videoCanvas is null
        if (videoCanvas == null)
        {
            Debug.LogError("Canvas not assigned! Assign 'videoCanvas' in the Inspector.");
            return;
        }
        videoCanvas.enabled = false;

        // Get the VideoPlayer component
        videoPlayer = GetComponentInChildren<VideoPlayer>();       

        // Check if videoPlayer is null
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer component not found within the assigned Canvas.");
            return;
        }

        // Set the sorting order to ensure the video is displayed on top of everything
        videoCanvas.sortingOrder = 100;
    }

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            DestroyImmediate(gameObject);
            return;
        }
    }

    private void OnDisable()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void PlayLoadingVideo()
    {
        StartCoroutine(PlayVideoRoutine());
    }

    private IEnumerator PlayVideoRoutine()
    {
       // Activate the video Canvas
    videoCanvas.enabled = true;

    // Start playing the video
    videoPlayer.Play();

    while (videoPlayer.isPlaying)
    {
        yield return null;
    }

    // Stop the video and reset its time position
    videoPlayer.Stop();
    videoPlayer.time = 0f;

    // Deactivate the video Canvas
    videoCanvas.enabled = false;
    }
}
