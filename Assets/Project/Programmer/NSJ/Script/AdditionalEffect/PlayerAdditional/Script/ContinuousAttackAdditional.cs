using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ContinuousAttack", menuName = "AdditionalEffect/Player/ContinuousAttack")]
public class ContinuousAttackAdditional : PlayerAdditional
{
    [Header("공격력 증가량")]
    [SerializeField] private int _attackPower;
    [Header("지속시간")]
    [SerializeField] private float _duration;

    private bool _isUseSpecial;
    private bool _isTimerStart;
    Coroutine _increaseAttackPowerRoutine;

    public override void Enter()
    {
        if (_increaseAttackPowerRoutine == null)
            _increaseAttackPowerRoutine = CoroutineHandler.StartRoutine(IncreaseAttackPowerRoutine());
    }

    public override void Exit()
    {
        if(_increaseAttackPowerRoutine != null)
        {
            CoroutineHandler.StopRoutine(_increaseAttackPowerRoutine);
            _increaseAttackPowerRoutine = null;
        }

        // 타이머 돌아가는 중일때 (공격력 증가 상태)
        if(_isTimerStart == true)
        {
            // 다시 공격력 감소
            Model.AttackPower = GetPlayerAttackPower(-_attackPower);
        }
    }

    public override void Trigger()
    {
        // 스킬 사용
        if (CurState != PlayerController.State.SpecialAttack)
            return;

        _isUseSpecial = true;
    }
    IEnumerator IncreaseAttackPowerRoutine()
    {
        _isTimerStart = false;
        float timer = _duration;
        while (true)
        {
            // 스킬 사용 시 타이머 5초 재 시작
            if (_isUseSpecial)
            {
                _isUseSpecial = false;

                timer = _duration;
                _isTimerStart = true;
                Model.AttackPower = GetPlayerAttackPower(_attackPower);
            }

            timer -= Time.deltaTime;
            if(timer < 0 && _isTimerStart == true)
            {
                _isTimerStart = false;

                // 다시 공격력 감소
                Model.AttackPower = GetPlayerAttackPower(-_attackPower);
            }
            yield return null;
        }
    }
}
