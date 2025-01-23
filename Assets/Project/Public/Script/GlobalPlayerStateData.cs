using System;
using System.Collections.Generic;
using UnityEngine.Playables;
using Zenject;

/// <summary>
/// �۷ι� �÷��̾� ���� ������
/// �����: ������
/// ��� �� ��� �����ÿ�
/// </summary>
[System.Serializable]
public class GlobalPlayerStateData
{
    [Inject]
    GlobalGameData gameData;

    // ScriptableObject ��������
    public PlayerStateBase playerStateBase;
    // GlobalGameData�� �κ� Ư�� ��ȭ �ܰ� Ȯ�� �� �÷��̾��� ���� ����
    // ���� json���� ������ �ʿ� ���� ���׷��̵� �ܰ踦 �����Ͽ� �׿� ���� ���� ����
    // �κ� ���׷��̵� ������ ���� ����

    // �κ񿡼� ���۵Ǵ� �÷��̾� ����
    // �ִ� ü�� �⺻ : 60
    public float maxHp;
    // ���� ���ݷ� �⺻ : 0
    public float commonAttack;
    // �ٰŸ� ���ݷ� �⺻ : 1Ÿ 25 2Ÿ 40 3Ÿ 60
    public float[] shortRangeAttack = new float[3];
    // ���Ÿ� ���ݷ� �⺻ : 1Ÿ 10 2Ÿ 40 3Ÿ 70 4Ÿ 110
    public float[] longRangeAttack = new float[4];
    // ���� �ӵ� �⺻ : 1
    public float attackSpeed;
    // �̵� �ӵ� �⺻ : 100
    public float movementSpeed;
    // ũ��Ƽ�� Ȯ�� �⺻ : 10
    public float criticalChance;
    // ���� �⺻ : 0
    public float defense;
    // ��� ȹ�� Ȯ�� ���� �⺻ 0 ���� �ϰ� ����� Ȯ���� �÷���
    public float equipmentDropUpgrade;
    // ����� ��� �⺻ 0
    public float drainLife;
    // ���׹̳� �ִ�ġ �⺻ : 100
    public float maxStamina;
    // ���׹̳� ȸ�� �⺻ : 20
    public float regainStamina;
    // ������ ���ݴ� ���� ȸ�� �⺻ : 1Ÿ 3 2Ÿ 8 3Ÿ 13 4Ÿ 20
    public float[] regainMana = new float[4];
    // ���� �Ҹ� ���� �⺻ : 1Ÿ 30 2Ÿ 70 3Ÿ 100
    public float[] manaConsumption = new float[3];
    // ���׹̳� �Ҹ� ���� �⺻ : 0 ��� ���׹̳� �Ҹ� ����
    public float consumesStamina;
    // ��ô�� �߰� ȹ�� Ȯ�� ���� �⺻ : 0
    public float gainMoreThrowables;
    // ���� ��ô�� ���� / �⺻ 50
    public float maxThrowables;

    // �κ񿡼� ���۵��� �ʴ� �÷��̾� ����

    // �޴� ���� ���� (Ȯ�� �ƴ�)
    // �ִ� ���� �⺻ : 100
    public float maxMana;
    // �ִ� ���� Ƚ�� �⺻ : 2
    public float maxJumpCount;
    // ������ �⺻ : 100
    public float jumpPower;
    // ���� �Ҹ� ���׹̳� �⺻ : 20
    public float jumpConsumesStamina;
    // ���� ���� �Ҹ� ���׹̳� �⺻ : 10
    public float doubleJumpConsumesStamina;
    // �뽬 �Ÿ� �⺻ : 200
    public float dashDistance;
    // �뽬 �Ҹ� ���׹̳� �⺻ : 50
    public float dashConsumesStamina;
    // ���� ���� ���׹̳� �⺻ : 1Ÿ 20 2Ÿ 30 3Ÿ 50
    public float[] shortRangeAttackStamina = new float[3];
    // Ư�� ���ݷ� ��ġ �⺻ : 1Ÿ 75 2Ÿ 150 3Ÿ 225
    public float[] specialAttack = new float[3];

    public void NewPlayerSetting()
    {
        // ������ ScriptableObject�� ������ ���� ����
        maxHp = playerStateBase.maxHp;
        commonAttack = playerStateBase.commonAttack;
        for (int i = 0; i < shortRangeAttack.Length; i++)
        {
            shortRangeAttack[i] = playerStateBase.shortRangeAttack[i];
        }
        for (int i = 0; i < longRangeAttack.Length; i++)
        {
            longRangeAttack[i] = playerStateBase.longRangeAttack[i];
        }
        attackSpeed = playerStateBase.attackSpeed;
        movementSpeed = playerStateBase.movementSpeed;
        criticalChance = playerStateBase.criticalChance;
        defense = playerStateBase.defense;
        equipmentDropUpgrade = playerStateBase.equipmentDropUpgrade;
        drainLife = playerStateBase.drainLife;
        maxStamina = playerStateBase.maxStamina;
        regainStamina = playerStateBase.regainStamina;
        for (int i = 0; i < regainMana.Length; i++)
        {
            regainMana[i] = playerStateBase.regainMana[i];
        }
        for (int i = 0; i < manaConsumption.Length; i++)
        {
            manaConsumption[i] = playerStateBase.manaConsumption[i];
        }
        consumesStamina = playerStateBase.consumesStamina;
        gainMoreThrowables = playerStateBase.gainMoreThrowables;
        maxThrowables = playerStateBase.maxThrowables;
        maxMana = playerStateBase.maxMana;
        maxJumpCount = playerStateBase.maxJumpCount;
        jumpPower = playerStateBase.jumpPower;
        jumpConsumesStamina = playerStateBase.jumpConsumesStamina;
        doubleJumpConsumesStamina = playerStateBase.doubleJumpConsumesStamina;
        dashDistance = playerStateBase.dashDistance;
        dashConsumesStamina = playerStateBase.dashConsumesStamina;
        for (int i = 0; i < shortRangeAttackStamina.Length; i++)
        {
            shortRangeAttackStamina[i] = playerStateBase.shortRangeAttackStamina[i];
        }
        for (int i = 0; i < specialAttack.Length; i++)
        {
            specialAttack[i] = playerStateBase.specialAttack[i];
        }
    }
}
