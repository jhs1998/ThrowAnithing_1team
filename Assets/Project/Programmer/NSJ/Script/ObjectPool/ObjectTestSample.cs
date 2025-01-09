using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTestSample : MonoBehaviour
{
    [SerializeField]Collider _prefab;
    Collider _instance;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
             _instance = ObjectPool.GetPool(_prefab);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ObjectPool.ReturnPool(_instance);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ObjectPool.ReturnPool(gameObject);
        }
       // Debug.Log(_instance);
    }
}
