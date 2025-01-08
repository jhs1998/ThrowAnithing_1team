using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MKH
{
    public class BlueChipChoicePanelTest : BlueChipBase
    {
        [SerializeField] Button button;

        private void OnEnable()
        {
            if (gameObject.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(button.gameObject);
            }
        }

        new private void Awake()
        {
            base.Awake();
            Setting();
        }

        public bool AcquireEffect(AdditionalEffect effect)
        {
            for (int i = 0; i < mSlots.Length; i++)
            {
                Debug.Log(i);
                if (mSlots[i].Effect == null)
                {
                    mSlots[i].AddEffect(effect);
                    Debug.Log(effect);
                    return true;
                }
            }
            return false;
        }

        public void Setting()
        {
            for (int i = 0; i < mSlots.Length; i++)
            {
                mSlots[i].SetSlot();
            }
        }
    }
}
