using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryFirstButton : MonoBehaviour
{
    [SerializeField] GameObject firstButton;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
    }
}
