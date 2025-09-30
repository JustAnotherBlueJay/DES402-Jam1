using System;
using UnityEngine;
using UnityEngine.Audio;

public class WindManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float volume = 0.0f;
    private float resonance = 0.0f;
    private float cutoff = 0.0f;

    public AudioMixer audioMixer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        audioMixer.SetFloat("Volume", volume);
        audioMixer.SetFloat("Resonance", resonance);
        audioMixer.SetFloat("Cutoff", cutoff);

        volume = 0.02f + 0.1f * Mathf.Abs((Mathf.Sin(Time.time*0.03f) /3f));
        resonance = 0.1f + 0.6f * Mathf.Abs((Mathf.Sin(Time.time * 0.3f)));
        cutoff = 0.1f + 0.1f * Mathf.Abs((Mathf.Sin(Time.time * 0.6f) /3f));

    }
}
