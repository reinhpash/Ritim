using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncedRotation : MonoBehaviour
{
    public Conductor currentConductor;
    private Vector3 startScale;

    public bool isZ = false;
    private void Start()
    {
        if(currentConductor == null)
            Destroy(this);

        startScale = this.transform.localScale;

        currentConductor.OnLoopCompleteEvent += OnLoop;

    }
    void OnLoop()
    {
        this.transform.localScale = startScale * 1.25f;
    }

    void Update()
    {
        if (!isZ)
        {
            this.transform.localScale = Vector3.Lerp(transform.localScale, startScale, Time.deltaTime * 5f);
            this.gameObject.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(0, 360, currentConductor.loopPositionInAnalog), 0);
        }
        else
        {
            this.gameObject.transform.localRotation = Quaternion.Euler(-90, Mathf.Lerp(0, 360, currentConductor.loopPositionInAnalog), 0);

        }

    }
}
