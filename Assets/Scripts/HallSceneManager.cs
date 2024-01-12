using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HallSceneManager : MonoBehaviour
{
    public float spawnPointX;
    public float spawnPointY;
    public GameObject player;
    void Awake()
    {
        spawnPointX = PlayerPrefs.GetFloat("SpawnX");
        spawnPointY = PlayerPrefs.GetFloat("SpawnY");
        player = GameObject.Find("/Player");
        player.transform.position = new Vector2(spawnPointX, spawnPointY);
    }
}
