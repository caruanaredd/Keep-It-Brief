using UnityEngine;

public class LoadRoomAudioOnTrigger : MonoBehaviour
{
    public SceneType sceneType;

    private void OnTriggerEnter2D(Collider2D col)
    {
        AudioManager.instance.LoadBGM(sceneType);
    }
}