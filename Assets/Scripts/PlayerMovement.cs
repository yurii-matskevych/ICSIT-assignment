using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float jumpForce = 0f;
    [SerializeField] private float moveForce = 0f;
    [SerializeField] private Rigidbody2D _rigidbody2D = null;

    [Header("Dependencies")]
    [SerializeField] private LayerMask groundLayer = 0;
    [SerializeField] private Chunk startChunk = null;
    [SerializeField] private Text gameOverText = null;
    [SerializeField] private Text MaxScoreText = null;
    [SerializeField] private Transform GFXTrasform = null;

    public static int score = 0;
    private float _xScale = 0;
    public Vector3 GetDeltaSpeed()
    {
        return _rigidbody2D.velocity;
    }
    private void Start()
    {
        _xScale = GFXTrasform.localScale.x;
        MaxScoreText.text = "MAX SCORE: " + PlayerPrefs.GetInt("MaxScore", 0).ToString();
        startChunk.NextChunk();
    }

    private void Update()
    {
        // Resets max score
        if (Input.GetKeyDown(KeyCode.Alpha2))
            PlayerPrefs.DeleteAll();

        if (Input.GetKeyDown(KeyCode.R))
            RestartGame();
        else CheckInput();

        Vector3 newScale = GFXTrasform.localScale;
        if (_rigidbody2D.velocity.x < 0)
            newScale.x = -_xScale;
        else newScale.x = _xScale;
        GFXTrasform.localScale = newScale;
    }

    private void RestartGame()
    {
        score = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Physics2D.OverlapCircle(transform.position, 1f, groundLayer))
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        var moveDelta = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveDelta) > 0.1f)
        {
            Vector2 newVelocity = _rigidbody2D.velocity;
            newVelocity.x = moveDelta * moveForce;
            _rigidbody2D.velocity = newVelocity;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Chunk"))
        {
            Chunk chunk = collision.gameObject.GetComponent<Chunk>();
            if (chunk.current)
            {
                chunk.NextChunk();
            }
        }
        else if (collision.CompareTag("DeathTrigger"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        int maxScore = Mathf.Max(PlayerPrefs.GetInt("MaxScore", 0), score);
        PlayerPrefs.SetInt("MaxScore", maxScore);
        score = 0;
        gameOverText.gameObject.SetActive(true);
        MaxScoreText.text = $"MAX SCORE: {maxScore}";
        Time.timeScale = 0;
    }
}
