using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 movement;
    public bool dash;
    public bool attack;
    public bool block;
    public bool thrust;
    public Vector3 mousePosition;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask mouseRaycastMask;

 
    public void UpdateInput()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        dash = Input.GetKey(KeyCode.Space) || dash;
        attack = Input.GetKey(KeyCode.Mouse0) || attack;
        block = Input.GetKey(KeyCode.Mouse1) || block;
        thrust = Input.GetKey(KeyCode.E) || thrust;
        mousePosition = GetWorldMousePosition();
    }

    public void ResetInput()
    {
        dash = false;
        attack = false;
        block = false;
        thrust = false;
    }
    private Vector3 GetWorldMousePosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 1000f, mouseRaycastMask)) return hit.point;
        else return transform.position;
    }

}
