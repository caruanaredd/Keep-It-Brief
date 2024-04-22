using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameObject pauseCanvas;
    PauseMenu pauseMenu;
    Countdown countdown;
    public GameObject HardCanvas;

    public int currentLevelnumber;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = pauseCanvas.GetComponent<PauseMenu>();
        countdown = HardCanvas.GetComponent<Countdown>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("Player"))
        {

            completeCurrentLevel();
            Destroy(gameObject);
            PauseMenu.completeLevels++;
            pauseMenu.UpdateScore();
        }
    }

    public void completeCurrentLevel()
    {
        pauseMenu.currentLevel = currentLevelnumber;
        if (currentLevelnumber == 3) //CHANGE TO CORRECT LEVEL
        {
            countdown.finishedLevel = true;
        }
        pauseMenu.CompleteLevel();
        pauseMenu.UnlockLevel();
    }
}
