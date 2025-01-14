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
    [SerializeField] private BlueChip _blueChipItem;
    public static BlueChip BlueChipItem { get { return Instance._blueChipItem; } }
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
        /// <summary>
        /// 노멀 몬스터용 아이템
        /// </summary>
        public DropItemTable Normal;
        /// <summary>
        /// 뮤턴트 몬스터용 아이템
        /// </summary>
        public DropItemTable Mutant;
        /// <summary>
        /// 엘리트 몬스터용 아이템
        /// </summary>
        public DropItemTable Elite;
        /// <summary>
        /// 중간보스 몬스터용 아이템
        /// </summary>
        public DropItemTable SubBoss;
        /// <summary>
        /// 스테이지보스 몬스터용 아이템
        /// </summary>
        public DropItemTable StageBoss;
    }
    [SerializeField] private ItemStruct _items;
    /// <summary>
    /// 아이템 데이터
    /// </summary>
    public static ItemStruct Items { get { return Instance._items; } }
    public static List<DropList> ItemList = new List<DropList>();
    public static List<DropItemTable> ItemTableList = new List<DropItemTable>();

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
        ItemTableList.Add(Items.Normal);
        ItemTableList.Add(Items.Mutant);
        ItemTableList.Add(Items.Elite);
        ItemTableList.Add(Items.SubBoss);
        ItemTableList.Add(Items.StageBoss);

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

    public static GameObject GetItemTablePrefab(Vector3 pos)
    {
        CoroutineHandler.StartRoutine(CreateItemTable(pos));
        return null;
    }

    public static BlueChip CreateRandomBlueChip(Vector3 pos, Quaternion rot)
    {
        BlueChip blueChip = Instantiate(BlueChipItem, pos, rot);
        return blueChip;
    }
    public static BlueChip CreateRandomBlueChip(Transform transform)
    {
        BlueChip blueChip = Instantiate(BlueChipItem, transform.position, transform.rotation);
        blueChip.transform.SetParent(transform, true);
        return blueChip;
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

    static IEnumerator CreateItemTable(Vector3 pos)
    {
        BaseEnemy monster = new BaseEnemy();
        GameObject obj = new GameObject();

        GameObject startEffect = ObjectPool.GetPool(ItemPaticle[0], pos, Quaternion.Euler(-90f, 0, 0));

        yield return 0.3f.GetDelay();

        ObjectPool.ReturnPool(startEffect);

        if (monster.curMonsterType == BaseEnemy.MonsterType.Nomal)
        {
            ItemTableList[0] = Items.Normal;
            Debug.Log(ItemTableList[0].name);
            Debug.Log(ItemTableList[0]);
            ItemTableList[0].DropListTable1(obj, pos + new Vector3(0, 1, 0), Quaternion.identity);
            Debug.Log("1");
            yield return Instance._destroyItemTime.GetDelay();
            if (obj != null)
            {
                Destroy(obj);
                Debug.Log($"{obj}사라짐");
                GameObject endEffect = ObjectPool.GetPool(ItemPaticle[1], pos + new Vector3(0, 1, 0), Quaternion.Euler(-90f, 0, 0));
                yield return 0.5f.GetDelay();
                ObjectPool.ReturnPool(endEffect);
            }
        }
        else if (monster.curMonsterType == BaseEnemy.MonsterType.Mutant)
        {
            ItemTableList[1] = Items.Mutant;
            ItemTableList[1].DropListTable2(obj, pos + new Vector3(0, 1, 0), Quaternion.identity);
            Debug.Log("2");
            yield return Instance._destroyItemTime.GetDelay();
            if (obj != null)
            {
                Destroy(obj);
                Debug.Log($"{obj}사라짐");
                GameObject endEffect = ObjectPool.GetPool(ItemPaticle[1], pos + new Vector3(0, 1, 0), Quaternion.Euler(-90f, 0, 0));
                yield return 0.5f.GetDelay();
                ObjectPool.ReturnPool(endEffect);
            }
        }
        else if (monster.curMonsterType == BaseEnemy.MonsterType.Elite)
        {
            ItemTableList[2] = Items.Elite;
            ItemTableList[2].DropListTable2(obj, pos + new Vector3(0, 1, 0), Quaternion.identity);
            Debug.Log("3");
            yield return Instance._destroyItemTime.GetDelay();
            if (obj != null)
            {
                Destroy(obj);
                Debug.Log($"{obj}사라짐");
                GameObject endEffect = ObjectPool.GetPool(ItemPaticle[1], pos + new Vector3(0, 1, 0), Quaternion.Euler(-90f, 0, 0));
                yield return 0.5f.GetDelay();
                ObjectPool.ReturnPool(endEffect);
            }
        }
        else if (monster.curMonsterType == BaseEnemy.MonsterType.SubBoss)
        {
            ItemTableList[3] = Items.SubBoss;
            ItemTableList[3].DropListTable2(obj, pos + new Vector3(0, 1, 0), Quaternion.identity);
            Debug.Log("4");
            yield return Instance._destroyItemTime.GetDelay();
            if (obj != null)
            {
                Destroy(obj);
                Debug.Log($"{obj}사라짐");
                GameObject endEffect = ObjectPool.GetPool(ItemPaticle[1], pos + new Vector3(0, 1, 0), Quaternion.Euler(-90f, 0, 0));
                yield return 0.5f.GetDelay();
                ObjectPool.ReturnPool(endEffect);
            }
        }
        else if (monster.curMonsterType == BaseEnemy.MonsterType.Boss)
        {
            ItemTableList[4] = Items.StageBoss;
            ItemTableList[4].DropListTable1(obj, pos + new Vector3(0, 1, 0), Quaternion.identity);
            Debug.Log($"{obj}사라짐");
            yield return Instance._destroyItemTime.GetDelay();
            if (obj != null)
            {
                Destroy(obj);
                Debug.Log("사라짐");
                GameObject endEffect = ObjectPool.GetPool(ItemPaticle[1], pos + new Vector3(0, 1, 0), Quaternion.Euler(-90f, 0, 0));
                yield return 0.5f.GetDelay();
                ObjectPool.ReturnPool(endEffect);
            }
        }
    }
}
