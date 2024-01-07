using UnityEngine;
using UnityEngine.Audio;

public class CreepyBGMHandler : MonoBehaviour
{
    // Link the audio mixer asset in the inspector!
    public AudioMixer mixer;

    // Set the lowest and highest Y values in the inspector
    public float lowestY = -20f;
    public float highestY = 20f;

    private void Awake()
    {
        // Find the player and set them as the parent of this object
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.zero; // centers this object on the pivot
        }
    }
    
    private void Update()
    {
        float totalDistance = highestY - lowestY; // get the total distance between points
        float calculateY = transform.position.y - lowestY;

        // this is a small fix for how the mixer volume works
        // we need to limit the number to a value between 0 and 1
        // otherwise, both songs will play at the same volume when beyond
        // the position limits
        float normalized = Mathf.Clamp(calculateY / totalDistance, 0.0001f, 0.9999f);
        
        // these values were exposed from the AudioMixer asset
        // we're also working with decibels, not a volume slider,
        // so we need a logarithm function to make it work correctly!
        mixer.SetFloat("BGMVolume", Mathf.Log10(1 - normalized) * 20f); // Reverse the volume value for the BGM
        mixer.SetFloat("BGMCreepyVolume", Mathf.Log10(normalized) * 20f);
    }
}
