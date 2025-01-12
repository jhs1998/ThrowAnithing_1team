using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectPol : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    Coroutine _effectRoutine;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ObjectPool.GetPool(prefab, 1f, 0.5f,5f, ref _effectRoutine);
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            ObjectPool.ReturnPool(ref _effectRoutine);
        }
        Debug.Log(_effectRoutine);
    }
}
