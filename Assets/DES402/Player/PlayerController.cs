using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InstanceManager intanceManager;
    [SerializeField] private int myInstanceNumber;

    private Rigidbody2D myRigidBody;

    //the button the player needs to press to move
    private WhaleButton expectedInput = WhaleButton.L_Button;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the player presses the correct button move them
        if (PlayerGaveExpectedInput())
        {

            Vector2 finalForce = Vector2.right * 1000f * Time.deltaTime;

            //reset velocity to prevent spamming
            myRigidBody.linearVelocity = Vector2.zero;

            //move the player
            myRigidBody.AddForce(finalForce, ForceMode2D.Impulse);

            //update the expected input
            expectedInput = UpdateExpectedInput();
        }


        
    }

    //checks if the player pressed the correct button to move
    private bool PlayerGaveExpectedInput()
    {
        switch (expectedInput) 
        {
            case WhaleButton.L_Button:
                return WhalesongInput.GetButton(myInstanceNumber, WhaleButton.L_Button);

            case WhaleButton.R_Button:
                return WhalesongInput.GetButton(myInstanceNumber, WhaleButton.R_Button);
        }

        return false;
    }

    //place holder, just switches the cuurrent expected button to the other button
    private WhaleButton UpdateExpectedInput()
    {
        //TODO: replace this with a timer that alternates between the expected buttons
        switch(expectedInput)
        {
            case WhaleButton.L_Button:
                return WhaleButton.R_Button;
            case WhaleButton.R_Button:
                return WhaleButton.L_Button;

        }

        return WhaleButton.L_Button;
    }



    public void ApplyInstanceData(int instanceNumber)
    {
        myInstanceNumber = instanceNumber;

        GetComponent<SpriteRenderer>().color = intanceManager.GetPlayerColor(myInstanceNumber);
    }


}
