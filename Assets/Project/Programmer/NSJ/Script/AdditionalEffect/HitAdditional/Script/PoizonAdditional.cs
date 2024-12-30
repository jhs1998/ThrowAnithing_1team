using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "AdditionalEffect/Hit/Poison")]
public class PoizonAdditional : HitAdditional
{

    public int Duration;
    [Range(0,1)]public float DamageMultiplier;

    public override void Enter()
    {
        Debug.Log($"{gameObject.name} µ¶");

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
        yield return 1f.GetDelay();
        int count = Duration;
        int damage = (int)(Battle.Debuff.MaxHp * 0.05f);
        while (count > 0)
        {
            Battle.Hit.TakeDamage(damage, false);
            yield return 1f.GetDelay(); 
            count--;
        }
        Battle.EndDebuff(this);
    }
}
