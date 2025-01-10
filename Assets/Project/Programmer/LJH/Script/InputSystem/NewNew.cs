using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class NewNew : BaseUI
{
    MainSceneBinding binding;
    PlayerActions input;

    Button slot1;
    Button slot2;
    Button slot3;

    private void Awake()
    {
        Bind();
        Init();
    }
    private void OnEnable()
    {
        input.Enable();
        input.UI.Cancel.performed += OnCancel;

    }

    private void OnDisable()
    {
        input.UI.Cancel.performed -= OnCancel;
        input.Disable();
    }
    void Start()
    {
    }

    private void Update()
    {
        binding.ButtonFirstSelect(slot1.gameObject);
    }


    void OnCancel(InputAction.CallbackContext context)
    {
        Debug.Log("취소실행");
        Cancel();
    }

    void Cancel()
    {
        binding.CanvasChange(binding.mainCanvas.gameObject, gameObject);
    }


    void Init()
    {
        binding = GetComponentInParent<MainSceneBinding>();
        input = new PlayerActions();

        slot1 = GetUI<Button>("NewSlot1");
        slot2 = GetUI<Button>("NewSlot2");
        slot3 = GetUI<Button>("NewSlot3");
    }
}
