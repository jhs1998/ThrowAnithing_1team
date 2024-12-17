using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using UnityEngine;

public class MeleeAttackState : PlayerState
{
    private float _atttackBufferTime;
    private bool _isCombe;
    private bool _isChangeAttack;
    private float _attackHeight;

    Collider[] colliders = new Collider[20];
    public MeleeAttackState(PlayerController controller) : base(controller)
    {
        _atttackBufferTime = Player.AttackBufferTime;
        _attackHeight = Player.AttackHeight;

        View.OnMeleeAttackEvent += AttackMelee;
    }

    public override void Enter()
    {
        _isChangeAttack = false;
        if (View.GetBool(PlayerView.Parameter.MeleeCombo) == false)
        {
            // 첫 공격일 경우 근접공격 애니메이션 시작
            View.SetTrigger(PlayerView.Parameter.MeleeAttack);
        }
        else
        {
            Model.MeleeComboCount++;
        }
        CoroutineHandler.StartRoutine(MeleeAttackRoutine());
    }

    public override void Update()
    {
        //Debug.Log("Melee");
    }

    public override void Exit()
    {

    }


    /// <summary>
    /// 근접 공격
    /// </summary>
    public void AttackMelee()
    {
        // 전방 앞에 있는 몬스터들을 확인하고 피격 진행
        // 1. 전방에 있는 몬스터 확인
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + _attackHeight, transform.position.z);
        Vector3 attackPos = playerPos;
        int hitCount = Physics.OverlapSphereNonAlloc(attackPos, Model.Range, colliders, 1 << 4);
        for (int i = 0; i < hitCount; i++)
        {
            // 2. 각도 내에 있는지 확인
            Vector3 source = transform.position;
            source.y = 0;
            Vector3 destination = colliders[i].transform.position;
            destination.y = 0;

            Vector3 targetDir = (destination - source).normalized;
            float targetAngle = Vector3.Angle(transform.forward, targetDir); // 아크코사인 필요 (느리다)
            if (targetAngle > Model.Angle * 0.5f)
                continue;

            IHit hit = colliders[i].GetComponent<IHit>();

            int attackDamage = (int)(Model.Damage * Model.DamageMultiplier);
            hit.TakeDamage(attackDamage);
        }
    }



    IEnumerator MeleeAttackRoutine()
    {
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

        yield return null;
        float timeCount = _atttackBufferTime;
        while (View.IsAnimationFinish == false)
        {       
            // 공격 버퍼
            if (Input.GetButtonDown("Fire1"))
            {
                // 다음 공격 대기
                _isCombe = true;
                View.SetBool(PlayerView.Parameter.MeleeCombo, true);
                timeCount = _atttackBufferTime;
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                // 던지기 공격 전환
                _isCombe = false;
                _isChangeAttack = true;
                View.SetBool(PlayerView.Parameter.MeleeCombo, false);
                timeCount = _atttackBufferTime;
            }

            // 버퍼 타이머
            timeCount -= Time.deltaTime;
            if (timeCount <= 0)
            {
                // 다음 공격 취소
                _isCombe = false;
                View.SetBool(PlayerView.Parameter.MeleeCombo, false);
                timeCount = _atttackBufferTime;
            }

            yield return null;
        }

        if (_isCombe == true)
        {
            Player.ChangeState(PlayerController.State.MeleeAttack);
        }
        else if (_isChangeAttack == true)
        {
            Model.MeleeComboCount = 0;
            Player.ChangeState(PlayerController.State.ThrowAttack);
        }
        else
        {
            Model.MeleeComboCount = 0;
            Player.ChangeState(PlayerController.State.Idle);
        }

    }
}
