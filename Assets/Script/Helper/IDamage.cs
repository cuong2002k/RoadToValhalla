using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    public void TakeDamage(WeaponConfig weaponConfig, int physicDame, Vector3 contactPoint);
}
