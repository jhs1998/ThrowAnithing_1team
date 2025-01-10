using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewOption : BaseUI
{
    MainSceneBinding binding;

    Button gamePlayButton;
    Button soundButton;
    Button inputButton;
    Button exitButton;

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

    }

    void Update()
    {
        binding.ButtonFirstSelect(gamePlayButton.gameObject);
    }

    void GamePlayButton()
    {

    }

    void SoundButton()
    {

    }

    void InputButton()
    { 
    
    }

    void ExitButton()
    {
        binding.CanvasChange(binding.mainCanvas.gameObject, gameObject);
    }


    void Init()
    {
        binding = GetComponentInParent<MainSceneBinding>();

        gamePlayButton = GetUI<Button>("GamePlay");
        soundButton = GetUI<Button>("Sound");
        inputButton = GetUI<Button>("Input");
        exitButton = GetUI<Button>("Exit");


    }
}
