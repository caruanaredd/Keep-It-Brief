using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton instance

    public AudioClip[] sceneClips; // Array to hold different scene background music
    public AudioClip[] soundEffects; // Array to hold various sound effects

    public AudioSource backgroundMusicSource;
    public AudioSource soundEffectSource;
    public AudioSource CreepyBGM;

    private float distanceTravelled = 0f;
    private Transform playerTransform;
    public float maxVolume = 1f;
    public float minVolume = 0f;
    public float maxDistance = 5f;

    private void Awake()
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

    private void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            instance = null;
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the scene loaded event
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Update the distance travelled based on player position
        if (playerTransform != null)
        {
            distanceTravelled = playerTransform.position.magnitude;
        }

        // Adjust the volume based on distance travelled
        AdjustVolume();
    }

    private void AdjustVolume()
    {
        // Assuming that both songs have the same volume control in the Audio Mixer
        float normalizedVolume = Mathf.Clamp01(distanceTravelled / maxDistance);
        float volume = Mathf.Lerp(minVolume, maxVolume, normalizedVolume);

        // Set the volume for both audio sources
        CreepyBGM.volume = volume;
        backgroundMusicSource.volume = volume;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check the scene and play the appropriate background music
        switch (scene.name)
        {
            case "Welcome":
                LoadBGM(SceneType.Welcome);
                break;
            case "Scene1":
                LoadBGM(SceneType.Scene1);
                break;
            case "Scene4":
                LoadBGM(SceneType.Scene4);
                break;
            case "Hall":
                LoadBGM(SceneType.Hall);
                break;
            // Add more cases for additional scenes as needed
        }
    }

    public void LoadBGM(SceneType type)
    {
        // Check the scene and play the appropriate background music
        switch (type)
        {
            case SceneType.Welcome:
                PlayBackgroundMusic(sceneClips[1]);
                break;
            case SceneType.Scene1:
                PlayBackgroundMusic(sceneClips[1]);
                break;
            case SceneType.Scene4:
                PlayBackgroundMusic(sceneClips[0]);
                break;
            case SceneType.Hall:
                PlayBackgroundMusic(sceneClips[0]);
                PlayCreepyBGM(sceneClips[2]);
                break;
            // Add more cases for additional scenes as needed
        }
    }

    private void PlayCreepyBGM(AudioClip clip)
    {
        if (CreepyBGM != null)
        {
            CreepyBGM.clip = clip;
            CreepyBGM.loop = true;
            CreepyBGM.Play();
        }
        else
        {
            Debug.LogError("CreepyBGM is null. Check if the GameObject is destroyed.");
        }
    }


    public void PlaySoundEffect(int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < soundEffects.Length)
        {
            soundEffectSource.PlayOneShot(soundEffects[soundIndex]);
        }
        else
        {
            Debug.LogWarning("Invalid sound effect index");
        }
    }

    private void PlayBackgroundMusic(AudioClip clip)
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.clip = clip;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
        else
        {
            Debug.LogError("BackgroundMusicSource is null. Check if the GameObject is destroyed.");
        }
    }
}