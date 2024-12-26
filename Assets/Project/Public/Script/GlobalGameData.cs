using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �۷ι� ���� ������
/// �����: ������
/// ��� �� ��� �����ÿ�
/// </summary>

[System.Serializable]
public partial class GlobalGameData
{   
    // ���� ��ȭ
    public int coin;    
    // ��¥�� �ð�
    public string saveDateTime;
    // �κ� Ư�� ��ȭ ����
    public int[] upgradeLevels = new int[20];
    // ��ȭ ��� 
    public int[] upgradeCosts = { 1000, 1000, 1000, 1000,
                                  5000, 5000, 5000, 5000,
                                  10000, 10000, 10000, 10000,
                                  30000, 30000, 30000, 30000,
                                  50000, 50000, 50000, 50000};

    // �κ� ���� Ư�� ��ȭ ����
    public bool BuyUpgradeSlot(int slot)
    {
        // ���� 20���� ���� �� ���� Ȯ��
        if (slot < 0 || slot >= upgradeLevels.Length)
        {
            Debug.Log("�߸��� ���� ��ȣ�Դϴ�.");
            return false;
        }

        // ��ȭ �ܰ� Ȯ��
        int currentLevel = upgradeLevels[slot];
        if (currentLevel >= 5)
        {
            Debug.Log("�̹� �ִ� �ܰ迡 �����߽��ϴ�.");
            return false;
        }

        // ��ȭ ��� üũ
        int cost = upgradeCosts[currentLevel];
        if (coin < cost)
        {
            Debug.Log("������ �����մϴ�.");
            return false;
        }

        // ��ȭ ����
        coin -= cost;
        upgradeLevels[slot]++;
        Debug.Log($"��ȭ �Ϸ�: �׸� {slot + 1}, ���� �ܰ�: {upgradeLevels[slot]}");

        return true;
    }
}

