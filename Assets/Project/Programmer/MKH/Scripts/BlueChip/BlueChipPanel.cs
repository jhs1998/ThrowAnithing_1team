using UnityEngine;

namespace MKH
{
    public class BlueChipPanel : BlueChipBase
    {
        public static bool IsBlueChipActive = false;

        [SerializeField] GameObject inventory;

        new private void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            TryOpenPanel();
        }

        private void TryOpenPanel()
        {
            if (inventory.activeSelf == true)
            {
                if (Input.GetKey(KeyCode.CapsLock))
                {
                    OpenPanel();
                }
                else
                {
                    ClosePanel();
                }
            }
        }

        private void OpenPanel()
        {
            mBlueChipBase.SetActive(true);
            IsBlueChipActive = true;
        }

        private void ClosePanel()
        {
            mBlueChipBase.SetActive(false);
            IsBlueChipActive = false;
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
    }
}
