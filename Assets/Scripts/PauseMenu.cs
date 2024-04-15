using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;

    public static bool isPaused;

    int briefsCollected;
    public TextMeshProUGUI briefDisplay;
    // Start is called before the first frame update

    void Awake()
    {
    }
    void Start()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0;

    }

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void UpdateScore()
    {
        briefsCollected = HallSceneManager.completeLevels;
        briefDisplay.text = briefsCollected + "/2";
    }
}
