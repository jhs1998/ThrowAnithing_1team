using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ContinuousAttack", menuName = "AdditionalEffect/Player/ContinuousAttack")]
public class ContinuousAttackAdditional : PlayerAdditional
{

    [Header("���ݷ� ������")]
    [SerializeField] private int _attackPower;
    [Header("���ӽð�")]
    [SerializeField] private float _duration;

    private bool _isUseSpecial;
    private bool _isTimerStart;

    private float _timer;
    Coroutine _increaseAttackPowerRoutine;

    public override void Enter()
    {
        _isTimerStart = false;
        _timer = _duration;
    }
    public override void Exit()
    {
        // Ÿ�̸� ���ư��� ���϶� (���ݷ� ���� ����)
        if(_isTimerStart == true)
        {
            // �ٽ� ���ݷ� ����
            Model.AttackPower = GetPlayerAttackPower(-_attackPower);
        }
    }

    public override void Update()
    {
        ProcessBuff();
    }

    public override void Trigger()
    {
        // ��ų ���
        if (CurState != PlayerController.State.SpecialAttack)
            return;

        _isUseSpecial = true;
    }
    private void ProcessBuff()
    {
        // ��ų ��� �� Ÿ�̸� 5�� �� ����
        if (_isUseSpecial)
        {
            _isUseSpecial = false;

            // Ÿ�̸Ӱ� ���ư��� �������� �� ���ݷ� ������(�ߺ� ����)
            if (_isTimerStart == false)
            {
                Model.AttackPower = GetPlayerAttackPower(_attackPower);
            }
            _timer = _duration;
            _isTimerStart = true;

        }

        _timer -= Time.deltaTime;
        if (_timer < 0 && _isTimerStart == true)
        {
            _isTimerStart = false;

            // �ٽ� ���ݷ� ����
            Model.AttackPower = GetPlayerAttackPower(-_attackPower);
        }
    }
}
