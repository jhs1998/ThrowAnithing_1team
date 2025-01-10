using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MKH
{
    public class BlueChipChoicePanelTest : MonoBehaviour
    {
        [SerializeField] public Button button;
        [SerializeField] GameObject blueChipSlotsParent;
        [SerializeField] public BlueChipSlot[] blueChipSlots;

        [SerializeField] GameObject choiceSlotsParnet;
        [SerializeField] public BlueChipSlot[] choiceSlots;

        [SerializeField] BlueChipList blueChipList;

        private void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(button.gameObject);
            RandomBlueChip();
        }

        private void Awake()
        {
            blueChipSlots = blueChipSlotsParent.GetComponentsInChildren<BlueChipSlot>();
            choiceSlots = choiceSlotsParnet.GetComponentsInChildren<BlueChipSlot>();
            Setting();
        }
        

        public bool AcquireEffect(AdditionalEffect effect)
        {
            for (int i = 0; i < blueChipSlots.Length; i++)
            {
                Debug.Log(i);
                if (blueChipSlots[i].Effect == null)
                {
                    blueChipSlots[i].AddEffect(effect);
                    Debug.Log(effect);
                    return true;
                }
            }
            return false;
        }

        public void Setting()
        {
            for (int i = 0; i < blueChipSlots.Length; i++)
            {
                blueChipSlots[i].SetSlot();
            }
        }

        private void Choice()
        {
            for (int i = 0; i < choiceSlots.Length; i++)
            {
                choiceSlots[i].SetSlot();
            }
        }

        public void RandomBlueChip()
        {
            for (int i = 0; i < choiceSlots.Length; i++)
            {
                AdditionalEffect effect = blueChipList[Random.Range(0, blueChipList.Count)];
                choiceSlots[i].AddEffect(effect);
                Debug.Log(effect.Name);
            }
        }
    }
}
