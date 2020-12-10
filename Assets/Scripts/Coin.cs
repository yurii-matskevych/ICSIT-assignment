using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement.score++;
            CurrentScore.instance.UpdateCurrentScore(PlayerMovement.score);
            gameObject.SetActive(false);
        }
    }
}
