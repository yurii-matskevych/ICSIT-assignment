using UnityEngine;
using UnityEngine.UI;

public class CurrentScore : MonoBehaviour
{
    public static CurrentScore instance;
    [SerializeField] private Text scoreText = null;

    private void Start()
    {
        instance = this;
    }

    public void UpdateCurrentScore(int value)
    {
        scoreText.text = $"SCORE: {value}";
    }
}
