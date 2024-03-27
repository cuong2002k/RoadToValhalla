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
    public override IEnumerator ProcessDeathEvent()
    {
        _aiControl.AiMachine.ChangeState(_aiControl.DeathState);
        yield return StartCoroutine(base.ProcessDeathEvent());
        Destroy(this.gameObject);
    }
}
