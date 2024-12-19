using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using UnityEngine;

public class MeleeAttackState : PlayerState
{
    private float _atttackBufferTime;
    private bool m_isCombo;
    private bool _isCombo
    {
        get { return m_isCombo; }
        set
        {
            m_isCombo = value;
            View.SetBool(PlayerView.Parameter.MeleeCombo, m_isCombo);
        }
    }
    private bool _isChangeAttack;
    private float _attackHeight; 

    Coroutine _meleeRoutine;
    public MeleeAttackState(PlayerController controller) : base(controller)
    {
        _atttackBufferTime = Player.AttackBufferTime;
        _attackHeight = Player.AttackHeight;

        View.OnMeleeAttackEvent += AttackMelee;
    }

    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;
        _isChangeAttack = false;

        if (_isCombo == false)
        {
            // 첫 공격일 경우 근접공격 애니메이션 시작
            View.SetTrigger(PlayerView.Parameter.MeleeAttack);
        }
        else
        {
            Model.MeleeComboCount++;
        }

        if(_meleeRoutine == null)
        {
            _meleeRoutine = CoroutineHandler.StartRoutine(MeleeAttackRoutine());
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

    public override void OnDash()
    {
        _isCombo = false;
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
        int hitCount = Physics.OverlapSphereNonAlloc(attackPos, Model.Range, Player.OverLapColliders, 1 << 4);
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
        while (View.GetIsAnimFinish(PlayerView.Parameter.MeleeAttack) == false)
        {       
            // 공격 버퍼
            if (Input.GetButtonDown("Fire1"))
            {
                // 다음 공격 대기
                _isCombo = true;
                timeCount = _atttackBufferTime;
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                // 던지기 공격 전환
                _isCombo = false;
                _isChangeAttack = true;
                timeCount = _atttackBufferTime;
            }

            // 버퍼 타이머
            timeCount -= Time.deltaTime;
            if (timeCount <= 0)
            {
                // 다음 공격 취소
                _isCombo = false;
                timeCount = _atttackBufferTime;
            }

            yield return null;
        }
        
        // 콤보선입력 되었을때 다시 근접 공격 
        if (_isCombo == true)
        {         
            ChangeState(PlayerController.State.MeleeAttack);
        }
        // 어택 명령이 바뀌었을 때 투척 공격
        else if (_isChangeAttack == true)
        {
            ChangeState(PlayerController.State.ThrowAttack);
        }
        // 아무 입력도 없었을 때 평상시모드
        else
        {
            ChangeState(PlayerController.State.Idle);
        }
    }

    public override void OnDrawGizmos()
    {
        if (Model == null)
            return;
        //거리
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + _attackHeight, transform.position.z);
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
