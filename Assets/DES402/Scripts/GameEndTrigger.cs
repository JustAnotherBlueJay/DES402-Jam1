using UnityEngine;

public class GameEndTrigger : MonoBehaviour
{
    [SerializeField] InstanceManager instanceManager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            collision.gameObject.GetComponent<PlayerController>().enabled = false;
            instanceManager.TransitionToEndScreen();
        }
    }
}
