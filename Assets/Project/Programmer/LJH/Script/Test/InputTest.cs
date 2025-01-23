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

public class InputTest : BaseUI
{
    [Inject]
    private GlobalGameData _gameData;

    //�÷��̾� / ī�޶� �����
    PlayerController player;
    float cameraSpeed;

    List<Button> slots = new List<Button>();
    List<Image> slotImages = new List<Image>();

    int index;

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

    bool axisInUse; // ���� ���� ������

    //�ؽ�Ʈ ó����
    [SerializeField] GameObject upText;

    int slot;

    private void Awake()
    {
        Bind();
        Init();
    }

    private void Start()
    {
        ShowMaxSlot();

    }

    private void OnEnable()
    {
        cameraSpeed = player.setting.cameraSpeed;
        player.setting.cameraSpeed = 0;
    }

    private void OnDisable()
    {
        index = 0;

        player.setting.cameraSpeed = cameraSpeed;
    }

    private void Update()
    {
        MaxSlot();

        Slot_Selected();

        //Comment : For test
        if (InputKey.GetButtonDown(InputKey.Choice))
        {
            slots[index].onClick.Invoke();
        }
        GameObject preButton;

        if (EventSystem.current.currentSelectedGameObject != gameObject.GetComponent<Button>())
            preButton = EventSystem.current.currentSelectedGameObject;

        //if (EventSystem.current.currentSelectedGameObject != gameObject.GetComponent<Button>())
        //EventSystem.current.currentSelectedGameObject = preButton;

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

        slots[tier * 4].GetComponent<Image>().color = new(0.1f, 0, 0.2f);


    }


    //Comment : ���� �̵� �Լ�
    void Slot_Selected()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = new(0.7f, 0.7f, 0.1f);


