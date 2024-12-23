using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "_power Special", menuName = "Arm/AttackType/_power/Special")]
public class PowerSpecialAttack : ArmSpecialAttack
{
    [System.Serializable]
    struct ChargeStruct
    {
        public GameObject DropObject;
        public float ChargeTime;
        public int ObjectCount;
        public int Damage;
    }
    [SerializeField] private ChargeStruct[] _charges;

    private int _index;
    private float _maxChargeTime => _charges[_charges.Length - 1].ChargeTime;
    private int _triggerIndex;
    private GameObject _instanceDropObject;
    Coroutine _chargeRoutine;
    public override void Enter()
    {
        if(Model.ThrowObjectStack.Count < _charges[_index].ObjectCount)
        {
            EndAnimation();
            return;
        }
            

        // 차징 모션 시작
        View.SetTrigger(PlayerView.Parameter.PowerSpecial);
        if (_chargeRoutine == null)
        {
            _chargeRoutine = CoroutineHandler.StartRoutine(ChargeRoutine());
        }
    }
    public override void Exit()
    {
        if (_chargeRoutine != null)
        {
            CoroutineHandler.StopRoutine(_chargeRoutine);
            _chargeRoutine = null;
        }
        Model.SpecialChargeGage = 0;
        _index = 0;
        _triggerIndex = 0;

    }
    public override void Update()
    {

    }
    public override void OnTrigger()
    {
        if (_triggerIndex == 0) 
        {
            CreateSpecialObject();
            _triggerIndex++;
        }
        else
        {
            AttackSpecial();
        }
    }
    public override void EndAnimation()
    {
        ChangeState(PlayerController.State.Idle);
    }
    IEnumerator ChargeRoutine()
    {
        while (true)
        {
            ProcessCharge();

            if (Input.GetButtonUp("Fire2"))
            {         
                Model.SpecialChargeGage = 0;
                if (_index != 0)
                {
                    _index--;
                    Model.CurSpecialGage -= (_charges[_index].ChargeTime / _maxChargeTime) * Model.MaxSpecialGage;
                    View.SetTrigger(PlayerView.Parameter.ChargeEnd);
                }
                else
                {
                    ChangeState(PlayerController.State.Idle);
                }
                _chargeRoutine = null;
                break;
            }
            yield return null;
        }
    }
    private void ProcessCharge()
    {
        // 차지시간 계산
        Model.SpecialChargeGage += Time.deltaTime / _maxChargeTime;
        // 인덱스가 배열 크기보다 작을떄만
        if (_index < _charges.Length)
        {
            // 소모 오브젝트가 부족한 경우
            if (Model.ThrowObjectStack.Count >= _charges[_index].ObjectCount)
            {
                // 차지 시간이 다음 단계 차징 조건시간을 넘긴 경우
                if (Model.SpecialChargeGage >= _charges[_index].ChargeTime / _maxChargeTime)
                {
                    _index++;
                }
                // 현재 특수자원량보다 차지량이 더 많은 경우
                else if (Model.SpecialChargeGage > Model.CurSpecialGage / Model.MaxSpecialGage)
                {
                    Model.SpecialChargeGage = Model.CurSpecialGage / Model.MaxSpecialGage;
                }
            }
            else
            {
                Model.SpecialChargeGage = _index == 0 ? 0 : _charges[_index - 1].ChargeTime / _maxChargeTime;
            }
        }
    }

    private void CreateSpecialObject()
    {
        Vector3 dropPos = new Vector3(transform.localPosition.x, transform.localPosition.y+ 4f, transform.localPosition.z + 2f);
        _instanceDropObject = Instantiate(_charges[_index].DropObject, dropPos, transform.rotation);
    }

    private void AttackSpecial()
    {

    }
}
