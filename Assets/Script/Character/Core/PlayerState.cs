using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : IState
{
    protected PlayerController _player;
    protected PlayerData _playerData;
    protected PlayerMachine _playerMachine;
    protected string _aminBoolName;

    protected float _startTime;

    public PlayerState(PlayerController player, PlayerMachine playerMachine, PlayerData playerData, string aminBoolName)
    {
        this._player = player;
        this._playerData = playerData;
        this._playerMachine = playerMachine;
        this._aminBoolName = aminBoolName;
    }


    public override void OnEnter()
    {
        DoCheck();
        _player.PlayerAmin.SetBool(_aminBoolName, true);
        _startTime = Time.time;
        //_player.PlayerAmin.applyRootMotion = true;
        //Debug.Log("Start state " + _aminBoolName);
    }

    public override void DoCheck()
    {

    }

    public override void LogicUpdate()
    {

    }

    public override void PhysicUpdate()
    {
        DoCheck();
    }

    public override void OnExit()
    {
        _player.PlayerAmin.SetBool(_aminBoolName, false);
        _startTime = 0f;
        //_player.PlayerAmin.applyRootMotion = false;
        //Debug.Log("Exit state " + _aminBoolName);
    }

    protected bool HasAnimationFinished(string animationName)
    {
        // Check if the specified animation has completed
        AnimatorStateInfo stateInfo = _player.PlayerAmin.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1.0f;
    }

}
