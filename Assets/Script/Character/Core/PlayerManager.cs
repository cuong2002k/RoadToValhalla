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

    public IEnumerator ProcessDeathEvent()
    {
        // PlayerStatsManager.CurrentHealth.Value = 0;
        this.IsDead = true;

        this.PlayerController.playerMachine.ChangeState(PlayerController.DeathState);
        PlayerUIManager.Instance.PlayerPopUpManager.ShowDeadPopUp();
        WorldSFXManager.Instance.PlayDeadSFX();
        yield return new WaitForSeconds(3f);
    }

    public void PlayerReceive()
    {
        Receive = false;
        IsDead = false;
        this.PlayerController.playerMachine.ChangeState(PlayerController.IdleState);
        this.PlayerStatsManager.RestartStats();
    }

}

