using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;

[CreateAssetMenu(fileName = "_power Throw", menuName = "Arm/AttackType/_power/Throw")]
public class PowerThrowAttack : ArmThrowAttack
{
    [System.Serializable]
    struct ChargeStruct
    {
        public float ChageTime;
        public int ObjectCount;
        public int Damage;
    }
    [SerializeField] private ChargeStruct[] _charges;
    private float _curChargeTime;
    private int _index;
    Coroutine _chargeRoutine;
    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;
        _curChargeTime = 0;
        _index = 0;

        View.SetTrigger(PlayerView.Parameter.PowerThrow);
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
    }
    public override void Update()
    {

    }

    public override void OnTrigger()
    {
        ThrowObject();
    }
    public override void EndCombo()
    {
        ChangeState(PlayerController.State.Idle);
    }

    IEnumerator ChargeRoutine()
    {
        _index = 0;
        while (true)
        {
            // 차지시간 계산
            _curChargeTime += Time.deltaTime * View.GetFloat(PlayerView.Parameter.AttackSpeed);
            if (_charges.Length  > _index + 1)
            {
                if (_curChargeTime > _charges[_index + 1].ChageTime)
                {
                    if (Model.ThrowObjectStack.Count <= _charges[_index].ObjectCount)
                        _curChargeTime = _charges[_index + 1].ChageTime - 0.01f;
                    else
                    {
                        _index++;
                    }
                }
            }
            else
            {
                _curChargeTime = _charges[_index].ChageTime + 0.01f;
               
            }



            // 차지 해제 시 던지는 애니메이션 실행
            if (Input.GetButtonUp("Fire2"))
            {
                Debug.Log(1);
                Player.LookAtCameraFoward();
                View.SetFloat(PlayerView.Parameter.Charge, _curChargeTime);
                View.SetTrigger(PlayerView.Parameter.ChargeEnd);
                _chargeRoutine = null;
                break;
            }
            yield return null;
        }
    }
    private void ThrowObject()
    {
        int throwObjectID = 0;
        if(Model.ThrowObjectStack.Count > 0)
        {
            throwObjectID = _index == 0 ? Model.PeekThrowObject().ID : Model.PopThrowObject().ID;
        }


        ThrowObject throwObject = Player.InstantiateObject(DataContainer.GetThrowObject(throwObjectID), _muzzlePoint.position, _muzzlePoint.rotation);
        throwObject.Init(Player, Model.HitAdditionals, Model.ThrowAdditionals);
        //TODO : 데미지 계산식 검토 필요
        throwObject.Damage += _charges[_index].Damage;
        UseThrowObject(_charges[_index].ObjectCount);
        throwObject.Shoot(Player.ThrowPower);
        throwObject.TriggerFirstThrowAddtional();
    }

    private void UseThrowObject(int count)
    {
        for (int i = 0; i < count - 1; i++)
        {
            if (Model.ThrowObjectStack.Count > 0)
            {
                Model.PopThrowObject();
            }
        }
    }
}
