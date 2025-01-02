using UnityEngine;

namespace MKH
{
    public class EquipActionManagerTest : MonoBehaviour
    {
        [Header("장비 인벤토리")]
        [SerializeField] private EquipmentInventoryTest mEquipmentInventory;

        // 장비 교체
        public bool UseEquip(Item item)
        {
            InventorySlotTest equipmentSlot = mEquipmentInventory.GetEquipmentSlot(item.Type);

            Item tempItem = equipmentSlot.Item;

            if (tempItem != null)
            {
                equipmentSlot.ClearSlot();
                equipmentSlot.AddItem(item);
            }
            else
            {
                equipmentSlot.AddItem(item);
            }

            mEquipmentInventory.CalculateEffect();
            return true;
        }

        // 장비 삭제
        public bool RemoveEquip(Item item)
        {
            if (item != null)
            {
                InventorySlotTest equipmentSlot = mEquipmentInventory.GetEquipmentSlot(item.Type);

                equipmentSlot.ClearSlot();
            }

            mEquipmentInventory.CalculateEffect();
            return true;
        }
    }
}
