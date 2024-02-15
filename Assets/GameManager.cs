using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ScoreManager ScoreManager;
    public AudioSource audioSource;
    public float noteTime;

    public List<NoteCatcher> noteCatchers = new List<NoteCatcher>();


    void Awake()
    {
        Instance = this;
    }

    public double GetAudioSourceTime()// müziðin þu anki zamanýný verir
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }

    public void RemoveNoteFromLists(FallingNotes note)
    {
        foreach (var catcher in noteCatchers)
        {
            catcher.RemoveNote(note);
        }
    }

}

