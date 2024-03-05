using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintingState : PlayerGroundedState
{

    public PlayerSprintingState(PlayerController player, PlayerMachine playerMachine, PlayerData playerData, string aminBoolName) : base(player, playerMachine, playerData, aminBoolName)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _player.CharacterStats.ResetRegeneratorStaminaTimer();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (inputMovement == Vector3.zero && _player.CurrentVelocity.y <= 0.1f)
        {
            _playerMachine.ChangeState(_player.IdleState);
        }
        else if (this._currentStamina >= _playerData.SprintingCost && _isSprinting)
        {
            _player.PlayerAmin.SetFloat("Horizontal", inputMovement.x * 2f);
            _player.PlayerAmin.SetFloat("Vertical", inputMovement.z * 2f);
            _player.SetMovementVelocity(_playerData.sprintingSpeed);
            _player.CharacterStats.CurrentStamina.Value -= _playerData.SprintingCost * Time.deltaTime;
        }
        else
        {
            _playerMachine.ChangeState(_player.MoveState);
        }
    }
}
