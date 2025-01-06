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
        _remainDuration = Duration;
        int damage = (int)(Battle.Debuff.MaxHp * 0.05f);
        while (_remainDuration > 0)
        {
            yield return 1f.GetDelay();
            Battle.TakeDamage(damage, false, DamageType.Posion);
            _remainDuration--;
        }
        Battle.EndDebuff(this);
    }
}
