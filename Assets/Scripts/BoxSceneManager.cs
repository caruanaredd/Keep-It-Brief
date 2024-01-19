using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class BoxSceneManager : MonoBehaviour
{
    public static int correctSlots;
    public GameObject brief;

    void Awake()
    {
        correctSlots = 0;
    }

    void Update()
    {
        if (correctSlots == 12)
        {
            brief.SetActive(true);
        }
    }
}
