using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStateBase", menuName = "Game/PlayerStateBase")]
public class PlayerStateBase : ScriptableObject
{
    [Header("�÷��̾� �⺻ ����")]
    [Tooltip("�ִ� ü��")]
    public float maxHp = 60;
    [Tooltip("���� ���ݷ�")]
    public float commonAttack = 0;
    [Tooltip("�ٰŸ� ���ݷ�")]
    public float[] shortRangeAttack = new float[] { 25, 40, 60 };
    [Tooltip("���Ÿ� ���ݷ�")]    
    public float[] longRangeAttack = new float[] { 10, 40, 70, 110 };
    [Tooltip("���� �ӵ�")]
    public float attackSpeed = 1;
    [Tooltip("�̵� �ӵ�")]
    public float movementSpeed = 100;
    [Tooltip("ũ��Ƽ�� Ȯ��")]
    public float criticalChance = 10;
    [Tooltip("����")]
    public float defense = 0;
    [Tooltip("���ȹ�� Ȯ�� ����")]
    public float equipmentDropUpgrade = 0;
    [Tooltip("����� ���")]
    public float drainLife = 0;
    [Tooltip("���׹̳� �ִ�ġ")]
    public float maxStamina = 100;
    [Tooltip("���׹̳� ȸ��")]
    public float regainStamina = 20;
    [Tooltip("������ ���ݴ� ���� ȸ��")]
    public float[] regainMana = new float[] { 3, 8, 13, 20 };
    [Tooltip("���� �Ҹ� ����")]
    public float[] manaConsumption = new float[] { 30, 70, 100 };
    [Tooltip("���׹̳� �Ҹ� ����")]
    public float consumesStamina = 0;
    [Tooltip("��ô�� �߰�ȹ�� ����")]
    public float gainMoreThrowables = 0;
    [Tooltip("���� ��ô�� ����")]
    public float maxThrowables = 50;
    [Tooltip("�ִ� ����")]
    public float maxMana = 100;
    [Tooltip("�ִ� ���� Ƚ��")]
    public float maxJumpCount = 2;
    [Tooltip("������")]
    public float jumpPower = 100;
    [Tooltip("���� �Ҹ� ���׹̳�")]
    public float jumpConsumesStamina = 20;
    [Tooltip("�������� �Ҹ� ���׹̳�")]
    public float doubleJumpConsumesStamina = 10;
    [Tooltip("�뽬 �Ÿ�")]
    public float dashDistance = 200;
    [Tooltip("�뽬 �Ҹ� ���׹̳�")]
    public float dashConsumesStamina = 50;
    [Tooltip("�������� �Ҹ� ���׹̳�")]
    public float[] shortRangeAttackStamina = new float[] { 20, 30, 50 };
    [Tooltip("Ư�� ���ݷ� ��ġ")]
    public float[] specialAttack = new float[] { 75, 150, 225 };
}
