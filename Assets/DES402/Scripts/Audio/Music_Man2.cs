using UnityEngine;
using UnityEngine.Rendering;

public class Music_Man2 : MonoBehaviour
{
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WhalesongAudio.PlayGlobalOneShot(clip1, 1.0f, 1.0f);
        WhalesongAudio.PlayGlobalOneShot(clip2, 1.0f, 1.0f);
        WhalesongAudio.PlayGlobalOneShot(clip3, 1.0f, 1.0f);
        WhalesongAudio.PlayGlobalOneShot(clip4, 1.0f, 1.0f);
        WhalesongAudio.PlayGlobalOneShot(clip5, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
