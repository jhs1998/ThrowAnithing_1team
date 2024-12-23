using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Basic Melee", menuName = "Arm/AttackType/Basic/Melee")]
public class BasicMeleeAttack : ArmMeleeAttack
{
    Coroutine _meleeRoutine;

    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;

        // 첫 공격 시 첫 공격 애니메이션 실행
        if (Player.PrevState != PlayerController.State.MeleeAttack)
        {
            View.SetTrigger(PlayerView.Parameter.BasicMelee);
        }
        else
        {
            View.SetTrigger(PlayerView.Parameter.OnCombo);
        }

        if (Player.IsAttackFoward == true)
        {
            // 카메라 방향으로 플레이어가 바라보게
            Quaternion cameraRot = Quaternion.Euler(0, Player.CamareArm.eulerAngles.y, 0);
            transform.rotation = cameraRot;
            // 카메라는 다시 로컬 기준 전방 방향
            if (Player.CamareArm.parent != null)
            {
                Player.CamareArm.localRotation = Quaternion.Euler(Player.CamareArm.localRotation.eulerAngles.x, 0, 0);
            }
        }

    }

    public override void Update()
    {
        //Debug.Log("Melee");
    }

    public override void Exit()
    {
        if (_meleeRoutine != null)
        {
            CoroutineHandler.StopRoutine(_meleeRoutine);
            _meleeRoutine = null;
        }

    }
    public override void OnTrigger()
    {
        AttackMelee();
    }
    /// <summary>
    /// 근접 공격
    /// </summary>
    public void AttackMelee()
    {
        // 전방 앞에 있는 몬스터들을 확인하고 피격 진행
        // 1. 전방에 있는 몬스터 확인
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + Player.AttackHeight, transform.position.z);
        Vector3 attackPos = playerPos;
        int hitCount = Physics.OverlapSphereNonAlloc(attackPos, Model.Range, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            // 2. 각도 내에 있는지 확인
            Vector3 source = transform.position;
            source.y = 0;
            Vector3 destination = Player.OverLapColliders[i].transform.position;
            destination.y = 0;

            Vector3 targetDir = (destination - source).normalized;
            float targetAngle = Vector3.Angle(transform.forward, targetDir); // 아크코사인 필요 (느리다)
            if (targetAngle > Model.Angle * 0.5f)
                continue;

            IHit hit = Player.OverLapColliders[i].GetComponent<IHit>();

            int attackDamage = (int)(Model.Damage * Model.DamageMultiplier);
            hit.TakeDamage(attackDamage);
        }
    }

    public override void OnCombo()
    {
        if (_meleeRoutine == null)
        {
            _meleeRoutine = CoroutineHandler.StartRoutine(OnComboRoutine());
        }
    }

    public override void EndCombo()
    {
        if (_meleeRoutine != null)
        {
            ChangeState(PlayerController.State.Idle);
        }

    }



    IEnumerator OnComboRoutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                ChangeState(PlayerController.State.MeleeAttack);
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                ChangeState(PlayerController.State.ThrowAttack);
            }
            yield return null;
        }
    }

    public override void OnDrawGizmos()
    {
        if (Model == null)
            return;
        //거리
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + Player.AttackHeight, transform.position.z);
        Vector3 attackPos = playerPos;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos, Model.Range);

        //각도
        Vector3 rightDir = Quaternion.Euler(0, Model.Angle * 0.5f, 0) * transform.forward;
        Vector3 leftDir = Quaternion.Euler(0, Model.Angle * -0.5f, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + rightDir * Model.Range);
        Gizmos.DrawLine(transform.position, transform.position + leftDir * Model.Range);
    }
}
