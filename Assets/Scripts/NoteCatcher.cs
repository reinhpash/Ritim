using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Unity.VisualScripting;
using System.Linq;

public class NoteCatcher : MonoBehaviour
{
    public Conductor currentConductor;

    public KeyCode buttonKey;
    public MeshRenderer currentRenderer;
    private Material currentMat;
    [ColorUsage(true,true)]public Color activeColor;
    [ColorUsage(true, true)] public Color deactiveColor;
    public Transform catcherUI;
    public GameObject scoreTextPrefab;
    List<FallingNotes> fallingNotes = new List<FallingNotes>();
    private void Start()
    {
        currentMat = currentRenderer.material;
        currentMat.color = deactiveColor;
        currentConductor.OnLoopCompleteEvent += OnLoop;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FallingNotes>(out FallingNotes note))
        {
            fallingNotes.Add(note);
            note.isClickable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<FallingNotes>(out FallingNotes note))
        {
            RemoveNote(note);
        }
    }

    public void RemoveNote(FallingNotes note)
    {
        if (fallingNotes.Contains(note))
        {
            fallingNotes.Remove(note);
            fallingNotes = fallingNotes.Where(x => x != null).ToList();
        }
    }

    private void Update()
    {
        currentMat.color = Color.Lerp(currentMat.color, deactiveColor, 5 * Time.deltaTime);
        if (Input.GetKeyDown(buttonKey))
        {
            var a = fallingNotes.Where(x => x != null).ToList();
            this.transform.DOShakeScale(.15f);
            if (fallingNotes.Count > 0)
            {
                for (int i = 0; i < a.Count; i++)
                {
                    if (fallingNotes[i] == null)
                        return;

                    if (fallingNotes[i].isClickable)
                        Hit(fallingNotes[i].GetComponent<FallingNotes>());
                }
            }
        }
    }
    private void Hit(FallingNotes obj)
    {
        var a = Instantiate(scoreTextPrefab, catcherUI);
        a.GetComponent<UIMover>().Init("+200");
        a.GetComponent<UIMover>().StartMove();
        GameManager.Instance.ScoreManager.UpdateScore(obj.value);
        GameManager.Instance.RemoveNoteFromLists(obj);
        obj.canInteract = false;
        obj.gameObject.SetActive(false);
        Destroy(obj.gameObject, 1.1f);
    }

    void OnLoop()
    {
        currentMat.color = activeColor;
    }

}
