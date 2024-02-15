using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSetter : MonoBehaviour
{
    public int value = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FallingNotes note))
        {
            note.value = this.value;
        }
    }
}
