using UnityEngine;
using UnityEngine.Events;

public class HitAdditional : AdditionalEffect
{
    [HideInInspector] public GameObject Target;
    public IBattle Battle;
    public int Damage;
    public virtual event UnityAction<HitAdditional> OnExitHitAdditional;
    protected Coroutine _debuffRoutine;

    public void Init(int damage)
    {
        Damage = damage;
    }
}
