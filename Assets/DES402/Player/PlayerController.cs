using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InstanceManager intanceManager;
    [SerializeField] private int myInstanceNumber;
    [SerializeField] private float speed;
    private float gravity = -9.8f;

    private Rigidbody2D myRigidBody;
    private WhaleButton expectedInput = WhaleButton.L_Button;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        



        if (PlayerGaveExpectedInput())
        {


            //transform.Translate(Vector2.right * speed * Time.deltaTime);

            Vector2 finalForce = Vector2.right * 1000f * Time.deltaTime;

            myRigidBody.linearVelocity = Vector2.zero;
            myRigidBody.AddForce(finalForce, ForceMode2D.Impulse);

            expectedInput = UpdateExpectedInput();
        }


        
    }

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
    private WhaleButton UpdateExpectedInput()
    {
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
