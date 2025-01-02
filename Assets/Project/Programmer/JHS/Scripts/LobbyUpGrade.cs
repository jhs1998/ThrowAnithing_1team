using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using Zenject;
using static GlobalPlayerStateData;

public class LobbyUpGrade : MonoBehaviour
{
    // GlobalGameData의 BuyUpgradeSlot에서 참 거짓을 받아와 참일경우 에만  업그레이드 
    [Inject]
    public GlobalGameData gameData;
    [Inject]
    public GlobalPlayerStateData playerState;
    [SerializeField] SaveSystem saveSystem;

    // 코인 변경 이벤트
    public event Action OnCoinChanged;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI usingCoinText;

    private void Start()
    {
        // 초기 UI 업데이트
        UpdateCoinUI();
    }
    private void OnEnable()
    {
        // 코인 변화 이벤트에 구독
        OnCoinChanged += UpdateCoinUI;
    }
    private void OnDisable()
    {
        // 코인 변화 이벤트 구독 해제
        OnCoinChanged -= UpdateCoinUI;
    }
    private void UpdateCoinUI()
    {
        if (coinText == null || usingCoinText == null)
        {
            return;
        }
        // 현재 코인과 최대 코인 값에 따라 텍스트 업데이트
        coinText.text = "coin: " + gameData.coin.ToString();
        usingCoinText.text = "usingCoin: " + gameData.usingCoin.ToString();
        saveSystem.SavePlayerData();
        // 뉴게임이라 true일 경우 false
        gameData.bringData = false;
    }
    public void ApplyUpgradeStats()
    {
        Debug.Log("스탯 세팅 시도");
        // 업그레이드 함수 배열
        Action<int>[] upgradeMethods = new Action<int>[]
        {
        OneLine_UpgradeShortAttack, OneLine_UpgradeLongAttack, OneLine_UpgradeMovementSpeed, OneLine_UpgradeMaxHpSlot,
        TwoLine_UpgradeMaxStamina, TwoLine_UpgradeAttackSpeed, TwoLine_UpgradeCriticalChance, TwoLine_UpgradeEquipmentDrop,
        ThreeLine_UpgradeCommonAttack, ThreeLine_UpgradeMaxThrowables, ThreeLine_UpgradeDefense, ThreeLine_UpgradeRegainMana,
        FourLine_UpgradeConsumesStamina, FourLine_UpgradeLongAttack, FourLine_UpgradeShortAttack, FourLine_UpgradeGainMoreThrowables,
        FiveLine_UpgradeManaConsumption, FiveLine_UpgradeDrainLife, FiveLine_UpgradeDefense, FiveLine_UpgradeEquipmentDrop
        };

        // upgradeLevels를 통해 업그레이드 적용
        for (int i = 0; i < 20; i++)
        {
            int upgradeLevel = gameData.upgradeLevels[i];

            // 해당 업그레이드 메서드를 upgradeLevel 횟수만큼 실행
            for (int j = 0; j < upgradeLevel; j++)
            {
                upgradeMethods[i](i); // 업그레이드 함수 실행
            }
        }
        Debug.Log("스탯 세팅 완료");
        gameData.bringData = false;
    }

