using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrozenDrain", menuName = "AdditionalEffect/Player/FrozenDrain")]
public class FrozenDrainAdditional : PlayerAdditional
{
    [SerializeField] private FreezeAdditional _freezeOrigin;
    HitAdditional _freeze;

    private float _drainDistance => Model.DrainDistance * 2;
    private float _curDrainDistance;


    List<GameObject> _hitTargets = new List<GameObject>();
    private bool _isStop;

    Coroutine _additonalRoutine;
    Coroutine _drainRangeRoutine;

    public override void Enter()
    {
        _freeze =Instantiate(_freezeOrigin);
    }
    public override void Exit()
    {
        Destroy(_freeze);
    }

    public override void EnterState()
    {
        if (CurState != PlayerController.State.Drain)
            return;

        _isStop = false;
        _additonalRoutine = CoroutineHandler.StartRoutine(_additonalRoutine, AdditonalRoutine());
        _drainRangeRoutine = CoroutineHandler.StartRoutine(_drainRangeRoutine, DrainRangeRoutine());
    }
    public override void ExitState()
    {
        if (CurState != PlayerController.State.Drain)
            return;

        _hitTargets.Clear();
        _additonalRoutine = CoroutineHandler.StopRoutine(_additonalRoutine);
        _drainRangeRoutine = CoroutineHandler.StopRoutine(_drainRangeRoutine);
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
            // 타겟 스캔
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _curDrainDistance, Player.OverLapColliders, 1 << Layer.Monster);
            for (int i = 0; i < hitCount; i++)
            {
                // 이미 맞았던 적은 안맞음
                if (_hitTargets.Contains(Player.OverLapColliders[i].gameObject))
                    continue;
                // 디버프 주기
                Player.Battle.TargetDebuff(Player.OverLapColliders[i], _freeze);
                _hitTargets.Add(Player.OverLapColliders[i].gameObject);
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
