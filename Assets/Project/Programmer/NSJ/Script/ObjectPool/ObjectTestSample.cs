using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTestSample : MonoBehaviour
{
    ObjectPool Pool;
    [SerializeField]GameObject _prefab;
    GameObject _instance;
    private void Awake()
    {
        Pool = ObjectPool.CreateObjectPool();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _instance = Pool.GetPool(_prefab, transform, true);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            _instance = Pool.GetPool(_prefab, transform, false);
        }
    }
}
