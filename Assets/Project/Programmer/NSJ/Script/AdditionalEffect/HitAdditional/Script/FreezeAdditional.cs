using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "AdditionalEffect/Hit/Freeze")]
public class FreezeAdditional : HitAdditional
{
    private float _originMoveSpeed;
    public override void Enter()
    {
        if (_debuffRoutine == null)
        {
            _debuffRoutine = CoroutineHandler.StartRoutine(FreezeRoutine());
        }
        _originMoveSpeed = Battle.Debuff.MoveSpeed;

    }

    public override void Update()
    {
        Battle.Debuff.MoveSpeed = 0;
    }
    public override void Exit()
    {
        if (_debuffRoutine != null)
        {
            CoroutineHandler.StopRoutine(_debuffRoutine);
            _debuffRoutine = null;
        }
        Battle.Debuff.MoveSpeed = _originMoveSpeed;
    }

    IEnumerator FreezeRoutine()
    {
        yield return 3f.GetDelay();

        Battle.EndDebuff(this);
    }
}
