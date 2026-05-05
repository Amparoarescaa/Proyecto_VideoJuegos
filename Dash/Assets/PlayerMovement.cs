using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4f;
    public float jumpForce = 6f;
    public float rotationSpeed = 200f;
    public float fallLimit = -10f;

    private Rigidbody2D rb;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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

        if (transform.position.y < fallLimit)
        {
            RestartGame();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
{
    foreach (ContactPoint2D contact in collision.contacts)
    {
        // toca el piso desde arriba
        if (contact.normal.y > 0.5f)
        {
            isGrounded = true;
            transform.rotation = Quaternion.identity;
        }

        // choque lateral contra plataforma/pared
        if (Mathf.Abs(contact.normal.x) > 0.5f)
        {
            RestartGame();
        }
    }
}

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}