using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="AdditionalEffect/Hit/Freeze")]
public class FreezeAdditional : HitAdditional
{
    public override event UnityAction<HitAdditional> OnExitHitAdditional;

    public override void Enter()
    {
        Debug.Log(_debuffRoutine);
        if (_debuffRoutine == null)
        {
           _debuffRoutine = CoroutineHandler.StartRoutine(FreezeRoutine());
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

    IEnumerator FreezeRoutine()
    {
        Debug.Log($"{Target.name} 얼어붙음");
        yield return 3f.GetDelay();
        OnExitHitAdditional?.Invoke(this);
    }
}
