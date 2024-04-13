using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PrepareVideo : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<VideoPlayer>().Prepare();
    }
}
