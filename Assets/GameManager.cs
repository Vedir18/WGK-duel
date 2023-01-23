using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class GameManager : ElympicsMonoBehaviour, IInitializable
{
    [SerializeField] private UIManager uIManager;
    [SerializeField] private PlayerInfo p1, p2;
    [SerializeField] private PlayerHandler ph1, ph2;

    public void Initialize()
    {
        p1.hp.ValueChanged += EndGameMayby;
        p2.hp.ValueChanged += EndGameMayby;
    }

    private void EndGameMayby(float a, float b)
    {
        if(b <= 0)
        {
            if (p1.hp.Value <= 0 && p2.hp.Value <= 0) uIManager.GameOver(2);
            else if (p1.hp.Value <= 0) uIManager.GameOver(1);
            else if (p2.hp.Value <= 0) uIManager.GameOver(0);

            ph1.canMove.Value = false;
            ph2.canMove.Value = false;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
