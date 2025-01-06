using TMPro;
using UnityEngine;

namespace MKH
{
    public class InventoryMain : InventoryBase
    {
        [SerializeField] GameObject state;

        new public void Awake()
        {
            base.Awake();
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

        public InventorySlot IsCanAquireItem(Item item)
        {
            foreach (InventorySlot slot in mSlots)
            {
                if (slot.Item == null)
                {
                    return slot;
                }
            }
            return null;
        }
    }
}
