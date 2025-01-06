using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FrozenDrain", menuName = "AdditionalEffect/Player/FrozenDrain")]
public class FrozenDrainAdditional : PlayerAdditional
{
    [SerializeField] HitAdditional _freeze;

    private float _drainDistance => Model.DrainDistance * 2;
    private float _curDrainDistance;

    private bool _isStop;

    Coroutine _additonalRoutine;
    Coroutine _drainRangeRoutine;
    public override void Enter()
    {
        if (_additonalRoutine == null)
            _additonalRoutine = CoroutineHandler.StartRoutine(AdditonalRoutine());
        if( _drainRangeRoutine == null)
            _drainRangeRoutine =CoroutineHandler.StartRoutine(DrainRangeRoutine());
    }
    public override void Exit()
    {
       if(_additonalRoutine != null)
        {
            CoroutineHandler.StopRoutine(_additonalRoutine);
            _additonalRoutine = null;
        }
       if( _drainRangeRoutine != null)
        {
            CoroutineHandler.StopRoutine(_drainRangeRoutine);
            _drainRangeRoutine = null;
        }
    }

    public override void Trigger()
    {
        if (Player.CurState != PlayerController.State.Drain)
            return;

        _isStop = true;
        _curDrainDistance = 0;
    }

    IEnumerator AdditonalRoutine()
    {
        while (true) 
        {
            if (_isStop == true)
            {
                yield return null;
                continue;
            }
            if (Player.CurState != PlayerController.State.Drain)
            {
                yield return null;
                continue;
            }
            // 타겟 스캔
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _curDrainDistance, Player.OverLapColliders, 1 << Layer.Monster);
            for (int i = 0; i < hitCount; i++)
            {
                // 디버프 주기
                Player.Battle.TargetDebuff(Player.OverLapColliders[i], _freeze);
            }
            yield return 0.2f.GetDelay();
        }
    }
    IEnumerator DrainRangeRoutine()
    {
        while (true)
        {
            if (_isStop == true)
            {
                if (Player.CurState != PlayerController.State.Drain)
                    _isStop = false;
                yield return null;
                continue;
            }
            if (Player.CurState != PlayerController.State.Drain)
            {
                yield return null;
                continue;
            }

            // 점차 증가
            _curDrainDistance += Time.deltaTime * 10;
            if (_curDrainDistance > Model.DrainDistance)
                _curDrainDistance = Model.DrainDistance;
            yield return null;
        }
    }
}
