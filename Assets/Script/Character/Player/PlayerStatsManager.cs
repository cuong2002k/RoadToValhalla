using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : CharacterStatsManager
{

    private PlayerHudManger _playerHubManager;
    private EquipmentManager _equipmentMananger;
    private PlayerManager _playerManager;
    [SerializeField] protected StatsModifield _damage;
    [SerializeField] protected StatsModifield _maxStamina;
    public ObserverValue<float> CurrentStamina;

    [Header("Stamina Regenerator")]
    private float _staminaCostRegerator = 2f;
    private float _staminaRegeneratorDelay = 2f;
    private float _staminaRegeneratorTimer = 0;
    private float _staminaTick = 0;

    protected override void Awake()
    {
        base.Awake();
        _equipmentMananger = GetComponent<EquipmentManager>();
        _playerManager = GetComponent<PlayerManager>();
    }

    protected override void Start()
    {
        base.Start();
        _playerHubManager = PlayerUIManager.Instance.PlayerHubManager;

        _playerHubManager.SetMaxStatsBar(_maxStamina.GetStatsValue());
        CurrentStamina.Set(_maxStamina.GetStatsValue());
        CurrentStamina.OnchangeValue += _playerHubManager.SetValueStatsBar;

        _playerHubManager.SetMaxHealthBar(_maxHp.GetStatsValue());
        CurrentHealth.Set(_maxHp.GetStatsValue());
        CurrentHealth.OnchangeValue += _playerHubManager.SetValueHealthBar;
        CurrentHealth.OnchangeValue += CheckHeath;
    }


    private void OnEnable()
    {
        _equipmentMananger.OnChangeEquipmentItem += UpdateModified;
    }

    private void OnDisable()
    {
        _equipmentMananger.OnChangeEquipmentItem -= UpdateModified;
        CurrentHealth.OnchangeValue -= _playerHubManager.SetValueHealthBar;
        CurrentStamina.OnchangeValue -= _playerHubManager.SetValueStatsBar;
        CurrentHealth.OnchangeValue -= CheckHeath;
    }

    private void UpdateModified(EquipmentItem newItem, EquipmentItem oldItem)
    {
        if (newItem != null)
        {
            _damage.AddModified(newItem.GetAttackModified());
            _defense.AddModified(newItem.GetArmorModifier());
        }

        if (oldItem != null)
        {
            _damage.RemoveModified(oldItem.GetAttackModified());
            _defense.RemoveModified(oldItem.GetArmorModifier());
        }

        // Debug.Log(_damage.GetStatsValue() + " " + _defense.GetStatsValue());
    }

    protected override void Update()
    {
        base.Update();
        RegeneratorStatmina();

    }

    private void RegeneratorStatmina()
    {
        if (InputManager.Instance.ShiftInput) return;
        if (InputManager.Instance.AttackInput) return;
        if (InputManager.Instance.JumpInput) return;

        _staminaRegeneratorTimer += Time.deltaTime;
        if (_staminaRegeneratorTimer >= _staminaRegeneratorDelay)
        {
            if (this.CurrentStamina.Value < _maxStamina.GetStatsValue())
            {
                _staminaTick += Time.deltaTime;
                if (_staminaTick >= 0.3f)
                {
                    _staminaTick = 0f;
                    this.CurrentStamina.Value += _staminaCostRegerator;
                }
            }
        }

    }

    public void ResetRegeneratorStaminaTimer()
    {
        _staminaRegeneratorTimer = 0;
    }

    public void CheckHeath(float Value)
    {
        if (Value <= 0)
        {
            StartCoroutine(_playerManager.ProcessDeathEvent());
        }
    }

    public void RestartStats()
    {
        CurrentStamina.Set(_maxStamina.GetStatsValue());
        CurrentHealth.Set(_maxHp.GetStatsValue());
    }
}
