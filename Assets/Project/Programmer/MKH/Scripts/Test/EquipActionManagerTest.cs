using UnityEngine;

namespace MKH
{
    public class EquipActionManagerTest : MonoBehaviour
    {
        [Header("��� �κ��丮")]
        [SerializeField] private EquipmentInventoryTest mEquipmentInventory;
        [SerializeField] private InventoryMainTest mInventory;

        // ��� ��ü
        public bool UseEquip(Item item)
        {
            InventorySlotTest equipmentSlot = mEquipmentInventory.GetEquipmentSlot(item.Type);
            InventorySlotTest inventorySlot = mInventory.IsCanAquireItem(item);

            Item tempItem = equipmentSlot.Item;

            equipmentSlot.AddItem(item);

            if (tempItem != null)
            {
                Item _itme = tempItem;
                equipmentSlot.ClearSlot();
                equipmentSlot.AddItem(item);
                inventorySlot.AddItem(tempItem);
            }

            mEquipmentInventory.CalculateEffect();
            return true;
        }

        // ��� ����
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
