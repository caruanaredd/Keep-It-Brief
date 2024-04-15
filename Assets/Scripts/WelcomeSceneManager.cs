using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeSceneManager : MonoBehaviour
{
    public Sprite[] pun;
    public Image punImage;
    public GameObject punObject;
    int randomNum;
    private GameObject pauseMenu;
    private Coroutine popupTracker;
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

    public void LoadNewBubble()
    {
        if (popupTracker != null)
        {
            StopCoroutine(popupTracker);
        }
        
        popupTracker = StartCoroutine(PunPopup());
    }

    private IEnumerator PunPopup()
    {
        randomNum = Random.Range(0, pun.Length);
        punImage.sprite = pun[randomNum];
        punObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        popupTracker = null;
        punObject.SetActive(false);
    }

}
