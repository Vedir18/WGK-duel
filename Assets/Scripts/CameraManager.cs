using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class CameraManager : ElympicsMonoBehaviour, IInitializable
{
    [SerializeField] private Transform p0, p1;
    private Transform playerToFollow;
    private Transform camTransform;
    [SerializeField] private float minFollowSpeed, maxFollowSpeed;
    [SerializeField] private float minOffset, maxOffset;
    public void Initialize()
    {
        camTransform = transform;
        switch((int)Elympics.Player)
        {
            case 0:
                playerToFollow = p0;
                break;
            case 1:
                playerToFollow = p1;
                break;
            default:
                camTransform.position = Vector3.up * 30;
                playerToFollow = camTransform;
                break;
        }
    }

    private void Update()
    {
        Vector3 desiredPosition = new Vector3(playerToFollow.position.x, camTransform.position.y, playerToFollow.position.z);
        float offset = Vector3.Distance(desiredPosition, transform.position);
        float ratio = (offset - minOffset) / (maxOffset - minOffset);
        ratio = Mathf.Max(0, Mathf.Min(1, ratio));

        float followSpeed = (1 - ratio) * minFollowSpeed + ratio * maxFollowSpeed;

        Vector3 newPosition = transform.position;
        transform.position = Vector3.MoveTowards(newPosition, desiredPosition, followSpeed * Time.deltaTime);
    }
}
