using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StepsCount : MonoBehaviour
{
    [SerializeField] public int stepsRemaining = 60;
    public int totalSteps;

    public TextMeshProUGUI stepCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeAStep()
    {
        stepsRemaining--;
        stepCount.text = stepsRemaining + " Steps";
    }

    public void ResetSteps()
    {
        stepsRemaining = totalSteps;
    }
}
