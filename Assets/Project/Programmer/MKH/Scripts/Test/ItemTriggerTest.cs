using System.Linq;
using UnityEngine;

namespace MKH
{
    public class ItemTriggerTest : MonoBehaviour
    {
        private ItemPickUp mCurrentItem;

        [SerializeField] GameObject particle;

        [SerializeField] private InventoryMainTest mInventory;      // �κ��丮

        private void OnTriggerEnter(Collider other)
        {
            // other�� ������Ʈ���� ItemPickUp �ҷ�����
            mCurrentItem = other.transform.GetComponent<ItemPickUp>();

            // Item �±� ���� other ������Ʈ
            if (other.gameObject.tag == Tag.Item)
            {
                for (int i = 0; i < mInventory.mSlots.Length; i++)
                {
                    // ������ Ÿ���� None�� �ƴ� ��, ������ �� ��á�� ��
                    if (mCurrentItem.Item.Type != ItemType.None && mInventory.mSlots[i].Item == null)
                    {
                        // �κ��丮�� ������ �߰�
                        mInventory.AcquireItem(mCurrentItem.Item);
                        Destroy(other.gameObject);
                        GameObject obj = Instantiate(particle, transform.position, Quaternion.Euler(-90f, 0, 0));
                        Destroy(obj,0.5f);
                        return;
                    }
                }
            }
        }
    }
}
