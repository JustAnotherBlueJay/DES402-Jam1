using UnityEngine;

public class Music_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public AudioSource Track1;

    public AudioSource Track2;

    public AudioSource Track3;

    public AudioSource Track4;

    public AudioSource Track5;
    void Start()
    {
        Track1.Play();
        Track2.Play();
        Track3.Play();
        Track4.Play();
        Track5.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