    // 첫번째 줄 1번 슬롯 근접 공격 강화
    public void OneLine_UpgradeShortAttack(int slot)
    {
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 근접 공격 강화
            for (int i = 0; i < 3; i++)
            {
                playerState.shortRangeAttack[i] += 1;
            }            
            Debug.Log($"근접 공격 증가");
            OnCoinChanged?.Invoke();
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
            for (int i = 0; i < 4; i++)
            {
                playerState.longRangeAttack[i] += 1;
            }
            Debug.Log($"원거리 공격 증가");
            OnCoinChanged?.Invoke();
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
            OnCoinChanged?.Invoke();
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
            OnCoinChanged?.Invoke();
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
            OnCoinChanged?.Invoke();
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
            OnCoinChanged?.Invoke();
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
            OnCoinChanged?.Invoke();
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
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    /**************************************************************/

    // 세번째 줄 1번 슬롯 공용 공격력 2증가
    public void ThreeLine_UpgradeCommonAttack(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 공용 공격력 2증가
            playerState.commonAttack += 2;
            Debug.Log($"공용 공격력: {playerState.commonAttack}");
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 세번째 줄 2번 슬롯 보유 투척물 6증가
    public void ThreeLine_UpgradeMaxThrowables(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 보유 투척물 6증가
            playerState.maxThrowables += 6;
            Debug.Log($"보유가능한 투척물: {playerState.maxThrowables}");
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 세번째 줄 3번 슬롯 방어력 0.4 증가
    public void ThreeLine_UpgradeDefense(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 방어력 0.4 증가
            playerState.defense += 0.4f;
            Debug.Log($"방어력: {playerState.defense}");
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 세번째 줄 4번 슬롯 마나 회복량 10퍼 증가
    public void ThreeLine_UpgradeRegainMana(int slot)
    {
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 마나 회복량 10퍼 증가 (힙연산)
            for (int i = 0; i < 4; i++)
            {
                playerState.regainMana[i] += playerState.regainMana[i]*0.1f;
            }
            Debug.Log($"마나 회복량 10퍼 증가");
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    /**************************************************************/

    // 네번째 줄 1번 슬롯 스테미나 소모량 6퍼 감소
    public void FourLine_UpgradeConsumesStamina(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 스테미나 소모량 6퍼 감소
            playerState.consumesStamina = 6;
            float reduction = playerState.consumesStamina / 100f;
            // 점프소모 스테미나
            playerState.jumpConsumesStamina -= playerState.jumpConsumesStamina * reduction;
            // 더블 점프 소모 스테미나
            playerState.doubleJumpConsumesStamina -= playerState.doubleJumpConsumesStamina * reduction;
            // 대쉬 소모 스테미나
            playerState.dashConsumesStamina -= playerState.dashConsumesStamina * reduction;
            // 근접 공격 스테미나
            for (int i = 0; i < playerState.shortRangeAttackStamina.Length; i++)
            {
                playerState.shortRangeAttackStamina[i] -= playerState.shortRangeAttackStamina[i] * reduction;
            }

            Debug.Log($"전체 스테미나 소모량 6퍼 감소");
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 네번째 줄 2번 슬롯 원거리 공격력 2증가
    public void FourLine_UpgradeLongAttack(int slot)
    {
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 원거리 공격 강화
            for (int i = 0; i < 4; i++)
            {
                playerState.longRangeAttack[i] += 2;
            }
            Debug.Log($"원거리 공격 2 증가");
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 네번째 줄 3번 슬롯 근거리 공격력 2증가
    public void FourLine_UpgradeShortAttack(int slot)
    {
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 근접 공격 강화
            for (int i = 0; i < 3; i++)
            {
                playerState.shortRangeAttack[i] += 2;
            }
            Debug.Log($"근접 공격 2 증가");
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 네번째 줄 4번 슬롯 투척물 추가 획득 20퍼 증가
    public void FourLine_UpgradeGainMoreThrowables(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 투척물 추가 획득 20퍼 증가
            playerState.gainMoreThrowables += 20;
            Debug.Log($"투척물 추가 획득 : {playerState.gainMoreThrowables}");
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    /**************************************************************/

    // 다섯번째 줄 1번 슬롯 마나 소모량 감소 6퍼
    public void FiveLine_UpgradeManaConsumption(int slot)
    {
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 마나 소모량 감소 6퍼
            for (int i = 0; i < 3; i++)
            {
                playerState.manaConsumption[i] -= playerState.manaConsumption[i] * 0.06f;
            }
            Debug.Log($"마나 소모량 감소 6퍼");
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 다섯번째 줄 2번 슬롯 생명력 흡수 0.6퍼 
    public void FiveLine_UpgradeDrainLife(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 생명력 흡수 0.6퍼 
            playerState.drainLife += 0.6f;
            Debug.Log($"생명력 흡수 : {playerState.drainLife}");
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 다섯번째 줄 3번 슬롯 방어력 0.6 증가
    public void FiveLine_UpgradeDefense(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 방어력 0.6 증가
            playerState.defense += 0.6f;
            Debug.Log($"방어력: {playerState.defense}");
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }
    // 다섯번째 줄 4번 슬롯 장비 획득 확률 3퍼 증가
    public void FiveLine_UpgradeEquipmentDrop(int slot)
    {
        // 슬롯 강화 시도
        if (gameData.BuyUpgradeSlot(slot))
        {
            // 강화 성공 시 추가 장비 획득 확률 3퍼 증가
            playerState.equipmentDropUpgrade += 3;
            Debug.Log($"추가 장비 획득 확률 : {playerState.equipmentDropUpgrade}");
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("강화 실패");
        }
    }

    // 개발자 모드 로비 재화 Max
    public void GetCoinMax()
    {
        // 코인을 최대치로 변경
        gameData.coin = gameData.maxCoin;
        OnCoinChanged?.Invoke();
    }

    //  로비 재화 리셋
    public void ResetCoin()
    {
        gameData.coin = 0;
        OnCoinChanged?.Invoke();
    }

}
