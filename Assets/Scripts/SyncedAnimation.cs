using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncedAnimation : MonoBehaviour
{
    private Animator animator;

    public AnimatorStateInfo animatorStateInfo;

    public int currentState;
    public Conductor currentConductor;

    void Start()
    {
        animator = GetComponent<Animator>();

        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        currentState = animatorStateInfo.fullPathHash;
    }

    void Update()
    {
        animator.Play(currentState, -1, (currentConductor.loopPositionInAnalog));
        animator.speed = 0;
    }
}
