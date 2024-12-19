using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
//using Zenject;

public class SlotManager : MonoBehaviour
{
    public Button[] continueSlotButtons; // 컨티뉴 슬롯 버튼 배열
    public TextMeshProUGUI[] continueSlotTexts;     // 컨티뉴 슬롯 텍스트 배열

    public Button[] newGameSlotButtons;  // 뉴게임 슬롯 버튼 배열
    public TextMeshProUGUI[] newGameSlotTexts;      // 뉴게임 슬롯 텍스트 배열

    private UserDataManager userDataManager;
    /*
    [Inject]
    public void Construct(UserDataManager userDataManager)
    {
        this.userDataManager = userDataManager;
    }*/

    private void Start()
    {
        userDataManager = UserDataManager.instance;

        // 슬롯 UI 초기화
        UpdateSlotUI();
    }
    private void UpdateSlotUI()
    {
        // 슬롯 UI 갱신
        for (int i = 0; i < continueSlotButtons.Length; i++)
        {
            string slotPath = userDataManager.path + $"slot_{i}.json"; // 각 슬롯마다 파일 경로 다르게 설정
            if (File.Exists(slotPath))
            {
                string data = File.ReadAllText(slotPath);
                GlobalPlayerData playerData = JsonUtility.FromJson<GlobalPlayerData>(data);

                // 저장된 시간 표시
                string saveTime = string.IsNullOrEmpty(playerData.saveDateTime) ? "No Date" : playerData.saveDateTime;

                continueSlotTexts[i].text = $"Last Saved: {saveTime}";
                newGameSlotTexts[i].text = $"Last Saved: {saveTime}";
            }
            else
            {
                continueSlotTexts[i].text = $"Slot {i + 1}: Empty";
                newGameSlotTexts[i].text = $"Slot {i + 1}: Empty";
            }
        }
    }
    // 컨티뉴 게임 버튼
    public void OnContinueSlotClicked(int slotIndex)
    {
        string slotPath = userDataManager.path + $"slot_{slotIndex}.json"; // 각 슬롯마다 파일 경로 다르게 설정
        if (File.Exists(slotPath))
        {
            userDataManager.nowSlot = slotIndex;  // 슬롯 번호 설정
            userDataManager.LoadData(); // 데이터를 로드하고
            Debug.Log($"Slot {slotIndex + 1} 데이터 불러오기");
            // 게임 로드 후 씬 전환 추가 가능
            UpdateSlotUI();
        }
        else
        {
            Debug.Log($"Slot {slotIndex + 1}에 데이터가 없습니다");
        }
    }
    // 뉴 게임 버튼 
    public void OnNewGameSlotClicked(int slotIndex)
    {
        string slotPath = userDataManager.path + $"slot_{slotIndex}.json"; // 각 슬롯마다 파일 경로 다르게 설정
        if (File.Exists(slotPath))
        {
            Debug.Log("데이터 있음");
        }
        else
        {
            userDataManager.nowSlot = slotIndex;  // 슬롯 번호 설정
            userDataManager.DataClear(); // 데이터 초기화 (nowSlot은 변경하지 않음)
            userDataManager.SaveData(); // 새 게임 저장
            Debug.Log($"새 게임 시작 {slotIndex + 1}");
            // 새 게임 시작 후 씬 전환 추가 가능
            UpdateSlotUI();
        }
    }

    //데이터 삭제 함수
    public void DeleteButtonClicked()
    {
        userDataManager.DeleteData(); // 모든 데이터 삭제
        UpdateSlotUI(); // 슬롯 UI 갱신
        Debug.Log("모든 게임 데이터가 삭제되었습니다.");
    }
}
