using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Zenject;


public class UserDataManager
{
    // 플레이어 데이터 생성
    public GlobalPlayerData nowPlayer { get; private set; } = new GlobalPlayerData();
    // 세이브 파일 저장 경로
    public string path { get; private set; }
    // 현재 슬롯번호
    public int nowSlot { get; set; }

    public UserDataManager()
    {
        path = Application.persistentDataPath + "/save";
        nowPlayer = new GlobalPlayerData(); // 초기화 추가
        Debug.Log($"Save path: {path}");
    }

    // 저장 기능
    public void SaveData()
    {
        // 현재 날짜와 시간 저장
        nowPlayer.saveDateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        // 데이터에 현재 데이터 저장
        string data = JsonUtility.ToJson(nowPlayer);
        // 각 슬롯에 대해 파일 경로 다르게 설정 (슬롯 번호에 맞는 파일로 저장)
        string slotPath = path + $"slot_{nowSlot}.json";
        File.WriteAllText(slotPath, data);
        Debug.Log($"슬롯 {nowSlot + 1}에 게임 저장 완료! 저장 시간: {nowPlayer.saveDateTime}");
    }
    // 로드 기능 
    public void LoadData()
    {
        // 각 슬롯에 대해 파일 경로 다르게 설정 (슬롯 번호에 맞는 파일로 불러오기)
        string slotPath = path + $"slot_{nowSlot}.json";
        if (File.Exists(slotPath))
        {
            string data = File.ReadAllText(slotPath);
            // 현재 플레이어에 불러온 데이터 적용
            nowPlayer = JsonUtility.FromJson<GlobalPlayerData>(data);
            Debug.Log($"슬롯 {nowSlot + 1}에서 게임 로드 완료!");
        }
        else
        {
            Debug.LogWarning($"슬롯 {nowSlot + 1}에 저장된 데이터가 없습니다.");
        }
    }
    public void DataClear()
    {
        nowPlayer = new GlobalPlayerData
        {
            playerName = "New Player",
            maxHp = 100,
            attackDamage = 10,
            speed = 5,
            luck = 1,
            coin = 0,
            upgrades = new Dictionary<string, int>()
        };
    }
    public void DeleteSlotData(int slotIndex)
    {
        string slotPath = path + $"slot_{slotIndex}.json";
        if (File.Exists(slotPath))
        {
            File.Delete(slotPath); // 해당 슬롯 파일 삭제
            Debug.Log($"슬롯 {slotIndex + 1} 데이터 삭제 완료");
        }
        else
        {
            Debug.LogWarning($"슬롯 {slotIndex + 1}에 데이터가 없습니다.");
        }
    }
}
