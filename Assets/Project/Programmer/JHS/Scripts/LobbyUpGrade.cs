using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUpGrade : MonoBehaviour
{
    // GlobalGameData의 BuyUpgradeSlot에서 참 거짓을 받아와 참일경우 에만  업그레이드 
    /*public void IncreaseStat(string stat, int amount)
    {
        switch (stat)
        {
            case "HP":
                UserDataManager.instance.nowPlayer.maxHp += amount;
                break;
            case "Speed":
                UserDataManager.instance.nowPlayer.speed += amount;
                break;
            case "Attack":
                UserDataManager.instance.nowPlayer.attackDamage += amount;
                break;
            case "Luck":
                UserDataManager.instance.nowPlayer.luck += amount;
                break;
            case "Coin":
                UserDataManager.instance.nowPlayer.coin += amount;
                break;
            default:
                Debug.LogWarning("Invalid stat type.");
                break;
        }
        UpdateSlotUI.instance.UpdateUI();
    }

    public void HpUpgrade(int amount)
    {
        // 지불금
        int payment = 100;
        if (UserDataManager.instance.nowPlayer.maxHp >= payment)
        {
            UserDataManager.instance.nowPlayer.coin -= payment;
            UserDataManager.instance.nowPlayer.maxHp += amount;
            UpdateSlotUI.instance.UpdateUI();
        }
        else
        {
            Debug.Log("coin 부족");
        }
    }
    public void SpeedUpgrade(int amount)
    {
        // 지불금
        int payment = 100;
        if (UserDataManager.instance.nowPlayer.coin >= payment)
        {
            UserDataManager.instance.nowPlayer.coin -= payment;
            UserDataManager.instance.nowPlayer.speed += amount;
            UpdateSlotUI.instance.UpdateUI();
        }
        else
        {
            Debug.Log("coin 부족");
        }
    }
    public void CoinUpgrade(int amount)
    {
        UserDataManager.instance.nowPlayer.coin += amount;
        UpdateSlotUI.instance.UpdateUI();
    }*/
}
