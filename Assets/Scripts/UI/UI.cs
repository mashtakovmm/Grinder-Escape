using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] ScoreManager scoreManager;
    public void OnRetry()
    {
        SceneManager.LoadScene(0);
    }

    private void Update() {
        score.text = $"SCORE: {(int)scoreManager.Score}";
    }
}
