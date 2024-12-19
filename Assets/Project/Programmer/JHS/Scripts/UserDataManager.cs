using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GlobalPlayerData
{
    // 플레이어 이름
    public string playerName;
    // 플레이어 스탯
    public int maxHp;
    public int attackDamage;
    public int speed;
    public int luck;
    // 보유 재화
    public int coin;
    // 암 유닛 선택 종류 (Balance, Power, Speed)
    public enum AmWeapon { Balance, Power, Speed }
    // 로비 씬 업그레이드 진행 상황
    public Dictionary<string, int> upgrades;

}


public class UserDataManager : MonoBehaviour
{
    // 싱글톤
    public static UserDataManager instance;
    // 플레이어 데이터 생성
    public GlobalPlayerData nowPlayer = new GlobalPlayerData();
    // 세이브 파일 저장 경로
    public string path;
    // 현재 슬롯번호
    public int nowSlot;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(instance.gameObject);

        DontDestroyOnLoad(this.gameObject);

        // 저장 경로 지정
        path = Application.persistentDataPath + "/save";
        print(path);
    }
    // 저장 기능
    public void SaveData()
    {
        // 데이터에 현재 데이터 저장
        string data = JsonUtility.ToJson(nowPlayer);
        // path 경로의 fileName파일에 데이터 저장 
        File.WriteAllText(path + nowSlot.ToString(), data);
        Debug.Log("게임 저장 완료!");
    }
    // 로드 기능 
    public void LoadData()
    {
        // 데이터에 path 경로의 fileName파일 불러오기
        string data = File.ReadAllText(path + nowSlot.ToString());
        // 현재 플레이어에 불러온 데이터 적용
        nowPlayer = JsonUtility.FromJson<GlobalPlayerData>(data);
        Debug.Log("게임 로드 완료!");
    }
    public void DataClear()
    {
        nowSlot = 0;
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
}
