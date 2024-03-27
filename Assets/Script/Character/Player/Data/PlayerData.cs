using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player/PlayerBaseData")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    [Range(1, 20)] public float walkSpeed = 4f;
    [Range(1, 20)] public float sprintingSpeed = 2f;
    [Range(1, 5)] public int SprintingCost = 5;


    [Header("Jump State")]
    [Range(1f, 10f)] public float jumpHeight = 2f;
    [Range(10, 20)] public int JumpCost = 20;

    [Header("Cround State")]
    [Range(1f, 10f)] public float croundSpeed = 2f;


    [Header("Check Variable")]
    [Range(0.01f, 0.3f)] public float groundCheckRadius = 0.1f;
    public LayerMask whatIsGrounded;

    [Header("Attack state")]
    [Range(15, 20)] public int AttackCost = 15;

}
