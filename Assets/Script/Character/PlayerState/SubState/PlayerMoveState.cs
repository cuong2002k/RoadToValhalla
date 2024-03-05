using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerController player,
    PlayerMachine playerMachine,
    PlayerData playerData,
    string aminBoolName) : base(player, playerMachine, playerData, aminBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _player.PlayerAmin.SetFloat("Horizontal", inputMovement.x);
        _player.PlayerAmin.SetFloat("Vertical", inputMovement.z);
        if (inputMovement == Vector3.zero && _player.CurrentVelocity.y <= 0.1f)
        {
            _playerMachine.ChangeState(_player.IdleState);
        }
        else if (_groundCheck && _isCround)
        {
            _playerMachine.ChangeState(_player.CroundState);
        }
        else if (inputMovement != Vector3.zero && _groundCheck && _isSprinting && _currentStamina >= _playerData.SprintingCost)
        {
            _playerMachine.ChangeState(_player.SprintState);
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


}
