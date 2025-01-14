using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class NewPause : BaseUI
{
    PlayerInput playerInput;

    MainSceneBinding binding;


    List<Button> SlotList = new();
    [HideInInspector] public Button continueButton;
    Button optionButton;
    Button lobbyButton;

    GameObject pause;
    GameObject optionPanel;

    GameObject exitPopUp;

    Coroutine firstCo;

    Color color;

    GameObject preButton;
    GameObject defaultButton;
    GameObject currentSelected;

    Navigation conOriginalNavi;
    Navigation opOriginalNavi;
    Navigation lobbyOriginalNavi;


    private void Awake()
    {
        Bind();
        Init();

    }

    private void OnEnable()
    {
        // �κ�� ù ���Զ� �׼Ǹ� �ٲ���
        playerInput.SwitchCurrentActionMap(ActionMap.GamePlay);
    }

    private void Update()
    {
        if (exitPopUp.activeSelf)
            NaviBlock();

        else if(!exitPopUp.activeSelf)
        {
            continueButton.navigation = conOriginalNavi;
            optionButton.navigation = opOriginalNavi;
            lobbyButton.navigation = lobbyOriginalNavi;
        }
            

        //���� ����
        if (playerInput.actions["Open_Settings"].WasPressedThisFrame())
        {
            Time.timeScale = 0f;
            playerInput.SwitchCurrentActionMap(ActionMap.UI);
            
            pause.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            if (firstCo == null)
                firstCo = StartCoroutine(FirstRoutine());
        }

        ButtonMissClick();

        if (!optionPanel.activeSelf && pause.activeSelf)
        {
            SelectedSlotColorChange();
        }
    }

    void NaviBlock()
    {
        Navigation navi = continueButton.navigation;
        navi.selectOnUp = null;
        navi.selectOnDown = null;
        continueButton.navigation = navi;

        navi = optionButton.navigation;
        navi.selectOnUp = null;
        navi.selectOnDown = null;
        continueButton.navigation = navi;

        navi = lobbyButton.navigation;
        navi.selectOnUp = null;
        navi.selectOnDown = null;
        continueButton.navigation = navi;
    }

    /// <summary>
    /// ��ư ����� Ŭ������ ��, �⺻ ��ư ������ �޼���
    /// </summary>
    void ButtonMissClick()
    {
        //���õ� ��ư�� ������ �⺻ ��ư���� �����ϱ� ���� ���� �Ҵ�
        if (defaultButton == null)
            defaultButton = continueButton.gameObject;
        //���� ��ư ����
        currentSelected = EventSystem.current.currentSelectedGameObject;

        if (playerInput.actions["LeftClick"].WasPressedThisFrame() || playerInput.actions["UIMove"].WasPressedThisFrame())
        {
            // �� ������ Ŭ������ ��
            if (currentSelected == null)
            {
                RestoreButton();
            }
            else
            {
                // ���� ���õ� ��ư�� ����
                preButton = currentSelected;
            }
        }
    }

    /// <summary>
    /// ����� ������ �� ��ư ������ �Լ�
    /// </summary>
    public void RestoreButton()
    {
        if (playerInput.actions["LeftClick"].WasPressedThisFrame())
        {
            if (preButton != null)
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                    EventSystem.current.SetSelectedGameObject(preButton);
            }
            else if (preButton == null)
                EventSystem.current.SetSelectedGameObject(defaultButton);
        }
    }

    /// <summary>
    /// ���Խ� ù ��ư �������ִ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator FirstRoutine()
    {

        while (true)
        {
            binding.ButtonFirstSelect(continueButton.gameObject);

            yield return 0.1f.GetDelay();
        }
    }

    /// <summary>
    /// ���õ� ��ư�� ������ ����
    /// </summary>
    void SelectedSlotColorChange()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            for (int i = 0; i < SlotList.Count; i++)
            {
                color = SlotList[i].GetComponent<Image>().color;
                color.a = 0.1f;
                SlotList[i].GetComponent<Image>().color = color;
            }

            color = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;
            color.a = 1f;
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = color;
        }

    }

    /// <summary>
    /// ����ϱ� ������ ��
    /// </summary>
    void ContinueGame()
    {
        //Todo : ����â �ݾƾ���
        playerInput.SwitchCurrentActionMap(ActionMap.GamePlay);
        Time.timeScale = 1f;
        firstCo = null;
        pause.SetActive(false);
    }

    /// <summary>
    /// ȯ�漳�� ������ ��
    /// </summary>
    void OptionButton()
    {
        //Todo : �ɼ� ĵ���� ���������
        firstCo = null;
        EventSystem.current.SetSelectedGameObject(null);
        optionPanel.SetActive(true);
        
    }

    /// <summary>
    /// ���ư��� ������ ��
    /// </summary>
    void ReturnLobby()
    {
        playerInput.SwitchCurrentActionMap(ActionMap.GamePlay);
        firstCo = null;
        exitPopUp.SetActive(true);

    }


    void Init()
    {

        playerInput = InputKey.PlayerInput;
        binding = GetComponent<MainSceneBinding>();

        pause = GetUI("pause");
        SlotList.Add(continueButton = GetUI<Button>("ContinueImage"));
        SlotList.Add(optionButton = GetUI<Button>("OptionImage"));
        SlotList.Add(lobbyButton = GetUI<Button>("ExitImage"));

        conOriginalNavi = continueButton.navigation;
        opOriginalNavi = optionButton.navigation;
        lobbyOriginalNavi = lobbyButton.navigation;

        optionPanel = GetUI("OptionCanvas");
        exitPopUp = GetUI("ExitPopUp");

        continueButton.onClick.AddListener(ContinueGame);
        optionButton.onClick.AddListener(OptionButton);
        lobbyButton.onClick.AddListener(ReturnLobby);
    }
}