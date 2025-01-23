using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class NewOption : BaseUI
{
    [Inject]
    OptionSetting setting;

    MainSceneBinding binding;

    PlayerInput playerInput;
    // ���� ����
    // 0 = �ɼ�
    // 1 = �����÷���
    // 2 = �Ҹ�
    int curDepth;

    // ����1 ��ư ���
    Button gamePlayButton;
    Button soundButton;
    Button inputButton;
    Button exitButton;

    List<Button> optionButtons = new();

    //����2 ��ư ��� - �����÷���
    Button minimapAct;
    Button minimapFix;
    Button LanguageChange;
    Button sens;
    Slider sensSlider;

    Button accept_Gameplay;
    Button cancel_Gameplay;
    Button default_Gameplay;

    List<Button> gameplayButtons = new();

    //�̴ϸ� ������ �Һ���
    bool preAct;
    bool newAct;
    bool defaultAct;

    bool preFix;
    bool newFix;
    bool defaultFix;

    float preSens;
    float newSens;
    float defaultSens;

    GameObject actChecked; //�̴ϸ� Ȱ��ȭ üũ ����
    GameObject fixChecked; //�̴ϸ�   ���� üũ ����
    GameObject actUnChecked;
    GameObject fixUnChecked;


    //����2 ��ư ��� - �Ҹ�
    Button totalVolume;
    Button bgmVolume;
    Button sfxVolume;

    Slider totalVolumeBar;
    Slider bgmVolumeBar;
    Slider sfxVolumeBar;

    Button accept_Sound;
    Button cancel_Sound;
    Button default_Sound;

    List<Button> soundButtons = new();

    //���� ������

    float preTotal;
    float newTotal;
    float defaultTotal;

    float preBgm;
    float newBgm;
    float defaultBgm;

    float preEffect;
    float newEffect;
    float defaultEffect;

    //����Ű ������
    GameObject inputPC;
    GameObject inputConsole;

    //�г�
    GameObject gameplayPanel;
    GameObject soundPanel;
    GameObject inputPanel;

    //�ڷ�ƾ
    Coroutine firstCo;

    [SerializeField] NewPause pausePanel;

    //Ŭ�� ����� ������
    GameObject preButton;
    GameObject defaultButton;
    GameObject currentSelected;

    private void Awake()
    {
        Bind();
        Init();
    }

    private void OnEnable()
    {
        actChecked.SetActive(setting.miniMapOnBool);
        fixChecked.SetActive(setting.miniMapFixBool);
        sensSlider.value = setting.cameraSpeed;

        totalVolumeBar.maxValue = 1f;
        bgmVolumeBar.maxValue = 1f;
        sfxVolumeBar.maxValue = 1f;

        //�����ٸ� ���õ� ���� �־���
        totalVolumeBar.value = setting.wholesound;
        bgmVolumeBar.value = setting.backgroundSound;
        sfxVolumeBar.value = setting.effectSound;



        firstCo = null;
        //Todo : �ڿ������� ó���ؾ���
        binding.ButtonFirstSelect(gamePlayButton.gameObject);
        //���� ���õ� ��ư ���� ��, ù��° ��ư ����
        if (firstCo == null)
        {
            firstCo = StartCoroutine(FirstRoutine());
        }
    }

    private void OnDisable()
    {
        EventSystem.current.SetSelectedGameObject(null);

        if (pausePanel != null)
            EventSystem.current.SetSelectedGameObject(pausePanel.continueButton.gameObject);

    }

    void Start()
    {
        //�ɼ�â �������� ���� ��ư ��� 0���� ����
        curDepth = 0;

    }

    IEnumerator FirstRoutine()
    {
        while (true)
        {
            Debug.Log("ù��° ��ư ���õǴ���");
            binding.ButtonFirstSelect(gamePlayButton.gameObject);
            yield return 0.1f.GetDelay();
        }
    }

    void Update()
    {
        ButtonMissClick();
        CurDepthReset();
        DepthCal();

        if((EventSystem.current.currentSelectedGameObject != null))
        curTabChecker(EventSystem.current.currentSelectedGameObject.GetComponent<Button>());

        SliderControl(sens.gameObject, sensSlider);
        SliderControl(totalVolume.gameObject, totalVolumeBar);
        SliderControl(bgmVolume.gameObject, bgmVolumeBar);
        SliderControl(sfxVolume.gameObject, sfxVolumeBar);

        SoundManager.SetVolumeMaster(setting.wholesound);
        SoundManager.SetVolumeBGM(setting.backgroundSound);
        SoundManager.SetVolumeSFX(setting.effectSound);

        if (playerInput.actions["UIMove"].WasPressedThisFrame())
        {
            SoundManager.PlaySFX(SoundManager.Data.UI.NaviMove);
        }

        if (playerInput.actions["LeftClick"].WasPressedThisFrame())
        {
            SoundManager.PlaySFX(SoundManager.Data.UI.ClickNull);
        }
    }

    /// <summary>
    /// ����� Ŭ���� Ŀ���� ����
    /// </summary>
    void CurDepthReset()
    {
        if (curDepth > 0)
            Invoke("Term", 0.5f);
    }

    void Term()
    {
        if (EventSystem.current.currentSelectedGameObject == gamePlayButton.gameObject)
            curDepth = 0;

        if (EventSystem.current.currentSelectedGameObject == soundButton.gameObject)
            curDepth = 0;
    }

    /// <summary>
    /// ��ư ����� Ŭ������ ��, �⺻ ��ư ������ �޼���
    /// </summary>
    void ButtonMissClick()
    {
        //���õ� ��ư�� ������ �⺻ ��ư���� �����ϱ� ���� ���� �Ҵ�
        if (defaultButton == null)
            defaultButton = gamePlayButton.gameObject;
        //���� ��ư ����
        currentSelected = EventSystem.current.currentSelectedGameObject;

        if (playerInput.actions["LeftClick"].WasPressedThisFrame() || playerInput.actions["UIMove"].WasPressedThisFrame())
        {
            // �� ������ Ŭ������ ��
            if (currentSelected == null)
            {
                Debug.Log("����� Ŭ����");
                RestoreButton();
            }
            else
            {
                Debug.Log("Ŭ��������");
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

    void SliderControl(GameObject button, Slider slider)
    {
        if (EventSystem.current.currentSelectedGameObject == button)
        {
            if (playerInput.actions["UIMove"].WasPressedThisFrame())
            {
                // �Է� ���� Ȯ��
                Vector2 input = playerInput.actions["UIMove"].ReadValue<Vector2>();

                // ������ �Է�
                if (input.x > 0)
                {
                    if (button == sens.gameObject)
                        slider.value += 0.3f;
                    else
                        slider.value += 0.1f;
                }
                // ���� �Է�
                else if (input.x < 0)
                {
                    if (button == sens.gameObject)
                        slider.value -= 0.3f;
                    else
                        slider.value -= 0.1f;
                }
            }
        }
    }

    //���� ������ �°� ��ư ���̶���Ʈ
    void DepthCal()
    {
        switch (curDepth)
        {
            case 0:
                binding.SelectedButtonHighlight(optionButtons);
                break;

            case 1:
                binding.SelectedButtonHighlight(gameplayButtons);
                break;

            case 2:
                binding.SelectedButtonHighlight(soundButtons);
                break;
        }
    }
    /// <summary>
    /// ���� ��Ŀ�̵� �޴��� �°� �г� ü����
    /// </summary>
    /// <param name="curButton">���� ���õ� ��ư</param>
    void curTabChecker(Button curButton)
    {

        if (curDepth == 0)
        {
            if (curButton == gamePlayButton)
            {
                gameplayPanel.SetActive(true);
                soundPanel.SetActive(false);
                InputPanel(false);
            }
            else if (curButton == soundButton)
            {
                gameplayPanel.SetActive(false);
                soundPanel.SetActive(true);
                InputPanel(false);
            }
            else if (curButton == inputButton)
            {
                gameplayPanel.SetActive(false);
                soundPanel.SetActive(false);
                InputPanel(true);
            }
            else if (curButton == exitButton)
            {
                gameplayPanel.SetActive(false);
                soundPanel.SetActive(false);
                InputPanel(false);
            }
        }
    }

    void InputPanel(bool onOff)
    {
        inputPanel.SetActive(onOff);


        inputPC.SetActive(InputKey.PlayerInput.currentControlScheme == InputType.PC);
        inputConsole.SetActive(InputKey.PlayerInput.currentControlScheme == InputType.CONSOLE);
    }

    //�ڷ�ƾ�� ����� UI �ʱ�ȭ�� �ð� ������
    IEnumerator SetSelectRoutine(GameObject obj)
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(obj);
    }

    public void GamePlayButton()
    {
        StartCoroutine(SetSelectRoutine(minimapAct.gameObject));
        curDepth = 1;
        firstCo = null;
    }

    public void SoundButton()
    {
        StartCoroutine(SetSelectRoutine(totalVolume.gameObject));
        curDepth = 2;
        firstCo = null;
    }

    public void InputButton()
    {
        StartCoroutine(SetSelectRoutine(inputButton.gameObject));
        firstCo = null;
    }

    public void ExitButton()
    {
        firstCo = null;

        gameplayPanel.SetActive(true);
        soundPanel.SetActive(false);
        InputPanel(false);

        binding.CanvasChange(binding.mainCanvas.gameObject, gameObject);
        curDepth = 0;
    }

    public void ExitButton_Pause()
    {
        firstCo = null;
        curDepth = 0;

        gameplayPanel.SetActive(true);
        soundPanel.SetActive(false);
        InputPanel(false);

        gameObject.SetActive(false);
    }

    public void MinimapAct()
    {
        actChecked.SetActive(!actChecked.activeSelf);
        setting.miniMapOnBool = actChecked.activeSelf;
        StartCoroutine(SetSelectRoutine(minimapAct.gameObject));
        curDepth = 1;
    }

    public void MinimapFix()
    {
        fixChecked.SetActive(!fixChecked.activeSelf);
        setting.miniMapFixBool = fixChecked.activeSelf;
        StartCoroutine(SetSelectRoutine(minimapFix.gameObject));
        curDepth = 1;
    }

    /// <summary>
    /// ����, ���, �ʱ�ȭ ������ ��ư ���� �ʱ�ȭ
    /// </summary>
    /// <param name="list"></param>
    void ButtonReset(List<Button> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<TMP_Text>().color = Color.white;
        }
    }

    public void AcceptButton_Gameplay()
    {
        ButtonReset(gameplayButtons);
        SoundManager.PlaySFX(SoundManager.Data.UI.SettingButton);
        EventSystem.current.SetSelectedGameObject(gamePlayButton.gameObject);

        curDepth = 0;
    }

    public void CancelButton_Gameplay()
    {
        ButtonReset(gameplayButtons);
        SoundManager.PlaySFX(SoundManager.Data.UI.SettingButton);
        EventSystem.current.SetSelectedGameObject(gamePlayButton.gameObject);

        curDepth = 0;
    }

    public void DefaultButton_Gameplay()
    {
        ButtonReset(gameplayButtons);
        SoundManager.PlaySFX(SoundManager.Data.UI.SettingButton);
        EventSystem.current.SetSelectedGameObject(gamePlayButton.gameObject);

        curDepth = 0;
    }

    public void AcceptButton_Sound()
    {
        ButtonReset(soundButtons);
        SoundManager.PlaySFX(SoundManager.Data.UI.SettingButton);
        EventSystem.current.SetSelectedGameObject(soundButton.gameObject);
        curDepth = 0;
    }


    public void CancelButton_Sound()
    {
        ButtonReset(soundButtons);
        SoundManager.PlaySFX(SoundManager.Data.UI.SettingButton);
        EventSystem.current.SetSelectedGameObject(soundButton.gameObject);

        curDepth = 0;
    }

    public void DefaultButton_Sound()
    {
        ButtonReset(soundButtons);
        SoundManager.PlaySFX(SoundManager.Data.UI.SettingButton);
        EventSystem.current.SetSelectedGameObject(soundButton.gameObject);

        curDepth = 0;
    }
    void Init()
    {
        binding = GetComponentInParent<MainSceneBinding>();

        playerInput = InputKey.PlayerInput;

        optionButtons.Add(gamePlayButton = GetUI<Button>("GamePlay"));
        optionButtons.Add(soundButton = GetUI<Button>("Sound"));
        optionButtons.Add(inputButton = GetUI<Button>("Input"));
        optionButtons.Add(exitButton = GetUI<Button>("Exit"));


        gameplayPanel = GetUI("GamePlayPackage");
        soundPanel = GetUI("SoundPackage");
        inputPanel = GetUI("InputGuide");

        gameplayButtons.Add(minimapAct = GetUI<Button>("MinimapAct"));
        gameplayButtons.Add(minimapFix = GetUI<Button>("MinimapFix"));
        gameplayButtons.Add(LanguageChange = GetUI<Button>("Language"));
        gameplayButtons.Add(sens = GetUI<Button>("Sens"));
        gameplayButtons.Add(accept_Gameplay = GetUI<Button>("GamePlay_Accept"));
        gameplayButtons.Add(cancel_Gameplay = GetUI<Button>("GamePlay_Cancel"));
        gameplayButtons.Add(default_Gameplay = GetUI<Button>("GamePlay_Default"));

        actChecked = GetUI("ActChecked");
        fixChecked = GetUI("FixChecked");
        actUnChecked = GetUI("ActUnChecked");
        fixUnChecked = GetUI("FixUnChecked");

        soundButtons.Add(totalVolume = GetUI<Button>("TotalSound"));
        soundButtons.Add(bgmVolume = GetUI<Button>("BGMSound"));
        soundButtons.Add(sfxVolume = GetUI<Button>("SFXSound"));
        soundButtons.Add(accept_Sound = GetUI<Button>("Sound_Accept"));
        soundButtons.Add(cancel_Sound = GetUI<Button>("Sound_Cancel"));
        soundButtons.Add(default_Sound = GetUI<Button>("Sound_Default"));

        sensSlider = GetUI<Slider>("SensSlider");
        totalVolumeBar = GetUI<Slider>("TotalSoundBar");
        bgmVolumeBar = GetUI<Slider>("BGMSoundBar");
        sfxVolumeBar = GetUI<Slider>("SFXSoundBar");

        gamePlayButton.onClick.AddListener(GamePlayButton);
        soundButton.onClick.AddListener(SoundButton);
        inputButton.onClick.AddListener(InputButton);


        // ���ξ��� ���� ���� > �ɼ��� ��� ����
        if (SceneManager.GetActiveScene().name == SceneName.MainScene)
            exitButton.onClick.AddListener(ExitButton);
        else
            exitButton.onClick.AddListener(ExitButton_Pause);

        minimapAct.onClick.AddListener(MinimapAct);
        minimapFix.onClick.AddListener(MinimapFix);
        actChecked.GetComponent<Button>().onClick.AddListener(MinimapAct);
        fixChecked.GetComponent<Button>().onClick.AddListener(MinimapFix);
        actUnChecked.GetComponent<Button>().onClick.AddListener(MinimapAct);
        fixUnChecked.GetComponent<Button>().onClick.AddListener(MinimapFix);

        preAct = setting.miniMapOnBool;
        preFix = setting.miniMapFixBool;

        accept_Gameplay.onClick.AddListener(AcceptButton_Gameplay);
        cancel_Gameplay.onClick.AddListener(CancelButton_Gameplay);
        default_Gameplay.onClick.AddListener(DefaultButton_Gameplay);

        inputPC = GetUI("inputPC");
        inputConsole = GetUI("inputConsole");



        accept_Sound.onClick.AddListener(AcceptButton_Sound);
        cancel_Sound.onClick.AddListener(CancelButton_Sound);
        default_Sound.onClick.AddListener(DefaultButton_Sound);

    }
}
