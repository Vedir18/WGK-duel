using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
public class PlayerInfo : ElympicsMonoBehaviour, IObservable
{
    public ElympicsFloat hp = new ElympicsFloat(20);
    [SerializeField] private int playerID;
    [SerializeField] private ActionManager actionManager;

    public int GetID()
    {
        return playerID;
    }

    public void DealDamage(float dmgValue)
    {
        if(actionManager.AIP != 3 && actionManager.AIP != 4) hp.Value -= dmgValue;
        else Debug.Log($"Player {playerID} Blocked a hit");
    }

}
