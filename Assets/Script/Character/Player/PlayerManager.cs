using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager, IBind<PlayerGameData>
{
    #region Singleton
    public static PlayerManager Instance;
    public string id { get; set; } = System.Guid.NewGuid().ToString();
    public PlayerGameData PlayerData = new PlayerGameData();
    [HideInInspector] public PlayerWeaponEquipment PlayerWeaponEquipment;
    [HideInInspector] public PlayerStatsManager PlayerStatsManager;
    [HideInInspector] public PlayerController PlayerController;
    [HideInInspector] public PlayerEffectManager PlayerEffectManager;
    [HideInInspector] public EquipmentManager EquipmentManager;

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

    //bind data
    public void Bind(PlayerGameData playerData)
    {
        this.PlayerData = playerData;
        transform.position = PlayerData.position;
    }

    protected override void Update()
    {

    }

    protected override void LateUpdate()
    {
        PlayerData.position = transform.position;
    }

    public override IEnumerator ProcessDeathEvent()
    {
        this.PlayerController.playerMachine.ChangeState(PlayerController.DeathState);
        PlayerUIManager.Instance.PlayerPopUpManager.ShowDeadPopUp();
        WorldSFXManager.Instance.PlayDeadSFX();
        yield return StartCoroutine(base.ProcessDeathEvent());
        PlayerReceive();
    }

    public void PlayerReceive()
    {
        IsDead = false;
        this.PlayerController.playerMachine.ChangeState(PlayerController.IdleState);
        this.PlayerStatsManager.RestartStats();
        this.transform.position = PlayerData.respawnPosition;
    }

    public void SetRespawnPos(Vector3 position)
    {
        PlayerData.respawnPosition = position;
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

