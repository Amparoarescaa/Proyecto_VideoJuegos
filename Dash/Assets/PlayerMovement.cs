using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public GameObject losePanel;
    public TMP_Text scoreText;

    public float speed = 7f;
    public float jumpForce = 8f;
    public float rotationSpeed = 200f;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private bool isGrounded = false;
    private bool isDead = false;

    private float startX;
    private int score;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        startX = transform.position.x;

        losePanel.SetActive(false);

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }

        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

        score = Mathf.FloorToInt(transform.position.x - startX);

        scoreText.text = "Puntos: " + score;

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
        if (isDead)
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

        LoseGame();
    }

    void LoseGame()
    {
        isDead = true;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        audioSource.Play();

        losePanel.SetActive(true);

        gameObject.SetActive(false);

        Time.timeScale = 0f;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}