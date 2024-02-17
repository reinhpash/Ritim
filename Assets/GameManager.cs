using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public LevelManager LevelManager;
    public ScoreManager ScoreManager;
    public AudioSource audioSource;
    public float noteTime;

    public List<NoteCatcher> noteCatchers = new List<NoteCatcher>();
    public TextMeshProUGUI songRemainingText;
    bool isLevelEnd = false;
    public NoteSpawner noteSpawner;
    public GameObject LevelEndObj;
    public Conductor currentConductor;
    public int currentLevel;

    void Awake()
    {
        Instance = this;
        currentLevel = PlayerPrefs.GetInt("level",0);
        noteSpawner.fileLocation = LevelManager.levels[currentLevel].midiName;
        audioSource.clip = LevelManager.levels[currentLevel].song;
        currentConductor.songBpm = LevelManager.levels[currentLevel].bpm;
        noteTime = LevelManager.levels[currentLevel].noteTime;

        for (int i = 0; i < noteSpawner.Lanes.Count; i++)
        {
            noteSpawner.Lanes[i].noteRestriction = LevelManager.levels[currentLevel].restrictions[i];
        }
    }

    public double GetAudioSourceTime()// müziðin þu anki zamanýný verir
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }

    void Update()
    {
        if (!isLevelEnd && noteSpawner.isGameStart)
        {
            if (audioSource.isPlaying)
            {
                float remainingTime = audioSource.clip.length - audioSource.time;
                string formattedTime = FormatTime(remainingTime);
                songRemainingText.SetText("Remaining: " + formattedTime);
            }
            else
            {
                isLevelEnd = true;
                StopGame();
            }
        }
        
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    public void StopGame()
    {
        songRemainingText.SetText("Done");
        ScoreManager.SaveScore(ScoreManager.Score);
        LevelEndObj.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}

