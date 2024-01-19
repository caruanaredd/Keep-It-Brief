using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeSceneManager : MonoBehaviour
{
    private GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("PhoneCanvas");
        Destroy(pauseMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
