using System;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] InstanceManager instanceManager;
    [SerializeField] Timer returnToTitleTimer;
    [SerializeField] Image image;
    [SerializeField] Sprite endScreenImage;
    [SerializeField] Image fadeToBlack;
    private int myInstanceNumber;
    private bool shouldFadeToBlack;
    private bool shouldFadeToImage;
    private float timeElapsed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        //start the timer
        returnToTitleTimer.OnTimeout = ReturnToTitleScreen;
        returnToTitleTimer.StartTimer();

        //FadeInScren
        //image.color = Color.white;

        shouldFadeToBlack = true;
        shouldFadeToImage = false;
        timeElapsed = 0f;
    }

    private void OnDisable()
    {
        fadeToBlack.color = new Color(0f,0f, 0f, 0f);
        image.color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            //interpolate the alpha of the image to fade into black
            fadeToBlack.color = new Color(fadeToBlack.color.r, fadeToBlack.color.g, fadeToBlack.color.b, Mathf.Lerp(0,1, timeElapsed / 2f));
            timeElapsed += Time.deltaTime;

            if (timeElapsed > 2f)
            {
                shouldFadeToBlack = false;
                shouldFadeToImage = true;
                timeElapsed = 0f;
            }
        }
        else if (shouldFadeToImage)
        {
            //slowly fade in the win screen
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0, 1, timeElapsed / 2f));
            timeElapsed += Time.deltaTime;

            if (timeElapsed > 2f)
            {
                shouldFadeToImage = false;
                timeElapsed = 0f;
            }
        }
    }

    public void ApplyInstanceData(int instanceNumber)
    {
        myInstanceNumber = instanceNumber;
    }

    //when the title screen timer ends return to the title screen
    private void ReturnToTitleScreen()
    {

        instanceManager.TransitionToTitle();

    }
}
