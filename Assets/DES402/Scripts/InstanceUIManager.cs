using UnityEngine;
using Unity.UI;
using UnityEngine.UI;
using TMPro;
public class InstanceUIManager : MonoBehaviour
{
    [SerializeField] Image[] moveButtons;
    [SerializeField] Sprite[] moveButtonStates;
    [SerializeField] Timer buttonFlashTimer;
    private bool isFlashing;

    [SerializeField] Image leftDialogueOption;
    [SerializeField] Sprite[] leftDialogueOptionStates;

    [SerializeField] Image rightDialogueOption;
    [SerializeField] Sprite[] rightDialogueOptionStaets;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonFlashTimer.OnTimeout = FlashMoveButtons;
    }

    // Update is called once per frame
    void Update()
    {
        //if the buttons are not ment to be flashing but the timer is still running then stop the timer
        if (!isFlashing && !buttonFlashTimer.IsStopped())
        {
            buttonFlashTimer.StopTimer();
        }
    }

    public void SetMoveButtonsFlash(bool shouldFlash)
    {
        isFlashing = shouldFlash;

        if (isFlashing)
        {
            buttonFlashTimer.StartTimer();
            //turn buttons on
            SetMoveButtonsActivity(true);
            
        }
        else
        {
            //turn buttons off
            SetMoveButtonsActivity(false);

        }

    }

    //update the movement buttons to the opposite state
    public void FlashMoveButtons()
    {
        foreach (Image image in moveButtons)
        {
            if (image.sprite == moveButtonStates[0])
            {
                image.sprite = moveButtonStates[1];
            }
            else
            {
                image.sprite = moveButtonStates[0];
            }
        }
    }

    //set the movement buttons to a specific state
    private void SetMoveButtonsActivity(bool isActive)
    {
        if(isActive)
        {
            moveButtons[0].sprite = moveButtonStates[1];
            moveButtons[1].sprite = moveButtonStates[1];
        }
        else
        {
            moveButtons[0].sprite = moveButtonStates[0];
            moveButtons[1].sprite = moveButtonStates[0];
        }
    }

    public void SetDialogueOptionsActivity(bool isActive)
    {
        if (!isActive)
        {
            leftDialogueOption.sprite = leftDialogueOptionStates[0];
            rightDialogueOption.sprite = leftDialogueOptionStates[0];
        }
        else
        {
            leftDialogueOption.sprite = leftDialogueOptionStates[1];
            rightDialogueOption.sprite = leftDialogueOptionStates[1];
        }
    }
}
