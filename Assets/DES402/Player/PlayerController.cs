using System;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InstanceManager intanceManager;

    //the unique ID for this instance of player
    [SerializeField] private int myInstanceNumber;
    //TODO: will affect the duration of inputPauseTime
    public enum PlayerWeight
    {
        FullWeight,
        MinusOne,
        MinusTwo
    }
    private PlayerWeight weight = PlayerWeight.FullWeight;
    //how long each player step is
    [SerializeField] private float stepLength;
    //the players rigid body, used for movement
    private Rigidbody2D myRigidBody;
    //reference to the timer gameObject
    [SerializeField] private Timer inputTimer;
    //the length of timer before a player can step again
    private float inputPauseTime;
    //how long the input pause time should be at each weight level
    [SerializeField] private float[] inputPauseTimes = new float[3];
    //when the timer ends the player is moveable
    private bool moveable = true;
    //a flag to indicate the player has made a movement input and should be moved on the next FixedUpdate frame
    private bool movePlayer = false;

    private Animator myAnimator;

    //script responsible for showing the UI

    //the button the player needs to press to move
    private WhaleButton expectedInput = WhaleButton.L_Button;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        inputPauseTime = inputPauseTimes[0];

    }

    private void Start()
    {
        //connecting the OnTimeout action to this scripts method
        inputTimer.OnTimeout = OnInputTimerTimeout;
    }

    // Update is called once per frame
    private void Update()
    {
        //if the input delay timer hasnnt ended then we return
        if (!moveable)
        {
            return;
        }

        // if the player presses the correct button mark them to be moved
        if (PlayerGaveExpectedInput())
        {
            //marks the player to be moved
            //this is because physics forces shouldnt be applied in update
            movePlayer = true;
            PlayWalkAnimation();

            //start the input delay
            moveable = false;
            inputTimer.StartTimer(inputPauseTime);
        }

        if (Input.GetKey(KeyCode.Keypad1))
        {
            SetPlayerWeight(PlayerWeight.FullWeight);
        }

        if (Input.GetKey(KeyCode.Keypad2))
        {
            SetPlayerWeight(PlayerWeight.MinusOne);
        }

        if (Input.GetKey(KeyCode.Keypad3))
        {
            SetPlayerWeight(PlayerWeight.MinusTwo);
        }

    }
    void FixedUpdate()
    {
        //stops the player sliding
        StickToSlope();

        //direction to move the player along
        Vector2 slopeDirection = GetSlopeVector();


        // if the player presses the correct button move them
        if (movePlayer && myRigidBody.IsTouchingLayers(LayerMask.GetMask("Slope")))
        {
            //set move player flag to false to stop them being moved on the next fixed update
            movePlayer = false;

            //calculate the final force to move the player
            Vector2 finalForce = slopeDirection * stepLength;

            //reset velocity to prevent spamming
            //myRigidBody.linearVelocity = Vector2.zero;

            //move the player
            myRigidBody.AddForce(finalForce, ForceMode2D.Impulse);

            //myAnimator.Play("FullWeightWalk");

        }


        
    }

    //checks if the player pressed the correct button to move
    private bool PlayerGaveExpectedInput()
    {
        //return if the left or right buttons were pressed this frame
        return (WhalesongInput.GetButtonDown(myInstanceNumber, WhaleButton.L_Button) || WhalesongInput.GetButtonDown(myInstanceNumber, WhaleButton.R_Button));
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

    //returns the angle of the slope as a vector
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

    public void OnInputTimerTimeout()
    {
        //allows the player to be moved
        moveable = true;
    }

    private void PlayWalkAnimation()
    {
        switch (weight)
        {
            case PlayerWeight.FullWeight:
                myAnimator.Play("FullWeightWalk");
                break;
            case PlayerWeight.MinusOne:
                myAnimator.Play("MinusOneWalk");
                break;
            case PlayerWeight.MinusTwo:
                myAnimator.Play("MinusTwoWalk");
                break;
        }
    }

    private void SetPlayerWeight(PlayerWeight newWeight)
    {
        //set the players weight variable to the new weight
        weight = newWeight;
        //update the player animator to the new idle animation
        myAnimator.SetInteger("PlayerWeight", (int)newWeight);
        //set the new wait time for the input delay
        inputPauseTime = inputPauseTimes[(int)newWeight];
    }

    //NPC doesnt need to know the players weight so it can just reduce it like this
    public void ReduceWeight()
    {
        //increase the enum by one to drop weight
        int newWeight = (int)weight + 1;

        SetPlayerWeight((PlayerWeight)newWeight);
    }
}
