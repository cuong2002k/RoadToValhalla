using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAIManager : CharacterManager
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
    }

    public override void TakeDamage(WeaponConfig weaponConfig, int physicDame, Vector3 contactPoint)
    {
        base.TakeDamage(weaponConfig, physicDame, contactPoint);
        TakeDamageEffect.targetPoint = contactPoint;
        TakeDamageEffect.PhysicsDamage = physicDame;
        TakeDamageEffect takeDamageEffect = Instantiate(TakeDamageEffect) as TakeDamageEffect;
        CharacterEffectManager.ProcessInstanceEffect(takeDamageEffect);
    }

    public override IEnumerator ProcessDeathEvent()
    {
        _aiControl.AiMachine.ChangeState(_aiControl.DeathState);
        yield return StartCoroutine(base.ProcessDeathEvent());
        PlayerUIManager.Instance.PlayerPopUpManager.ShowWinPopUp();
        // GameManager.Instance.LoadSceneWin();
        Destroy(this.gameObject);
    }
}
