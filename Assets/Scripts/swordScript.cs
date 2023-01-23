using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordScript : MonoBehaviour
{
    [SerializeField] private PlayerInfo playerInfo;

    [SerializeField] private float swipeAngle;
    [SerializeField] private float thrustAngle;
    [SerializeField] private ActionManager actionManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerInfo>(out var hitPlayer))
        {
            Debug.Log(other.name);
            float hitAngle = Vector3.Angle(transform.forward, other.transform.position - transform.position);
            float checkAngle = 0;
            switch(actionManager.AIP)
            {
                case 1:
                    checkAngle = swipeAngle;
                    break;
                case 2:
                    checkAngle = thrustAngle;
                    break;
            }
            if (hitPlayer.GetID() != playerInfo.GetID() && hitAngle  < checkAngle/2)

            {
                hitPlayer.DealDamage(2);
                Debug.Log($"Hit player {other.gameObject.name}");
            }
        }
    }
}
