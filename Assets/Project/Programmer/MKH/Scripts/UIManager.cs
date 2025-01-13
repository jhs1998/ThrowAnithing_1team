using UnityEngine;

namespace MKH
{
    public class UIManager : BaseBinder
    {
        [SerializeField] GameObject _Inventory;
        [SerializeField] GameObject _EquipInventory;
        [SerializeField] GameObject _State;
        [SerializeField] GameObject _BlueChipPanel;
        [SerializeField] GameObject _BlueChipChoice;
        [SerializeField] GameObject _BlueChipChoicePanel;
        private PlayerController _player;
        [SerializeField]
        PlayerController player
        {
            get
            {
                if (_player == null)
                {
                    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                }
                return _player;
            }
            set { _player = value; }
        }

        private bool _isOpenInventory;
        private void Awake()
        {
            Bind();
            _Inventory = GetObject("Inventory");
            _EquipInventory = GetObject("EquipmentInventory");
            _State = GetObject("State");
            _BlueChipPanel = GetObject("BlueChipPanel");
            _BlueChipChoice = GetObject("BlueChipChoice");
            _BlueChipChoicePanel = GetObject("BlueChipChoicePanel");
        }

        private void Start()
        {
            _Inventory.SetActive(false);
            _EquipInventory.SetActive(false);
            _State.SetActive(false);
            _BlueChipChoice.SetActive(false);
            _BlueChipPanel.SetActive(false);
            _BlueChipChoicePanel.SetActive(false);
        }

        private void Update()
        {
            OpenBlueChip();
            Inventory();
            CloseBlueChip();
        }

        private void Inventory()
        {
            if (InputKey.GetButtonDown(InputKey.Inventory))
            {
                if (_Inventory.activeSelf)
                    return;
                if (InputKey.GetActionMap() == ActionMap.UI)
                    return;

                _isOpenInventory = true;
                _Inventory.SetActive(true);
                _EquipInventory.SetActive(true);
                _State.SetActive(true);

                InputKey.SetActionMap(ActionMap.UI);
            }

            if (InputKey.GetButtonDown(InputKey.PopUpClose))
            {
                if (_BlueChipPanel.activeSelf)
                    return;
                if (_isOpenInventory == false)
                    return;


                _isOpenInventory = false;
                _Inventory.SetActive(false);
                _EquipInventory.SetActive(false);
                _State.SetActive(false);
                InputKey.SetActionMap(ActionMap.GamePlay);
            }
        }

        private void OpenBlueChip()
        {
            if (!_Inventory.activeSelf)
                return;

            if (InputKey.GetButtonDown(InputKey.Inventory))
            {
                _BlueChipPanel.SetActive(true);
            }
        }

        private void CloseBlueChip()
        {
            if (!_Inventory.activeSelf)
                return;

            if (InputKey.GetButtonDown(InputKey.PopUpClose))
            {
                _BlueChipPanel.SetActive(false);
            }
        }
    }
}
