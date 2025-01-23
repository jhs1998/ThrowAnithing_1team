/*using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class Upgrade : BaseUI
{
    [Inject]
    private GlobalGameData _gameData;

    [SerializeField] GameObject pause;

    //�÷��̾� / ī�޶� �����
    PlayerController player;
    float cameraSpeed;

    Button[,] slots;
    Image[,] slotImages;

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


        if (pause.activeSelf)
            return;

        MaxSlot();

        Slot_Selected();

        //Comment : For test
        if (InputKey.GetButtonDown(InputKey.PrevInteraction))
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
                slots[i, j].interactable = false;
            }
        }

    }


    //Comment : ���� �̵� �Լ�
    void Slot_Selected()
    {

        float x = InputKey.GetAxisRaw(InputKey.Horizontal);
        float y = -InputKey.GetAxisRaw(InputKey.Vertical);

        if (x > 0)
        {
            if (axisInUse == false)
            {
                ho += 1;
                axisInUse = true;
            }
        }
        else if (x < 0)
        {
            if (axisInUse == false)
            {
                ho -= 1;
                axisInUse = true;
            }
        }

        else if (y > 0)
        {
            if (axisInUse == false)
            {
                ver += 1;
                axisInUse = true;
            }
        }
        else if (y < 0)
        {
            if (axisInUse == false)
            {
                ver -= 1;
                axisInUse = true;
            }
        }
        else
        {
            axisInUse = false;
        }


        ho = ho == -1 ? 3 : ho == 4 ? 0 : ho;
        ver = ver == -1 ? tier - 1 : ver == tier ? 0 : ver;

        if (ver == -1)
            ver = 0;

        // Comment : �ٸ� ���� �� ����
        ColorReset();
        // Comment : ������ ���� ���������

        slots[ver, ho].GetComponent<Image>().color = new(0.7f, 0.7f, 0.1f);

        itemName.text = slots[ver, ho].name;
        itemImage.sprite = slotImages[ver, ho].sprite;
        itemInfo.text = slots[ver, ho].name;

        
        int slotNum = ver * 4 + ho; // 1���� �迭�� �ε����� ���
        infotext.text = $"{_gameData.upgradeLevels[slotNum]} / 5";
        slot = slotNum;

        //�׽�Ʈ��
        //MaxSlot();

        SlotLimit();
    }

    void MaxSlot()
    {
        //�׽�Ʈ��
        for (int i = 0; i < slotMaxCheck.Length; i++)
        {
            if (_gameData.upgradeLevels[i] == 5)
            {
                int ver;
                int ho;

                ver = i / 4;
                ho = i % 4;
                slots[ver, ho].transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(true);
                slots[ver, ho].onClick.RemoveAllListeners();

                SaveMaxSlot();
            }
        }
    }

    bool[] slotMaxCheck = new bool[20];

    //5���� ���� Ư���� ��ȭ �Ϸ� ǥ�� ����
    void SaveMaxSlot()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                int index = i * 4 + j;
                slotMaxCheck[index] = slots[i, j].transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
            }
        }
    }

    
    //���� Ŭ����
    void ClickedSlots(Button button)
    {
        if (button.GetComponent<Image>().color != lockedColor)
        {
            slots[ver, ho].GetComponent<Image>().color = new(0.2f, 0.25f, 0.6f);

            if (EventSystem.current.currentInputModule != InputKey.GetButtonDown(InputKey.PrevInteraction))
            {
                (ver, ho) = FindButton(EventSystem.current.currentSelectedGameObject.GetComponent<Button>());
            }

            itemName.text = button.name;
            itemInfo.text = button.name;
            slotImages[ver, ho] = button.transform.GetChild(0).GetComponent<Image>();
            slots[ver, ho].GetComponent<Image>().color = new(0.7f, 0.7f, 0.1f);

            if (button.GetComponent<Image>().color != lockedColor)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        slots[i, j].interactable = true;
                    }
                }
            }
        }
    }

    // ������ ��ư�� �ε����� slots �ε��� ��ü
    (int, int) FindButton(Button button)
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
        //�ν� ������ �� 0,0 ���� �ʱ�ȭ
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

    public void UpgradeText()
    {
        // ���׷��̵� �Ϸ� ���� �������� ����
        int cost = _gameData.upgradeCosts[slot];

        if (_gameData.coin >= cost)
            Instantiate(upText, slots[2,3].transform.position, Quaternion.identity);
    }

    void Init()
    {
        slots = new Button[5, 4];
        slotImages = new Image[5, 4];

        slots[0, 0] = GetUI<Button>("���� ���ݷ� ����");
        slots[0, 1] = GetUI<Button>("���Ÿ� ���ݷ� ����");
        slots[0, 2] = GetUI<Button>("�̵� �ӵ� ����");
        slots[0, 3] = GetUI<Button>("�ִ� ü�� ����");

        slots[1, 0] = GetUI<Button>("���׹̳� ����");
        slots[1, 1] = GetUI<Button>("���� �ӵ� ����");
        slots[1, 2] = GetUI<Button>("ġ��Ÿ Ȯ�� ����");
        slots[1, 3] = GetUI<Button>("��� �߰� Ȯ�� ����");

        slots[2, 0] = GetUI<Button>("���ݷ� ����");
        slots[2, 1] = GetUI<Button>("������Ʈ ������ ����");
        slots[2, 2] = GetUI<Button>("���� ����");
        slots[2, 3] = GetUI<Button>("���� ȸ���� ����");

        slots[3, 0] = GetUI<Button>("���׹̳� ���ҷ� ����");
        slots[3, 1] = GetUI<Button>("���Ÿ� ���ݷ� ����2");
        slots[3, 2] = GetUI<Button>("���� ���ݷ� ����2");
        slots[3, 3] = GetUI<Button>("������Ʈ ȹ�淮 ����");

        slots[4, 0] = GetUI<Button>("���� ���ҷ� ����");
        slots[4, 1] = GetUI<Button>("ü�� ����� ����");
        slots[4, 2] = GetUI<Button>("���� ����2");
        slots[4, 3] = GetUI<Button>("��� �߰� Ȯ�� ����2");

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                slotImages[i, j] = slots[i, j].transform.GetChild(0).GetComponent<Image>();

                int row = i;
                int col = j;

                slots[i, j].onClick.AddListener(() => ClickedSlots(slots[row, col]));
                slots[i, j].onClick.AddListener(() => UpgradeText());
                
            }
        }

        player = GameObject.FindWithTag(Tag.Player).GetComponent<PlayerController>();
    }
}*/