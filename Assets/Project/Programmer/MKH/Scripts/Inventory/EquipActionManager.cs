
using UnityEngine;

namespace MKH
{
    public class EquipActionManager : MonoBehaviour
    {
        [Header("��� �κ��丮")]
        [SerializeField] private EquipmentInventory mEquipmentInventory;
        [SerializeField] private InventoryMain minventoryMain;

        // ��� ��ü
        public bool UseEquip(Item item)
        {
            InventorySlot equipmentSlot = mEquipmentInventory.GetEquipmentSlot(item.Type);
            InventorySlot inventorySlot = minventoryMain.IsCanAquireItem(item);

            Item tempItem = equipmentSlot.Item;

            equipmentSlot.AddItem(item);

            if (tempItem != null)
            {
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
                InventorySlot equipmentSlot = mEquipmentInventory.GetEquipmentSlot(item.Type);

                equipmentSlot.ClearSlot();
            }

            mEquipmentInventory.CalculateEffect();
            return true;
        }
    }
}
