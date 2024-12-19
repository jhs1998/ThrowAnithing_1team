using MKH;
using UnityEngine;

public class ItemActionManager : MonoBehaviour
{
    [SerializeField] private EquipmentInventory mEquipmentInventory;
    [SerializeField] private InventoryMain mManinInventory;

    public bool UseItem(Item item, InventorySlot calledSlot = null)
    {
        switch (item.Type)
        {
            case ItemType.Helmet:
            case ItemType.Shirts:
            case ItemType.Glasses:
            case ItemType.Gloves:
            case ItemType.Pants:
            case ItemType.Earring:
            case ItemType.Ring:
            case ItemType.Shoes:
            case ItemType.Necklace:
                {
                    InventorySlot equipmentSlot = mEquipmentInventory.GetEquipmentSlot(item.Type);

                    Item tempItem = equipmentSlot.Item;

                    equipmentSlot.AddItem(item);

                    if (tempItem != null)
                    {
                        calledSlot.ClearSlot();
                    }

                    mEquipmentInventory.CalculateEffect();
                    break;
                }

        }
        return true;
    }


}
