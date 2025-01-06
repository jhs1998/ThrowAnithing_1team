using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LobbyUpGradeState", menuName = "Game/LobbyUpGradeState")]
public class LobbyUpGradeState : ScriptableObject
{
    [Header("로비 강화 수치")]
    [Tooltip("근거리 공격 1")]
    public float shortRangeAttackUPO = 1;
    [Tooltip("원거리 공격 1")]
    public float longRangeAttackUPO = 1;
    [Tooltip("이동속도 증가")]
    public float movementSpeedUP = 4;
    [Tooltip("최대 체력")]
    public float maxHpUP = 6;  
    public float commonAttackUP = 2;        
    public float attackSpeedUP = 0.05f;   
    public float criticalChanceUP = 2;
    public float defenseUPO = 0.4f;
    public float equipmentDropUpgradeUPO = 2;
    public float drainLifeUP = 0.6f;
    public float maxStaminaUP = 10;
    public float regainManaUP = 0.1f;
    public float manaConsumptionUP = 0.06f;
    public float consumesStaminaUP = 6;
    public float gainMoreThrowablesUP = 20;
    public float maxThrowablesUP = 6;
    public float longRangeAttackUPT = 2;
    public float shortRangeAttackUPT = 2;
    public float defenseUPT = 0.06f;
    public float equipmentDropUpgradeUPT = 3;
}
