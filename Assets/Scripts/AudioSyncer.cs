using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncer : MonoBehaviour
{
    public AudioSource master;
    public AudioSource slave;

    void Update()
    {
        UpdateSyncAudio();
    }

    private void UpdateSyncAudio()
    {
        slave.timeSamples = master.timeSamples;
    }
}
