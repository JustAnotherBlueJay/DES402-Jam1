using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] InstanceManager instanceManager;
    private int myInstanceNumber;

    [SerializeField] private Image background;
    [SerializeField] private Sprite backgroundImage;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private TextMeshProUGUI loadingText;
    //if the bar should be getting filled
    private bool fillLoadingBar;
    //how long the loading bar should take
    private float timeElapsed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (fillLoadingBar)
        {
            loadingBar.value = Mathf.Lerp(0, 1, timeElapsed / 2f);
            timeElapsed += Time.deltaTime;

            //when the loading bar is done
            if (loadingBar.value >= 1)
            {
                //restart the screen for when we come back if the player is inactive
                RestartTitleScreen();
                //start the game
                instanceManager.TransitionToGame();
                //turn this object off
                gameObject.SetActive(false);
            }
        }
        //if we arent filling the loading bar then we are on the title screen
        else
        {
            //listen for any input and sturt the loading bar
            foreach (WhaleButton whaleButon in Enum.GetValues(typeof(WhaleButton)))
            {
                if (WhalesongInput.GetButtonDown(myInstanceNumber, whaleButon))
                {
                    StartLoadingScreen();
                }
            }

        }
    }

    public void ApplyInstanceData(int instanceNumber)
    {
        myInstanceNumber = instanceNumber;
    }

    public void StartLoadingScreen()
    {
        background.sprite = null;
        background.color = Color.gray2;
        loadingBar.gameObject.SetActive(true);
        loadingText.gameObject.SetActive(true);
        fillLoadingBar = true;
    }

    public void RestartTitleScreen()
    {
        background.sprite = backgroundImage;
        background.color = Color.white;

        loadingBar.value = 0;

        loadingBar.gameObject.SetActive(false);
        loadingText.gameObject.SetActive(false);

        fillLoadingBar = false;

    }

}
