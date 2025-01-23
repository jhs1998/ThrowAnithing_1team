using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Balance PrevThrow", menuName = "Arm/AttackType/Balance/PrevThrow")]
public class BalanceThrowAttack : ArmThrowAttack
{
    [System.Serializable]
    struct AttackStruct
    {
        [Tooltip("�߰� ������")]
        public float Damage;
        [Tooltip("CC��")]
        public CrowdControlType CCType;
        [Tooltip("�˹� �Ÿ�")]
        public float KnockBackDistance;
    }
    [SerializeField] private AttackStruct[] _attacks;

    private BalanceArm _balance => arm as BalanceArm;

    private int _comboCount; // �޺� Ƚ��
    Coroutine _throwRoutine;
    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;

        // ù ���� �� ù ���� �ִϸ��̼� ����
        if (Player.PrevState != PlayerController.State.ThrowAttack)
        {
            // �޺�ī��Ʈ 0���� ����
            _comboCount = 0;
          
            if (_balance.OnFirstSpecial == true)
            {
                // 1�ܰ� ������ ���������� Ư�� ��ô���
                View.SetTrigger(PlayerView.Parameter.BalanceSpecial1);
            }
            else
            {

                View.SetTrigger(PlayerView.Parameter.BalanceThrow);
            }
        }
        else
        {
            // Ư������ ���� ������ 2�ܰ� ������
            int maxCount = _balance.OnFirstSpecial == true ? 1 : 3;


            // �޺�ī��Ʈ 1�� ���
            _comboCount = _comboCount < maxCount ? _comboCount + 1 : 0;
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
        int throwObjectID = default;
        if (_balance.OnFirstSpecial == true)
        {
            // Ư�� ���� �����ÿ� ��ô�� �Ҹ� ����
            throwObjectID = Model.ThrowObjectStack.Count > 0 ? Model.PeekThrowObject().ID : 0;
        }
        else
        {
            throwObjectID = Model.ThrowObjectStack.Count > 0 ? Model.PopThrowObject().ID : 0;
        }
    

        ThrowObject throwObject = Instantiate(DataContainer.GetThrowObject(throwObjectID), _muzzlePoint.position, _muzzlePoint.rotation);

        bool isBoom = _comboCount == 3 ? true : false;


        throwObject.Init(Player, _attacks[_comboCount].CCType, isBoom, (int)_attacks[_comboCount].Damage, Model.ThrowAdditionals);
        throwObject.KnockBackDistance = _attacks[_comboCount].KnockBackDistance;

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
                _throwRoutine = null;
                yield break;
            }
            yield return null;
        }
    }
}
