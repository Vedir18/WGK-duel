using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private ActionManager actionManager;
    [SerializeField] private LayerMask wall;
    [SerializeField] private float checkStep = .2f;
    private int frontChecks = 10;
    private int safeChecks = 50;

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
            if(actionManager.lastAction == 3)
            {
                Vector3 checkPosition = rb.position;
                Vector3 checkDirection = Vector3.forward * movement.y + Vector3.right * movement.x;
                if (movement == Vector2.zero) checkDirection = -transform.forward;
                bool safe = false;
                int steps = 0;
                while (!safe && steps < frontChecks)
                {
                    steps++;
                    Collider[] hits = Physics.OverlapSphere(checkPosition, .5f, wall);
                    if (hits.Length != 0)
                    {
                        Debug.Log(hits);
                        checkPosition += checkDirection * checkStep;
                        Debug.Log("FAILF C: " +checkPosition + " P: " + rb.position);
                    }
                    else
                    {
                        Debug.Log("SUCCF C: " + checkPosition + " P: " + rb.position);
                        safe = true;
                        rb.position = checkPosition;
                    }
                }

                if(!safe)
                {
                    steps = 0;
                    checkDirection = -checkDirection;
                    while (!safe && steps < safeChecks)
                    {
                        steps++;
                        Collider[] hits = Physics.OverlapSphere(checkPosition, .5f, wall);
                        if (hits.Length != 0)
                        {
                            Debug.Log(hits);
                            checkPosition += checkDirection * checkStep;
                            Debug.Log("FAILB C: " + checkPosition + " P: " + rb.position);
                        }
                        else
                        {
                            Debug.Log("SUCCB C: " + checkPosition + " P: " + rb.position);
                            safe = true;
                            rb.position = checkPosition;
                        }
                    }
                }

            }
            mousePosition.y = rb.position.y;
            rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.y * speed);
            if (actionManager.AIP == 0 || actionManager.AIP == 4) rb.MoveRotation(Quaternion.LookRotation(mousePosition - rb.position));
        }
        
    }
}
