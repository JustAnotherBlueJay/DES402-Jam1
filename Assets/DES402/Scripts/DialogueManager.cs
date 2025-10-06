using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{

    //[SerializeField] DialogueManager instance;

   // [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject dialogueNPC;
    [SerializeField] private GameObject dialoguePlayer;
    [SerializeField] private InstanceManager myInstanceManager;
    [SerializeField] private PlayerController player;

    private Action onDialogueComplete;

    private int myInstanceNumber;

    private void Awake()
    {
       // instance = this;
        dialoguePlayer.SetActive(false);
        dialogueNPC.SetActive(false);
    }

    public void startDialogue(Sprite NPCImage, Action OnComplete)
        {
        onDialogueComplete = OnComplete;
        dialoguePlayer.SetActive(true);
        dialogueNPC.SetActive(true);
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
        if (dialoguePlayer.activeSelf)
        {
            if (WhalesongInput.GetButton(myInstanceNumber, WhaleButton.Left))
                {
                player.ReduceWeight();
                EndDialogue();
            }
            else if (WhalesongInput.GetButton(myInstanceNumber, WhaleButton.Right))
            {
                EndDialogue();
            }
        }
    }

    public void ApplyInstanceData(int instanceNumber)
    {
        myInstanceNumber = instanceNumber;
        print("my number (as a canvas) is: " + instanceNumber);
    }


    private void EndDialogue()
    {
        dialoguePlayer.SetActive(false);
        dialogueNPC.SetActive(false);
        onDialogueComplete?.Invoke();
    }
}
