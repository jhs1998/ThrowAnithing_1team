/*using System.Collections;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zenject.SpaceFighter;

public class ArmChange : BaseUI
{
    [Inject]
    GlobalGameData playerStateData;
    [Inject]
    PlayerData playerData;
    int arm_cur;
    GameObject[] armUnits;
    Button[] armButtons;
    [SerializeField] SaveSystem saveSystem;

    Forge _forge;

    float inputDelay = 0.25f;

    Coroutine armCo;

    Color color;

    //ī�޶� �����
    PlayerController player;
    float cameraSpeed;

    private void Awake()
    {
        Bind();
        Init();
        _forge = GetComponentInParent<Forge>();
    }


    private void OnEnable()
    {
        cameraSpeed = player.setting.cameraSpeed;
        player.setting.cameraSpeed = 0;

        if (armCo == null)
            armCo = StartCoroutine(ArmUnit_Select());
    }
    private void OnDisable()
    {

        player.setting.cameraSpeed = cameraSpeed;

        if (armCo != null)
        {
            StopCoroutine(armCo);
            armCo = null;
        }
    }
    private void Update()
    {
        Select_ArmUnit();
    }
    private IEnumerator ArmUnit_Select()
    {
        while (true)
        {
            float x = InputKey.GetAxisRaw(InputKey.Horizontal);

            arm_cur += (int)x;
            if (arm_cur == armUnits.Length)
            {
                arm_cur = 0;
                // Comment ���� ��ư�� ������ 0.1�� ����
                color = armUnits[armUnits.Length - 1].GetComponent<Image>().color;
                color.a = 0.1f;
                armUnits[armUnits.Length - 1].GetComponent<Image>().color = color;

                // Comment : ���õ� ��ư�� ������ 1�� ����
                color = armUnits[arm_cur].GetComponent<Image>().color;
                color.a = 1;
                armUnits[arm_cur].GetComponent<Image>().color = color;
                yield return null;
            }

            if (arm_cur == -1)
            {
                arm_cur = armUnits.Length - 1;
                // Comment ���� ��ư�� ������ 0.1�� ����
                color = armUnits[0].GetComponent<Image>().color;
                color.a = 0.1f;
                armUnits[0].GetComponent<Image>().color = color;

                // Comment : ���õ� ��ư�� ������ 1�� ����
                color = armUnits[arm_cur].GetComponent<Image>().color;
                color.a = 1;
                armUnits[arm_cur].GetComponent<Image>().color = color;
                yield return null;
            }

            //Comment : ��� ��ư�� ������ 0.1�� ����
            ButtonReset();

            //Comment : ���õ� ��ư�� ������ 1�� ����
            color = armUnits[arm_cur].GetComponent<Image>().color;
            color.a = 1f;
            armUnits[arm_cur].GetComponent<Image>().color = color;
            // ������ ���̺�
            saveSystem.SavePlayerData();
            if (x == 0)
                yield return null;
            else
                yield return inputDelay.GetDelay();
        }

    }

    void ButtonReset()
    {
        for (int i = 0; i < armUnits.Length; i++)
        {
            color = armUnits[i].GetComponent<Image>().color;
            color.a = 0.1f;
            armUnits[i].GetComponent<Image>().color = color;
        }
    }

    void Select_ArmUnit()
    {
        if (InputKey.GetButtonDown(InputKey.PrevInteraction))
        {
            armButtons[arm_cur].onClick.Invoke();
        }
    }
    #region �׽�Ʈ�� �Լ�
    public void Power()
    {
        Debug.Log("�Ŀ� Ÿ�� ����");
        playerStateData.nowWeapon = GlobalGameData.AmWeapon.Power;
        playerData.NowWeapon = playerStateData.nowWeapon;
        // ������ ���̺�
        saveSystem.SavePlayerData();
    }

    public void Balance()
    {
        Debug.Log("�뷱�� Ÿ�� ����");
        playerStateData.nowWeapon = GlobalGameData.AmWeapon.Balance;
        playerData.NowWeapon = playerStateData.nowWeapon;
        // ������ ���̺�
        saveSystem.SavePlayerData();
    }

    public void Speed()
    {
        Debug.Log("���ǵ� Ÿ�� ����");
    }
    #endregion

    public void ArmUnitClose()
    {
       // gameObject.SetActive(false);
       _forge.SetUnActiveUI();
    }
    void Init()
    {
        armUnits = new GameObject[3];

        armUnits[0] = GetUI("PowerButton");
        armUnits[1] = GetUI("BalanceButton");
        armUnits[2] = GetUI("SpeedButton");

        armButtons = new Button[3];

        armButtons[0] = GetUI<Button>("PowerButton");
        armButtons[1] = GetUI<Button>("BalanceButton");
        armButtons[2] = GetUI<Button>("SpeedButton");

        armButtons[0].onClick.AddListener(Power);
        armButtons[1].onClick.AddListener(Balance);
        armButtons[2].onClick.AddListener(Speed);

        player = GameObject.FindWithTag(Tag.Player).GetComponent<PlayerController>();
    }
}*/