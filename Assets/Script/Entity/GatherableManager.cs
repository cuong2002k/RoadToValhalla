using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherableManager : CharacterManager
{
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private GameObject _logWoodPrefabs;
    [SerializeField] private BaseItem _itemDrop;
    private int from = 2;
    private int to = 4;
    public override void TakeDamage(WeaponConfig weaponConfig, int physicDame, Vector3 contactPoint)
    {
        base.TakeDamage(weaponConfig, physicDame, contactPoint);
        if (weaponConfig.GetWeaponType() == _weaponType)
        {
            TakeDamageEffect.targetPoint = contactPoint;
            TakeDamageEffect.PhysicsDamage = physicDame;
            GatherableEffects takeDamageEffect = Instantiate(TakeDamageEffect) as GatherableEffects;
            CharacterEffectManager.ProcessInstanceEffect(takeDamageEffect);
        }
    }

    public override IEnumerator ProcessDeathEvent()
    {
        yield return new WaitForSeconds(0.1f);
        IsDead = true;

        int logNum = Random.Range(from, to);
        for (int i = 0; i < logNum; i++)
        {
            float randomX = Random.Range(-2f, 2f);
            float randomY = Random.Range(2f, 3f);
            float randomZ = Random.Range(-2f, 2f);
            Vector3 pos = new Vector3(randomX, randomY, randomZ);
            GameObject WoodObject = Instantiate(_logWoodPrefabs, this.transform.position + pos, Quaternion.identity);
            ItemPickUp item = WoodObject.GetComponent<ItemPickUp>();
            item.SetItemDrop(new ItemStack(_itemDrop, Random.Range(1, 5)));
        }
        Destroy(this.gameObject);
    }


}
