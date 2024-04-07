using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager, IBind<PlayerGameData>
{
    #region Singleton
    public static PlayerManager Instance;
    public string id { get; set; } = System.Guid.NewGuid().ToString();
    public PlayerGameData _playerData = new PlayerGameData();
    [HideInInspector] public PlayerWeaponEquipment PlayerWeaponEquipment;
    [HideInInspector] public PlayerStatsManager PlayerStatsManager;
    [HideInInspector] public PlayerController PlayerController;
    [HideInInspector] public PlayerEffectManager PlayerEffectManager;
    [HideInInspector] public EquipmentManager EquipmentManager;

    public bool Receive = false;

    protected override void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        PlayerWeaponEquipment = GetComponent<PlayerWeaponEquipment>();
        PlayerStatsManager = GetComponent<PlayerStatsManager>();
        PlayerController = GetComponent<PlayerController>();
        PlayerEffectManager = GetComponent<PlayerEffectManager>();
        EquipmentManager = GetComponent<EquipmentManager>();

    }
    #endregion
    public void Bind(PlayerGameData playerData)
    {
        this._playerData = playerData;
        transform.position = _playerData.position;
    }

    protected override void Update()
    {
        if (Receive)
        {
            PlayerReceive();
        }
    }

    protected override void LateUpdate()
    {
        _playerData.position = transform.position;
    }

    public override IEnumerator ProcessDeathEvent()
    {
        this.PlayerController.playerMachine.ChangeState(PlayerController.DeathState);
        PlayerUIManager.Instance.PlayerPopUpManager.ShowDeadPopUp();
        WorldSFXManager.Instance.PlayDeadSFX();
        return base.ProcessDeathEvent();
    }

    public void PlayerReceive()
    {
        Receive = false;
        IsDead = false;
        this.PlayerController.playerMachine.ChangeState(PlayerController.IdleState);
        this.PlayerStatsManager.RestartStats();
    }

    public override void TakeDamage(WeaponConfig weaponConfig, int physicDame, Vector3 contactPoint)
    {
        base.TakeDamage(weaponConfig, physicDame, contactPoint);
        TakeDamageEffect.targetPoint = contactPoint;
        TakeDamageEffect.PhysicsDamage = physicDame;
        TakeDamageEffect takeDamageEffect = Instantiate(TakeDamageEffect) as TakeDamageEffect;
        CharacterEffectManager.ProcessInstanceEffect(takeDamageEffect);
    }

}

