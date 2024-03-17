using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private bool _isAttack;
    private int attackCount = 0;
    private readonly int maxAttackCount = 2;
    private float _attackDelay = 1f;
    public PlayerAttackState(PlayerController player, PlayerMachine playerMachine, PlayerData playerData, string aminBoolName) : base(player, playerMachine, playerData, aminBoolName)
    {
    }
    public override void OnEnter()
    {
        base.OnEnter();
        attackCount = 0;
        this._player.PlayerAmin.SetInteger("AttackCount", attackCount);
        this._player.PlayerAmin.applyRootMotion = true;
        _player.CharacterStats.ResetRegeneratorStaminaTimer();
        _player.CharacterStats.CurrentStamina.Value -= _playerData.AttackCost;
        AudioClip attackSound = _player.PlayerWeapon.RightHandWeapon().GetAttackSound();
        WorldSFXManager.Instance.PlaySoundFXOneShot(attackSound);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _isAttack = this._player.InputHandler.AttackInput;

        if (Time.time >= _startTime + _attackDelay && _isAttack && _currentStamina >= _playerData.AttackCost)
        {
            _startTime = Time.time;
            attackCount = (attackCount + 1) % maxAttackCount;
            _player.CharacterStats.CurrentStamina.Value -= _playerData.AttackCost;
            this._player.PlayerAmin.SetInteger("AttackCount", attackCount);
            _player.CharacterStats.ResetRegeneratorStaminaTimer();

            AudioClip attackSound = _player.PlayerWeapon.RightHandWeapon().GetAttackSound();
            WorldSFXManager.Instance.PlaySoundFXOneShot(attackSound);
        }
        else if (HasAnimationFinished() && !_isAttack && Time.time >= _startTime + _attackDelay + 0.2f)
        {
            this._playerMachine.ChangeState(_player.IdleState);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        this._player.PlayerAmin.applyRootMotion = false;
    }


}
