using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnLocation : MonoBehaviour
{
    void Awake()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && (SceneManager.GetActiveScene().name == "Hall"))
        {
            PlayerPrefs.SetFloat("SpawnX", transform.position.x);
            PlayerPrefs.SetFloat("SpawnY", transform.position.y);
        }

        if (other.CompareTag("Player") && (SceneManager.GetActiveScene().name == "Main"))
        {
            PlayerPrefs.SetFloat("StaffX", transform.position.x);
            PlayerPrefs.SetFloat("StaffY", transform.position.y);
        }
    }
}
