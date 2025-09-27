using UnityEngine;
using WhaleInput;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] private InstanceManager intanceManager;
    [SerializeField] private int myInstanceNumber;
    [SerializeField] private float speed;

    private DES_GameManager gameManager;


    // Update is called once per frame
    void Update()
    {
        //basic inputstuff
        if (WhalesongInput.GetButton(myInstanceNumber, WhaleButton.Left)) 
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (WhalesongInput.GetButton(myInstanceNumber, WhaleButton.Right))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        if (WhalesongInput.GetButton(myInstanceNumber, WhaleButton.Up))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        if (WhalesongInput.GetButton(myInstanceNumber, WhaleButton.Down))
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }

    public void ApplyInstanceData(int instanceNumber)
    {
        myInstanceNumber = instanceNumber;

        GetComponent<SpriteRenderer>().color = intanceManager.GetPlayerColor(myInstanceNumber);
    }
}
