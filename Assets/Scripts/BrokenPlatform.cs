using UnityEngine;

public class BrokenPlatform : MonoBehaviour
{
    private float _countdown = 0.25f;

    private bool _startCountdown;

    private void Update()
    {
        if (_countdown < 0)
            gameObject.SetActive(false);
        if (_startCountdown)
            _countdown -= Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _startCountdown = true;
        }
    }
}
