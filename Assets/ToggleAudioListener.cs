using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAudioListener : MonoBehaviour
{
    public Sprite spriteOn;
    public Sprite spriteOff;

    private bool isOn = true;
    private Image parentImage;

    private void Start()
    {
        parentImage = GetComponentInParent<Image>();
        if (parentImage == null)
        {
            Debug.LogWarning("Parent object does not have an Image component!");
        }
        else
        {
            UpdateSprite();
        }
    }

    public void Toggle()
    {
        isOn = !isOn;
        UpdateAudioListener();
        UpdateSprite();
    }

    private void UpdateAudioListener()
    {
        AudioListener.volume = isOn ? 1 : 0;
    }

    private void UpdateSprite()
    {
        if (parentImage != null)
        {
            parentImage.sprite = isOn ? spriteOn : spriteOff;
        }
        else
        {
            Debug.LogWarning("Parent object does not have an Image component!");
        }
    }
}