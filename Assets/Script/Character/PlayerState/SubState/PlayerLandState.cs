using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public bool animationFinish = false;
    public PlayerLandState(PlayerController player, PlayerMachine playerMachine, PlayerData playerData, string aminBoolName) : base(player, playerMachine, playerData, aminBoolName)
    {
    }
    public override void OnEnter()
    {
        base.OnEnter();
        animationFinish = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (inputMovement != Vector3.zero && _player.CurrentVelocity.y <= 0.1f)
        {
            _playerMachine.ChangeState(_player.MoveState);
        }
        else if (inputMovement == Vector3.zero && HasAnimationFinished(_aminBoolName))
        {
            _playerMachine.ChangeState(_player.IdleState);
        }
    }








}
