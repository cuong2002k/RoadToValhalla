using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDameManager : MonoBehaviour
{
    public Collider _collider;
    public InstanceEffects takeDamage;
    public int PhysicsDamage = 10;
    public void OpenRightCollider()
    {
        _collider.enabled = true;
        Collider[] colliders = Physics.OverlapBox(_collider.transform.position, (_collider as BoxCollider).size);
        foreach (Collider dameCollider in colliders)
        {
            PlayerManager characterManager = dameCollider.gameObject.GetComponent<PlayerManager>();
            if (characterManager != null)
            {
                if (takeDamage == null) return;
                TakeDamageEffect takeDamageEffect = Instantiate(takeDamage) as TakeDamageEffect;
                takeDamageEffect.PhysicsDamage = PhysicsDamage;
                takeDamageEffect.targetPoint = dameCollider.ClosestPoint(_collider.transform.position);
                characterManager.PlayerEffectManager.ProcessInstanceEffect(takeDamageEffect);
            }
        }
    }

    public void DisableRightCollider()
    {
        _collider.enabled = true;
    }
}
