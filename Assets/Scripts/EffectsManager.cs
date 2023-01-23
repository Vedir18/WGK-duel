using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class EffectsManager : ElympicsMonoBehaviour, IUpdatable, IInitializable
{
    [SerializeField] private ActionManager actionManager;
    [SerializeField] private PlayerInfo playerInfo;

    [SerializeField] private TrailRenderer swordTrail;
    [SerializeField] private Color swipeColor;
    [SerializeField] private Color thrustColor;
    
    [SerializeField] private GameObject shieldEffect;
    [SerializeField] private ParticleSystem dmgEffect;

    [SerializeField] private GameObject cubeClone;

    private int lastAction = 0;
    private bool dashActive;

    public void ElympicsUpdate()
    {
        int currentAction = actionManager.AIP;
        if (currentAction != lastAction)
        {
            switch (lastAction)
            {
                case 1: //Swipe
                case 2: //Thrust
                    swordTrail.enabled = false;
                    break;
                case 3: //Dash
                    dashActive = false;
                    break;
                case 4: //Block
                    shieldEffect.SetActive(false);
                    break;
            }

            switch (currentAction)
            {
                case 1:
                    swordTrail.enabled = true;
                    swordTrail.material.color = swipeColor;
                    break;
                case 2:
                    swordTrail.enabled = true;
                    swordTrail.material.color = thrustColor;
                    break;
                case 3:
                    dashActive = true;
                    break;
                case 4:
                    shieldEffect.SetActive(true);
                    break;
            }
        }else if(dashActive)
        {
            var clone = Instantiate(cubeClone);
            clone.transform.position = transform.position;
            clone.transform.rotation = transform.rotation;
        }

        lastAction = currentAction;
    }

    public void Initialize()
    {
        playerInfo.hp.ValueChanged += PlayDmged;
    }

    private void PlayDmged(float a, float b)
    {
        dmgEffect.Play();
    }
}
