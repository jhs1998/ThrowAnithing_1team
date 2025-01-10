using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Balance PrevSpecial", menuName = "Arm/AttackType/Balance/PrevSpecial")]
public class BalanceSpecialAttack : ArmSpecialAttack
{
    [System.Serializable]
    struct ChargeStruct
    {
        public float ChargeTime;
        public float ChargeMana;
        public int ObjectCount;
    }
    [System.Serializable]
    struct FirstStruct
    {
        public float AttackSpeed;
        public float Duration;
    }
    [System.Serializable]
    struct SecondStruct
    {
        public float Damage;
        public float Range;
    }
    [System.Serializable]
    struct ThirdStruct
    {
        public float Damage;
        public float Range;
    }

    [SerializeField] private ChargeStruct[] _charges;
    [SerializeField] private FirstStruct _first;
    [SerializeField] private SecondStruct _second;
    [SerializeField] private ThirdStruct _third;
    private float _maxChargeTime => _charges[_charges.Length - 1].ChargeTime;
    private float _maxChargeMana => _charges[_charges.Length - 1].ChargeMana;
    private bool _isChargeEnd;
    public override void Init(PlayerController player)
    {
        base.Init(player);
        for (int i = 0; i < _charges.Length; i++)
        {
            View.Panel.StepTexts[i].SetText(_charges[i].ObjectCount.GetText());
            _charges[i].ChargeMana = Model.ManaConsumption[i];
        }
    }
    public override void Enter()
    {
        if (_isChargeEnd == false 
            && (Model.ThrowObjectStack.Count < _charges[_index].ObjectCount || Model.CurMana < Model.ManaConsumption[0]))
        {
            _isChargeEnd = false;
            ChangeState(Player.PrevState);
            return;
        }

        if (_isChargeEnd == false)
        {
            ChangeState(Player.PrevState);     
            _isChargeEnd = true;
            CoroutineHandler.StartRoutine(ChargeRoutine());
        }
        else
        {
            _index--;
            if(_index < 0)
            {
                Model.SpecialChargeGage = 0;
                _isChargeEnd = false;
                ChangeState(Player.PrevState);
                return;
            }

            // 차지 사용량만큼 제거
            Model.CurMana -= _charges[_index].ChargeMana;
            Model.SpecialChargeGage = 0;

            SelectSpecialAttack(_index);
        }
    }
    public override void Exit()
    {
        if (_isChargeEnd == false)
        {
            
        }
        else
        {
         
            _isChargeEnd = false;
        }
        _index = 0;
    }

    IEnumerator ChargeRoutine()
    {
        while (true)
        {
            ProcessCharge();
            if (InputKey.GetButtonUp(InputKey.Special))
            {
                ChangeState(PlayerController.State.SpecialAttack);   
                yield break;
            }
            yield return null;
        }
    }

    private void ProcessCharge()
    {
        // 차지시간 계산
        Model.SpecialChargeGage += Time.deltaTime * 100 / _maxChargeTime;
        // 인덱스가 배열 크기보다 작을떄만
        if (_index < _charges.Length)
        {
            // 소모 오브젝트가 부족한 경우
            if (Model.ThrowObjectStack.Count >= _charges[_index].ObjectCount)
            {
                // 차지 시간이 다음 단계 차징 조건시간을 넘긴 경우
                if (Model.SpecialChargeGage >= _charges[_index].ChargeMana)
                {

                    _index++;
                }
                // 현재 특수자원량보다 차지량이 더 많은 경우
                else if (Model.SpecialChargeGage > Model.CurMana)
                {
                    Model.SpecialChargeGage = Model.CurMana;
                }
            }
            else
            {
                Model.SpecialChargeGage = _charges[_index - 1].ChargeMana;
            }
        }
        else
        {
            Model.SpecialChargeGage = _charges[_index - 1].ChargeMana;
        }
    }

    private void SelectSpecialAttack(int index)
    {
        switch (index) 
        {
            case 0:
                ProcessFirstSpecial();
                break;
            case 1:
                ProcessSecondSpecial();
                break;
            case 2:
                ProcessThirdSpecial();
                break;
        }
    }

    /// <summary>
    /// 첫번째 특수
    /// </summary>
    private void ProcessFirstSpecial()
    {
        Debug.Log("첫번쨰 특수기");
        CoroutineHandler.StartRoutine(FirstSpecialBuffRoutine());
        ChangeState(Player.PrevState);
    }
    IEnumerator FirstSpecialBuffRoutine()
    {
        Model.AttackSpeedMultiplier += _first.AttackSpeed;
        yield return _first.Duration.GetDelay();
        Model.AttackSpeedMultiplier -= _first.AttackSpeed;
    }
    /// <summary>
    /// 두번째 특수
    /// </summary>
    private void ProcessSecondSpecial()
    {
        Debug.Log("두번쨰 특수기");
    }
    /// <summary>
    /// 세번째 특수
    /// </summary>
    private void ProcessThirdSpecial()
    {
        Debug.Log("세번쨰 특수기");
    }
}
