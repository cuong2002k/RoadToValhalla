using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerState
{
    public PlayerHurtState(PlayerController player, PlayerMachine playerMachine, PlayerData playerData, string aminBoolName) : base(player, playerMachine, playerData, aminBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (HasAnimationFinished())
        {
            _playerMachine.ChangeState(_player.IdleState);
        }
    }
}
