using UnityEngine;

namespace MKH
{
    public class InventoryMain : InventoryBase
    {
        public static bool IsInventoryActive = false;


        new private void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            TryOpenInventory();
        }

        private void TryOpenInventory()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!IsInventoryActive)
                    OpenInventory();
                else
                    CloseInventory();
            }
        }

        private void OpenInventory()
        {
            mInventoryBase.SetActive(true);
            IsInventoryActive = true;
        }

        private void CloseInventory()
        {
            mInventoryBase.SetActive(false);
            IsInventoryActive = false;
        }

        public void AcquireItem(Item item)
        {
            for (int i = 0; i < mSlots.Length; i++)
            {
                //마스크를 사용하여 해당 슬롯이 마스크에 허용되는 위치인경우에만 아이템을 집어넣도록 한다.
                if (mSlots[i].Item == null && mSlots[i].IsMask(item))
                {
                    mSlots[i].AddItem(item);
                    return;
                }
            }

        }
    }
}