            itemName.text = EventSystem.current.currentSelectedGameObject.name;
            itemImage.sprite = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite;
            itemInfo.text = EventSystem.current.currentSelectedGameObject.name;
        }

        // ��ȭ Ƚ�� ���
        infotext.text = $"{_gameData.upgradeLevels[index]} / 5";

        //�׽�Ʈ��
        MaxSlot();

        SlotLimit();
    }

    void MaxSlot()
    {
        //�׽�Ʈ��
        if (_gameData.upgradeLevels[slot] == 5)
        {
            slots[index].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(true);
            slots[index].onClick.RemoveAllListeners();

            SaveMaxSlot();
        }
    }

    // ����� �������� �ʾ� �ϴ� �ϵ��ڵ����� ��ü.. ���� ���ܿ�� �ִ��� ������ ���� © ��..

    bool slot1;
    bool slot2;
    bool slot3;
    bool slot4;
    bool slot5;
    bool slot6;
    bool slot7;
    bool slot8;
    bool slot9;
    bool slot10;
    bool slot11;
    bool slot12;
    bool slot13;
    bool slot14;
    bool slot15;
    bool slot16;
    bool slot17;
    bool slot18;
    bool slot19;
    bool slot20;
    void SaveMaxSlot()
    {
        slot1 = slots[0].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot2 = slots[1].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot3 = slots[2].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot4 = slots[3].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot5 = slots[4].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot6 = slots[5].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot7 = slots[6].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot8 = slots[7].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot9 = slots[8].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot10 = slots[9].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot11 = slots[10].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot12 = slots[11].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot13 = slots[12].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot14 = slots[13].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot15 = slots[14].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot16 = slots[15].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot17 = slots[16].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot18 = slots[17].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot19 = slots[18].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
        slot20 = slots[19].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;

        PlayerPrefs.SetInt("slot1", System.Convert.ToInt16(slot1));
        PlayerPrefs.SetInt("slot2", System.Convert.ToInt16(slot2));
        PlayerPrefs.SetInt("slot3", System.Convert.ToInt16(slot3));
        PlayerPrefs.SetInt("slot4", System.Convert.ToInt16(slot4));
        PlayerPrefs.SetInt("slot5", System.Convert.ToInt16(slot5));
        PlayerPrefs.SetInt("slot6", System.Convert.ToInt16(slot6));
        PlayerPrefs.SetInt("slot7", System.Convert.ToInt16(slot7));
        PlayerPrefs.SetInt("slot8", System.Convert.ToInt16(slot8));
        PlayerPrefs.SetInt("slot9", System.Convert.ToInt16(slot9));
        PlayerPrefs.SetInt("slot10", System.Convert.ToInt16(slot10));
        PlayerPrefs.SetInt("slot11", System.Convert.ToInt16(slot11));
        PlayerPrefs.SetInt("slot12", System.Convert.ToInt16(slot12));
        PlayerPrefs.SetInt("slot13", System.Convert.ToInt16(slot13));
        PlayerPrefs.SetInt("slot14", System.Convert.ToInt16(slot14));
        PlayerPrefs.SetInt("slot15", System.Convert.ToInt16(slot15));
        PlayerPrefs.SetInt("slot16", System.Convert.ToInt16(slot16));
        PlayerPrefs.SetInt("slot17", System.Convert.ToInt16(slot17));
        PlayerPrefs.SetInt("slot18", System.Convert.ToInt16(slot18));
        PlayerPrefs.SetInt("slot19", System.Convert.ToInt16(slot19));
        PlayerPrefs.SetInt("slot20", System.Convert.ToInt16(slot20));




        Debug.Log($"����� �Ұ� {slot1}");
    }

    void ShowMaxSlot()
    {
        slot1 = System.Convert.ToBoolean(PlayerPrefs.GetInt("slot1"));

        slots[0].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot1")));
        slots[1].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot2")));
        slots[2].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot3")));
        slots[3].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot4")));
        slots[4].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot5")));
        slots[5].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot6")));
        slots[6].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot7")));
        slots[7].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot8")));
        slots[8].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot9")));
        slots[9].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot10")));
        slots[10].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot11")));
        slots[11].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot12")));
        slots[12].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot13")));
        slots[13].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot14")));
        slots[14].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot15")));
        slots[15].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot16")));
        slots[16].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot17")));
        slots[17].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot18")));
        slots[18].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot19")));
        slots[19].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(System.Convert.ToBoolean(PlayerPrefs.GetInt("slot20")));


        Debug.Log($"�ҷ��� �Ұ� {slot1}");
    }



    //���� Ŭ����
    void ClickedSlots(Button button)
    {
        if (button.GetComponent<Image>().color != lockedColor)
        {
            slots[index].GetComponent<Image>().color = new(0.2f, 0.25f, 0.6f);

            if (EventSystem.current.currentInputModule != InputKey.GetButtonDown(InputKey.Choice))
            {
                index = FindButton(EventSystem.current.currentSelectedGameObject.GetComponent<Button>());
            }

            itemName.text = button.name;
            itemInfo.text = button.name;
            slotImages[index] = button.transform.GetChild(0).GetComponent<Image>();
            slots[index].GetComponent<Image>().color = new(0.7f, 0.7f, 0.1f);
        }
    }

    // ������ ��ư�� �ε����� slots �ε��� ��ü
    int FindButton(Button button)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].name == button.name)
            {
                return i;
            }
        }
        //�ν� ������ �� 0,0 ���� �ʱ�ȭ
        return 0;

    }

    void ColorReset()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].GetComponent<Image>().color = new(0.2f, 0.25f, 0.6f);
        }
    }

    public void UpgradeText()
    {
        // ���׷��̵� �Ϸ� ���� �������� ����
        Instantiate(upText, slots[10].transform.position, Quaternion.identity);
    }

    void Init()
    {
        slots.Add(GetUI<Button>("���� ���ݷ� ����"));
        slots.Add(GetUI<Button>("���Ÿ� ���ݷ� ����"));
        slots.Add(GetUI<Button>("�̵� �ӵ� ����"));
        slots.Add(GetUI<Button>("�ִ� ü�� ����"));
        slots.Add(GetUI<Button>("���׹̳� ����"));
        slots.Add(GetUI<Button>("���� �ӵ� ����"));
        slots.Add(GetUI<Button>("ġ��Ÿ Ȯ�� ����"));
        slots.Add(GetUI<Button>("��� �߰� Ȯ�� ����"));
        slots.Add(GetUI<Button>("���ݷ� ����"));
        slots.Add(GetUI<Button>("������Ʈ ������ ����"));
        slots.Add(GetUI<Button>("���� ����"));
        slots.Add(GetUI<Button>("���� ȸ���� ����"));
        slots.Add(GetUI<Button>("���׹̳� ���ҷ� ����"));
        slots.Add(GetUI<Button>("���Ÿ� ���ݷ� ����2"));
        slots.Add(GetUI<Button>("���� ���ݷ� ����2"));
        slots.Add(GetUI<Button>("������Ʈ ȹ�淮 ����"));
        slots.Add(GetUI<Button>("���� ���ҷ� ����"));
        slots.Add(GetUI<Button>("ü�� ����� ����"));
        slots.Add(GetUI<Button>("���� ����2"));
        slots.Add(GetUI<Button>("��� �߰� Ȯ�� ����2"));

        for (int i = 0; i < slots.Count; i++)
        {
            slotImages.Add(slots[i].transform.GetChild(0).GetComponent<Image>());

            int row = i;

            slots[i].onClick.AddListener(() => ClickedSlots(slots[row]));
            slots[i].onClick.AddListener(() => UpgradeText());

        }

        player = GameObject.FindWithTag(Tag.Player).GetComponent<PlayerController>();

        EventSystem.current.firstSelectedGameObject = slots[0].gameObject;
    }
}