using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
