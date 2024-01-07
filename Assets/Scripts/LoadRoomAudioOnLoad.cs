using System;
using UnityEngine;

public class LoadRoomAudioOnLoad : MonoBehaviour
{
    public SceneType sceneType;

    private void Start()
    {
        AudioManager.instance.LoadBGM(sceneType);
    }
}