using UnityEngine;
using UnityEngine.Events;

public class HitAdditional : AdditionalEffect
{
    [HideInInspector] public GameObject Target;
    public virtual event UnityAction<HitAdditional> OnExitHitAdditional;
    protected Coroutine _debuffRoutine;

    protected int _damage;
    public void Init(int damage)
    {
        _damage = damage;
    }
}
