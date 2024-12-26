using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LobbyUpGrade : MonoBehaviour
{
    // GlobalGameData의 BuyUpgradeSlot에서 참 거짓을 받아와 참일경우 에만  업그레이드 
    [Inject]
    public GlobalGameData gameData;
    [Inject]
    public GlobalPlayerStateData playerState;

    // 첫번째 줄 1번째 슬롯 근접 공격 강화
    public void OneLine_UpgradeShortAttack(int slot)
    {
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 근접 공격 강화
            for (int i = 0; i < 3;)
            {
                playerState.shortRangeAttack[i] += 1;
            }            
            Debug.Log($"근접 공격 증가");
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 첫번째 줄 2번째 슬롯 원거리 공격 강화
    public void OneLine_UpgradeLongAttack(int slot)
    {
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 원거리 공격 강화
            for (int i = 0; i < 4;)
            {
                playerState.longRangeAttack[i] += 1;
            }
            Debug.Log($"원거리 공격 증가");
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 첫번째 줄 3번째 슬롯 이동 속도 강화 
    public void OneLine_UpgradeMovementSpeed(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 이동 속도 증가
            playerState.movementSpeed *= 1.04f;
            Debug.Log($"이동 속도 증가: {playerState.movementSpeed}");
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 첫번째 줄 4번째 슬롯 최대 체력 강화
    public void OneLine_UpgradeMaxHpSlot(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 최대 체력 증가
            playerState.maxHp += 6;
            Debug.Log($"최대 체력 증가: {playerState.maxHp}");
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }

    /**************************************************************/

    
}
