using UnityEngine;
using Zenject;

namespace MKH
{
    public class ItemTrigger : MonoBehaviour
    {
        [Inject]
        private PlayerData playerData;

        private ItemPickUp mCurrentItem;

        [SerializeField] private InventoryMain mInventory;      // �κ��丮

        [SerializeField] GameObject effect;
        [SerializeField] AudioClip clip;

        private void Awake()
        {
            mInventory = playerData.Inventory.InventoryMain;
        }

        private void OnTriggerEnter(Collider other)
        {
            // other�� ������Ʈ���� ItemPickUp �ҷ�����
            mCurrentItem = other.transform.GetComponent<ItemPickUp>();

            // Item �±� ���� other ������Ʈ
            if (other.gameObject.tag == Tag.Item)
            {
                for (int i = 0; i < mInventory.mSlots.Length; i++)
                {
                    // ������ Ÿ���� None�� �ƴϰ� �κ��丮 ������ null �� ��
                    if (mCurrentItem.Item.Type != ItemType.None && mInventory.mSlots[i].Item == null)
                    {
                        // �κ��丮�� ������ �߰�
                        ObjectPool.GetPool(effect, transform.position + new Vector3(0, 1, 0), Quaternion.identity, 0.3f);
                        SoundManager.PlaySFX(clip);
                        mInventory.AcquireItem(mCurrentItem.Item);
                        Destroy(other.gameObject);
                        return;
                    }
                }
            }
        }
    }
}
