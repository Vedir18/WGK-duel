using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private ActionManager actionManager;
    public void HandleMovement(Vector2 movement, Vector3 mousePosition) 
    {
        if (actionManager.AIP == 3)
        {
            Vector3 dashDirection = Vector3.forward * movement.y + Vector3.right * movement.x;
            if (movement == Vector2.zero) dashDirection = transform.forward;
            rb.velocity = dashDirection * dashSpeed;
        }
        else
        {
            mousePosition.y = rb.position.y;
            rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.y * speed);
            if (actionManager.AIP == 0 || actionManager.AIP == 4) rb.MoveRotation(Quaternion.LookRotation(mousePosition - rb.position));
        }
        
    }
}
