using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LJH_UIInput : MonoBehaviour
{
    
    public GameObject firstButton;

    void Start()
    {
        // ù ��° ��ư ��Ŀ��
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    void Update()
    {
        // Ű���� �Է¿� ���� ��ư Ž��
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameObject current = EventSystem.current.currentSelectedGameObject;
            if (current != null)
            {
                SelectNextButton(current);
            }

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject current = EventSystem.current.currentSelectedGameObject;
            if (current != null)
            {
                ClickButton(current);
            }

        }
    }

    void SelectNextButton(GameObject current)
    {
        // ���ϴ� ��ư Ž�� ������ ���⿡ �߰�
        var nextButton = current.GetComponent<Selectable>().FindSelectableOnDown();
        if (nextButton != null)
        {
            EventSystem.current.SetSelectedGameObject(nextButton.gameObject);
        }
    }

    void ClickButton(GameObject current)
    {
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        
    }
}

