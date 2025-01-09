using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    [SerializeField] public GameObject Prefab;
    protected Queue<GameObject> _pool = new Queue<GameObject>();
    public abstract GameObject GetPool(Vector3 pos, Quaternion rot);
    public abstract void ReturnPool(GameObject instance);
}
