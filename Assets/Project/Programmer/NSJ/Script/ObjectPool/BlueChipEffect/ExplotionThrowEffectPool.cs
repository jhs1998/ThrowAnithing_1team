using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionThrowEffectPool : ObjectPool
{
    public override GameObject GetPool(Vector3 pos, Quaternion rot)
    {
        if (_pool.Count > 0)
        {
            GameObject instance = _pool.Dequeue();
            instance.gameObject.SetActive(true);
            instance.transform.position = pos;
            instance.transform.rotation = rot;
            instance.transform.SetParent(null);
            return instance;
        }
        else
        {
            GameObject instance = Instantiate(Prefab, pos, rot);
            return instance;
        }
    }

    public override void ReturnPool(GameObject instance)
    {
        instance.transform.SetParent(transform);
        instance.gameObject.SetActive(false);
        _pool.Enqueue(instance);
    }
}
