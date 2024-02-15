using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIMover : MonoBehaviour
{
    public RectTransform rT;
    public TextMeshProUGUI scoreText;

    public void Init(string score)
    {
        scoreText.SetText(score);
        int value = Convert.ToInt32(score);
        if (value < 0)
        {
            scoreText.color = Color.red;
        }
        else
        {
            scoreText.color = Color.blue;
        }
        this.transform.DOScale(new Vector3(.5f, .5f, .5f), 1f).SetEase(Ease.InBounce);
    }

    public void Init(string score, string message)
    {
        scoreText.SetText(score + " " + message);
        int value = Convert.ToInt32(score);
        if (value < 0)
        {
            scoreText.color = Color.red;
        }
        else
        {
            scoreText.color = Color.blue;
        }
        this.transform.DOScale(new Vector3(.5f, .5f, .5f), 1f).SetEase(Ease.InBounce);
    }

    public void StartMove()
    {
        rT.DOAnchorPos(new Vector2(931, 817), 1f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject, 1f);
        });
    }

    public void StartMove(float speed)
    {
        rT.DOAnchorPos(new Vector2(931, 817), speed).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject, 1f);
        });
    }
}
