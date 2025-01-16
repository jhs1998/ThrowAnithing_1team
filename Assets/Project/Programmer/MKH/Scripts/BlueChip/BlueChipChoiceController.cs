using System.Collections;
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
        [SerializeField] GameObject errorPopUp;
        [SerializeField] BlueChipPanel blueChipPanel;
        [SerializeField] BlueChipChoicePanel blueChipChoicePanel;
        [SerializeField] BlueChipList blueChipList;
        int popUpChoice;

        [SerializeField] AudioClip choiceClip;
        [SerializeField] AudioClip removeClip;
        [SerializeField] AudioClip cancelClip;
        [SerializeField] AudioClip popUpClip;

        [SerializeField] GameObject effectUI;
        [SerializeField] GameObject choiceFinishEffect;

        PlayerController m_player;
        PlayerController _player
        {
            get
            {
                if (m_player == null)
                {
                    m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                }
                return m_player;
            }
            set { m_player = value; }
        }

        public void Canecel()
        {
            SoundManager.PlaySFX(cancelClip);
            CloseUI();
        }

        public void Choice(int number)
        {
            for (int i = 0; i < blueChipPanel.mSlots.Length; i++)
            {
                if (blueChipPanel.mSlots[i].Effect == null)
                {
                    SoundManager.PlaySFX(choiceClip);
                    blueChipPanel.mSlots[i].AddEffect(blueChipChoicePanel.choiceSlots[number].Effect);
                    blueChipChoicePanel.blueChipSlots[i].AddEffect(blueChipChoicePanel.choiceSlots[number].Effect);
                    _player.AddAdditional(blueChipChoicePanel.choiceSlots[number].Effect);
                    blueChipChoicePanel.blueChipList.RemoveAt(blueChipChoicePanel.choiceSlots[number].ListIndex);

                    GameObject obj1 = ObjectPool.GetPool(choiceFinishEffect, effectUI.transform.position, Quaternion.identity, 1f);
                    obj1.transform.SetParent(effectUI.transform);

                    CloseUI();
                    return;
                }
                else if (blueChipPanel.mSlots[0].Effect != null
                && blueChipPanel.mSlots[1].Effect != null
                && blueChipPanel.mSlots[2].Effect != null
                && blueChipPanel.mSlots[3].Effect != null)
                {
                    ErrorPopUp();
                }
            }
        }

        public void PopUp(int number)
        {
            if (blueChipChoicePanel.blueChipSlots[number].Effect != null)
            {
                SoundManager.PlaySFX(popUpClip);
                popUp.SetActive(true);
                popUpChoice = number;
            }
        }

        public void ErrorPopUp()
        {
            StartCoroutine(Error());
        }

        public void Remove()
        {
            SoundManager.PlaySFX(removeClip);
            blueChipChoicePanel.blueChipList.Add(blueChipPanel.mSlots[popUpChoice].Effect);
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

        IEnumerator Error()
        {
            errorPopUp.SetActive(true);
            yield return 1f.GetDelay();
            errorPopUp.SetActive(false);
        }

        private void CloseUI()
        {
            InputKey.SetActionMap(InputType.GAMEPLAY);
            gameObject.SetActive(false);

        }

        public void ClearBlueChipForTester()
        {
            for (int i = 0; i < blueChipPanel.mSlots.Length; i++)
            {
                blueChipChoicePanel.blueChipList.Add(blueChipPanel.mSlots[i].Effect);
                blueChipPanel.mSlots[i].ClearSlot();
                blueChipChoicePanel.blueChipSlots[i].ClearSlot();
            }
        }
        public bool AddBlueChipForTester(AdditionalEffect effect, int index)
        {
            if (index >= blueChipPanel.mSlots.Length)
                return false;


            blueChipPanel.mSlots[index].AddEffect(effect);
            blueChipChoicePanel.blueChipSlots[index].AddEffect(effect);
            return true;
        }
    }
}
