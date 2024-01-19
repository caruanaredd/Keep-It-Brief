using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DomSceneManager : MonoBehaviour
{
    private GameObject audioManager;
    private GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager");
        pauseMenu = GameObject.Find("PhoneCanvas");
        Destroy(audioManager);
        Destroy(pauseMenu);
        StartCoroutine(LoadSceneDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadSceneDelay()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(1);
    }
}
