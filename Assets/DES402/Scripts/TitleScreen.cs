using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] InstanceManager instanceManager;
    private int myInstanceNumber;

    [SerializeField] private Image background;
    [SerializeField] private Slider loadingBar;

    private bool fillLoadingBar;
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

            if (loadingBar.value >= 1)
            {
                instanceManager.TransitionToGame();
                gameObject.SetActive(false);
            }
        }
        else
        {
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
        background.color = Color.black;
        loadingBar.gameObject.SetActive(true);
        fillLoadingBar = true;
    }


}
