using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Basic JumpDown", menuName = "Arm/AttackType/Basic/JumpDown")]
public class BasicJumpDown : ArmJumpDown
{
    [Range(0, 20)]
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] GameObject _attackEffect;
    [SerializeField] private float _maxScaleEffectTime;

    private Vector3 _landingPoint;
    Coroutine _fallRoutine;
    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;
        Player.Rb.AddForce(Vector3.down * _attackSpeed, ForceMode.Impulse);
        View.SetTrigger(PlayerView.Parameter.JumpDown);

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
            // 떨어지기 시작했을때 지면과 충분히 가까워졌다면 지면 체크 로직 종료
            if (Rb.velocity.y < 0)
            {
                Vector3 CheckPos = new Vector3(transform.position.x, transform.position.y + 0.31f, transform.position.z);
                if (Physics.SphereCast(CheckPos, 0.3f, Vector3.down, out RaycastHit hit, _attackSpeed / (_attackSpeed / 2)))
                {
                    _landingPoint = hit.point;
                    break;
                }

            }
            yield return null;
        }

        View.SetTrigger(PlayerView.Parameter.Landing);

        // 플레이어가 실제로 땅에 닿았을때까지 대기
        while (Player.IsGround == false)
        {
            yield return null;
        }
        // 데미지 주기
        AttackJumpDown();
    }

    private void AttackJumpDown()
    {
        CoroutineHandler.StartRoutine(CreateAttackEffectRoutien());

        int hitCount = Physics.OverlapSphereNonAlloc(_landingPoint, _range, Player.OverLapColliders, 1 << Layer.Monster);
        int finalDamage = Player.GetFinalDamage(_damage, out bool isCritical);
        for (int i = 0; i < hitCount; i++)
        {
            Battle.TargetAttackWithDebuff(Player.OverLapColliders[i], finalDamage, true, isCritical);
            // 넉백
            Player.DoKnockBack(Player.OverLapColliders[i].transform, transform, 1f);
        }
    }

    IEnumerator CreateAttackEffectRoutien()
    {
        if (_attackEffect == null)
            yield break;

        GameObject instance = Instantiate(_attackEffect, _landingPoint, transform.rotation);
        while (true)
        {
            instance.transform.localScale = new Vector3(
              instance.transform.localScale.x + _range * 2 * Time.deltaTime * (1 / _maxScaleEffectTime),
              instance.transform.localScale.y + _range * 2 * Time.deltaTime * (1 / _maxScaleEffectTime),
              instance.transform.localScale.z + _range * 2 * Time.deltaTime * (1 / _maxScaleEffectTime));
            if (instance.transform.localScale.x > _range * 2)
            {
                break;
            }
            yield return null;
        }

        Destroy(instance);
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
