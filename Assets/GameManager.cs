using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Elympics;

public class GameManager : ElympicsMonoBehaviour, IUpdatable
{
    [SerializeField] private UIManager uIManager;
    [SerializeField] private PlayerInfo p1, p2;
    [SerializeField] private PlayerHandler ph1, ph2;
    private bool gameEnded = false;



    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void EndGame()
    {

        Elympics.EndGame();
    }

    public void ElympicsUpdate()
    {
        if (p1.hp <= 0 || p2.hp <= 0)
        {
            if (p1.hp.Value <= 0 && p2.hp.Value <= 0) uIManager.GameOver(2);
            else if (p1.hp.Value <= 0) uIManager.GameOver(1);
            else if (p2.hp.Value <= 0) uIManager.GameOver(0);

            ph1.canMove.Value = false;
            ph2.canMove.Value = false;

            gameEnded = true;

            if (Elympics.IsServer) EndGame();
        }else if(gameEnded)
        {
            uIManager.RevertGameOver();
        }
    }
}
