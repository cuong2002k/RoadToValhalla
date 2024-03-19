using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    protected bool _groundCheck;
    protected Vector3 _movementInput;


    public PlayerInAirState(PlayerController player,
    PlayerMachine playerMachine,
    PlayerData playerData,
    string aminBoolName) :
    base(player, playerMachine, playerData, aminBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
        // check grounded
        _groundCheck = _player.CheckIfGrounded();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // player is ground
        if (_groundCheck && _player.CurrentVelocity.y < 0.01f)
        {
            _playerMachine.ChangeState(_player.LandState);
        }
        else
        {
            _player.SetMovementVelocity(_playerData.walkSpeed);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    protected override void HandlerInput()
    {
        base.HandlerInput();
        _movementInput = new Vector3(_player.InputHandler.XInput, 0f, _player.InputHandler.YInput);
    }
}
