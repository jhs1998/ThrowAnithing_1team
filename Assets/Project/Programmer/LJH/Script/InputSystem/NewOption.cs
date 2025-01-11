using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewOption : BaseUI
{
    MainSceneBinding binding;
    // 현재 상태
    // 0 = 옵션
    // 1 = 게임플레이
    // 2 = 소리
    int curDepth;

    // 뎁스1 버튼 목록
    Button gamePlayButton;
    Button soundButton;
    Button inputButton;
    Button exitButton;

    List<Button> optionButtons = new();

    //뎁스2 버튼 목록 - 게임플레이
    Button minimapAct;
    Button minimapFix;
    Button LanguageChange;
    Button sens;

    Button accept_Gameplay;
    Button cancel_Gameplay;
    Button default_Gameplay;

    List<Button> gameplayButtons = new();

    //뎁스2 버튼 목록 - 소리
    Button totalVolume;
    Button bgmVolume;
    Button sfxVolume;

    Button accept_Sound;
    Button cancel_Sound;
    Button default_Sound;

    List<Button> soundButtons = new();


    GameObject gameplayPannel;
    GameObject soundPannel;
    GameObject inputPannel;


    private void Awake()
    {
        Bind();
        Init();
    }
    private void OnEnable()
    {
    }

    private void OnDisable()
    {

    }

    void Start()
    {
        //옵션창 열었을때 현재 버튼 목록 0으로 설정
        curDepth = 0;
    }

    void Update()
    {
        EventSystem.current.IsPointerOverGameObject(1);
        Debug.Log(EventSystem.current.IsPointerOverGameObject());

        binding.ButtonFirstSelect(gamePlayButton.gameObject);

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
        //현재 버튼을 입력받아서 필요한 패널 띄워줌
        curTabChecker(EventSystem.current.currentSelectedGameObject.GetComponent<Button>());
    }

    void curTabChecker(Button curButton)
    {
        if (curButton == gamePlayButton)
        {
            gameplayPannel.SetActive(true);
            soundPannel.SetActive(false);
            inputPannel.SetActive(false);
        }
        else if (curButton == soundButton)
        {
            gameplayPannel.SetActive(false);
            soundPannel.SetActive(true);
            inputPannel.SetActive(false);
        }
        else if (curButton == inputButton)
        {
            gameplayPannel.SetActive(false);
            soundPannel.SetActive(false);
            inputPannel.SetActive(true);
        }
    }


    public void GamePlayButton()
    {
        curDepth = 1;
        EventSystem.current.SetSelectedGameObject(minimapAct.gameObject);
    }

    public void SoundButton()
    {
        curDepth = 2;
        EventSystem.current.SetSelectedGameObject(totalVolume.gameObject);
        
    }

    public void InputButton()
    {

    }

    public void ExitButton()
    {
        binding.CanvasChange(binding.mainCanvas.gameObject, gameObject);
    }


    void Init()
    {
        binding = GetComponentInParent<MainSceneBinding>();

        optionButtons.Add(gamePlayButton = GetUI<Button>("GamePlay"));
        optionButtons.Add(soundButton = GetUI<Button>("Sound"));
        optionButtons.Add(inputButton = GetUI<Button>("Input"));
        optionButtons.Add(exitButton = GetUI<Button>("Exit"));


        gameplayPannel = GetUI("GamePlayPackage");
        soundPannel = GetUI("SoundPackage");
        inputPannel = GetUI("InputGuide");

        gameplayButtons.Add(minimapAct = GetUI<Button>("MinimapAct"));
        gameplayButtons.Add(minimapFix = GetUI<Button>("MinimapFix"));
        gameplayButtons.Add(LanguageChange = GetUI<Button>("Language"));
        gameplayButtons.Add(sens = GetUI<Button>("Sens"));
        gameplayButtons.Add(accept_Gameplay = GetUI<Button>("GamePlay_Accept"));
        gameplayButtons.Add(cancel_Gameplay = GetUI<Button>("GamePlay_Cancel"));
        gameplayButtons.Add(default_Gameplay = GetUI<Button>("GamePlay_Default"));

        soundButtons.Add(totalVolume = GetUI<Button>("TotalSound"));
        soundButtons.Add(bgmVolume = GetUI<Button>("BGMSound"));
        soundButtons.Add(sfxVolume = GetUI<Button>("SFXSound"));
        soundButtons.Add(accept_Sound = GetUI<Button>("Sound_Accept"));
        soundButtons.Add(cancel_Sound = GetUI<Button>("Sound_Cancel"));
        soundButtons.Add(default_Sound = GetUI<Button>("Sound_Default"));

        gamePlayButton.onClick.AddListener(GamePlayButton);
        soundButton.onClick.AddListener(SoundButton);

    }
}
