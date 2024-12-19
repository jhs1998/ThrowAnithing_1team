using UnityEngine;

namespace MKH
{
    public class ItemTrigger : MonoBehaviour
    {
        private ItemPickUp mCurrentItem;

        [SerializeField] private InventoryMain mInventory;

        private void OnTriggerEnter(Collider other)
        {
            mCurrentItem = other.transform.GetComponent<ItemPickUp>();

            if (other.CompareTag("Item"))
            {
                print("´ê¾Ò´Ù.1");
                if (mCurrentItem.Item.Type != ItemType.None)
                {
                    print("´ê¾Ò´Ù.2");
                    mInventory.AcquireItem(mCurrentItem.Item);
                }
            }
        }
    }
}
