using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �۷ι� ���� ������
/// �����: ������
/// ��� �� ��� �����ÿ�
/// </summary>
//public enum AmWeapon { Balance, Power, Speed }
[System.Serializable]
public partial class GlobalGameData
{
    // ���� ��ȭ
    public int coin;
    // �ִ� ���� ��ȭ
    public int maxCoin = 9999999;
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
    public int usingCoin;
    public bool bringData = true;

    public enum AmWeapon { Balance, Power, Speed }
    public AmWeapon nowWeapon;
    // �κ� ���� Ư�� ��ȭ ����
    public bool BuyUpgradeSlot(int slot)
    {
        if (!bringData)
        {
            // ���� 20���� ���� �� ���� Ȯ��
            if (slot < 0 || slot >= upgradeLevels.Length)
            {
                Debug.Log("�߸��� ���� ��ȣ�Դϴ�.");
                return false;
            }

            // ��ȭ �ܰ� Ȯ��
            int currentLevel = upgradeLevels[slot];
            if (currentLevel > 4)
            {
                Debug.Log("�̹� �ִ� �ܰ迡 �����߽��ϴ�.");
                return false;
            }

            // ��ȭ ��� üũ
            int cost = upgradeCosts[slot];
            if (coin < cost)
            {
                Debug.Log("������ �����մϴ�.");
                return false;
            }

            // ��ȭ ����
            coin -= cost;
            usingCoin += cost;
            upgradeLevels[slot]++;
            Debug.Log($"��ȭ �Ϸ�: �׸� {slot + 1}, ���� �ܰ�: {upgradeLevels[slot]}");
        }
        return true;
    }

    // ���� ���� �Լ� (���� �� GetCoin(���μ�) �����ؾߵ�)
    public void GetCoin(int getcoin)
    {
        coin += getcoin;
        if (coin > maxCoin)
        {
            coin = maxCoin;  // �ִ밪�� �ʰ��ϸ� �ִ밪���� ����
        }
        Debug.Log($"{getcoin}Coin ���� \n �� Coin �� : {coin}");
    }
}


