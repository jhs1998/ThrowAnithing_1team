using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewMainScene : BaseUI
{
    MainSceneBinding binding;
    
    //버튼들
    Button continueButton;
    Button newButton;
    Button optionButton;
    Button exitButton;


    private void Awake()
    {
        Bind();
        Init();
    }
    private void OnEnable()
    {
    }
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
    }

    void Update()
    {
        binding.ButtonFirstSelect(continueButton.gameObject);
    }


    public void EnterContinue()
    {
        binding.CanvasChange(binding.continueCanvas.gameObject, gameObject);
    }

    public void EnterNew()
    {
        binding.CanvasChange(binding.newCanvas.gameObject, gameObject);
    }

    public void EnterOption()
    {
        binding.CanvasChange(binding.optionCanvas.gameObject, gameObject);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        //Comment : 유니티 에디터상에서 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //Comment : 빌드 상에서 종료
        Application.Quit();
#endif
    }
    void Init()
    {
        binding = GetComponentInParent<MainSceneBinding>();



        //버튼 바인딩 및 버튼 주입
        continueButton = GetUI<Button>("Continue");
        newButton = GetUI<Button>("New");
        optionButton = GetUI<Button>("Option");
        exitButton = GetUI<Button>("Exit");

        //버튼 주입
        continueButton.onClick.AddListener(EnterContinue);
        newButton.onClick.AddListener(EnterNew);
        optionButton.onClick.AddListener(EnterOption);
        exitButton.onClick.AddListener(ExitGame);

    }
}
