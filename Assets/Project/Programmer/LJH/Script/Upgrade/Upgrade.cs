using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using static UnityEngine.Rendering.DebugUI.Table;

public class Upgrade : UpgradeBinding
{
    [Inject]
    private GlobalGameData _gameData;

    Button[,] slots;
    Image[,] slotImages;

    int ho;
    int ver;

    Coroutine slotCo;
    Coroutine buttonCo;
    float inputDelay = 0.25f;

    //Comment : Infomation > name
    [SerializeField] TMP_Text itemName;
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemInfo;


    //Comment : for Test
    [SerializeField] int usedCost;

    int costLimit1 = 5000;
    int costLimit2 = 30000;
    int costLimit3 = 80000;
    int costLimit4 = 200000;

    // Comment : Cost Tier
    int tier;
    Color lockedColor = new(0.1f, 0, 0.2f);

    private void Awake()
    {
        Bind();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (slotCo == null)
            slotCo = StartCoroutine(Slot_Selected());

        //Comment : For test
        if (InputKey.GetButtonDown(InputKey.Interaction))
        {
            slots[ver, ho].onClick.Invoke();
        }
    }

    //Comment : if usedCost greater than costLimit Method
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

    void SlotLimit()
    {
        TierCal();

        for (int i = tier; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                //Todo : Change Color
                slots[i, j].GetComponent<Image>().color = new(0.1f, 0, 0.2f);
            }
        }

    }


    //Comment : 슬롯 이동 함수
    IEnumerator Slot_Selected()
    {
        float x = InputKey.GetAxisRaw(InputKey.Horizontal);
        float y = -InputKey.GetAxisRaw(InputKey.Horizontal);

        ho += (int)x;
        ver += (int)y;

        ho = ho == -1 ? 3 : ho == 4 ? 0 : ho;
        ver = ver == -1 ? tier - 1 : ver == tier ? 0 : ver;

        if (ho == -1 )
            ho = 3;

        if (ho == 4)
            ho = 0;

        if (ver == -1)
            ver = tier - 1;

        if (ver == tier)
            ver = 0;

        // Comment : 다른 슬롯 색 리셋
        ColorReset();
        // Comment : 선택한 슬롯 노란색으로

        slots[ver, ho].GetComponent<Image>().color = new(0.7f, 0.7f, 0.1f);

        itemName.text = slots[ver, ho].name;
        itemImage.sprite = slotImages[ver, ho].sprite;
        itemInfo.text = slots[ver, ho].name;

        SlotLimit();
        yield return inputDelay.GetDelay();
        slotCo = null;

    }


    //슬롯 클릭시
    void ClickedSlots(Button button)
    {
        if (button.GetComponent<Image>().color != lockedColor)
        {
            slots[ver, ho].GetComponent<Image>().color = new(0.2f, 0.25f, 0.6f);

            if (EventSystem.current.currentInputModule != InputKey.GetButtonDown(InputKey.Horizontal))
            {
                (ver, ho) = FindButton(EventSystem.current.currentSelectedGameObject.GetComponent<Button>());
            }

            itemName.text = button.name;
            itemInfo.text = button.name;
            slotImages[ver, ho] = button.transform.GetChild(0).GetComponent<Image>();
            slots[ver, ho].GetComponent<Image>().color = new(0.7f, 0.7f, 0.1f);
        }
    }

    // 선택한 버튼의 인덱스로 slots 인덱스 교체
    (int,int) FindButton(Button button)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (slots[i, j].name == button.name)
                {
                    return (i, j);
                }
            }
        }
        //인식 못했을 때 0,0 으로 초기화
        return (0, 0);

    }

    void ColorReset()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                slots[i, j].GetComponent<Image>().color = new(0.2f, 0.25f, 0.6f);
            }
        }
    }

    void Init()
    {
        slots = new Button[5, 4];
        slotImages = new Image[5, 4];

        slots[0, 0] = GetUI<Button>("근접 공격력 증가");
        slots[0, 1] = GetUI<Button>("원거리 공격력 증가");
        slots[0, 2] = GetUI<Button>("이동 속도 증가");
        slots[0, 3] = GetUI<Button>("최대 체력 증가");

        slots[1, 0] = GetUI<Button>("스테미너 증가");
        slots[1, 1] = GetUI<Button>("공격 속도 증가");
        slots[1, 2] = GetUI<Button>("치명타 확률 증가");
        slots[1, 3] = GetUI<Button>("장비 발견 확률 증가");

        slots[2, 0] = GetUI<Button>("공격력 증가");
        slots[2, 1] = GetUI<Button>("오브젝트 보유량 증가");
        slots[2, 2] = GetUI<Button>("방어력 증가");
        slots[2, 3] = GetUI<Button>("마나 회복력 증가");

        slots[3, 0] = GetUI<Button>("스테미너 감소량 증가");
        slots[3, 1] = GetUI<Button>("원거리 공격력 증가2");
        slots[3, 2] = GetUI<Button>("근접 공격력 증가2");
        slots[3, 3] = GetUI<Button>("오브젝트 획득량 증가");

        slots[4, 0] = GetUI<Button>("마나 감소량 증가");
        slots[4, 1] = GetUI<Button>("체력 흡수량 증가");
        slots[4, 2] = GetUI<Button>("방어력 증가2");
        slots[4, 3] = GetUI<Button>("장비 발견 확률 증가2");

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                slotImages[i, j] = slots[i, j].transform.GetChild(0).GetComponent<Image>();

                int row = i;
                int col = j;
                
                slots[i, j].onClick.AddListener(() => ClickedSlots(slots[row,col]));
            }
        }
    }
}