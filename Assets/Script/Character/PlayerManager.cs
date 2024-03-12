using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IBind<PlayerGameData>
{
    #region Singleton
    public static PlayerManager Instance;
    public string id { get; set; } = System.Guid.NewGuid().ToString();
    public PlayerGameData _playerData = new PlayerGameData();
    [HideInInspector] public PlayerWeaponEquipment PlayerWeaponEquipment;

    private void Awake()
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

    }
    #endregion
    public void Bind(PlayerGameData playerData)
    {
        this._playerData = playerData;
        transform.position = _playerData.position;
    }

    private void LateUpdate()
    {
        _playerData.position = transform.position;
    }


}
[Serializable]
public class PlayerGameData : ISaveData
{
    public string id { get; set; } = System.Guid.NewGuid().ToString();
    public Vector3 position;

    public PlayerGameData()
    {
        position = Vector3.zero;
    }

}
