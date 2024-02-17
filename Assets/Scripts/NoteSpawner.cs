using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using System.Collections;
using Melanchall.DryWetMidi.MusicTheory;
using TMPro;

public class NoteSpawner : MonoBehaviour
{
    public GameObject spawnObject;
    public float spawnHeight = 7f;
    public static MidiFile midiFile;
    public List<double> timeStamps = new List<double>();
    public string fileLocation;
    public List<Lane> Lanes = new List<Lane>();

    List<FallingNotes> notes = new List<FallingNotes>();
    int spawnIndex = 0;
    public float songDelayInSeconds;
    public TextMeshProUGUI counterText;
    public GameObject counterUI;
    public bool isGameStart;


    void Start()
    {
        ReadFromFile();
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        counterText.text = "3";
        yield return new WaitForSeconds(1f);
        counterText.text = "2";
        yield return new WaitForSeconds(1f);
        counterText.text = "1";
        yield return new WaitForSeconds(1f);
        counterUI.SetActive(false);
    }

    private void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (GameManager.Instance.GetAudioSourceTime() >= timeStamps[spawnIndex] - GameManager.Instance.noteTime)
            {
                var note = Instantiate(spawnObject);
                var p = Lanes[Random.Range(0, Lanes.Count)].spawnPoint.position;
                note.transform.position = p;
                notes.Add(note.GetComponent<FallingNotes>());
                note.GetComponent<FallingNotes>().assignedTime = (float)timeStamps[spawnIndex];
                note.GetComponent<FallingNotes>().initialPosition = p;
                note.GetComponent<FallingNotes>().targetPosition = new Vector3(p.x, -0.4379781f, p.z);
                spawnIndex++;
            }
        }
    }
    private void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);
        SetTimeStamps(array);
        Invoke(nameof(StartSong), songDelayInSeconds);
    }

    public void StartSong()
    {
        GameManager.Instance.audioSource.Play();
        isGameStart = true;
    }

    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, midiFile.GetTempoMap());
            var t = (double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f;
            timeStamps.Add(t);
        }
    }
}

[System.Serializable]
public class Lane
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public Transform spawnPoint;
}
