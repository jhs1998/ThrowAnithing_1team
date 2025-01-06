using UnityEngine;
using UnityEngine.Events;

public class HitAdditional : AdditionalEffect
{
    public IBattle Battle;
    public float Duration;


    protected int _damage;
    protected bool _isCritical;
    protected float _remainDuration;
    protected Coroutine _debuffRoutine;
    [HideInInspector] public Transform transform;
    protected GameObject gameObject => transform.gameObject;

    public void Init(int damage, bool isCritical, float remainDuration)
    {
        _damage = damage;
        _isCritical = isCritical;
        _remainDuration = remainDuration;
    }
}
