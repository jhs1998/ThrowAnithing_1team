using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;
using Zenject.SpaceFighter;

public class NewArmChange : BaseUI
{
    PlayerInput playerInput;
    [Inject]
    GlobalGameData playerStateData;
    [Inject]
    PlayerData playerData;
    int arm_cur;
    List<GameObject> armUnits = new();
    List<Button> armButtons = new();

    [SerializeField] SaveSystem saveSystem;

    Forge _forge;

    Color color;

    private void Awake()
    {
        Bind();
        Init();
        _forge = GetComponentInParent<Forge>();
    }


    private void OnEnable()
    {
        playerInput.SwitchCurrentActionMap(ActionMap.UI);
    }
    private void OnDisable()
    {
        playerInput.SwitchCurrentActionMap(ActionMap.GamePlay);
    }
    private void Update()
    {
        ArmUnit_changeColor();
    }
    
    //Todo : 함수 이름 바꿔야함
    void ArmUnit_changeColor()
    {
        for (int i = 0; i < armUnits.Count; i++)
        {
            color = armUnits[i].GetComponent<Image>().color;
            color.a = 0.1f;
            armUnits[i].GetComponent<Image>().color = color;
        }

        color = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;
        color.a = 1f;
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = color;

        saveSystem.SavePlayerData();

    }

    #region 테스트용 함수
    public void Power()
    {
        Debug.Log("파워 타입 선택");
        playerStateData.nowWeapon = GlobalGameData.AmWeapon.Power;
        playerData.NowWeapon = playerStateData.nowWeapon;
        // 데이터 세이브
        saveSystem.SavePlayerData();
    }

    public void Balance()
    {
        Debug.Log("밸런스 타입 선택");
        playerStateData.nowWeapon = GlobalGameData.AmWeapon.Balance;
        playerData.NowWeapon = playerStateData.nowWeapon;
        // 데이터 세이브
        saveSystem.SavePlayerData();
    }
    #endregion

    public void ArmUnitClose()
    {
        // gameObject.SetActive(false);
        _forge.SetUnActiveUI();
    }
    void Init()
    {
        playerInput = InputKey.PlayerInput;

        armUnits.Add(GetUI("PowerButton"));
        armUnits.Add(GetUI("BalanceButton"));

        armButtons.Add(GetUI<Button>("PowerButton"));
        armButtons.Add(GetUI<Button>("BalanceButton"));

        armButtons[0].onClick.AddListener(Power);
        armButtons[1].onClick.AddListener(Balance);

    }
}