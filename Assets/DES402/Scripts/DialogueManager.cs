using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{

    //[SerializeField] DialogueManager instance;

   // [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Image dialogueImage;
    [SerializeField] private InstanceManager myInstanceManager;

    private Action onDialogueComplete;

    private int myInstanceNumber;

    private void Awake()
    {
       // instance = this;
        dialoguePanel.SetActive(false);
    }

    public void startDialogue(Sprite NPCImage, Action OnComplete)
        {
        onDialogueComplete = OnComplete;
        dialoguePanel.SetActive(true);
        dialogueImage.sprite = NPCImage;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // if (cameraTransform == null)
        //    cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 cameraPos = cameraTransform.position;
       // transform.position = new Vector3(cameraPos.x - (myInstanceNumber * 200), cameraPos.y, 0f);  
        if (dialoguePanel.activeSelf && ( WhalesongInput.GetButton(myInstanceNumber, WhaleButton.Left ) || WhalesongInput.GetButton(myInstanceNumber, WhaleButton.Right)))
        {
            EndDialogue();
        }
    }

    public void ApplyInstanceData(int instanceNumber)
    {
        myInstanceNumber = instanceNumber;
        print("my number (as a canvas) is: " + instanceNumber);
    }


    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        onDialogueComplete?.Invoke();
    }
}
