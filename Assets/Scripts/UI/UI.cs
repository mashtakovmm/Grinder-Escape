using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] GameObject dead;
    PlayerController playerController;
    public void OnRetry()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        int scoreText = (int)scoreManager.Score;
        score.text = $"SCORE: {scoreText:D8}";
        if (playerController.IsDead)
        {
            dead.SetActive(true);
        }
    }
}
