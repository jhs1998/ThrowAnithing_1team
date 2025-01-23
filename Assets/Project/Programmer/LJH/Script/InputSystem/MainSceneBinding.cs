using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class MainSceneBinding : BaseUI
{
    //ĵ������
    [HideInInspector] public Canvas mainCanvas;
    [HideInInspector] public Canvas continueCanvas;
    [HideInInspector] public Canvas newCanvas;
    [HideInInspector] public Canvas optionCanvas;


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

    /// <summary>
    /// ĵ���� ��ü �Լ�
    /// </summary>
    /// <param name="loadCanvas">�ҷ��� ĵ����</param>
    /// <param name="curCanvas">���� ĵ����(���� �����ִ� ĵ����)</param>
    public void CanvasChange(GameObject loadCanvas, GameObject curCanvas)
    {

        loadCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        curCanvas.SetActive(false);
    }

    /// <summary>
    /// ���õ� ��ư ���� ����
    /// </summary>
    /// <param name="buttons">��Ŀ�̵� ��ư �迭</param>
    public void SelectedButtonHighlight(List<Button> buttons)
    {
        if (EventSystem.current.currentSelectedGameObject != null)
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].GetComponent<TMP_Text>().color = Color.white;

                if (buttons[i] == EventSystem.current.currentSelectedGameObject.GetComponent<Button>())
                {
                    buttons[i].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
                }
            }
    }
    /// <summary>
    /// ���õ� ���� ���� ����
    /// </summary>
    /// <param name ="slots">���� ���� ��ư ����Ʈ</param>
    public void SelectedSlotHighlight(List<Button> slots)
    {
        if (EventSystem.current.currentSelectedGameObject != null)
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].GetComponent<Image>().color = new Color(1, 0.5f, 0, 0.1f);

                if (slots[i] == EventSystem.current.currentSelectedGameObject.GetComponent<Image>())
                    slots[i].GetComponent<Image>().color = new Color(1, 0.5f, 0, 1);
            }
    }

    /// <summary>
    /// ������ ���Խ� �ʱ� ��ư ����
    /// </summary>
    /// <param name="firstButton">��Ŀ�̵� ��ư ����Ʈ�� ù��° ��ư</param>
    public void ButtonFirstSelect(GameObject firstButton)
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
        }
    }

    void Init()
    {
        //ĵ���� ���ε�
        mainCanvas = GetUI<Canvas>("MainCanvas");
        continueCanvas = GetUI<Canvas>("ContinueCanvas");
        newCanvas = GetUI<Canvas>("NewCanvas");
        optionCanvas = GetUI<Canvas>("OptionCanvas");


    }
}
