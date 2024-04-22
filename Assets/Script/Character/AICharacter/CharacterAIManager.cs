using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAIManager : CharacterManager
{
    private AIControlManager _aiControl;
    protected override void Awake()
    {
        base.Awake();
        _aiControl = GetComponent<AIControlManager>();
    }
    protected override void Start()
    {
        base.Start();
        IngnoreCollider();
    }
    public override IEnumerator ProcessDeathEvent()
    {
        _aiControl.AiMachine.ChangeState(_aiControl.DeathState);
        yield return StartCoroutine(base.ProcessDeathEvent());
        Destroy(this.gameObject);
    }


    private void IngnoreCollider()
    {
        Collider[] characterCollider = GetComponentsInChildren<Collider>();
        List<Collider> ingnoreCollider = new List<Collider>();
        Collider collider = GetComponent<Collider>();

        foreach (var collision in characterCollider)
        {
            ingnoreCollider.Add(collision);
        }
        ingnoreCollider.Add(collider);

        foreach (var collision in characterCollider)
        {
            foreach (var other in characterCollider)
            {
                Physics.IgnoreCollision(collision, other, true);
            }
        }

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
