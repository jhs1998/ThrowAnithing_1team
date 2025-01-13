using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectPol : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    private void Start()
    {
        CoroutineHandler.StartRoutine(Test());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ObjectPool.GetPool(prefab, 1f, 0.5f, 100f);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //ObjectPool.ReturnPool(key);
        }
    }
    IEnumerator Test()
    {
        yield return null;
    }
}
