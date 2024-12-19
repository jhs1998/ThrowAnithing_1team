using UnityEngine;
using UnityEngine.Events;

public class HitAdditional : ScriptableObject
{
    [HideInInspector] public GameObject Target;
    public HitAdditional Origin;
    public virtual event UnityAction<HitAdditional> OnExitHitAdditional;
    protected Coroutine _debuffRoutine;

    protected int _damage;

    public virtual void Execute() { }
    public virtual void UnExcute() { }
    public void Init(int damage)
    {
        _damage = damage;
    }
}
