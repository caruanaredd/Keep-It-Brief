using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void quit() 
    {
        Application.Quit();
    }
}
