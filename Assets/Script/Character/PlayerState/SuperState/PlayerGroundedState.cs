using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected Vector3 inputMovement;
    protected bool _isJumping;
    protected bool _isSprinting;
    protected bool _isCround;
    protected bool _groundCheck;

    public PlayerGroundedState(PlayerController player, PlayerMachine playerMachine, PlayerData playerData, string aminBoolName)
    : base(player, playerMachine, playerData, aminBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
        _groundCheck = _player.CheckIfGrounded();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        HandlerAllInput();
        _player.PlayerAmin.SetBool("Grounded", _groundCheck);
        if (_isJumping && _groundCheck && _player.CurrentVelocity.y <= 0.1f)
        {
            _playerMachine.ChangeState(_player.JumpState);
        }
        else if (!_groundCheck)
        {
            _playerMachine.ChangeState(_player.InAirState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

    }


    protected void HandlerAllInput()
    {
        // movement input
        inputMovement = new Vector3(_player.InputHandler.XInput, 0f, _player.InputHandler.YInput);
        // jump input
        _isJumping = _player.InputHandler.JumpInput;
        _isSprinting = _player.InputHandler.ShiftInput;
        _isCround = _player.InputHandler.CroundInput;
    }



}
