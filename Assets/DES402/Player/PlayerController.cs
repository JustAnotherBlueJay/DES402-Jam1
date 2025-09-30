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
    void FixedUpdate()
    {
        //stops the player sliding
        StickToSlope();

        //direction to move the player along
        Vector2 slopeDirection = GetSlopeVector();


        // if the player presses the correct button move them
        if (PlayerGaveExpectedInput())
        {
            //TODO: Replace this 50 with a variable relating to the strength stat
            Vector2 finalForce = slopeDirection * 50f;

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

    //if the player is touching the slope ignore gravity and stop any sliding
    private void StickToSlope()
    {
        if (myRigidBody.IsTouchingLayers(LayerMask.GetMask("Slope")))
        {
            myRigidBody.gravityScale = 0f;
            myRigidBody.linearVelocity = Vector3.zero;
        }
        else
        {
            myRigidBody.gravityScale = 1f;
        }
    }

    private Vector2 GetSlopeVector()
    {
        //ray cast to find the slope
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 20f, LayerMask.GetMask("Slope"));

        if (hit.collider != null)
        {
            //get the normal of the slope
            Vector2 normal = hit.normal;

            Debug.DrawRay(transform.position, new Vector2(normal.y, -normal.x), Color.yellow);

            //return the slopes normal rotates 90 degrees (the direction of the slope)
            return new Vector2(normal.y, -normal.x);
        }

        else
        {
            return Vector2.zero;
        }
    }
    public void ApplyInstanceData(int instanceNumber)
    {
        myInstanceNumber = instanceNumber;

        GetComponent<SpriteRenderer>().color = intanceManager.GetPlayerColor(myInstanceNumber);
    }


}
