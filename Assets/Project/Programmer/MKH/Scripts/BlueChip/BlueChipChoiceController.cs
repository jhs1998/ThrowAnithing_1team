using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace MKH
{
    public class BlueChipChoiceController : MonoBehaviour
    {
        [Inject]
        PlayerData playerData;

        [SerializeField] Button[] removeButtons;
        [SerializeField] GameObject popUp;
        [SerializeField] BlueChipPanel blueChipPanel;
        [SerializeField] BlueChipChoicePanel blueChipChoicePanel;
        int popUpChoice;
        PlayerController m_player;
        PlayerController _player
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
            
        }

        public void Update()
        {

        }

        public void Canecel()
        {
            CloseUI();

            blueChipPanel = playerData.Inventory.BlueChipPanel;
            blueChipChoicePanel = playerData.Inventory.BlueChipChoicePanel;
        }

        public void Choice(int number)
        {
            for (int i = 0; i < blueChipPanel.mSlots.Length; i++)
            {
                if (blueChipPanel.mSlots[i].Effect == null)
                {
                    blueChipPanel.mSlots[i].AddEffect(blueChipChoicePanel.choiceSlots[number].Effect);
                    blueChipChoicePanel.blueChipSlots[i].AddEffect(blueChipChoicePanel.choiceSlots[number].Effect);
                    _player.AddAdditional(blueChipChoicePanel.choiceSlots[number].Effect);


                    CloseUI();
                    return;
                }
            }
        }

        public void PopUp(int number)
        {
            popUp.SetActive(true);
            popUpChoice = number;
        }

        public void Remove()
        {
            _player.RemoveAdditional(blueChipPanel.mSlots[popUpChoice].Effect);
            blueChipPanel.mSlots[popUpChoice].ClearSlot();
            blueChipChoicePanel.blueChipSlots[popUpChoice].ClearSlot();
            popUp.SetActive(false);
            EventSystem.current.SetSelectedGameObject(blueChipChoicePanel.button.gameObject);
        }

        public void ClosePopUp()
        {
            popUp.SetActive(false);
            EventSystem.current.SetSelectedGameObject(blueChipChoicePanel.button.gameObject);
        }

        private void CloseUI()
        {
            InputKey.SetActionMap(ActionMap.GamePlay);
            gameObject.SetActive(false);

        }
    }
}
