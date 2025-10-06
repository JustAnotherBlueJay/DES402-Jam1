using UnityEngine;

public class ChatTrigger : MonoBehaviour
{

    [SerializeField] private Sprite NPCDialogueSprite;
    [SerializeField] private DialogueManager dialogueManager;

    private bool hasTalked = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTalked && other.CompareTag("Player"));
        {
            var player = other.GetComponent<PlayerController>();
            var rigidBody = other.GetComponent<Rigidbody2D>();

            player.isActive = false;
            rigidBody.linearVelocity = Vector2.zero;

            print("yeah we stopped him");

            dialogueManager.startDialogue(NPCDialogueSprite, () =>
            {
                player.isActive = true;
            });

            //dialogue

            //StartCoroutine(ResumeMovement(other.GetComponent<PlayerController>()));

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
