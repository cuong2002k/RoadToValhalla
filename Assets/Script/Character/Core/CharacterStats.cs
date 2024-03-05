using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private PlayerHudManger _playerHubManager;
    private EquipmentManager _equipmentMananger;

    [SerializeField] StatsModifield _maxHp;
    [SerializeField] StatsModifield _maxStamina;
    [SerializeField] StatsModifield _damage;
    [SerializeField] StatsModifield _defense;


    public ObserverValue<float> CurrentStamina;
    private float _staminaCostRegerator = 2f;
    private float _staminaRegeneratorDelay = 2f;
    private float _staminaRegeneratorTimer = 0;
    private float _staminaTick = 0;

    private void Awake()
    {
        _equipmentMananger = GetComponent<EquipmentManager>();
    }

    private void Start()
    {
        _playerHubManager = PlayerUIManager.Instance.PlayerHubManager;

        _playerHubManager.SetMaxStatsBar(_maxStamina.GetStatsValue());
        CurrentStamina.Set(_maxStamina.GetStatsValue());
        CurrentStamina.OnchangeValue += _playerHubManager.SetValueStatsBar;
    }


    private void OnEnable()
    {
        _equipmentMananger.OnChangeEquipmentItem += UpdateModified;
    }

    private void OnDisable()
    {
        _equipmentMananger.OnChangeEquipmentItem -= UpdateModified;
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

    private void Update()
    {
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

}
