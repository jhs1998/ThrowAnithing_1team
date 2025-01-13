using MKH;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotButton : BaseUI, ISelectHandler
{
    [SerializeField] bool _isFirstSelect;
    [SerializeField] int _index;

    public GameObject Outline;
    private InventoryController _controller;
    private InventorySlot _slot;
    private Button _button;

    private void Awake()
    {
        Bind();
        _controller = GetComponentInParent<InventoryController>();
        _slot = GetComponentInParent<InventorySlot>();
        Outline = GetUI("Outline");
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
        _controller.ChangeSelectButton(_slot);
    }
}
