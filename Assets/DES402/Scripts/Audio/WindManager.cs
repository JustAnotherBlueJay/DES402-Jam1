using System;
using UnityEngine;
using UnityEngine.Audio;

public class WindManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float volume = 0.0f;
    private float resonance = 0.0f;
    private float cutoff = 0.0f;

    private bool fadein = false;

    public AudioMixer audioMixer;
    void Start()
    {
        fadein = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        audioMixer.SetFloat("Volume", volume);
        audioMixer.SetFloat("Resonance", resonance);
        audioMixer.SetFloat("Cutoff", cutoff);

        if (fadein == false & volume < 0.08f)
        {
            volume += 0.001f;
        }
       else
        {
            fadein = true;
            volume = 0.1f + 0.1f * Mathf.Abs((Mathf.Sin(Time.time * 0.03f) / 2.4f));
        }
        

        resonance = 0.1f + 0.6f * Mathf.Abs((Mathf.Sin(Time.time * 0.3f))) * Mathf.Abs((Mathf.Sin(Time.time * 0.2f)));

        cutoff = 0.1f + 0.1f * Mathf.Abs((Mathf.Sin(Time.time * 0.6f) /3f));

    }

  
}
