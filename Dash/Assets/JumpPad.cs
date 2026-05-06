using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float boostForce = 12f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, boostForce);
            }
        }
    }
}