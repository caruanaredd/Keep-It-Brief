using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffSceneManager : MonoBehaviour
{
    public float staffSpawnPointX;
    public float staffSpawnPointY;

    public GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        staffSpawnPointX = PlayerPrefs.GetFloat("StaffX");
        staffSpawnPointY = PlayerPrefs.GetFloat("StaffY");
        player = GameObject.Find("/Player");
        player.transform.position = new Vector2(staffSpawnPointX, staffSpawnPointY);
    }
}
