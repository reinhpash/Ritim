using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Level> levels = new List<Level>();
}

[System.Serializable]
public struct Level
{
    public string midiName;
    public AudioClip song;
    public int bpm;
    public float noteTime;
    public List<Melanchall.DryWetMidi.MusicTheory.NoteName> restrictions;
}
