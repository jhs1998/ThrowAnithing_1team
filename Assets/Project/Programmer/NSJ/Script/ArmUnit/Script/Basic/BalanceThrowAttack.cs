using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Balance PrevThrow", menuName = "Arm/AttackType/Balance/PrevThrow")]
public class BalanceThrowAttack : ArmThrowAttack
{
    [System.Serializable]
    struct AttackStruct
    {
        [Tooltip("추가 데미지")]
        public float Damage;
        [Tooltip("경직 걸리는 가?")]
        public bool isStiff;
        [Tooltip("넉백 거리")]
        public float KnockBackDistance;
    }
    [SerializeField] private AttackStruct[] _attacks;

    private int _comboCount; // 콤보 횟수
    Coroutine _throwRoutine;
    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;

        // 첫 공격 시 첫 공격 애니메이션 실행
        if (Player.PrevState != PlayerController.State.ThrowAttack)
        {
            // 콤보카운트 0부터 시작
            _comboCount = 0;
            View.SetTrigger(PlayerView.Parameter.BalanceThrow);
        }
        else
        {
            // 콤보카운트 1씩 상승
            _comboCount = _comboCount < 4 ? _comboCount + 1 : 0;
            View.SetTrigger(PlayerView.Parameter.OnCombo);
        }

        if (Player.IsAttackFoward == true)
        {
            Player.LookAtAttackDir();
        }
    }
    public override void Exit()
    {
        if (_throwRoutine != null)
        {
            CoroutineHandler.StopRoutine(_throwRoutine);
            _throwRoutine = null;
        }
    }

    public override void OnTrigger()
    {
        ThrowObject();
    }
    public override void EndAnimation()
    {

    }
    public override void OnCombo()
    {


        if (_throwRoutine == null)
        {
            _throwRoutine = CoroutineHandler.StartRoutine(OnComboRoutine());
        }
    }
    public override void EndCombo()
    {
        if (_throwRoutine != null)
        {
            ChangeState(PlayerController.State.Idle);
        }
    }



    private void ThrowObject()
    {
        int throwObjectID = Model.ThrowObjectStack.Count > 0 ? Model.PopThrowObject().ID : 0;

        ThrowObject throwObject = Instantiate(DataContainer.GetThrowObject(throwObjectID), _muzzlePoint.position, _muzzlePoint.rotation);
        throwObject.Init(Player, (int)Model.PowerThrowAttack[0],Model.ThrowAdditionals);
        throwObject.Shoot(Player.ThrowPower);
    }
    IEnumerator OnComboRoutine()
    {
        while (true)
        {
            if (Player.CurState != PlayerController.State.ThrowAttack)
                yield break;

            if (InputKey.GetButtonDown(InputKey.Throw))
            {
                ChangeState(PlayerController.State.ThrowAttack);
            }
            else if (InputKey.GetButtonDown(InputKey.Melee))
            {
                ChangeState(PlayerController.State.MeleeAttack);
            }
            yield return null;
        }
    }
}
