using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Elympics;

public class UIManager : ElympicsMonoBehaviour
{
    [Header("Stamina")]
    [SerializeField] private ActionManager actionManager1, actionManager2;
    [SerializeField] private GameObject[] staminaBars1;
    [SerializeField] private GameObject[] staminaBars2;

    [Header("Health")]
    [SerializeField] private PlayerInfo p1, p2;
    [SerializeField] private Slider hp1, hp2;

    [Header("Cooldowns")]
    [SerializeField] private Slider swipe, thrust, block, dash;

    [Header("GameOver")]
    [SerializeField] private GameObject p1Won, p2Won, draw;
    [SerializeField] private GameObject quitButton;

    private ActionManager curPlayer;
    // Start is called before the first frame update
    void Start()
    {
        switch((int)Elympics.Player)
        {
            case 1:
                curPlayer = actionManager2;
                break;
            case 0:
            default:
                curPlayer = actionManager1;
                break;
        }
}

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < staminaBars1.Length; i++)
        {
            if (i <= Mathf.Floor(actionManager1.curStam - 1)) staminaBars1[i].SetActive(true);
            else staminaBars1[i].SetActive(false);
            
            if (i <= Mathf.Floor(actionManager2.curStam - 1)) staminaBars2[i].SetActive(true);
            else staminaBars2[i].SetActive(false);
        }

        hp1.value = p1.hp / p1.maxHP;
        hp2.value = p2.hp / p2.maxHP;


        swipe.value = 1 - (float)(curPlayer.attackCooldown - p1.curTick) / curPlayer.attackCDDuration;
        thrust.value = 1 - (float)(curPlayer.thrustCooldown - p1.curTick) / curPlayer.thrustCDDuration;
        block.value = 1 - (float)(curPlayer.blockCooldown - p1.curTick) / curPlayer.blockCDDuration;
        dash.value = 1 - (float)(curPlayer.dashCooldown - p1.curTick) / curPlayer.dashCDDuration;

    }

    public void GameOver(int winner)
    {
        if (winner == 0) p1Won.SetActive(true);
        else if (winner == 1) p2Won.SetActive(true);
        else draw.SetActive(true);

        quitButton.SetActive(true);
    }
}
