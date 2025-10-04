using UnityEngine;

public class ChatTrigger : MonoBehaviour
{

    private bool hasTalked = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTalked && other.CompareTag("Player"));
        {
            other.GetComponent<PlayerController>().enabled = false;
            other.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            print("yeah we stopped him");

            //dialogue

            StartCoroutine(ResumeMovement(other.GetComponent<PlayerController>()));

            hasTalked = true;   
        }
    }

    private System.Collections.IEnumerator ResumeMovement(PlayerController player)
    {
        yield return new WaitForSeconds(3f);
        player.enabled = true;
        print("we are moving again");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
