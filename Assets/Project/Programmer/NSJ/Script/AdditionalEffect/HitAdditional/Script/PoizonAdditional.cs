using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "AdditionalEffect/Hit/Poison")]
public class PoizonAdditional : HitAdditional
{
    public override event UnityAction<HitAdditional> OnExitHitAdditional;

    public int Duration;
    [Range(0,1)]public float DamageMultiplier;

    public override void Enter()
    {
        if (_debuffRoutine == null)
        {
            _debuffRoutine = CoroutineHandler.StartRoutine(PoisonRoutine());
        }
    }

    public override void Exit()
    {
        if (_debuffRoutine != null)
        {
            CoroutineHandler.StopRoutine(_debuffRoutine);
            _debuffRoutine = null;
        }
    }

    IEnumerator PoisonRoutine()
    {
        int count = Duration;
        int damage = Battle.Debuff.MaxHp * (5/100);
        while (count > 0)
        {
            Battle.Hit.TakeDamage(damage, false);
            yield return 1f.GetDelay(); 
            count--;
        }
        OnExitHitAdditional?.Invoke(this);
    }
}
