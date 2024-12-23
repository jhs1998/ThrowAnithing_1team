using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_power Melee", menuName = "Arm/AttackType/_power/Melee")]
public class PowerMeleeAttack : ArmMeleeAttack
{
    [System.Serializable]
    struct ChargeStruct
    {
        public float ChargeTime;
        public int Damage;
        public float AttackRange;
        [Range(0,180)]public float AttackAngle;
    }
    [SerializeField] private ChargeStruct[] _charges;
    private float m_curChargeTime;
    private float _curChargeTime
    {
        get { return m_curChargeTime; }
        set
        {
            m_curChargeTime = value;
            View.SetFloat(PlayerView.Parameter.Charge, m_curChargeTime);
        }
    }
    private int _index;
    Coroutine _chargeRoutine;
    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;
        _curChargeTime = 0;
        _index = 0;
        Player.LookAtCameraFoward();
        View.SetTrigger(PlayerView.Parameter.PowerMelee);
        if(_chargeRoutine == null)
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
        AttackMelee();
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
            ProcessCharge();

            if (Input.GetKeyUp(KeyCode.V))
            {
                Player.LookAtCameraFoward();
                View.SetTrigger(PlayerView.Parameter.ChargeEnd);
                _chargeRoutine = null;
                break;
            }
            yield return null;
        }
    }

    private void ProcessCharge()
    {
        // 차지시간 계산
        _curChargeTime += Time.deltaTime * View.GetFloat(PlayerView.Parameter.AttackSpeed);
        if (_charges.Length > _index + 1)
        {
            if (_curChargeTime > _charges[_index + 1].ChargeTime)
            {
                _index++;
            }
        }
        else
        {
            _curChargeTime = _charges[_index].ChargeTime + 0.01f;

        }
    }
    public void AttackMelee()
    {
        // 전방 앞에 있는 몬스터들을 확인하고 피격 진행
        // 1. 전방에 있는 몬스터 확인
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + Player.AttackHeight, transform.position.z);
        Vector3 attackPos = playerPos;
        int hitCount = Physics.OverlapSphereNonAlloc(attackPos, _charges[_index].AttackRange, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            // 2. 각도 내에 있는지 확인
            Vector3 source = transform.position;
            source.y = 0;
            Vector3 destination = Player.OverLapColliders[i].transform.position;
            destination.y = 0;

            Vector3 targetDir = (destination - source).normalized;
            float targetAngle = Vector3.Angle(transform.forward, targetDir); // 아크코사인 필요 (느리다)
            if (targetAngle > _charges[_index].AttackAngle * 0.5f)
                continue;

            IHit hit = Player.OverLapColliders[i].GetComponent<IHit>();

            int attackDamage = (int)(Model.Damage + _charges[_index].Damage);
            hit.TakeDamage(attackDamage);

            if (_index == 0)
                break;
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
        Gizmos.DrawWireSphere(attackPos, _charges[_index].AttackRange);

        //각도
        Vector3 rightDir = Quaternion.Euler(0, _charges[_index].AttackAngle * 0.5f, 0) * transform.forward;
        Vector3 leftDir = Quaternion.Euler(0, _charges[_index].AttackAngle* -0.5f, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + rightDir * _charges[_index].AttackRange);
        Gizmos.DrawLine(transform.position, transform.position + leftDir * _charges[_index].AttackRange);
    }
}
