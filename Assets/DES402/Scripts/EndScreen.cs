using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] InstanceManager instanceManager;
    [SerializeField] Timer returnToTitleTimer;
    [SerializeField] Timer showStatsTimer;
    [SerializeField] GameObject statsScreen;
    [SerializeField] TextMeshProUGUI statsText;
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
        //start the timer to return to the start screen
        returnToTitleTimer.OnTimeout = ReturnToTitleScreen;
        returnToTitleTimer.StartTimer();

        //start the timer to transition to the stats screen
        showStatsTimer.OnTimeout = ShowStatsScreen;
        showStatsTimer.StartTimer();

        //FadeInScren
        shouldFadeToBlack = true;
        shouldFadeToImage = false;
        timeElapsed = 0f;
    }

    private void OnDisable()
    {
        fadeToBlack.color = new Color(0f,0f, 0f, 0f);
        image.color = new Color(1f, 1f, 1f, 0f);
        statsScreen.SetActive(false);
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

    private void ShowStatsScreen()
    {
        DES_GameManager.AddToTotalDistanceClimbed(0.91f);
        float totalDistance = DES_GameManager.GetTotalDistanceClimbed();

        statsText.text = "\r\nDedicated to all those who didnt make the climb.\r\n\r\nThe city of Dundee has collectively climbed X km's\r\n\r\n".Replace("X",totalDistance.ToString());
        statsScreen.SetActive(true);

    }
}
