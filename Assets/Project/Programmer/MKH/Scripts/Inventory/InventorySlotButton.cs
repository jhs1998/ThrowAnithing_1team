using MKH;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotButton : BaseUI, ISelectHandler
{
    [SerializeField] bool _isFirstSelect;

     private InventoryController _controller;

     private Button _button;
     private GameObject _outline;
     
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
    private void Update()
    {
    }
    public void OnSelect(BaseEventData eventData)
    {
        _controller.ChangeSelectButton(_button);
    }
}
