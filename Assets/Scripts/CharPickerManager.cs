using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharPickerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("StaffX", 0);
        PlayerPrefs.SetFloat("StaffY", 0);
        
        PlayerPrefs.SetFloat("SpawnX", 0);
        PlayerPrefs.SetFloat("SpawnY", -41f);

        PauseMenu.completeLevels = 0;
    }
}
