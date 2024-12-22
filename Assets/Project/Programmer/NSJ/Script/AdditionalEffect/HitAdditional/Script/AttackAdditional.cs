using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "AdditionalEffect/Hit/Attack")]
public class AttackAdditional : HitAdditional
{
    public override event UnityAction<HitAdditional> OnExitHitAdditional;

    public override void Enter()
    {
        Attack();
        OnExitHitAdditional?.Invoke(this);
    }

    public override void Exit()
    {
        
    }

    private void Attack()
    {
        IHit hit = Target.GetComponent<IHit>();
        hit.TakeDamage(_damage);
    }
}
