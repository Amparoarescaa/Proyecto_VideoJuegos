using UnityEditor.Media;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed =7f;
    public float jumpForce = 8f;
    public float rotationSpeed = 200f;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Movimiento automático
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }

        // Rotación en el aire
        if (!isGrounded)
        {
            transform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
        }
    }

void OnCollisionEnter2D(Collision2D collision)
{
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

    RestartGame();
}

   void RestartGame()
{
    audioSource.Play();

    Invoke(nameof(RestartScene), audioSource.clip.length);
}

void RestartScene()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
}