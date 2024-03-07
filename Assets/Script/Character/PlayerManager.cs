using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IBind<PlayerGameData>
{
    #region Singleton
    public static PlayerManager Instance;
    public Guid id { get; set; } = System.Guid.NewGuid();
    public PlayerGameData playerData = new PlayerGameData();
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
        this.playerData = playerData;
        this.playerData.id = id;
        transform.position = playerData.position;
        transform.rotation = playerData.rotation;
    }
    private void Update()
    {
        playerData.position = transform.position;
        playerData.rotation = transform.rotation;
    }

    [HideInInspector] public PlayerWeaponEquipment PlayerWeaponEquipment;


}
[Serializable]
public class PlayerGameData : ISaveData
{
    public Guid id { get; set; } = System.Guid.NewGuid();
    public Vector3 position;
    public Quaternion rotation;

}
