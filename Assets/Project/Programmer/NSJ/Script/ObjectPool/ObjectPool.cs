using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    struct PoolInfo
    {
        public Queue<GameObject> Pool;
        public GameObject Prefab;
        public Transform Parent;
    }
    private Dictionary<GameObject, PoolInfo> _poolDic = new Dictionary<GameObject, PoolInfo>();
    public static ObjectPool CreateObjectPool(Transform transform)
    {
        ObjectPool pool = transform.GetComponentInChildren<ObjectPool>();
        if (pool == null)
        {
            Transform newPool = new GameObject("ObjectPool").transform;
            newPool.SetParent(transform, true);
            pool = newPool.AddComponent<ObjectPool>();
        }
        return pool;
    }
    public GameObject GetPool(GameObject prefab)
    {
        PoolInfo info = FindPool(prefab);
        if (info.Pool.Count > 0)
        {
            GameObject instance = info.Pool.Dequeue();
            instance.gameObject.SetActive(true);
            instance.transform.position = Vector3.zero;
            instance.transform.rotation = Quaternion.identity;
            instance.transform.SetParent(null);
            return instance;
        }
        else
        {
            GameObject instance = Instantiate(info.Prefab);
            return instance;
        }
    }
    public GameObject GetPool(GameObject prefab, Transform transform)
    {
        PoolInfo info = FindPool(prefab);
        if (info.Pool.Count > 0)
        {
            GameObject instance = info.Pool.Dequeue();
            instance.gameObject.SetActive(true);
            instance.transform.SetParent(transform);
            instance.transform.position = transform.position;
            instance.transform.rotation = transform.rotation;
            return instance;
        }
        else
        {
            GameObject instance = Instantiate(info.Prefab, transform);
            return instance;
        }
    }
    public GameObject GetPool(GameObject prefab, Transform transform, bool worldPositionStay)
    {
        PoolInfo info = FindPool(prefab);
        if (info.Pool.Count > 0)
        {
            GameObject instance = info.Pool.Dequeue();
            instance.gameObject.SetActive(true);
            instance.transform.SetParent(transform);
            if(worldPositionStay == true)
            {
                instance.transform.position = prefab.transform.position;
                instance.transform.rotation = prefab.transform.rotation;
            }
            else
            {
                instance.transform.position = transform.position;
                instance.transform.rotation = transform.rotation;
            }

            return instance;
        }
        else
        {
            GameObject instance = Instantiate(info.Prefab, transform, worldPositionStay);
            return instance;
        }
    }
    public GameObject GetPool(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        PoolInfo info = FindPool(prefab);
        if (info.Pool.Count > 0)
        {
            GameObject instance = info.Pool.Dequeue();
            instance.gameObject.SetActive(true);
            instance.transform.position = pos;
            instance.transform.rotation = rot;
            instance.transform.SetParent(null);
            return instance;
        }
        else
        {
            GameObject instance = Instantiate(info.Prefab, pos, rot);
            return instance;
        }
    }
    public void ReturnPool(GameObject prefab,GameObject instance)
    {
        PoolInfo info = FindPool(prefab);
        instance.transform.SetParent(info.Parent);
        instance.gameObject.SetActive(false);
        info.Pool.Enqueue(instance);
    }
    private  PoolInfo FindPool(GameObject poolPrefab)
    {
        PoolInfo pool = default;
        if (_poolDic.ContainsKey(poolPrefab) == false)
        {
            Transform newParent = new GameObject(poolPrefab.name).transform;
            newParent.SetParent(transform, true); // parent
            Queue<GameObject> newPool = new Queue<GameObject>(); // pool
            PoolInfo newPoolInfo = GetPoolInfo(newPool, poolPrefab, newParent);
            _poolDic.Add(poolPrefab, newPoolInfo);
        }
        pool = _poolDic[poolPrefab];
        return pool;
    }
    private PoolInfo GetPoolInfo(Queue<GameObject> pool, GameObject prefab, Transform parent)
    {
        PoolInfo info = new PoolInfo();
        info.Pool = pool;
        info.Parent = parent;
        info.Prefab = prefab;
        return info;
    }
}