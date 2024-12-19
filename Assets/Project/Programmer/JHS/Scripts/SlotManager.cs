using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SlotManager : MonoBehaviour
{
    public Button[] continueSlotButtons; // 컨티뉴 슬롯 버튼 배열
    public TextMeshProUGUI[] continueSlotTexts;     // 컨티뉴 슬롯 텍스트 배열

    public Button[] newGameSlotButtons;  // 뉴게임 슬롯 버튼 배열
    public TextMeshProUGUI[] newGameSlotTexts;      // 뉴게임 슬롯 텍스트 배열

    private UserDataManager userDataManager;

    private void Start()
    {
        userDataManager = UserDataManager.instance;

        // 슬롯 UI 초기화
        UpdateSlotUI();
    }
    private void UpdateSlotUI()
    {
        // 컨티뉴 패널 슬롯 UI 갱신
        for (int i = 0; i < continueSlotButtons.Length; i++)
        {
            string slotPath = userDataManager.path + i.ToString();
            if (File.Exists(slotPath))
            {
                string data = File.ReadAllText(slotPath);
                GlobalPlayerData playerData = JsonUtility.FromJson<GlobalPlayerData>(data);
                continueSlotTexts[i].text = $"Slot {i + 1}: {playerData.playerName} (Coins: {playerData.coin})";
                newGameSlotTexts[i].text = $"Slot {i + 1}: Data Exists";
            }
            else
            {
                continueSlotTexts[i].text = $"Slot {i + 1}: Empty";
                newGameSlotTexts[i].text = $"Slot {i + 1}: Empty";
            }
        }
    }
    public void OnContinueSlotClicked(int slotIndex)
    {
        string slotPath = userDataManager.path + slotIndex.ToString();
        if (File.Exists(slotPath))
        {
            userDataManager.nowSlot = slotIndex;
            userDataManager.LoadData();
            Debug.Log($"Slot {slotIndex + 1} loaded: {userDataManager.nowPlayer.playerName}");
            // 게임 로드 후 씬 전환 추가 가능
        }
        else
        {
            Debug.Log($"Slot {slotIndex + 1} is empty.");
        }
    }
    public void OnNewGameSlotClicked(int slotIndex)
    {
        string slotPath = userDataManager.path + slotIndex.ToString();
        if (File.Exists(slotPath))
        {
            Debug.Log($"Slot {slotIndex + 1} already has data.");
        }
        else
        {
            userDataManager.nowSlot = slotIndex;
            userDataManager.DataClear();
            userDataManager.SaveData();
            Debug.Log($"New game started in Slot {slotIndex + 1}");
            // 새 게임 시작 후 씬 전환 추가 가능
        }
    }
}
