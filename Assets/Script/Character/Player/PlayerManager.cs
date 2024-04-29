using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager, ISaveData
{
    #region Singleton
    public static PlayerManager Instance;
    public string id { get; set; }
    private PlayerGameData playerData = new PlayerGameData();
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

    }

    public void SetRespawnPos(Vector3 position)
    {
        Vector3Serializer vector3Serializer = new Vector3Serializer(position);
        playerData.respawnPosition = vector3Serializer;
    }

    public override void TakeDamage(WeaponConfig weaponConfig, int physicDame, Vector3 contactPoint)
    {
        base.TakeDamage(weaponConfig, physicDame, contactPoint);
        TakeDamageEffect.targetPoint = contactPoint;
        TakeDamageEffect.PhysicsDamage = physicDame;
        TakeDamageEffect takeDamageEffect = Instantiate(TakeDamageEffect) as TakeDamageEffect;
        CharacterEffectManager.ProcessInstanceEffect(takeDamageEffect);
    }

    public object CaptureState()
    {
        Vector3Serializer currentPosition = new Vector3Serializer(this.transform.position);
        playerData.position = currentPosition;
        return playerData;
    }

    public void RestoreState(object state)
    {
        playerData = (PlayerGameData)state;
        Vector3Serializer currentPosition = playerData.position;
        this.transform.position = currentPosition.GetVector3();
    }
}

