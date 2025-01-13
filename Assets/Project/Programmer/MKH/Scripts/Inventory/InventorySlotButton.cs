using MKH;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotButton : BaseUI, ISelectHandler
{
    [SerializeField] bool _isFirstSelect;

    [SerializeField] private InventoryController _controller;

    [SerializeField] private Button _button;
    [SerializeField] private GameObject _outline;
     
    private void Awake()
    {
        Bind();
        _controller = GetComponentInParent<InventoryController>();

        _outline = GetUI("Outline");
        _button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        if (_isFirstSelect == true)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
    public void OnSelect(BaseEventData eventData)
    {   
        _controller.ChangeSelectButton(_button);
    }
}
