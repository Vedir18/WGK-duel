using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class ActionManager : ElympicsMonoBehaviour
{
    [SerializeField] private ElympicsInt actionInProgress = new ElympicsInt(0); //0 - N, 1 - S, 2 - T, 3 - D, 4 - B
    public int AIP => actionInProgress.Value;

    [Header("General refs")]
    private long actionStart, actionEnd;
    private Color a0 = Color.grey;
    [SerializeField] private GameObject AS;
    [SerializeField] private Collider playerCollider;

    [Header("Sword Refs")]
    [SerializeField] private Transform swordTr;
    [SerializeField] private Collider swordCollider;
    [SerializeField] private Transform shieldTr;

    [Header("AttackVars")]
    [SerializeField] private int attackDuration;
    [SerializeField] private int attackCDDuration;
    [SerializeField] private long attackCooldown = -1;

    private bool swipingRight;

    [SerializeField] private float swipeAngle;
    private Color a1 = Color.red;

    [Header("ThrustVars")]
    [SerializeField] private int thrustDuration;
    [SerializeField] private int thrustCDDuration;
    [SerializeField] private long thrustCooldown = -1;

    [SerializeField] private float thrustReach;
    [SerializeField] private float lean;
    private Color a2 = Color.yellow;

    [Header("BlockVars")]
    [SerializeField] private int blockDuration;
    [SerializeField] private int blockCDDuration;
    [SerializeField] private long blockCooldown = -1;
    private Color a4 = Color.blue;

    [Header("DashVars")]
    [SerializeField] private int dashDuration;
    [SerializeField] private int dashCDDuration;
    [SerializeField] private long dashCooldown = -1;

    private Color a3 = Color.green;

    public void HandleActions(bool attack, bool thrust, bool block, bool dash, long tick)
    {
        int lastAction = AIP;
        //Debug.Log($"A { attack} T { thrust} B {block} D {dash} TICK { tick}");
        if(block && actionInProgress.Value != 4 && tick > blockCooldown) StartBlock(tick);
        else if(dash && actionInProgress.Value != 3 && tick > dashCooldown) StartDash(tick);
        else if(thrust && actionInProgress.Value == 0 && tick > thrustCooldown) StartThrust(tick);
        else if(attack && actionInProgress.Value == 0 && tick > attackCooldown) StartAttack(tick);

        actionInProgress.Value = tick > actionEnd ? 0 : actionInProgress.Value;

        if(AIP != lastAction)
        {
            switch(lastAction)
            {
                case 1:
                case 2:
                    swordTr.localPosition = Vector3.zero;
                    float swordRestAngle = (swipingRight? 1:0) * swipeAngle - swipeAngle / 2;
                    swordTr.localEulerAngles = Vector3.up * swordRestAngle;
                    break;
                case 3:
                    playerCollider.enabled = true;
                    break;
                case 4:
                    shieldTr.localEulerAngles = Vector3.up * -90;
                    break;
            }
        }

        Color a = Color.black;
        switch (actionInProgress)
        {
            case 0:
                a = a0;
                break;
            case 1:
                a = a1;
                ProcessAttack(tick);
                break;
            case 2:
                a = a2;
                ProcessThrust(tick);
                break;
            case 3:
                a = a3;
                break;
            case 4:
                a = a4;
                break;
        }

        AS.GetComponent<MeshRenderer>().material.color = a;
        swordCollider.enabled = actionInProgress.Value == 1 || actionInProgress.Value == 2;

    }

    private void StartAttack(long tick)
    {
        swipingRight = !swipingRight;
        actionInProgress.Value = 1;
        actionStart = tick;
        actionEnd = tick + attackDuration;
        attackCooldown = tick + attackCDDuration;
        swordTr.localPosition = Vector3.zero;
    }
    private void ProcessAttack(long tick)
    {
        float currAngle = (swipingRight?(1-(float)(actionEnd - tick) / attackDuration):((float)(actionEnd - tick) / attackDuration)) * swipeAngle - swipeAngle / 2;
        //Debug.Log($"T: {tick}, E: {actionEnd}, R: {(float)(actionEnd - tick) / attackDuration}, A: {currAngle}");
        swordTr.localEulerAngles = Vector3.up * currAngle;
    }

    private void StartThrust(long tick)
    {
        swipingRight = !swipingRight;
        actionInProgress.Value = 2;
        actionStart = tick;
        actionEnd = tick + thrustDuration;
        thrustCooldown = tick + thrustCDDuration;
        swordTr.localEulerAngles = Vector3.zero;
        swordTr.localPosition = Vector3.zero;
    }

    private void ProcessThrust(long tick)
    {
        float ratio = (float)(actionEnd - tick) / thrustDuration;
        float currReach = (1 - Mathf.Abs(3*ratio - 1.5f)) * thrustReach;
        float currLean = (swipingRight ? (1 - ratio) : ratio) * lean - lean / 2;
        swordTr.localPosition = new Vector3(currLean, 0, currReach);
    }

    private void StartDash(long tick)
    {
        actionInProgress.Value = 3;
        actionStart = tick;
        actionEnd = tick + dashDuration;
        dashCooldown = tick + dashCDDuration;
        playerCollider.enabled = false;
    }

    private void StartBlock(long tick) 
    {
        actionInProgress.Value = 4;
        actionStart = tick;
        actionEnd = tick + blockDuration;
        blockCooldown = tick + blockCDDuration;
        shieldTr.localEulerAngles = Vector3.zero;
    }
}
