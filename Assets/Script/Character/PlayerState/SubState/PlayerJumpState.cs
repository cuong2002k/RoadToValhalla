using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{

    public PlayerJumpState(PlayerController player, PlayerMachine playerMachine, PlayerData playerData, string aminBoolName) : base(player, playerMachine, playerData, aminBoolName)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _player.InputHandler.ResetJumpInput();
        _player.SetJumpVelocity();
        _player.CharacterStats.ResetRegeneratorStaminaTimer();
        _player.CharacterStats.CurrentStamina.Value -= this._playerData.JumpCost;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (HasAnimationFinished(_aminBoolName))
        {
            _playerMachine.ChangeState(_player.InAirState);
        }
    }


}
