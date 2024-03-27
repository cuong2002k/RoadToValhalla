using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerController player,
    PlayerMachine playerMachine,
    PlayerData playerData,
    string aminBoolName) :
    base(player, playerMachine, playerData, aminBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_groundCheck && _isCround)
        {
            _playerMachine.ChangeState(_player.CroundState);
        }
        else if (inputMovement != Vector3.zero && _groundCheck)
        {
            if (_groundCheck && _isSprinting && this._currentStamina >= _playerData.SprintingCost)
            {
                _playerMachine.ChangeState(_player.SprintState);
            }
            else if (inputMovement != Vector3.zero)
            {
                _playerMachine.ChangeState(_player.MoveState);
            }
        }

    }
}
