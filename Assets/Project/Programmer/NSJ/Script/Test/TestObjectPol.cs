using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectPol : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ObjectPool.GetPool(prefab, 1f, 0.5f,1f);
        }
    }
}
