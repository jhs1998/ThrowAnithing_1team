using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTestSample : MonoBehaviour
{
    ObjectPool Pool;
    GameObject _prefab;
    GameObject _instance;
    private void Awake()
    {
        Pool = ObjectPool.CreateObjectPool(transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _instance = Pool.GetPool(_prefab, transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Pool.ReturnPool(_prefab, _instance);
        }
    }
}
