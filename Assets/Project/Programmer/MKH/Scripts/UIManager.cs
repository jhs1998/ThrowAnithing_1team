using UnityEngine;
using UnityEngine.EventSystems;

namespace MKH
{
    public class UIManager : BaseBinder
    {
        [Header("���� ������Ʈ")]
        [SerializeField] GameObject _Inventory;                 // �κ��丮
        [SerializeField] GameObject _EquipInventory;            // ���
        [SerializeField] GameObject _State;                     // �ɷ�ġ
        [SerializeField] GameObject _BlueChipPanel;             // ����Ĩ �г�
        [SerializeField] GameObject _BlueChipChoice;            // ����Ĩ �Ա� �� �ȳ�
        [SerializeField] GameObject _BlueChipChoicePanel;       // ����Ĩ ���� �г�

        [Header("ȿ����")]
        [SerializeField] AudioClip open;                        // �� �� ȿ����
        [SerializeField] AudioClip close;                       // ���� �� ȿ����

        private PlayerController _player;
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

        #region �κ��丮
        private void Inventory()
        {
            // ����
            if (InputKey.GetButtonDown(InputKey.InvenOpen))
            {
                if (InputKey.GetActionMap() == ActionMap.UI)
                    return;

                SoundManager.PlaySFX(open);
                _Inventory.SetActive(true);
                _EquipInventory.SetActive(true);
                _State.SetActive(true);

                InputKey.SetActionMap(InputType.UI);
            }

            // �ݱ�
            if (InputKey.GetButtonDown(InputKey.InvenClose))
            {
                if (_BlueChipPanel.activeSelf)
                    return;

                _Inventory.SetActive(false);
                _EquipInventory.SetActive(false);
                _State.SetActive(false);
                InputKey.SetActionMap(ActionMap.GamePlay);
                SoundManager.PlaySFX(close);
            }
        }
        #endregion

        #region ����Ĩ
        // ����
        private void OpenBlueChip()
        {
            if (!_Inventory.activeSelf)
                return;

            if (InputKey.GetButtonDown(InputKey.InvenOpen))
            {
                SoundManager.PlaySFX(open);
                _BlueChipPanel.SetActive(true);
                EventSystem.current.currentSelectedGameObject.SetActive(false);
            }
        }

        // �ݱ�
        private void CloseBlueChip()
        {
            if (!_Inventory.activeSelf)
                return;

            if (InputKey.GetButtonDown(InputKey.InvenClose))
            {
                _BlueChipPanel.SetActive(false);
                SoundManager.PlaySFX(close);
                EventSystem.current.currentSelectedGameObject.SetActive(true);
            }
        }
        #endregion
    }
}
