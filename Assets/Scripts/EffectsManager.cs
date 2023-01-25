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

    [Header("SoundEffects")]
    [SerializeField] private AudioSource swipe1, swipe2;
    [SerializeField] private AudioSource thrust1, thrust2;
    [SerializeField] private AudioSource block1, block2;
    [SerializeField] private AudioSource dash1, dash2;
    [SerializeField] private AudioSource dmg1, dmg2;
    private bool s, t, b, d, dmg;

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
                    
                    if (s) swipe1.Play();
                    else swipe2.Play();
                    break;
                case 2:
                    swordTrail.enabled = true;
                    swordTrail.material.color = thrustColor;
                    if (t) thrust1.Play();
                    else thrust2.Play();
                    break;
                case 3:
                    dashActive = true;
                    if (d) dash1.Play();
                    else dash2.Play();
                    break;
                case 4:
                    shieldEffect.SetActive(true);
                    if (b) block1.Play();
                    else block2.Play();
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
        if (dmg) dmg1.Play();
        else dmg2.Play();
    }
}
