using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCroundState : PlayerGroundedState
{
    public PlayerCroundState(PlayerController player, PlayerMachine playerMachine, PlayerData playerData, string aminBoolName) : base(player, playerMachine, playerData, aminBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        SetAmimationValue();

        if (!_isCround)
        {
            _playerMachine.ChangeState(_player.IdleState);
        }
        else if (inputMovement != Vector3.zero)
        {
            _player.SetMovementVelocity(_playerData.croundSpeed);
        }
    }

    private void SetAmimationValue()
    {
        _player.PlayerAmin.SetFloat("Horizontal", inputMovement.x);
        _player.PlayerAmin.SetFloat("Vertical", inputMovement.z);
    }

    
}
