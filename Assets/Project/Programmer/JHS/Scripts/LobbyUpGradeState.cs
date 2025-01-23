using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LobbyUpGradeState", menuName = "Game/LobbyUpGradeState")]
public class LobbyUpGradeState : ScriptableObject
{
    [Header("�κ� ��ȭ ��ġ")]
    [Tooltip("�ٰŸ� ���� 1")]
    public float shortRangeAttackUPO = 1;
    [Tooltip("���Ÿ� ���� 1")]
    public float longRangeAttackUPO = 1;
    [Tooltip("�̵��ӵ� ����")]
    public float movementSpeedUP = 4;
    [Tooltip("�ִ� ü��")]
    public float maxHpUP = 6;
    [Tooltip("���׹̳� �ִ�ġ")]
    public float maxStaminaUP = 10;
    [Tooltip("���� �ӵ�")]
    public float attackSpeedUP = 5;
    [Tooltip("ũ��Ƽ�� Ȯ��")]
    public float criticalChanceUP = 2;
    [Tooltip("���ȹ�� Ȯ�� ���� 1")]
    public float equipmentDropUpgradeUPO = 2;
    [Tooltip("���� ���ݷ�")]
    public float commonAttackUP = 2;
    [Tooltip("���� ��ô�� ����")]
    public float maxThrowablesUP = 6;
    [Tooltip("���� 1")]
    public float defenseUPO = 0.4f;
    [Tooltip("����ȸ���� ����")]
    public float regainManaUP = 10;
    [Tooltip("���׹̳� �Ҹ� ����")]
    public float consumesStaminaUP = 6;
    [Tooltip("���Ÿ� ���� 2")]
    public float longRangeAttackUPT = 2;
    [Tooltip("�ٰŸ� ���� 2")]
    public float shortRangeAttackUPT = 2;
    [Tooltip("��ô�� �߰�ȹ�� Ȯ��")]
    public float gainMoreThrowablesUP = 20;
    [Tooltip("���� �Ҹ� ����")]
    public float manaConsumptionUP = 6;
    [Tooltip("����� ���")]
    public float drainLifeUP = 0.6f;
    [Tooltip("���� 2")]
    public float defenseUPT = 0.6f;
    [Tooltip("���ȹ�� Ȯ�� ���� 2")]
    public float equipmentDropUpgradeUPT = 3;
}
