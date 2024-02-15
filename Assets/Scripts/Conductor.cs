using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Conductor : MonoBehaviour
{
    //The current relative position of the song within the loop measured between 0 and 1.
    public float loopPositionInAnalog;

    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //The number of seconds for each song beat
    private float secPerBeat; // note falling speed

    //Current song position, in seconds
    private float songPosition;

    //Current song position, in beats
    private float songPositionInBeats;

    //How many seconds have passed since the song started
    private float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;

    //the number of beats in each loop
    public float beatsPerLoop;

    //the total number of loops completed since the looping clip first started
    private int completedLoops = 0;

    //The current position of the song within the loop in beats.
    public float loopPositionInBeats;

    public Action OnLoopCompleteEvent;
    public bool shouldSourcePlay = false;

    public float SecPerBeat { get => secPerBeat; set => secPerBeat = value; }
    public int CompletedLoops { get => completedLoops; set => completedLoops = value; }

    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        if(shouldSourcePlay)
            musicSource.Play();
    }

    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        //calculate the loop position
        if (songPositionInBeats >= (completedLoops + 1) * beatsPerLoop)
        {
            completedLoops++;
            OnLoopCompleteEvent?.Invoke();
        }

        loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;

        loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;
    }

    public void OnChangeBeatsPerLoop(float value)
    {
        beatsPerLoop = value;
    }
}
