using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

[CreateAssetMenu(fileName = "ElectricDrain", menuName = "AdditionalEffect/Player/ElectricDrain")]
public class ElectricDrainAddtional : PlayerAdditional
{
    [System.Serializable]
    struct EffectStruct
    {
        public GameObject Effect;
        public GameObject Particle;
        [HideInInspector] public GameObject OriginEffect;
    }
    [SerializeField] EffectStruct _effect;
    [SerializeField] ElectricShockAdditonal _electricShockOrigin;
    private ElectricShockAdditonal _electricShock;
    [Header("������")]
    [SerializeField] private int _damage;

    private float _drainDistance => Model.DrainDistance * 2;
    private float _curDrainDistance;

    private bool _isStop;

    Coroutine _additonalRoutine;
    Coroutine _drainRangeRoutine;
    public override void Enter()
    {
        // �巹�� ����Ʈ ��ü
        _effect.OriginEffect = Player.Effect.Drain_Range;
        Player.Effect.Drain_Range = _effect.Effect;
       // ����� Ŭ�� ����
       _electricShock = Instantiate(_electricShockOrigin);
        _electricShock.Probability = 100;


        if (_additonalRoutine == null)
            _additonalRoutine = CoroutineHandler.StartRoutine(AdditonalRoutine());
        if (_drainRangeRoutine == null)
            _drainRangeRoutine = CoroutineHandler.StartRoutine(DrainRangeRoutine());
    }
    public override void Exit()
    {
        // Ŭ�� ����� ����
        Destroy(_electricShock);
        // �巹�� ����Ʈ ���󺹱�
        Player.Effect.Drain_Range = _effect.OriginEffect;

        if (_additonalRoutine != null)
        {
            CoroutineHandler.StopRoutine(_additonalRoutine);
            _additonalRoutine = null;
        }
        if (_drainRangeRoutine != null)
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
            // Ÿ�� ��ĵ
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _curDrainDistance, Player.OverLapColliders, 1 << Layer.Monster);
            for (int i = 0; i < hitCount; i++)
            {
                // ������, ����� �ֱ�
                int finalDamage = Player.GetDamage(_damage);
                Player.Battle.TargetAttack(Player.OverLapColliders[i], finalDamage, false);
                Player.Battle.TargetDebuff(Player.OverLapColliders[i], _electricShock);
            }

            // ��ƼŬ ����Ʈ ����
            if (_curDrainDistance >= Model.DrainDistance)
            {
                GameObject effect = ObjectPool.GetPool(_effect.Particle, transform, 1f);
            }
            yield return 1f.GetDelay();
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

            // ���� ����
            _curDrainDistance += Time.deltaTime * 10;
            if (_curDrainDistance > Model.DrainDistance)
                _curDrainDistance = Model.DrainDistance;
            yield return null;
        }
    }
}
