using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
public class Matchmaker : MonoBehaviour
{
   public void FindGame()
    {
        Debug.Log("Finding game");
        ElympicsLobbyClient.Instance.PlayOnline(null, null, "Default");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
