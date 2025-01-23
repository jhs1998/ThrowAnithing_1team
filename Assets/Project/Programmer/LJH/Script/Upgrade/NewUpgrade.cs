using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

public class NewUpgrade : BaseUI
{
    [Inject]
    private GlobalGameData _gameData;

    PlayerInput playerInput;

    [SerializeField] GameObject pause;

    //�÷��̾� / ī�޶� �����
    PlayerController player;
    float cameraSpeed;

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

    //�ؽ�Ʈ ó����
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
    List<Navigation> buttonNavigation = new();

    Button curButton;
    [SerializeField] GameObject textPos;

    [SerializeField] GameObject maxCoin;
    [SerializeField] GameObject zeroCoin;
    [SerializeField] GameObject save;

    [SerializeField] AudioClip upgradeSound;
    [SerializeField] AudioClip upgradeOpen;
    [SerializeField] AudioClip upgradeClose;

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
        EventSystem.current.SetSelectedGameObject(buttonIndex[0].gameObject);
        playerInput.SwitchCurrentActionMap(InputType.UI);
        SoundManager.PlaySFX(upgradeOpen);
    }

    private void OnDisable()
    {
        playerInput.SwitchCurrentActionMap(InputType.GAMEPLAY);
        SoundManager.PlaySFX(upgradeClose);
    }

    private void Update()
    {
        curButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        //�ƽ�����, ��������, ���̺� �������� 1��° ��ư���� ���� �̵�
        if (EventSystem.current.currentSelectedGameObject == maxCoin ||
            EventSystem.current.currentSelectedGameObject == zeroCoin || 
            EventSystem.current.currentSelectedGameObject == save)
        {
            EventSystem.current.SetSelectedGameObject (buttonIndex[0].gameObject);
        }


        if (pause.activeSelf)
            return;

        MaxSlot();

        Slot_Selected();

        if (playerInput.actions["UIMove"].WasPressedThisFrame())
        {
            SoundManager.PlaySFX(SoundManager.Data.UI.NaviMove);
        }
    }




    /// <summary>
    /// Comment : if usedCost greater than costLimit Method
    /// </summary>
    void TierCal()
    {
        tier = 1;
        if (_gameData.usingCoin >= costLimit1)
        {
            tier = 2;
        }
        if (_gameData.usingCoin >= costLimit2)
        { 
            tier = 3;
        }
        if (_gameData.usingCoin >= costLimit3)
        { 
            tier = 4; 
        }
        if (_gameData.usingCoin >= costLimit4)
        { 
            tier = 5; 
        }

        int i = (tier - 1) * 4;
        switch (tier)
        {
            // Ƽ�� ���� �ش��ϴ� ���� ��ư ���
            // Ƽ�� ���� ���Ʒ��� ��� ����
            case 1:
                for (int j = 0; j < 4; j++)
                {
                    NavigationLock(buttonIndex[(i + j)]);
                }
                break;

            case 2:
                for (int j = 0; j < 4; j++)
                {
                    NavigationLock(buttonIndex[(i + j)]);
                    NavigationUnLock(buttonIndex[j], buttonIndex[j + 4], buttonIndex[j + 4]);
                    NavigationUnLock(buttonIndex[j + 4], buttonIndex[j], buttonIndex[j]);
                    for (int index = 0; index < 8; index++)
                    {
                        buttonIndex[index].interactable = true;
                    }
                }
                break;

            case 3:
                for (int j = 0; j < 4; j++)
                {
                    NavigationLock(buttonIndex[(i + j)]);
                    NavigationUnLock(buttonIndex[j], buttonIndex[j + 8], buttonIndex[j + 4]);
                    NavigationUnLock(buttonIndex[j + 4], buttonIndex[j], buttonIndex[j + 8]);
                    NavigationUnLock(buttonIndex[j + 8], buttonIndex[j + 4], buttonIndex[j]);
                    for (int index = 0; index < 12; index++)
                    {
                        buttonIndex[index].interactable = true;
                    }
                }
                break;

            case 4:
                for (int j = 0; j < 4; j++)
                {
                    NavigationLock(buttonIndex[(i + j)]);
                    NavigationUnLock(buttonIndex[j], buttonIndex[j + 12], buttonIndex[j + 4]);
                    NavigationUnLock(buttonIndex[j + 8], buttonIndex[j + 4], buttonIndex[j + 12]);
                    NavigationUnLock(buttonIndex[j + 12], buttonIndex[j + 8], buttonIndex[j]);
                    for (int index = 0; index < 16; index++)
                    {
                        buttonIndex[index].interactable = true;
                    }
                }
                break;

            case 5:
                for (int j = 0; j < 4; j++)
                {
                    NavigationUnLock(buttonIndex[j + 12], buttonIndex[j + 8], buttonIndex[j + 16]);
                    NavigationUnLock(buttonIndex[j], buttonIndex[j + 16], buttonIndex[j + 4]);
                    for (int index = 0; index < 20; index++)
                    {
                        buttonIndex[index].interactable = true;
                    }
                }
                break;

        }
    }

    /// <summary>
    /// Ƽ� �°� �׺���̼� �̵� ����
    /// </summary>
    /// <param name="buttonIndex">��ư �ε���</param>
    void NavigationLock(Button buttonIndex)
    {
        Navigation navi = buttonIndex.navigation;
        navi.selectOnUp = null;
        navi.selectOnDown = null;
        buttonIndex.navigation = navi;
    }

    /// <summary>
    /// Ƽ� �°� �׺���̼� �̵� ���� ����
    /// </summary>
    /// <param name="buttonIndex">��ư �ε���</param>
    /// <param name="upbutton">selectOnUp �� �־��� ��ư</param>
    /// <param name="downButton">selectOnDown �� �־��� ��ư</param>
    void NavigationUnLock(Button buttonIndex, Button upbutton, Button downButton)
    {
        Navigation navi = buttonIndex.navigation;
        navi.selectOnUp = upbutton;
        navi.selectOnDown = downButton;
        buttonIndex.navigation = navi;
    }

    /// <summary>
    /// ����� ���� ����Ͽ� ��ư Ȱ��ȭ / ��Ȱ��ȭ
    /// </summary>
    void SlotLimit()
    {
        TierCal();

        for (int i = tier; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                //Todo : Change Color
                buttons[i][j].GetComponent<Image>().color = new(0.1f, 0, 0.2f);
                buttons[i][j].interactable = false;
            }
        }

    }


    /// <summary>
    /// Comment : ���� ó��
    /// </summary>
    void Slot_Selected()
    {
        Debug.Log("���� ����Ƽ�� ������");
        // Comment : �ٸ� ���� �� ����
        ColorReset();
        // Comment : ������ ���� ���������
        curButton.GetComponent<Image>().color = new(0.7f, 0.7f, 0.1f);

        itemName.text = curButton.name;
        itemInfo.text = curButton.name;

        for (int i = 0; i < buttonIndex.Count; i++)
        {
            if (curButton == buttonIndex[i])
            {
                itemImage.sprite = slotImage[i].sprite;
                infotext.text = $"{_gameData.upgradeLevels[i]} / 5";
            }
        }

        //�׽�Ʈ��
        //MaxSlot();

        SlotLimit();
    }

    void MaxSlot()
    {
        //�׽�Ʈ��
        for (int i = 0; i < buttonIndex.Count; i++)
        {
            if (_gameData.upgradeLevels[i] == 5)
            {
                buttonIndex[i].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(true);
                buttonIndex[i].onClick.RemoveAllListeners();

                SaveMaxSlot();
            }
        }
    }

    bool[] slotMaxCheck = new bool[20];

    /// <summary>
    /// 5���� ���� Ư���� ��ȭ �Ϸ� ǥ�� ����
    /// </summary>
    void SaveMaxSlot()
    {
        for (int i = 0; i < buttonIndex.Count; i++)
        {
            slotMaxCheck[i] = buttonIndex[i].transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;

        }
    }

    /// <summary>
    /// ��� ��ư ���� �ʱ�ȭ
    /// </summary>
    void ColorReset()
    {
        Debug.Log("�÷����� �����");
        for (int i = 0; i < buttonIndex.Count; i++)
            buttonIndex[i].GetComponent<Image>().color = new(0.2f, 0.25f, 0.6f);
    }

    public void UpgradeText()
    {
        // ���׷��̵� �Ϸ� ���� �������� ����
        int cost = _gameData.upgradeCosts[slot];

        if (_gameData.coin >= cost)
            Instantiate(upText, textPos.transform.position, Quaternion.identity);
    }

    void Init()
    {
        playerInput = InputKey.PlayerInput;


        // 0 1 2 3  tier 1
        tier1.Add(GetUI<Button>("���� ���ݷ� ����"));
        tier1.Add(GetUI<Button>("���Ÿ� ���ݷ� ����"));
        tier1.Add(GetUI<Button>("�̵� �ӵ� ����"));
        tier1.Add(GetUI<Button>("�ִ� ü�� ����"));
        // 4 5 6 7  tier 2
        tier2.Add(GetUI<Button>("���׹̳� ����"));
        tier2.Add(GetUI<Button>("���� �ӵ� ����"));
        tier2.Add(GetUI<Button>("ġ��Ÿ Ȯ�� ����"));
        tier2.Add(GetUI<Button>("��� �߰� Ȯ�� ����"));
        // 8 9 10 11 tier 3
        tier3.Add(GetUI<Button>("���ݷ� ����"));
        tier3.Add(GetUI<Button>("������Ʈ ������ ����"));
        tier3.Add(GetUI<Button>("���� ����"));
        tier3.Add(GetUI<Button>("���� ȸ���� ����"));
        // 12 13 14 15 tier 4
        tier4.Add(GetUI<Button>("���׹̳� ���ҷ� ����"));
        tier4.Add(GetUI<Button>("���Ÿ� ���ݷ� ����2"));
        tier4.Add(GetUI<Button>("���� ���ݷ� ����2"));
        tier4.Add(GetUI<Button>("������Ʈ ȹ�淮 ����"));
        // 16 17 18 19 tier 5
        tier5.Add(GetUI<Button>("���� ���ҷ� ����"));
        tier5.Add(GetUI<Button>("ü�� ����� ����"));
        tier5.Add(GetUI<Button>("���� ����2"));
        tier5.Add(GetUI<Button>("��� �߰� Ȯ�� ����2"));

        buttons.Add(tier1);
        buttons.Add(tier2);
        buttons.Add(tier3);
        buttons.Add(tier4);
        buttons.Add(tier5);

        for (int i = 0; i < buttonIndex.Count; i++)
        {
            slotImage.Add(buttonIndex[i].transform.GetChild(0).GetComponent<Image>());
            buttonNavigation.Add(buttonIndex[i].navigation);

            int row = i;

            buttonIndex[i].onClick.AddListener(() => { UpgradeText(); SoundManager.PlaySFX(upgradeSound); });

        }

        player = GameObject.FindWithTag(Tag.Player).GetComponent<PlayerController>();


    }
}