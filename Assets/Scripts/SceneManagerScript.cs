using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneManagerScript : MonoBehaviour
{
    public VideoPlayer transitionVideo; // Reference to your VideoPlayer component
    private int nextSceneIndex; // Index of the scene to load after the video
    private Canvas canvasComponent;

    private void Awake()
    {
        canvasComponent = GetComponent<Canvas>();
    }

    // Sets the next scene to load.
    public void SetNextScene(int nextScene)
    {
        nextSceneIndex = nextScene;
    }

    public void PlaySelectionVideo(VideoPlayer videoPlayer)
    {
        StartCoroutine(PlayVideoAndLoadScene(videoPlayer));
    }

    public void Quit() 
    {
        Application.Quit();
    }

    private IEnumerator PlayVideoAndLoadScene(VideoPlayer videoPlayer)
    {
        // Disabling the canvas will put the video in front
        canvasComponent.enabled = false;
        
        videoPlayer.Prepare();
        videoPlayer.Play();

        // Wait for the video to finish playing
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        transitionVideo.Play();
        // Wait for the video to finish playing
        while (transitionVideo.isPlaying)
        {
            yield return null;
        }

        Debug.Log(transitionVideo.isPlaying);
        // Load the next scene
        SceneManager.LoadScene(nextSceneIndex);
    }
}