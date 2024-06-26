using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(PlayerController player, PlayerMachine playerMachine, PlayerData playerData, string aminBoolName) : base(player, playerMachine, playerData, aminBoolName)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        this._player.PlayerAmin.applyRootMotion = true;

    }


    public override void OnExit()
    {
        base.OnExit();
        this._player.PlayerAmin.applyRootMotion = false;
    }
}
