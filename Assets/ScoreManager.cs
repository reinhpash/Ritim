using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public Canvas GameCanvas;
    public TextMeshProUGUI scoreText;
    const string scoreConst = "SCORE: ";
    int score = 0;

    private void Start()
    {
        UpdateScore(0);
    }

    public void UpdateScore(int val)
    {
        score += val;

        scoreText.SetText(scoreConst+score);
    }
}
