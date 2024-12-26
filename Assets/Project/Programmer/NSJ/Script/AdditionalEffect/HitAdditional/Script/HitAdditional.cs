using UnityEngine;
using UnityEngine.Events;

public class HitAdditional : AdditionalEffect
{
    [HideInInspector] public GameObject Target;
    public int Damage;
    public virtual event UnityAction<HitAdditional> OnExitHitAdditional;
    protected Coroutine _debuffRoutine;

    public void Init(int damage)
    {
        Damage = damage;
    }
}
