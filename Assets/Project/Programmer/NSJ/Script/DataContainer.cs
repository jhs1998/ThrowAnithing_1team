using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer : MonoBehaviour
{
    public static DataContainer Instance;

    // TODO : 젠젝트로 변환 시도
    public PlayerData PlayerData;


    [SerializeField] ThrowObject[] _throwObjects;
    private Dictionary<int, ThrowObject> _throwObjectDic = new Dictionary<int, ThrowObject>();
    private static Dictionary<int, ThrowObject> s_ThrowObjectDic { get { return Instance._throwObjectDic; } }
    private void Awake()
    {
        InitSingleTon();

        foreach (ThrowObject throwObject in _throwObjects)
        {
            _throwObjectDic.Add(throwObject.Data.ID, throwObject);
        }
    }

    /// <summary>
    /// 투척오브젝트 얻기
    /// </summary>
    public static ThrowObject GetThrowObject(int ID)
    {
        return  s_ThrowObjectDic[ID];
    }

    private void InitSingleTon()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
