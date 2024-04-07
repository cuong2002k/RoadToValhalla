using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private bool _isAttack;
    private float _timeChangeState = 0.2f;
    private int attackCount = 0;
    private readonly int maxAttackCount = 2;
    private float _attackDelay = 1f;
    public PlayerAttackState(PlayerController player, PlayerMachine playerMachine, PlayerData playerData, string aminBoolName) : base(player, playerMachine, playerData, aminBoolName)
    {
    }
    public override void OnEnter()
    {
        base.OnEnter();
        this._player.PlayerAmin.applyRootMotion = false;
        attackCount = -1;
        SetAttackCount();
        _player.PlayerStats.ResetRegeneratorStaminaTimer();
        _player.PlayerStats.CurrentStamina.Value -= _playerData.AttackCost;
        PlaySoundFX();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= _startTime + _attackDelay && _isAttack && _currentStamina >= _playerData.AttackCost)
        {
            _startTime = Time.time;
            SetAttackCount();
            _player.PlayerStats.CurrentStamina.Value -= _playerData.AttackCost;
            _player.PlayerStats.ResetRegeneratorStaminaTimer();
            PlaySoundFX();

        }
        else if (HasAnimationFinished() && !_isAttack && Time.time >= _startTime + _attackDelay + _timeChangeState)
        {
            this._playerMachine.ChangeState(_player.IdleState);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        this._player.PlayerAmin.applyRootMotion = false;
    }

    protected override void HandlerInput()
    {
        base.HandlerInput();
        _isAttack = this._player.InputHandler.AttackInput;
    }

    public void PlaySoundFX()
    {
        AudioClip attackSound = _player.PlayerWeapon.RightHandWeapon().GetAttackSound();
        WorldSFXManager.Instance.PlaySoundFXOneShot(attackSound);
    }

    public void SetAttackCount()
    {
        attackCount = (attackCount + 1) % maxAttackCount;
        this._player.PlayerAmin.SetInteger("AttackCount", attackCount);
    }



}
