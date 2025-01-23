using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace MKH
{
    public class InventorySlot : MonoBehaviour
    {
        // ������
        private Item mItem;
        public Item Item { get { return mItem; } set { mItem = value; } }

        [Header("�ش� ���Կ� �� �� �ִ� Ÿ��")]
        [SerializeField] private ItemType mSlotMask;

        [Header("���Կ� �ִ� UI ������Ʈ")]
        [SerializeField] private Image mItemImage;
        public Image ItemImage { get {  return mItemImage; } set { mItemImage = value; } }

        [Header("��� ��ü �Ŵ���")]
        [SerializeField] private EquipActionManager equipActionManager;

        public bool isEquip;

        public InventorySlotButton SlotButton;

        private void Awake()
        {
            //SlotButton = GetComponentInChildren<InventorySlotButton>();
        }
        // ������ �̹��� ���� ����
        private void SetColor(float _alpha)
        {
            Color color = mItemImage.color;
            color.a = _alpha;
            mItemImage.color = color;
        }

        // ������ �� Ÿ��
        public bool IsMask(Item item)
        {
            return ((int)item.Type & (int)mSlotMask) == 0 ? false : true;
        }

        // ������ ����
        public void AddItem(Item item)
        {
            mItem = item;
            mItemImage.sprite = mItem.Image;
            SetColor(1);
        }

        // ������ ����
        public void ClearSlot()
        {
            mItem = null;
            mItemImage.sprite = null;
            SetColor(0);
        }

        // ������ ���
        public void UseItem()
        {
            if (mItem != null)
            {
                if(mItem.Type >= ItemType.Helmet && mItem.Type <= ItemType.Necklace)
                {
                    ChangeEquipmentSlot();
                }
            }
        }

        // ��� ��ü
        public void ChangeEquipmentSlot()
        {
            Item item = mItem;
            ClearSlot();
            equipActionManager.UseEquip(item);
        }

        // ��� ����
        public void RemoveEquipmentSlot()
        {
            equipActionManager.RemoveEquip(mItem);
        }

        
    }
}
