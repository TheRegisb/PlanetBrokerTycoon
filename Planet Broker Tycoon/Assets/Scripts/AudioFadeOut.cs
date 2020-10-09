using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeOut : MonoBehaviour
{
    public float fadeSpeed = 16f;
    private bool fading = false;
    private float lastUpdateTime = 0f;
    private AudioSource aSrc;

    // Start is called before the first frame update
    void Start()
    {
        aSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       if (fading && aSrc.volume > 0.0f)
        {
            aSrc.volume -= fadeSpeed * (Time.realtimeSinceStartup - lastUpdateTime);
        }
        lastUpdateTime = Time.realtimeSinceStartup;
    }

    public void FadeOut()
    {
        fading = true;
    }
}
