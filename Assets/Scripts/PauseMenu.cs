using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using Unity.Mathematics;

public class PauseMenu : MonoBehaviour
{
    public Image [] level;

    public int currentLevel;
    public Sprite completed;
    public GameObject pausePanel;

    public static bool isPaused;

    int briefsCollected;

    public static int completeLevels = 0;
    public TextMeshProUGUI briefDisplay;
    // Start is called before the first frame update

    void Awake()
    {
        
    }
    void Start()
    {
        completeLevels = 0; //TEMPORARY FOR THE SAKE OF RESETTING
        isPaused = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;

    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void UpdateScore()
    {
        briefsCollected = completeLevels;
        briefDisplay.text = briefsCollected + " / 4";
    }

    public void UnlockLevel()
    {
        Debug.Log("Unlock" + currentLevel);
        level[currentLevel].sprite = null;
    }

    public void CompleteLevel()
    {
        Debug.Log("Complete" + currentLevel);
        level[currentLevel].sprite = completed;
    }
}
