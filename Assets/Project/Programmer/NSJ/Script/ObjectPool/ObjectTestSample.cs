using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTestSample : MonoBehaviour
{
    [SerializeField]Effector2D _prefab;
    GameObject _instance;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _instance = ObjectPool.GetPool(_prefab, transform, true);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ObjectPool.ReturnPool(_prefab,_instance);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ObjectPool.ReturnPool(gameObject);
        }
    }
}
