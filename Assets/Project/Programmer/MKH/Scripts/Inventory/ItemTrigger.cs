using UnityEngine;

namespace MKH
{
    public class ItemTrigger : MonoBehaviour
    {
        private ItemPickUp mCurrentItem;

        [SerializeField] private InventoryMain mInventory;      // 인벤토리

        private void OnTriggerEnter(Collider other)
        {
            // other의 오브젝트에서 ItemPickUp 불러오기
            mCurrentItem = other.transform.GetComponent<ItemPickUp>();

            // Item 태그 붙은 other 오브젝트
            if (other.CompareTag("Item"))
            {
                // 아이템 타입이 None이 아닐 시 
                if (mCurrentItem.Item.Type != ItemType.None)
                {
                    // 인벤토리에 아이템 추가
                    mInventory.AcquireItem(mCurrentItem.Item);
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
