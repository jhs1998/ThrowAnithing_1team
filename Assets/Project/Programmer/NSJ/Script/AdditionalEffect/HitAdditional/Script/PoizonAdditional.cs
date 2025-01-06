using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "AdditionalEffect/Hit/Poison")]
public class PoizonAdditional : HitAdditional
{ 
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
        RemainDuraiton = Duration;
        int damage = (int)(Battle.Debuff.MaxHp * 0.05f);
        while (RemainDuraiton > 0)
        {
            yield return 1f.GetDelay();
            Battle.TakeDamage(damage, false, DamageType.Posion);
            RemainDuraiton--;
        }
        Battle.EndDebuff(this);
    }
}
