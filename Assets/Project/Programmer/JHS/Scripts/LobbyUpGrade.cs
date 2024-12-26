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

    // 첫번째 줄 1번 슬롯 근접 공격 강화
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
    // 첫번째 줄 2번 슬롯 원거리 공격 강화
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
    // 첫번째 줄 3번 슬롯 이동 속도 강화 
    public void OneLine_UpgradeMovementSpeed(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 이동 속도 증가
            playerState.movementSpeed += 4;
            Debug.Log($"이동 속도: {playerState.movementSpeed}");
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 첫번째 줄 4번 슬롯 최대 체력 강화
    public void OneLine_UpgradeMaxHpSlot(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 최대 체력 증가
            playerState.maxHp += 6;
            Debug.Log($"최대 체력: {playerState.maxHp}");
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }

    /**************************************************************/

    // 두번째 줄 1번 슬롯 스테미나 최대치 증가
    public void TwoLine_UpgradeMaxStamina(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 스테미나 최대치 증가
            playerState.maxStamina += 10;
            Debug.Log($"스테미나 최대치 : {playerState.maxStamina}");
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 두번째 줄 2번 슬롯 공격 속도 5퍼 증가
    public void TwoLine_UpgradeAttackSpeed(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 공격 속도 5퍼 증가
            playerState.attackSpeed += 0.05f;
            Debug.Log($"공격 속도: {playerState.attackSpeed}");
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 두번째 줄 3번 슬롯 크리티컬 확률 2 증가
    public void TwoLine_UpgradeCriticalChance(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 크리티컬 확률 2 증가
            playerState.criticalChance += 2;
            Debug.Log($"크리티컬 확률 : {playerState.criticalChance}");
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 두번째 줄 4번 슬롯 추가 장비 획득 확률 2퍼 증가
    public void TwoLine_UpgradeEquipmentDrop(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 추가 장비 획득 확률 2퍼 증가
            playerState.equipmentDropUpgrade += 2;
            Debug.Log($"추가 장비 획득 확률 : {playerState.equipmentDropUpgrade}");
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    /**************************************************************/

    // 세번째 줄 1번 슬롯 공용 공격력 2증가
    public void threeLine_UpgradeCommonAttack(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 공용 공격력 2증가
            playerState.commonAttack += 2;
            Debug.Log($"공용 공격력: {playerState.commonAttack}");
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 세번째 줄 2번 슬롯 보유 투척물 6증가
    public void threeLine_UpgradeMaxThrowables(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 보유 투척물 6증가
            playerState.maxThrowables += 6;
            Debug.Log($"보유가능한 투척물: {playerState.maxThrowables}");
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 세번째 줄 3번 슬롯 방어력 0.4 증가
    public void threeLine_UpgradeDefense(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 방어력 0.4 증가
            playerState.defense += 0.4f;
            Debug.Log($"방어력: {playerState.defense}");
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 세번째 줄 4번 슬롯 마나 회복량 10퍼 증가
    public void threeLine_UpgradeRegainMana(int slot)
    {
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 마나 회복량 10퍼 증가 (힙연산)
            for (int i = 0; i < 4;)
            {
                playerState.regainMana[i] += playerState.regainMana[i]*0.1f;
            }
            Debug.Log($"마나 회복량 10퍼 증가");
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    /**************************************************************/

    // 네번째 줄 1번 슬롯 스테미나 소모량 6퍼 감소
    // 네번째 줄 2번 슬롯 원거리 공격력 2증가
    // 네번째 줄 3번 슬롯 근거리 공격력 2증가
    // 네번째 줄 4번 슬롯 투척물 추가 획득 20퍼 증가

    /**************************************************************/

    // 다섯번째 줄 1번 슬롯 마나 소모량 감소 6퍼
    // 다섯번째 줄 2번 슬롯 생명력 흡수 0.6퍼 
    // 다섯번째 줄 3번 슬롯 방어력 0.6 증가
    // 다섯번째 줄 4번 슬롯 장비 획득 확률 3퍼 증가
}
