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
        private Item mItem;
        public Item Item { get { return mItem; } }

        [Header("해당 슬롯에 들어갈 수 있는 타입")]
        [SerializeField] private ItemType mSlotMask;

        [Header("슬롯에 있는 UI 오브젝트")]
        [SerializeField] private Image mItemImage;


        [SerializeField] private ItemActionManager itemActionManager;

        [SerializeField] TMP_Text nameText;
        [SerializeField] TMP_Text descriptionText;

        // 아이템 이미지 투명도 조절
        private void SetColor(float _alpha)
        {
            Color color = mItemImage.color;
            color.a = _alpha;
            mItemImage.color = color;
        }

        // 아이템 들어갈 타입
        public bool IsMask(Item item)
        {
            return ((int)item.Type & (int)mSlotMask) == 0 ? false : true;
        }

        // 아이템 습득
        public void AddItem(Item item)
        {
            mItem = item;
            mItemImage.sprite = mItem.Image;
            nameText.text = item.Name;
            descriptionText.text = item.Description;

            SetColor(1);
        }

        // 아이템 사용
        public void ClearSlot()
        {
            mItem = null;
            mItemImage.sprite = null;
            SetColor(0);
        }

        public void UseItem()
        {
            if (mItem != null)
            {
                if(mItem.Type >= ItemType.Helmet && mItem.Type <= ItemType.Necklace)
                {
                    ChangeEquipmentSlot();
                    ClearSlot();
                }
            }
        }

        public void ChangeEquipmentSlot()
        {
            itemActionManager.UseItem(mItem);
        }
    }
}
