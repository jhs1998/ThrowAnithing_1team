using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Balance PowerJumpDown", menuName = "Arm/AttackType/Balance/PowerJumpDown")]
public class BalanceJumpDown : ArmJumpDown
{
    [System.Serializable]
    struct AttackStruct
    {
        public GameObject Effect;
        [Range(0, 20)]
        public float FallSpeed;
        public float Range;
        public int Damage;
        public float KnockBackDistance;
        public float EffectDuration;
    }
    [SerializeField] AttackStruct _attack;
     private float _fallSpeed { get { return _attack.FallSpeed; } set { _attack.FallSpeed = value; } }
     private float _range { get { return _attack.Range; } set { _attack.Range = value; } }
    private int _damage { get { return _attack.Damage; } set { _attack.Damage = value; } }
    GameObject _attackEffect { get { return _attack.Effect; } set { _attack.Effect = value; } }
    private float _maxScaleEffectTime { get { return _attack.EffectDuration; } set { _attack.EffectDuration = value; } }

    private Vector3 _landingPoint;
    Coroutine _fallRoutine;
    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;
        Player.Rb.AddForce(Vector3.down * _fallSpeed, ForceMode.Impulse);
        View.SetTrigger(PlayerView.Parameter.BalanceJumpDown);

        if(_fallRoutine == null)
        {
            _fallRoutine = CoroutineHandler.StartRoutine(FallRoutine());
        }
    
    }
    public override void Exit()
    {
        if (_fallRoutine != null)
        {
            CoroutineHandler.StopRoutine(_fallRoutine);
            _fallRoutine = null;
        }

        Player.IsJumpAttack = false;
        Player.IsDoubleJump = false;
    }


    public override void Update()
    {
        if (Player.IsGround)
        {
            Rb.velocity = Vector3.zero;
        }
    }



    public override void EndAnimation()
    {
        ChangeState(PlayerController.State.Idle);
    }

    IEnumerator FallRoutine()
    {
        while (Player.IsGround == false)
        {
            // �������� ���������� ����� ����� ��������ٸ� ���� üũ ���� ����
            if (Rb.velocity.y < 0)
            {
                Vector3 CheckPos = new Vector3(transform.position.x, transform.position.y + 0.31f, transform.position.z);
                if (Physics.SphereCast(CheckPos, 0.3f, Vector3.down, out RaycastHit hit, _fallSpeed / (_fallSpeed / 2),Layer.EveryThing,QueryTriggerInteraction.Ignore))
                {
                    if (hit.transform.gameObject.layer != Layer.Monster)
                    {
                        _landingPoint = hit.point;
                        break;
                    }
                
                }

            }
            yield return null;
        }

        View.SetTrigger(PlayerView.Parameter.Landing);

        // ����
        SoundManager.PlaySFX(Player.Sound.Move.JumpAttack);

        // �÷��̾ ������ ���� ����������� ���
        while (Player.IsGround == false)
        {
            yield return null;
        }
        // ������ �ֱ�
        AttackJumpDown();
    }

    private void AttackJumpDown()
    {

        int hitCount = Physics.OverlapSphereNonAlloc(_landingPoint, _range, Player.OverLapColliders, 1 << Layer.Monster);
        int finalDamage = Player.GetFinalDamage(_damage, out bool isCritical);
        for (int i = 0; i < hitCount; i++)
        {
            Battle.TargetAttackWithDebuff(Player.OverLapColliders[i], isCritical, finalDamage,   false );
            Battle.TargetCrowdControl(Player.OverLapColliders[i],CrowdControlType.Stiff);
            // �˹�
            Player.DoKnockBack(Player.OverLapColliders[i].transform, transform, _attack.KnockBackDistance);
        }

        // ����Ʈ ����
        ObjectPool.GetPool(_attack.Effect, _landingPoint, Quaternion.identity, 2f);
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
