using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingNotes : MonoBehaviour
{
    public float assignedTime;// tam olarak 0'da olmasý gereken süre
    public bool isClickable = false;

    public Vector3 initialPosition;
    public Vector3 targetPosition;
    double timeInstantiated;
    bool hasPerformedPhysicChange;
    MeshRenderer mr;
    [ColorUsage(true,true)]public Color initialColor;
    [ColorUsage(true, true)]public Color targetColor;
    private bool hasPerformedColorChange;
    public float colorTime;
    public int value = 0;
    public bool canInteract = true;
    public GameObject scoreTextPrefab;
    public TrailRenderer tr;
    void Start()
    {
        mr = this.GetComponentInChildren<MeshRenderer>();
        mr.material.color = initialColor;
        tr.material.color = initialColor;
        timeInstantiated = GameManager.Instance.GetAudioSourceTime();
        timeInstantiated = assignedTime - GameManager.Instance.noteTime;
    }


    void Update()
    {
        double timeSinceInstantiated = GameManager.Instance.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (GameManager.Instance.noteTime * 2));


        if (t > 1.1f)
        {
            Destroy(gameObject);
            var a = Instantiate(scoreTextPrefab, GameManager.Instance.ScoreManager.GameCanvas.transform);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            Vector2 adjustedScreenPos = new Vector2(screenPos.x - Screen.width / 2, screenPos.y - Screen.height / 2);
            a.GetComponent<RectTransform>().anchoredPosition = adjustedScreenPos;
            a.GetComponent<UIMover>().Init("-100", "MISS");
            a.GetComponent<UIMover>().StartMove(3);
            GameManager.Instance.ScoreManager.UpdateScore(-200);
        }
        else if (t > 1)
        {
            if (!hasPerformedPhysicChange)
            {

                this.GetComponent<Rigidbody>().useGravity = true;
                this.GetComponent<Rigidbody>().isKinematic = false;
                hasPerformedPhysicChange = true;
            }
        }
        else
        {
            transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, t);
            if (t > 1f - colorTime && !hasPerformedColorChange)
            {
                mr.material.DOColor(targetColor, 1f - colorTime);
                tr.material.DOColor(targetColor, 1f - colorTime);
                hasPerformedColorChange = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isClickable = false;
    }

}
