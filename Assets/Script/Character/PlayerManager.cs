using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager Instance;
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
    }
    #endregion

    [HideInInspector] public PlayerWeaponEquipment PlayerWeaponEquipment;

    private void Start()
    {
        PlayerWeaponEquipment = GetComponent<PlayerWeaponEquipment>();
    }

}
