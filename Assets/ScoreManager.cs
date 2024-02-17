using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public Canvas GameCanvas;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI OTHERscoreText;
    const string scoreConst = "SCORE: ";
    int score = 0;

    public int Score { get => score; set => score = value; }
    public TextMeshProUGUI myName;

    private void Start()
    {
        UpdateScore(0);
    }

    public void UpdateScore(int val)
    {
        score += val;

        scoreText.SetText(scoreConst + score);
    }

    public void SaveScore(int v)
    {
        if (v > PlayerPrefs.GetInt("score", 0))
        {
            PlayerPrefs.SetInt("score", v);

        }

        OTHERscoreText.text = v.ToString();
    }

    public void SubmitScore()
    {
        if (myName.text == string.Empty)
            return;
        Leaderboard.UploadScore(myName.text, Score);
    }
}
