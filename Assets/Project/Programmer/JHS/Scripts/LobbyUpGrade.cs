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
    // GlobalGameData�� BuyUpgradeSlot���� �� ������ �޾ƿ� ���ϰ�� ����  ���׷��̵� 
    [Inject]
    public GlobalGameData gameData;
    [Inject]
    public GlobalPlayerStateData playerState;
    [Inject]
    public PlayerData player;
    [SerializeField] SaveSystem saveSystem;

    // ���� ���� �̺�Ʈ
    public event Action OnCoinChanged;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI usingCoinText;

    // ScriptableObject ��������
    [Inject]
    public LobbyUpGradeState lobbyUpGradeState;

    private void Start()
    {
        // �ʱ� UI ������Ʈ
        UpdateCoinUI();      
    }
    private void OnEnable()
    {
        // ���� ��ȭ �̺�Ʈ�� ����
        OnCoinChanged += UpdateCoinUI;
    }
    private void OnDisable()
    {
        // ���� ��ȭ �̺�Ʈ ���� ����
        OnCoinChanged -= UpdateCoinUI;
    }
    private void UpdateCoinUI()
    {
        if (coinText == null || usingCoinText == null)
        {
            return;
        }
        // ���� ���ΰ� �ִ� ���� ���� ���� �ؽ�Ʈ ������Ʈ
        coinText.text = "coin: " + gameData.coin.ToString();
        usingCoinText.text = "usingCoin: " + gameData.usingCoin.ToString();
        player.CopyGlobalPlayerData(playerState, gameData);
        saveSystem.SavePlayerData();
        // �������̶� true�� ��� false
        gameData.bringData = false;
    }
    public void ApplyUpgradeStats()
    {
        Debug.Log("���� ���� �õ�");
        // ���׷��̵� �Լ� �迭
        Action<int>[] upgradeMethods = new Action<int>[]
        {
        OneLine_UpgradeShortAttack, OneLine_UpgradeLongAttack, OneLine_UpgradeMovementSpeed, OneLine_UpgradeMaxHpSlot,
        TwoLine_UpgradeMaxStamina, TwoLine_UpgradeAttackSpeed, TwoLine_UpgradeCriticalChance, TwoLine_UpgradeEquipmentDrop,
        ThreeLine_UpgradeCommonAttack, ThreeLine_UpgradeMaxThrowables, ThreeLine_UpgradeDefense, ThreeLine_UpgradeRegainMana,
        FourLine_UpgradeConsumesStamina, FourLine_UpgradeLongAttack, FourLine_UpgradeShortAttack, FourLine_UpgradeGainMoreThrowables,
        FiveLine_UpgradeManaConsumption, FiveLine_UpgradeDrainLife, FiveLine_UpgradeDefense, FiveLine_UpgradeEquipmentDrop
        };

        // upgradeLevels�� ���� ���׷��̵� ����
        for (int i = 0; i < 20; i++)
        {
            int upgradeLevel = gameData.upgradeLevels[i];

            // �ش� ���׷��̵� �޼��带 upgradeLevel Ƚ����ŭ ����
            for (int j = 0; j < upgradeLevel; j++)
            {
                upgradeMethods[i](i); // ���׷��̵� �Լ� ����
            }
        }
        player.CopyGlobalPlayerData(playerState, gameData);
        Debug.Log("���� ���� �Ϸ�");
        gameData.bringData = false;
    }

    // ù��° �� 1�� ���� ���� ���� ��ȭ
    public void OneLine_UpgradeShortAttack(int slot)
    {
        if (gameData == null)
        {
            Debug.LogError("gameData is null");
            return;
        }
        if (playerState == null)
        {
            Debug.LogError("playerState is null");
            return;
        }
        if (lobbyUpGradeState == null)
        {
            Debug.LogError("lobbyUpGradeState is null");
            return;
        }
        if (playerState.shortRangeAttack == null || playerState.shortRangeAttack.Length < 3)
        {
            Debug.LogError("playerState.shortRangeAttack is not properly initialized or too small");
            return;
        }
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���� ���� ��ȭ
            for (int i = 0; i < 3; i++)
            {
                playerState.shortRangeAttack[i] += lobbyUpGradeState.shortRangeAttackUPO;
            }                    
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // ù��° �� 2�� ���� ���Ÿ� ���� ��ȭ
    public void OneLine_UpgradeLongAttack(int slot)
    {
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���Ÿ� ���� ��ȭ
            for (int i = 0; i < 4; i++)
            {
                playerState.longRangeAttack[i] += lobbyUpGradeState.longRangeAttackUPO;
            }       
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // ù��° �� 3�� ���� �̵� �ӵ� ��ȭ 
    public void OneLine_UpgradeMovementSpeed(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� �̵� �ӵ� ����
            playerState.movementSpeed += lobbyUpGradeState.movementSpeedUP;
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // ù��° �� 4�� ���� �ִ� ü�� ��ȭ
    public void OneLine_UpgradeMaxHpSlot(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� �ִ� ü�� ����
            playerState.maxHp += lobbyUpGradeState.maxHpUP;
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }

    /**************************************************************/

    // �ι�° �� 1�� ���� ���׹̳� �ִ�ġ ����
    public void TwoLine_UpgradeMaxStamina(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���׹̳� �ִ�ġ ����
            playerState.maxStamina += lobbyUpGradeState.maxStaminaUP;
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // �ι�° �� 2�� ���� ���� �ӵ� 5�� ����
    public void TwoLine_UpgradeAttackSpeed(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���� �ӵ� 5�� ����
            playerState.attackSpeed += (0.01f * lobbyUpGradeState.attackSpeedUP);
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // �ι�° �� 3�� ���� ũ��Ƽ�� Ȯ�� 2 ����
    public void TwoLine_UpgradeCriticalChance(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ũ��Ƽ�� Ȯ�� 2 ����
            playerState.criticalChance += lobbyUpGradeState.criticalChanceUP;
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // �ι�° �� 4�� ���� �߰� ��� ȹ�� Ȯ�� 2�� ����
    public void TwoLine_UpgradeEquipmentDrop(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� �߰� ��� ȹ�� Ȯ�� 2�� ����
            playerState.equipmentDropUpgrade += lobbyUpGradeState.equipmentDropUpgradeUPO;
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    /**************************************************************/

    // ����° �� 1�� ���� ���� ���ݷ� 2����
    public void ThreeLine_UpgradeCommonAttack(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���� ���ݷ� 2����
            playerState.commonAttack += lobbyUpGradeState.commonAttackUP;
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // ����° �� 2�� ���� ���� ��ô�� 6����
    public void ThreeLine_UpgradeMaxThrowables(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���� ��ô�� 6����
            playerState.maxThrowables += lobbyUpGradeState.maxThrowablesUP;
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // ����° �� 3�� ���� ���� 0.4 ����
    public void ThreeLine_UpgradeDefense(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���� 0.4 ����
            playerState.defense += lobbyUpGradeState.defenseUPO;
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // ����° �� 4�� ���� ���� ȸ���� 10�� ����
    public void ThreeLine_UpgradeRegainMana(int slot)
    {
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���� ȸ���� 10�� ���� (������)
            for (int i = 0; i < 4; i++)
            {
                playerState.regainMana[i] += playerState.regainMana[i]* (0.01f * lobbyUpGradeState.regainManaUP);
            }
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    /**************************************************************/

    // �׹�° �� 1�� ���� ���׹̳� �Ҹ� 6�� ����
    public void FourLine_UpgradeConsumesStamina(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���׹̳� �Ҹ� 6�� ����
            playerState.consumesStamina = lobbyUpGradeState.consumesStaminaUP;
            float reduction = playerState.consumesStamina / 100f;
            // �����Ҹ� ���׹̳�
            playerState.jumpConsumesStamina -= playerState.jumpConsumesStamina * reduction;
            // ���� ���� �Ҹ� ���׹̳�
            playerState.doubleJumpConsumesStamina -= playerState.doubleJumpConsumesStamina * reduction;
            // �뽬 �Ҹ� ���׹̳�
            playerState.dashConsumesStamina -= playerState.dashConsumesStamina * reduction;
            // ���� ���� ���׹̳�
            for (int i = 0; i < playerState.shortRangeAttackStamina.Length; i++)
            {
                playerState.shortRangeAttackStamina[i] -= playerState.shortRangeAttackStamina[i] * reduction;
            }
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // �׹�° �� 2�� ���� ���Ÿ� ���ݷ� 2����
    public void FourLine_UpgradeLongAttack(int slot)
    {
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���Ÿ� ���� ��ȭ
            for (int i = 0; i < 4; i++)
            {
                playerState.longRangeAttack[i] += lobbyUpGradeState.longRangeAttackUPT;
            }
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // �׹�° �� 3�� ���� �ٰŸ� ���ݷ� 2����
    public void FourLine_UpgradeShortAttack(int slot)
    {
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���� ���� ��ȭ
            for (int i = 0; i < 3; i++)
            {
                playerState.shortRangeAttack[i] += lobbyUpGradeState.shortRangeAttackUPT;
            }
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // �׹�° �� 4�� ���� ��ô�� �߰� ȹ�� 20�� ����
    public void FourLine_UpgradeGainMoreThrowables(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ��ô�� �߰� ȹ�� 20�� ����
            playerState.gainMoreThrowables += lobbyUpGradeState.gainMoreThrowablesUP;
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    /**************************************************************/

    // �ټ���° �� 1�� ���� ���� �Ҹ� ���� 6��
    public void FiveLine_UpgradeManaConsumption(int slot)
    {
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���� �Ҹ� ���� 6��
            for (int i = 0; i < 3; i++)
            {
                playerState.manaConsumption[i] -= playerState.manaConsumption[i] * (0.01f * lobbyUpGradeState.manaConsumptionUP);
            }
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // �ټ���° �� 2�� ���� ����� ��� 0.6�� 
    public void FiveLine_UpgradeDrainLife(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ����� ��� 0.6�� 
            playerState.drainLife += lobbyUpGradeState.drainLifeUP;
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // �ټ���° �� 3�� ���� ���� 0.6 ����
    public void FiveLine_UpgradeDefense(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���� 0.6 ����
            playerState.defense += lobbyUpGradeState.defenseUPT;
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // �ټ���° �� 4�� ���� ��� ȹ�� Ȯ�� 3�� ����
    public void FiveLine_UpgradeEquipmentDrop(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� �߰� ��� ȹ�� Ȯ�� 3�� ����
            playerState.equipmentDropUpgrade += lobbyUpGradeState.equipmentDropUpgradeUPT;    
            OnCoinChanged?.Invoke();
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }

    // ������ ��� �κ� ��ȭ Max
    public void GetCoinMax()
    {
        // ������ �ִ�ġ�� ����
        gameData.coin = gameData.maxCoin;
        OnCoinChanged?.Invoke();
    }

    //  �κ� ��ȭ ����
    public void ResetCoin()
    {
        gameData.coin = 0;
        OnCoinChanged?.Invoke();
    }

}
