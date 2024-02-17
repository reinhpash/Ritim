using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class NoteCatcher : MonoBehaviour
{
    public Conductor currentConductor;

    public KeyCode buttonKey;
    public MeshRenderer currentRenderer;
    private Material currentMat;
    [ColorUsage(true, true)] public Color activeColor;
    [ColorUsage(true, true)] public Color deactiveColor;
    public Transform catcherUI;
    public GameObject scoreTextPrefab;
    public Vector3 halfsize = new Vector3(.1f, .1f, .1f);

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
            note.isClickable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<FallingNotes>(out FallingNotes note))
        {
            note.isClickable = false;
        }
    }

    private void Update()
    {
        currentMat.color = Color.Lerp(currentMat.color, deactiveColor, 5 * Time.deltaTime);
        if (Input.GetKeyDown(buttonKey))
        {
            this.transform.DOShakeScale(.15f);
            Collider[] colliders = Physics.OverlapBox(transform.position, halfsize, Quaternion.identity);
            foreach (var collider in colliders)
            {
                FallingNotes fallingNote = collider.GetComponent<FallingNotes>();
                if (fallingNote != null && fallingNote.isClickable)
                {
                    Hit(fallingNote);
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
        obj.canInteract = false;
        obj.gameObject.SetActive(false);
        Destroy(obj.gameObject, 1.1f);
    }

    void OnLoop()
    {
        currentMat.color = activeColor;
    }

    private void OnDrawGizmos()
    {
        Bounds bounds = new Bounds(transform.position, halfsize * 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, bounds.size);
    }
}
