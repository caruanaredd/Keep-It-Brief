using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneManagerScript : MonoBehaviour
{
    public VideoPlayer transitionVideo; // Reference to your VideoPlayer component
    public int nextSceneIndex; // Index of the scene to load after the video

    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
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

        // Load the next scene
        SceneManager.LoadScene(nextSceneIndex);
    }
}