using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    [Header("Stats base")]
    [SerializeField] protected StatsModifield _maxHp;

    [Header("Stats Value")]
    public ObserverValue<float> CurrentHealth;

    protected virtual void Awake() { }

    protected virtual void Update() { }

    protected virtual void Start() { }
}
