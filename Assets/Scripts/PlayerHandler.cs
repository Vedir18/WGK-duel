using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elympics;

public class PlayerHandler : ElympicsMonoBehaviour, IUpdatable, IInputHandler
{
    [SerializeField] public ElympicsBool canMove = new ElympicsBool(true);

    [SerializeField] private InputManager inputManager;
    [SerializeField] private MovementHandler movementHandler;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private ActionManager actionManager;

    private Vector2 movement;
    private bool attack;
    private bool thrust;
    private bool dash;
    private bool block;
    private Vector3 mousePosition;

    void Update()
    {
        if (Elympics.Player != PredictableFor) return;
        inputManager.UpdateInput();
        //Debug.Log($"UPDATE: P {PredictableFor} MVM {inputManager.movement} | A {inputManager.attack} | B {inputManager.block} | T {inputManager.thrust} | D {inputManager.dash} | MP {inputManager.mousePosition}");
    }

    public void ElympicsUpdate()
    {
        if (!canMove)
        {
            movementHandler.HandleMovement(Vector2.zero, mousePosition);
            return;
        }

        if(ElympicsBehaviour.TryGetInput(PredictableFor, out var inputReader, 4))
        {
            float x1;
            float y1;
            float x2;
            float y2;
            float z2;

            inputReader.Read(out x1);
            inputReader.Read(out y1);
            movement = new Vector2(x1, y1).normalized;
            inputReader.Read(out attack);
            inputReader.Read(out thrust);
            inputReader.Read(out dash);
            inputReader.Read(out block);
            inputReader.Read(out x2);
            inputReader.Read(out y2);
            inputReader.Read(out z2);
            mousePosition = new Vector3(x2, y2, z2);
            //Debug.Log($"EL UPDATE: P {PredictableFor} MVM {movement} | A {attack} | B {block} | T {thrust} | D {dash} | MP {mousePosition}");
        }
        else
        {
            Debug.Log($"WHY??? {PredictableFor} {Elympics.Player}");
        }

        movementHandler.HandleMovement(movement, mousePosition);
        actionManager.HandleActions(attack, thrust, block, dash, Elympics.Tick);

    }

    public void OnInputForBot(IInputWriter inputSerializer)
    {

    }

    public void OnInputForClient(IInputWriter inputSerializer)
    {
        if (Elympics.Player != PredictableFor) return;

        inputSerializer.Write(inputManager.movement.x);
        inputSerializer.Write(inputManager.movement.y);
        inputSerializer.Write(inputManager.attack);
        inputSerializer.Write(inputManager.thrust);
        inputSerializer.Write(inputManager.dash);
        inputSerializer.Write(inputManager.block);
        inputSerializer.Write(inputManager.mousePosition.x);
        inputSerializer.Write(inputManager.mousePosition.y);
        inputSerializer.Write(inputManager.mousePosition.z);

        inputManager.ResetInput();
    }
}
