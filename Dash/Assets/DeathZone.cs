using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameObject losePanel;

    void Start()
    {
        losePanel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            losePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}