using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer : MonoBehaviour
{
    public static DataContainer Instance;


    [SerializeField] ThrowObject[] _throwObjects;
    private Dictionary<int, ThrowObject> _throwObjectDic = new Dictionary<int, ThrowObject>();
    private static Dictionary<int, ThrowObject> s_ThrowObjectDic { get { return Instance._throwObjectDic; } }
    [SerializeField] ArmUnit[] _armUnits;
    private Dictionary<GlobalPlayerStateData.AmWeapon, ArmUnit> _armUnitDic = new Dictionary<GlobalPlayerStateData.AmWeapon, ArmUnit>();
    private static Dictionary<GlobalPlayerStateData.AmWeapon, ArmUnit> s_armUnitDic { get { return Instance._armUnitDic; } }
    private void Awake()
    {
        InitSingleTon();

        foreach (ThrowObject throwObject in _throwObjects)
        {
            _throwObjectDic.Add(throwObject.Data.ID, throwObject);
        }
        foreach(ArmUnit armUnit in _armUnits)
        {
            _armUnitDic.Add(armUnit.ArmType, armUnit);
        }
    }

    /// <summary>
    /// 투척오브젝트 얻기
    /// </summary>
    public static ThrowObject GetThrowObject(int ID)
    {
        return  s_ThrowObjectDic[ID];
    }
    /// <summary>
    /// 암유닛 얻기
    /// </summary>
    public static ArmUnit GetArmUnit(GlobalPlayerStateData.AmWeapon armUnit)
    {
        return s_armUnitDic[armUnit];
    }

    private void InitSingleTon()
    {
        if(Instance == null)
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
}
