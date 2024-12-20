using MKH;
using UnityEngine;

namespace MKH
{
    public class ItemActionManager : MonoBehaviour
    {
        [SerializeField] private EquipmentInventory mEquipmentInventory;
        [SerializeField] private InventoryMain mManinInventory;


        public bool UseItem(Item item)
        {
            InventorySlot equipmentSlot = mEquipmentInventory.GetEquipmentSlot(item.Type);

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
    }
}
