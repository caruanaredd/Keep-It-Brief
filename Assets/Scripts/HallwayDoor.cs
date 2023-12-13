using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HallwayDoor : MonoBehaviour
{
    public int sceneToLoad;
    void OnTriggerEnter2D(Collider2D other){
        //other.name should equal the root of your Player object
        if (other.name == "Player") {
            //The scene number to load (in File->Build Settings)
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}