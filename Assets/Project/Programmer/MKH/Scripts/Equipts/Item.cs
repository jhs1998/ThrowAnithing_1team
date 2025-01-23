using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    [System.Flags]
    public enum ItemType
    {
        None        = 0b0,
        
        Helmet      = 0b1,
        Shirts      = 0b10,
        Glasses     = 0b100,
        Gloves      = 0b1000,
        Pants       = 0b10000,
        Earring     = 0b100000,
        Ring        = 0b1000000,
        Shoes       = 0b10000000,
        Necklace    = 0b100000000,

    }

    public enum RateType
    {
        Nomal, Magic, Rare
    }

    [CreateAssetMenu(fileName = "Item", menuName = "Add Item/Item")]
    public class Item : ScriptableObject
    {
        #region ��� X
        //[Header("���� ���̵� (�ߺ�X)")]
        //[SerializeField] private int mItmeID;
        //public int ItemID => mItmeID;
        #endregion

        [Header("�̸�")]
        [SerializeField] private string mName;
        public string Name { get { return mName; } set { mName = value; } }

        [Header("����")]
        [Multiline]
        [SerializeField] private string mDescription;
        public string Description { get { return mDescription; } set { mDescription = value; } }

        [Header("������ Ÿ��")]
        [SerializeField] private ItemType mItemType;
        public ItemType Type { get { return mItemType; } }

        [Header("������ ���")]
        [SerializeField] private RateType mRateType;
        public RateType Rate { get { return mRateType; } }


        [Header("�κ��丮���� �������� �̹���")]
        [SerializeField] private Sprite mItemImage;
        public Sprite Image { get { return mItemImage; } }

        [Header("������ ������Ʈ�� ������ �������� ������")]
        [SerializeField] private GameObject mItemPrefab;
        public GameObject Prefab { get { return mItemPrefab; } }

        public virtual Item Create()
        {
            return null;
        }
    }
}
