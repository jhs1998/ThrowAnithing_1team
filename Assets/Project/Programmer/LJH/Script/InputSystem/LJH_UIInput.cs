using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LJH_UIInput : MonoBehaviour
{
    PlayerInput playerInput;
    Button button;
    private void Start()
    {
        
    }
    void Update()
    {
        button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        changeColor();
    }

    void changeColor()
    {
        ColorBlock colorBlock = button.colors;

        //(r, g, b, a) 기준 빨간색으로 normal Color 지정
        colorBlock.normalColor = Color.red;

        button.colors = colorBlock;
    }
}
