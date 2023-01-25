using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;
public class Matchmaker : MonoBehaviour
{
    private bool searchInProgress = false;
   public void FindGame()
    {
        if (searchInProgress) return;
        Debug.Log("Finding game");
        searchInProgress = true;
        ElympicsLobbyClient.Instance.PlayOnline(null, null, "Default");
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
