using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoManager : MonoBehaviour
{
    public static VideoManager instance; // Singleton instance

    [SerializeField] private CinemachineVirtualCamera offWorldCamera;
    private CinemachineBrain _cinemachineBrain;
    
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        _cinemachineBrain = FindObjectOfType<CinemachineBrain>();

        // Get the VideoPlayer component
        videoPlayer = GetComponentInChildren<VideoPlayer>();
        videoPlayer.Prepare();
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
        // Start playing the video
        videoPlayer.Play();

        // Move the camera out of the world
        _cinemachineBrain.m_DefaultBlend.m_Time = 0;
        offWorldCamera.Priority = 1000;

        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        // Prepare the video for another viewing
        videoPlayer.Stop();
        videoPlayer.Prepare();

        // Restore the Cinemachine brain
        offWorldCamera.Priority = -1;
        _cinemachineBrain.m_DefaultBlend.m_Time = _cinemachineBrain.m_DefaultBlend.BlendTime;
    }
}
