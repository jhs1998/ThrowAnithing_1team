using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerModel))]
[RequireComponent(typeof(PlayerView))]
public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerModel Model;
    [HideInInspector] public PlayerView View;
    [HideInInspector] public Rigidbody Rb;

    #region 이벤트
    #endregion
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
    public Transform MuzzletPoint { get { return _attackStruct.MuzzlePoint; } set { _attackStruct.MuzzlePoint = value; } }
    public float AttackHeight { get { return _attackStruct.AttackHeight; } set { _attackStruct.AttackHeight = value; } }
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
        [Range(0f, 50f)] public float CameraRotateAngle;
        [Range(0f, 5f)] public float CameraRotateSpeed;
        public bool IsVerticalCameraMove;
    }
    [Header("카메라 관련 필드")]
    [SerializeField] private CameraStruct _cameraStruct;
    public Transform CamareArm { get { return _cameraStruct.CamaraArm; } set { _cameraStruct.CamaraArm = value; } }
    private Transform _cameraPos { get { return _cameraStruct.CameraPos; } set { _cameraStruct.CameraPos = value; } }
    private float _cameraRotateAngle { get { return _cameraStruct.CameraRotateAngle; } set { _cameraStruct.CameraRotateAngle = value; } }
    private float _cameraRotateSpeed { get { return _cameraStruct.CameraRotateSpeed; } set { _cameraStruct.CameraRotateSpeed = value; } }
    private bool _isVerticalCameraMove { get { return _cameraStruct.IsVerticalCameraMove; } set { _cameraStruct.IsVerticalCameraMove = value; } }
    #endregion
    #region 감지 관련 필드
    [System.Serializable]
    struct CheckStruct
    {
        public Transform GroundCheckPos;
        [Range(0, 1)] public float SlopeAngle;
        public WallCheckStruct WallCheckPos;
        public float WallCheckDistance;
        [Space(10)]
        public bool IsGround; // 지면 접촉 여부
        public bool IsWall; // 벽 접촉 여부
        public bool CanClimbSlope; // 오를 수 있는 경사면 각도 인지 체크
    }
    [System.Serializable]
    struct WallCheckStruct
    {
        public Transform Head;
        public Transform Foot;
    }
    [SerializeField] private CheckStruct _checkStruct;
    private Transform _groundCheckPos => _checkStruct.GroundCheckPos;
    private WallCheckStruct _wallCheckPos => _checkStruct.WallCheckPos;

    private float _wallCheckDistance { get { return _checkStruct.WallCheckDistance; } set { _checkStruct.WallCheckDistance = value; } }
    private float _slopeAngle { get { return _checkStruct.SlopeAngle; } set { _checkStruct.SlopeAngle = value; } }

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
    public enum State { Idle, Run, MeleeAttack, ThrowAttack, Jump, Fall, Dash, Size }

    private PlayerState[] _states = new PlayerState[(int)State.Size];
    public State CurState;
    public State PrevState;

    public bool IsGround { get { return _checkStruct.IsGround; } set { _checkStruct.IsGround =value; } }// 지면 접촉 여부
    public bool IsWall { get { return _checkStruct.IsWall; } set { _checkStruct.IsWall = value; } } // 벽 접촉 여부
    public bool CanClimbSlope { get { return _checkStruct.CanClimbSlope; } set { _checkStruct.CanClimbSlope = value; } } // 오를 수 있는 경사면 각도 인지 체크

    public Collider[] OverLapColliders = new Collider[100];

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        InitUIEvent();
        StartRoutine();
        Camera.main.transform.SetParent(_cameraPos, true);
        _states[(int)CurState].Enter();
    }

    private void OnDisable()
    {
        _states[(int)CurState].Exit();
    }

    private void Update()
    {
        _states[(int)CurState].Update();

        CheckAnyState();
        RotateCamera();

        TestInput();
    }

    private void FixedUpdate()
    {
        _states[(int)CurState].FixedUpdate();
        CheckGround();
        CheckWall();
    }

    private void OnDrawGizmos()
    {
        if (_states[(int)CurState] == null)
            return;
        _states[(int)CurState].OnDrawGizmos();

        DrawCheckGround();
        DrawWallCheck();
    }

    /// <summary>
    /// 상태 변경
    /// </summary>
    public void ChangeState(State state)
    {
        if(_states[(int)state].UseStamina == true && Model.CurStamina < 0.1f)
        {
            return;
        }

        _states[(int)CurState].Exit();
        PrevState = CurState;
        CurState = state;
        _states[(int)CurState].Enter();

        //Debug.Log(CurState);
    }

    #region Instantiate 대리 메서드
    public T InstantiateObject<T>(T instance) where T : Component
    {
        T instanceObject = Instantiate(instance);
        return instanceObject;
    }
    public T InstantiateObject<T>(T instance, Transform parent) where T : Component
    {
        T instanceObject = Instantiate(instance, parent);
        return instanceObject;
    }
    public T InstantiateObject<T>(T instance, Vector3 pos, Quaternion rot) where T : Component
    {
        T instanceObject = Instantiate(instance, pos, rot);
        return instanceObject;
    }
    #endregion

    /// <summary>
    /// 오브젝트 줍기
    /// </summary>
    public void AddThrowObject(ThrowObject throwObject)
    {

        if (Model.CurThrowCount < Model.MaxThrowCount)
        {
            Model.PushThrowObject(DataContainer.GetThrowObject(throwObject.Data.ID).Data);
            Destroy(throwObject.gameObject);
        }
    }

    /// <summary>
    /// 추가 공격효과 추가
    /// </summary>
    /// <param name="hitAdditional"></param>
    public void AddHitAdditional(HitAdditional hitAdditional)
    {
        Model.HitAdditionals.Add(hitAdditional);
    }

    private void CheckAnyState()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && CurState != State.Dash)
        {
            _states[(int)CurState].OnDash();
            ChangeState(PlayerController.State.Dash);
        }
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
        x = x < 180 ? Mathf.Clamp(x, -10f, 50f) : Mathf.Clamp(x, 360f - _cameraRotateAngle, 361f);
        CamareArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);

        // 머즐포인트 각도조절
        MuzzletPoint.rotation = Quaternion.Euler(x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    /// <summary>
    /// 지면 체크
    /// </summary>
    private void CheckGround()
    {
        // 살짝위에서 쏨
        Vector3 CheckPos = _groundCheckPos.position;

        if (Physics.SphereCast(CheckPos, 0.25f, Vector3.down, out RaycastHit hit, 0.4f))
        {
            //Debug.Log("지면");
            IsGround = true;
            // 오를 수 있는 경사면 체크
            Vector3 normal = hit.normal;
            if (normal.y > 1 - _slopeAngle)
            {
                CanClimbSlope = true;
            }
            else
            {
                CanClimbSlope = false;
            }
        }
        else
        {
            // Debug.Log("공중");
            IsGround = false;
            CanClimbSlope = false;
        }
    }

    private void DrawCheckGround()
    {
        Gizmos.color = Color.yellow;

        Vector3 CheckPos = _groundCheckPos.position;

        if (Physics.SphereCast(CheckPos, 0.25f, Vector3.down, out RaycastHit hit, 0.4f))
        {
            Gizmos.DrawLine(CheckPos, hit.point);
            Gizmos.DrawWireSphere(CheckPos + Vector3.down * hit.distance, 0.3f);
        }
    }

    /// <summary>
    /// 벽체크
    /// </summary>
    private void CheckWall()
    {
        int hitCount = Physics.OverlapCapsuleNonAlloc(_wallCheckPos.Foot.position, _wallCheckPos.Head.position, _wallCheckDistance, OverLapColliders, 1 << 6);

        if (hitCount > 0)
        {
            IsWall = true;
        }
        else
        {
            IsWall = false;
        }

    }

    private void DrawWallCheck()
    {
        Gizmos.color = Color.green;

        Vector3 footPos = _wallCheckPos.Foot.position + transform.forward * _wallCheckDistance;
        Vector3 headPos = _wallCheckPos.Head.position + transform.forward * _wallCheckDistance;

        Gizmos.DrawLine(footPos, headPos);
    }

    private void TestInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ThrowObject throwObject = Instantiate(_throwPrefab);
            Model.PushThrowObject(DataContainer.GetThrowObject(throwObject.Data.ID).Data);
            Destroy(throwObject.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(1);
        }
    }

    /// <summary>
    /// 스테미나 회복 코루틴
    /// </summary>
    IEnumerator RecoveryStamina()
    {
        while (true)
        {
            // 초당 MaxStamina / StaminaRecoveryTime 만큼 회복
            // 현재 스테미나가 꽉찼으면 더이상 회복안함
            // 만약 스테미나가 0이하로 떨어지면 일정시간동안 스테미나 회복 안함
            Model.CurStamina += (Model.MaxStamina / Model.StaminaRecoveryTime) * Time.deltaTime;
            if(Model.CurStamina >= Model.MaxStamina)
            {
                Model.CurStamina = Model.MaxStamina;
            }

            if(Model.CurStamina < 0)
            {
                yield return Model.StaminaCoolTime.GetDelay();
                Model.CurStamina = 0;
            }

            yield return null;
        }
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
        _states[(int)State.ThrowAttack] = new ThrowState(this);         // 투척공격
        _states[(int)State.Jump] = new JumpState(this);                 // 점프
        _states[(int)State.Fall] = new FallState(this);                 // 추락
        _states[(int)State.Dash] = new DashState(this);                 // 대쉬
    }

    /// <summary>
    /// UI이벤트 설정
    /// </summary>
    private void InitUIEvent()
    {
        Model.CurThrowCountSubject
            .DistinctUntilChanged()
            .Subscribe(x => View.UpdateText(View.Panel.ThrowCount, $"{x} / {Model.MaxThrowCount}"));
        View.UpdateText(View.Panel.ThrowCount, $"{Model.CurThrowCount} / {Model.MaxThrowCount}");


        Model.CurStaminaSubject
            .DistinctUntilChanged()
            .Subscribe(x => View.Panel.StaminaSlider.value = x);
        View.Panel.StaminaSlider.value = Model.CurStamina;
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

    private void StartRoutine()
    {
        StartCoroutine(RecoveryStamina());
    }

}
