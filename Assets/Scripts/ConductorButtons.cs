using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ConductorButtons : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("Button is pressed");
        this.transform.DOShakeScale(.15f);
    }
}
