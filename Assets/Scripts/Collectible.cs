using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    GameObject pauseCanvas;
    PauseMenu pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas = GameObject.Find("PhoneCanvas");
        pauseMenu = pauseCanvas.GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            HallSceneManager.completeLevels++;
            Destroy(gameObject);
            pauseMenu.UpdateScore();
        }
    }
}
