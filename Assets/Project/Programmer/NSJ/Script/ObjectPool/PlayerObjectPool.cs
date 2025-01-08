using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectPool : MonoBehaviour
{
    [System.Serializable]
    public struct PoolPrefabStruct
    {
        public LifeDrainEffectPool LifeDrainEffectPrefab;
    }
    [SerializeField] private PoolPrefabStruct _poolPrefab;

    public struct PoolStruct
    {
        public LifeDrainEffectPool LifeDrainEffect;
    }
    [SerializeField] public PoolStruct Pool;
    private void Awake()
    {
        Pool.LifeDrainEffect = Instantiate(_poolPrefab.LifeDrainEffectPrefab, transform);
    }
}
