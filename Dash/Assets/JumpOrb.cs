using UnityEngine;

public class JumpOrb : MonoBehaviour
{
    public float boostForce = 8f;
    private Rigidbody2D playerRb;
    private bool playerInside = false;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, boostForce);
            playerInside = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerRb = other.GetComponent<Rigidbody2D>();
            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            playerRb = null;
        }
    }
}