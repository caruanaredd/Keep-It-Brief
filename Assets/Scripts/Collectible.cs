using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameObject pauseCanvas;
    PauseMenu pauseMenu;

    public int currentLevelnumber;
    // Start is called before the first frame update
    void Start()
    {
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
            /*HallSceneManager.completeLevels++;
            Destroy(gameObject);
            pauseMenu.UpdateScore();*/

            completeCurrentLevel();
            Destroy(gameObject);
        }
    }

    public void completeCurrentLevel()
    {
        pauseMenu.currentLevel = currentLevelnumber;
        pauseMenu.CompleteLevel();
    }
}
