using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 7f;
    public float jumpForce = 8f;
    public float rotationSpeed = 200f;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!canMove)
        {
            return;
        }

        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }

        if (!isGrounded)
        {
            transform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canMove)
        {
            return;
        }

        bool touchedGround = false;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.4f)
            {
                touchedGround = true;
            }
        }

        if (touchedGround)
        {
            isGrounded = true;
            transform.rotation = Quaternion.identity;
            return;
        }

        if (GameManager.Instance != null)
        {
            Debug.Log("Choqué con obstáculo");
            GameManager.Instance.LoseGame();
        }
        else
        {
            Debug.LogError("No hay GameManager en la escena");
        }
    }

    public void StopMovement()
    {
        canMove = false;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}