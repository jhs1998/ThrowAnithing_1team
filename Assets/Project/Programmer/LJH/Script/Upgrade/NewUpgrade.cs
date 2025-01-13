using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class NewUpgrade : BaseUI
{
    [Inject]
    private GlobalGameData _gameData;

    [SerializeField] GameObject pause;

    //플레이어 / 카메라 제어용
    PlayerController player;
    float cameraSpeed;

    int ho;
    int ver;

    //Comment : Infomation > name
    [SerializeField] TMP_Text itemName;
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemInfo;
    [SerializeField] TMP_Text infotext;


    //Comment : for Test
    [SerializeField] int usedCost;

    int costLimit1 = 5000;
    int costLimit2 = 30000;
    int costLimit3 = 80000;
    int costLimit4 = 200000;

    // Comment : Cost Tier
    int tier;
    Color lockedColor = new(0.1f, 0, 0.2f);

    bool axisInUse; // 연속 조작 방지용

    //텍스트 처리용
    [SerializeField] GameObject upText;

    int slot;

    List<List<Button>> buttons = new();
    List<Button> tier1 = new();
    List<Button> tier2 = new();
    List<Button> tier3 = new();
    List<Button> tier4 = new();
    List<Button> tier5 = new();

    [SerializeField] List<Button> buttonIndex = new();
    [SerializeField] List<Image> slotImage = new();

    Button curButton;

    private void Awake()
    {
        Bind();
        Init();
    }

    private void Start()
    {

    }

    private void OnEnable()
    {

        cameraSpeed = player.setting.cameraSpeed;
        player.setting.cameraSpeed = 0;
    }

    private void OnDisable()
    {
        ver = 0;
        ho = 0;

        player.setting.cameraSpeed = cameraSpeed;
    }

    private void Update()
    {
        curButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();


        if (pause.activeSelf)
            return;

        MaxSlot();

        Slot_Selected();

        //Comment : For test
        if (InputKey.GetButtonDown(InputKey.PrevInteraction))
        {
            curButton.onClick.Invoke();
        }
        
    

    }



    /// <summary>
    /// Comment : if usedCost greater than costLimit Method
    /// </summary>
    void TierCal()
    {
        tier = 1;
        if (_gameData.usingCoin >= costLimit1)
            tier = 2;
        if (_gameData.usingCoin >= costLimit2)
            tier = 3;
        if (_gameData.usingCoin >= costLimit3)
            tier = 4;
        if (_gameData.usingCoin >= costLimit4)
            tier = 5;
    }

    /// <summary>
    /// 사용한 코인 계산하여 버튼 활성화 / 비활성화
    /// </summary>
    void SlotLimit()
    {
        TierCal();

        for (int i = tier; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                //Todo : Change Color
                buttons[i][j].GetComponent<Image>().color = new(0.1f, 0, 0.2f);
                buttons[i][j].interactable = false;
            } 
        }

    }


    /// <summary>
    /// Comment : 슬롯 처리
    /// </summary>
    void Slot_Selected()
    {

        // Comment : 다른 슬롯 색 리셋
        ColorReset();
        // Comment : 선택한 슬롯 노란색으로

        curButton.GetComponent<Image>().color = new(0.7f, 0.7f, 0.1f);

        itemName.text = curButton.name;
        itemInfo.text = curButton.name;

        for (int i = 0; i < buttonIndex.Count; i++)
        {
            if(curButton == buttonIndex[i])
                itemImage.sprite = slotImage[i].sprite;
            infotext.text = $"{_gameData.upgradeLevels[i]} / 5";
        }

        //테스트용
        //MaxSlot();

        SlotLimit();
    }

    void MaxSlot()
    {
        //테스트용
        for (int i = 0; i < slotMaxCheck.Length; i++)
        {
            if (_gameData.upgradeLevels[i] == 5)
            {
                int ver;
                int ho;

                ver = i / 4;
                ho = i % 4;
                curButton.transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(true);
                curButton.onClick.RemoveAllListeners();

                SaveMaxSlot();
            }
        }
    }

    bool[] slotMaxCheck = new bool[20];

    //5강이 찍힌 특성에 강화 완료 표시 저장
    void SaveMaxSlot()
    {
        for (int i = 0; i < buttonIndex.Count; i++)
        {
                slotMaxCheck[i] = buttonIndex[i].transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
            
        }
    }

    
    //슬롯 클릭시
    void ClickedSlots(Button button)
    {
        if (button.GetComponent<Image>().color != lockedColor)
        {
            curButton.GetComponent<Image>().color = new(0.2f, 0.25f, 0.6f);

            itemName.text = button.name;
            itemInfo.text = button.name;
            for (int i = 0; i < buttonIndex.Count; i++)
            {
                if (curButton == buttonIndex[i])
                    slotImage[i] = button.transform.GetChild(0).GetComponent<Image>();
            }
            curButton.GetComponent<Image>().color = new(0.7f, 0.7f, 0.1f);

            if (button.GetComponent<Image>().color != lockedColor)
            {
                for (int i = 0; i < tier; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        buttons[i][j].interactable = true;
                    }
                }
            }
        }
    }

    // 선택한 버튼의 인덱스로 slots 인덱스 교체
   //(int, int) FindButton(Button button)
   //{
   //    for (int i = 0; i < 5; i++)
   //    {
   //        for (int j = 0; j < 4; j++)
   //        {
   //            if (slots[i, j].name == button.name)
   //            {
   //                return (i, j);
   //            }
   //        }
   //    }
   //    //인식 못했을 때 0,0 으로 초기화
   //    return (0, 0);
   //
   //}

    void ColorReset()
    {
        curButton.GetComponent<Image>().color = new(0.2f, 0.25f, 0.6f);
    }

    public void UpgradeText()
    {
        // 업그레이드 완료 문구 없을때만 동작
        int cost = _gameData.upgradeCosts[slot];

        if (_gameData.coin >= cost)
            Instantiate(upText, buttonIndex[10].transform.position, Quaternion.identity);
    }

    void Init()
    {
        // 0 1 2 3  tier 1
        tier1.Add(GetUI<Button>("근접 공격력 증가"));
        tier1.Add(GetUI<Button>("원거리 공격력 증가"));
        tier1.Add(GetUI<Button>("이동 속도 증가"));
        tier1.Add(GetUI<Button>("최대 체력 증가"));
        // 4 5 6 7  tier 2
        tier2.Add(GetUI<Button>("스테미너 증가"));
        tier2.Add(GetUI<Button>("공격 속도 증가"));
        tier2.Add(GetUI<Button>("치명타 확률 증가"));
        tier2.Add(GetUI<Button>("장비 발견 확률 증가"));
        // 8 9 10 11 tier 3
        tier3.Add(GetUI<Button>("공격력 증가"));
        tier3.Add(GetUI<Button>("오브젝트 보유량 증가"));
        tier3.Add(GetUI<Button>("방어력 증가"));
        tier3.Add(GetUI<Button>("마나 회복력 증가"));
        // 12 13 14 15 tier 4
        tier4.Add(GetUI<Button>("스테미너 감소량 증가"));
        tier4.Add(GetUI<Button>("원거리 공격력 증가2"));
        tier4.Add(GetUI<Button>("근접 공격력 증가2"));
        tier4.Add(GetUI<Button>("오브젝트 획득량 증가"));
        // 16 17 18 19 tier 5
        tier5.Add(GetUI<Button>("마나 감소량 증가"));
        tier5.Add(GetUI<Button>("체력 흡수량 증가"));
        tier5.Add(GetUI<Button>("방어력 증가2"));
        tier5.Add(GetUI<Button>("장비 발견 확률 증가2"));

        buttons.Add(tier1);
        buttons.Add(tier2);
        buttons.Add(tier3);
        buttons.Add(tier4);
        buttons.Add(tier5);


        for (int i = 0; i < slotImage.Count; i++)
        {
                slotImage[i] = buttonIndex[i].transform.GetChild(0).GetComponent<Image>();

                int row = i;

                buttonIndex[i].onClick.AddListener(() => ClickedSlots(buttonIndex[row]));
                buttonIndex[i].onClick.AddListener(() => UpgradeText());
                
        }

        player = GameObject.FindWithTag(Tag.Player).GetComponent<PlayerController>();

        
    }
}