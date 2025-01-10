using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class MainSceneBinding : BaseUI
{
    //캔버스들
    /*[SerializeField]*/ public Canvas mainCanvas;
    /*[SerializeField]*/ public Canvas continueCanvas;
    /*[SerializeField]*/ public Canvas newCanvas;
    /*[SerializeField]*/ public Canvas optionCanvas;
    

    private void Awake()
    {
        Bind();
        Init();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //캔버스 교체 함수
    public void CanvasChange(GameObject loadCanvas, GameObject curCanvas)
    {
       
        loadCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        curCanvas.SetActive(false);
    }

    public void ButtonFirstSelect(GameObject firstButton)
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
        }

        if(!EventSystem.current.currentSelectedGameObject.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
        }
    }

    void Init()
    {
        //캔버스 바인딩
        mainCanvas = GetUI<Canvas>("MainCanvas");
        continueCanvas = GetUI<Canvas>("ContinueCanvas");
        newCanvas = GetUI<Canvas>("NewCanvas");
        optionCanvas = GetUI<Canvas>("OptionCanvas");

        
    }
}
