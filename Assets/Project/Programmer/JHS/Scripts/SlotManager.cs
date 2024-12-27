using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Zenject;
using UnityEngine.SceneManagement;

public class SlotManager : MonoBehaviour
{
    public Button[] continueSlotButtons; // 컨티뉴 슬롯 버튼 배열
    public TextMeshProUGUI[] continueSlotTexts;     // 컨티뉴 슬롯 텍스트 배열

    public Button[] newGameSlotButtons;  // 뉴게임 슬롯 버튼 배열
    public TextMeshProUGUI[] newGameSlotTexts;      // 뉴게임 슬롯 텍스트 배열
    [Inject]
    private UserDataManager userDataManager;
    [Inject]
    private GlobalGameData globalPlayerData;
    [Inject]
    private LobbyUpGrade lobbyUpGrade;

    public GameObject confirmDeleteUI; // 확인 UI
    public Button confirmButton; // 확인 버튼
    public Button cancelButton; // 취소 버튼

    private int selectedSlotIndex; // 선택된 슬롯 인덱스

    private void Start()
    {
        // 슬롯 UI 초기화
        UpdateSlotUI();

        // 확인/취소 버튼 이벤트 연결
        confirmButton.onClick.AddListener(OnConfirmDelete);
        cancelButton.onClick.AddListener(OnCancelDelete);

        // UI 초기 비활성화
        confirmDeleteUI.SetActive(false);
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
                GlobalGameData playerData = JsonUtility.FromJson<GlobalGameData>(data);

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

            // 로비 강화 스탯 세팅 (나중에 로딩 창으로 변경)
            lobbyUpGrade.ApplyUpgradeStats();

            UpdateSlotUI();
            SceneManager.LoadScene("LobbyTestScene");
        }
        else
        {
            Debug.Log($"Slot {slotIndex + 1}에 데이터가 없습니다");
        }
    }
    // 뉴 게임 버튼 
    public void OnNewGameSlotClicked(int slotIndex)
    {
        // 각 슬롯마다 파일 경로 다르게 설정
        string slotPath = userDataManager.path + $"slot_{slotIndex}.json";
        selectedSlotIndex = slotIndex; // 선택된 슬롯 인덱스 저장
        if (File.Exists(slotPath))
        {
            // 데이터가 있는 경우 확인 UI 활성화
            confirmDeleteUI.SetActive(true);
        }
        else
        {
            // 데이터가 없는 경우 바로 새 게임 시작
            StartNewGame(slotIndex);
        }
    }
    private void StartNewGame(int slotIndex)
    {
        userDataManager.nowSlot = slotIndex; // 슬롯 번호 설정
        userDataManager.DataClear(); // 데이터 초기화
        userDataManager.SaveData(); // 새 게임 저장
        Debug.Log($"새 게임 시작 {slotIndex + 1}");
        UpdateSlotUI(); // 슬롯 UI 갱신
        SceneManager.LoadScene("LobbyTestScene");
    }

    private void OnConfirmDelete()
    {
        string slotPath = userDataManager.path + $"slot_{selectedSlotIndex}.json";
        if (File.Exists(slotPath))
        {
            File.Delete(slotPath); // 슬롯 데이터 삭제
            Debug.Log($"슬롯 {selectedSlotIndex + 1} 데이터 삭제 완료");
        }

        // 새 게임 시작
        StartNewGame(selectedSlotIndex);

        // 확인 UI 비활성화
        confirmDeleteUI.SetActive(false);
    }
    // 확인 UI 취소 버튼 
    private void OnCancelDelete()
    {
        // 확인 UI 비활성화
        confirmDeleteUI.SetActive(false);
    }
    // 전부 삭제 함수
    public void DeleteButtonClicked()
    {
        userDataManager.DeleteData(); // 모든 데이터 삭제
        UpdateSlotUI(); // 슬롯 UI 갱신
        Debug.Log("모든 게임 데이터가 삭제되었습니다.");
    }
}
