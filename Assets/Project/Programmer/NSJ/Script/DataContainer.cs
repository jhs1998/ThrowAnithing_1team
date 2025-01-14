using MKH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer : MonoBehaviour
{
    public static DataContainer Instance;
    [SerializeField] BaseEnemy[] _monsters;
    /// <summary>
    /// 몬스터 데이터
    /// </summary>
    public static BaseEnemy[] Monsters { get { return Instance._monsters; } }
    [SerializeField] AdditionalEffect[] _blueChips;
    /// <summary>
    /// 블루칩 데이터
    /// </summary>
    public static AdditionalEffect[] BlueChips { get { return Instance._blueChips; } }
    [SerializeField] private TestBlueChip _blueChipItem;
    public static TestBlueChip BlueChipItem { get { return Instance._blueChipItem; } }
    [System.Serializable]
    public struct ItemStruct
    {
        /// <summary>
        /// 노말 아이템 (1단계)
        /// </summary>
        public DropList NormalItems;
        /// <summary>
        /// 매직 아이템 (2단계)
        /// </summary>
        public DropList MagicItems;
        /// <summary>
        /// 레어 아이템 (3단계)
        /// </summary>
        public DropList RareItems;
    }
    [SerializeField] private ItemStruct _items;
    /// <summary>
    /// 아이템 데이터
    /// </summary>
    public static ItemStruct Items { get { return Instance._items; } }
    public static List<DropList> ItemList = new List<DropList>();

    [SerializeField] GameObject[] _itemPaticle;
    /// <summary>
    /// 아이템 파티클 데이터
    /// </summary>
    public static GameObject[] ItemPaticle { get { return Instance._itemPaticle; } }

    [SerializeField] float _destroyItemTime = 0;
    /// <summary>
    /// 아이템 사라지는 시간
    /// </summary>
    public static float DestroyItemTime { get { return Instance._destroyItemTime; } }

    /// <summary>
    /// 투척물 데이터
    /// </summary>
    [SerializeField] ThrowObject[] _throwObjects;
    public static ThrowObject[] ThrowObjects { get { return Instance._throwObjects; } }
    private Dictionary<int, ThrowObject> _throwObjectDic = new Dictionary<int, ThrowObject>();
    private static Dictionary<int, ThrowObject> s_ThrowObjectDic { get { return Instance._throwObjectDic; } }
    /// <summary>
    /// 암유닛 데이터
    /// </summary>
    [SerializeField] ArmUnit[] _armUnits;
    private Dictionary<GlobalGameData.AmWeapon, ArmUnit> _armUnitDic = new Dictionary<GlobalGameData.AmWeapon, ArmUnit>();
    private static Dictionary<GlobalGameData.AmWeapon, ArmUnit> s_armUnitDic { get { return Instance._armUnitDic; } }

    [SerializeField] DamageText[] _damageTexts;
    private Dictionary<CrowdControlType, DamageText> _damageTextDic = new Dictionary<CrowdControlType, DamageText>();
    private static Dictionary<CrowdControlType, DamageText> s_damageTextDic { get { return Instance._damageTextDic; } }
    private void Awake()
    {
        Instance = this;
        InitSingleTon();

        for(int i = 0; i < _throwObjects.Length; i++)
        {
            _throwObjects[i].Data.ID = i;
            _throwObjectDic.Add(_throwObjects[i].Data.ID, _throwObjects[i]);
        }
        foreach (ArmUnit armUnit in _armUnits)
        {
            _armUnitDic.Add(armUnit.ArmType, armUnit);
        }
        foreach (DamageText damageText in _damageTexts)
        {
            _damageTextDic.Add(damageText.Type, damageText);
        }
        ItemList.Add(Items.NormalItems);
        ItemList.Add(Items.MagicItems);
        ItemList.Add(Items.RareItems);
    }

    /// <summary>
    /// 투척오브젝트 얻기
    /// </summary>
    public static ThrowObject GetThrowObject(int ID)
    {
        return s_ThrowObjectDic[ID];
    }
    /// <summary>
    /// 암유닛 얻기
    /// </summary>
    public static ArmUnit GetArmUnit(GlobalGameData.AmWeapon armUnit)
    {
        return s_armUnitDic[armUnit];
    }
    /// <summary>
    /// 데미지 UI 프리팹 얻기
    /// </summary>
    public static DamageText GetDamageText(CrowdControlType type)
    {
        return s_damageTextDic[type];
    }
    /// <summary>
    /// 랜덤 아이템 오브젝트 얻기
    /// </summary>
    public static GameObject GetItemPrefab(DropList dropList)
    {
        return dropList.itemList[Random.Range(0, dropList.Count)];
    }
    public static GameObject GetItemPrefab(Vector3 pos)
    {
        DropList dropList = ItemList[Random.Range(0, ItemList.Count)];
        CoroutineHandler.StartRoutine(CreateItem(pos, dropList));
        return dropList.itemList[Random.Range(0, dropList.Count)];
    }

    public static TestBlueChip CreateRandomBlueChip(Vector3 pos, Quaternion rot)
    {
        TestBlueChip testBlueChip = Instantiate(BlueChipItem, pos, rot);
        testBlueChip.Effect = BlueChips[Random.Range(0, BlueChips.Length)];
        testBlueChip.SetBlueChip(testBlueChip.Effect);
        return testBlueChip;
    }
    public static TestBlueChip CreateRandomBlueChip(Transform transform)
    {
        TestBlueChip testBlueChip = Instantiate(BlueChipItem, transform.position, transform.rotation);
        testBlueChip.transform.SetParent(transform, true);
        testBlueChip.Effect = BlueChips[Random.Range(0, BlueChips.Length)];
        testBlueChip.SetBlueChip(testBlueChip.Effect);
        return testBlueChip;
    }
    private void InitSingleTon()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    static IEnumerator CreateItem(Vector3 pos, DropList dropList)
    {
        GameObject startEffect = ObjectPool.GetPool(ItemPaticle[0], pos, Quaternion.Euler(-90f, 0, 0));
        yield return 0.3f.GetDelay();
        ObjectPool.ReturnPool(startEffect);
        GameObject dropPrefab = dropList.itemList[Random.Range(0, dropList.Count)];
        GameObject obj = Instantiate(dropPrefab, pos + new Vector3(0, 1, 0), Quaternion.identity);
        yield return Instance._destroyItemTime.GetDelay();
        if (obj != null)
        {
            Destroy(obj);
            GameObject endEffect = ObjectPool.GetPool(ItemPaticle[1], pos + new Vector3(0, 1, 0), Quaternion.Euler(-90f, 0, 0));
            yield return 0.5f.GetDelay();
            ObjectPool.ReturnPool(endEffect);
        }
    }
}
