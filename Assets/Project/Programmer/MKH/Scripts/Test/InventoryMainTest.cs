using TMPro;
using UnityEngine;

namespace MKH
{
    public class InventoryMainTest : InventoryBaseTest
    {
        public static bool IsInventoryActive = false;
        [SerializeField] GameObject state;

        [SerializeField] GameObject blueChipPanel;


        new private void Awake()
        {
            base.Awake();
            state.SetActive(false);
        }

        private void Update()
        {
            //TryOpenInventory();
            //TryCloseInventory();
        }

        private void TryOpenInventory()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (!IsInventoryActive)
                    OpenInventory();
            }

        }

        private void TryCloseInventory()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (blueChipPanel.activeSelf)
                {
                    Debug.Log("1");
                    return;
                }
                
                if (IsInventoryActive && !blueChipPanel.activeSelf)
                {
                    CloseInventory();
                }
            }
        }

        private void OpenInventory()
        {
            mInventoryBase.SetActive(true);
            IsInventoryActive = true;
            state.SetActive(true);
        }

        private void CloseInventory()
        {
            mInventoryBase.SetActive(false);
            IsInventoryActive = false;
            state.SetActive(false);
        }

        public void AcquireItem(Item item)
        {
            Item _item = item.Create();

            for (int i = 0; i < mSlots.Length; i++)
            {
                //마스크를 사용하여 해당 슬롯이 마스크에 허용되는 위치인경우에만 아이템을 집어넣도록 한다.
                if (mSlots[i].Item == null && mSlots[i].IsMask(_item))
                {
                    mSlots[i].AddItem(_item);
                    return;
                }
            }
        }
    }
}
