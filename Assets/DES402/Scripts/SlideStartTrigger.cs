using UnityEngine;

public class SlideStartTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //if the player is at full weight then they slide
            PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();

            if (playerScript.GetPlayerWeight() == PlayerController.PlayerWeight.FullWeight)
            {
                playerScript.sliding = true;

            }
        }
    }
}
