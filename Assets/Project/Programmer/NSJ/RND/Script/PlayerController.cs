
using Assets.Project.Programmer.NSJ.RND.Script;
using UnityEngine;

[RequireComponent(typeof(PlayerModel))]
[RequireComponent(typeof(PlayerView))]
public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerModel Model;
    [HideInInspector] public PlayerView View;
    [HideInInspector] public Rigidbody Rb;

    Collider[] colliders = new Collider[20];

    #region 공격 관련 필드
    [System.Serializable]
    struct AttackStruct
    {
        public float AttackHeight;
        public float AttackBufferTime;
        public Transform MuzzlePoint;
        public ThrowObject ThrowPrefab;
    }
    [Header("공격 관련 필드")]
    [SerializeField] private AttackStruct _attackStruct;
    public float AttackBufferTime { get { return _attackStruct.AttackBufferTime; } set { _attackStruct.AttackBufferTime = value; } }
    private float _attackHeight { get { return _attackStruct.AttackHeight; } set { _attackStruct.AttackHeight = value; } }
    private Transform _muzzltPoint { get { return _attackStruct.MuzzlePoint; } set { _attackStruct.MuzzlePoint = value; } }
    private ThrowObject _throwPrefab { get { return _attackStruct.ThrowPrefab; } set { _attackStruct.ThrowPrefab = value; } }
    #endregion
    #region Camera 관련 필드
    /// <summary>
    /// 카메라 관련
    /// </summary>
    [System.Serializable]
    struct CameraStruct
    {
        public Transform CamaraArm;
        public Transform CameraPos;
        [Range(0f, 5f)] public float CameraRotateSpeed;
        public bool IsVerticalCameraMove;
    }
    [Header("카메라 관련 필드")]
    [SerializeField] private CameraStruct _cameraStruct;
    public Transform CamareArm { get { return _cameraStruct.CamaraArm; } set { _cameraStruct.CamaraArm = value; } }
    private Transform _cameraPos { get { return _cameraStruct.CameraPos; } set { _cameraStruct.CameraPos = value; } }
    private float _cameraRotateSpeed { get { return _cameraStruct.CameraRotateSpeed; } set { _cameraStruct.CameraRotateSpeed = value; } }
    private bool _isVerticalCameraMove { get { return _cameraStruct.IsVerticalCameraMove; } set { _cameraStruct.IsVerticalCameraMove = value; } }
    #endregion
    #region 테스트 관련 필드
    [System.Serializable]
    public struct TestStruct
    {
        public bool IsAttackForward;
    }
    [Header("테스트 관련 필드")]
    [SerializeField] private TestStruct _testStruct;
    public bool IsAttackFoward { get { return _testStruct.IsAttackForward; } }
    #endregion
    public enum State { Idle, Run, MeleeAttack, ThrowAttack, Size }

    private PlayerState[] _states = new PlayerState[(int)State.Size];
    private State _curState;


    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        Camera.main.transform.SetParent(_cameraPos, true);
        _states[(int)_curState].Enter();
    }

    private void OnDisable()
    {
        _states[(int)_curState].Exit();
    }

    private void Update()
    {
        _states[(int)_curState].Update();

        RotateCamera();
    }

    private void FixedUpdate()
    {
        _states[(int)_curState].FixedUpdate();
    }

    /// <summary>
    /// 상태 변경
    /// </summary>
    public void ChangeState(State state)
    {
        _states[(int)_curState].Exit();
        _curState = state;
        _states[(int)_curState].Enter();
    }

    /// <summary>
    /// 오브젝트 던지기 공격
    /// </summary>
    public void ThrowObject()
    {
        ThrowObject throwObject = Instantiate(_throwPrefab, _muzzltPoint.position, _muzzltPoint.rotation);
        throwObject.Init(Model.Throw, Model.HitAdditionals);
        throwObject.Shoot();
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

            int attackDamage = (int)(Model.MeleeDamage * Model.DamageMultiplier);
            hit.TakeDamage(attackDamage);
        }
    }

    public void AddHitAdditional(HitAdditional hitAdditional)
    {
        Model.HitAdditionals.Add(hitAdditional);
    }

    private void OnDrawGizmos()
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
    /// <summary>
    /// TPS 시점 카메라 회전
    /// </summary>
    private void RotateCamera()
    {
        float angleX = Input.GetAxis("Mouse X");
        float angleY = default;
        // 체크시 마우스 상하도 가능
        if (_isVerticalCameraMove == true)
            angleY = Input.GetAxis("Mouse Y");
        Vector2 mouseDelta = new Vector2(angleX, angleY) * _cameraRotateSpeed;
        Vector3 camAngle = CamareArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;
        x = x < 180 ? Mathf.Clamp(x, -10f, 50f) : Mathf.Clamp(x, 340f, 361f);
        CamareArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    // 초기 설정 ============================================================================================================================================ //
    /// <summary>
    /// 초기 설정
    /// </summary>
    private void Init()
    {
        InitGetComponent();
        InitPlayerStates();
    }

    /// <summary>
    /// 플레이어 상태 배열 설정
    /// </summary>
    private void InitPlayerStates()
    {
        _states[(int)State.Idle] = new IdleState(this);                 // Idle
        _states[(int)State.Run] = new RunState(this);                   // 이동(달리기)
        _states[(int)State.MeleeAttack] = new MeleeAttackState(this);   // 근접공격
        _states[(int)State.ThrowAttack] = new ThrowState(this);
    }

    /// <summary>
    /// 초기 겟컴포넌트 설정
    /// </summary>
    private void InitGetComponent()
    {
        Model = GetComponent<PlayerModel>();
        View = GetComponent<PlayerView>();
        Rb = GetComponent<Rigidbody>();
    }
}
