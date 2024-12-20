using UnityEngine;

namespace MKH
{
    [CreateAssetMenu(fileName = "Item", menuName = "Add Item/BlueChip")]
    public class BlueChipData : ScriptableObject
    {
        [Header("고유 아이디 (중복X)")]
        [SerializeField] private int mItmeID;
        public int ItemID { get { return mItmeID; } }

        [Header("이름")]
        [SerializeField] private string mName;
        public string Name { get { return mName; } }

        [Header("설명")]
        [SerializeField] private string mDescription;
        public string Description { get { return mDescription; } }

        [Header("인벤토리에서 보여지는 이미지")]
        [SerializeField] private Sprite mItemImage;
        public Sprite Image { get { return mItemImage; } }

        [Header("씬에서 오브젝트로 보여질 아이템의 프리팹")]
        [SerializeField] private GameObject mItemPrefab;
        public GameObject Prefab { get { return mItemPrefab; } }

    }
}
