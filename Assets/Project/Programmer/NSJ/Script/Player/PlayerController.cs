
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PlayerModel))]
[RequireComponent(typeof(PlayerView))]
[RequireComponent(typeof(BattleSystem))]
public class PlayerController : MonoBehaviour, IHit
{
    [SerializeField] public Transform ArmPoint;
    [Inject]
    public OptionSetting setting;

    [HideInInspector] public PlayerModel Model;
    [HideInInspector] public PlayerView View;
    [HideInInspector] public Rigidbody Rb;
    [HideInInspector] public BattleSystem Battle;
    public enum State
    {
        Idle,
        Run,
        MeleeAttack,
        ThrowAttack,
        Jump,
        DoubleJump,
        Fall,
        DoubleJumpFall,
        JumpAttack,
        JumpDown,
        Dash,
        Drain,
        SpecialAttack,
        Hit,
        Dead,
        Size
    }

    private PlayerState[] _states = new PlayerState[(int)State.Size];
    public State CurState;
    public State PrevState;

    #region 이벤트
    public event UnityAction<int, bool> OnPlayerHitEvent;
    public event UnityAction OnPlayerDieEvent;
    #endregion
    #region 공격 관련 필드
    [System.Serializable]
    struct AttackStruct
    {
        public float AttackHeight;
        public float ThrowPower;
        public Transform MuzzlePoint;
        [HideInInspector] public bool IsTargetHolding;
        [HideInInspector] public bool IsTargetToggle;
        [HideInInspector] public Vector3 TargetPos;
    }
    [Header("공격 관련 필드")]
    [SerializeField] private AttackStruct _attackStruct;
    public Transform MuzzletPoint { get { return _attackStruct.MuzzlePoint; } set { _attackStruct.MuzzlePoint = value; } }
    public float AttackHeight { get { return _attackStruct.AttackHeight; } set { _attackStruct.AttackHeight = value; } }
    public float ThrowPower { get { return _attackStruct.ThrowPower; } set { _attackStruct.ThrowPower = value; } }
    public bool IsTargetHolding { get { return _attackStruct.IsTargetHolding; } set { _attackStruct.IsTargetHolding = value; } }
    public bool IsTargetToggle { get { return _attackStruct.IsTargetToggle; } set { _attackStruct.IsTargetToggle = value; } }
    public Vector3 TargetPos { get { return _attackStruct.TargetPos; } set { _attackStruct.TargetPos = value; } }
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
        public PlayerCameraHold CameraHolder;
        public bool IsVerticalCameraMove;
    }
    [Header("카메라 관련 필드")]
    [SerializeField] private CameraStruct _cameraStruct;
    public Transform CamareArm { get { return _cameraStruct.CamaraArm; } set { _cameraStruct.CamaraArm = value; } }
    private Transform _cameraPos { get { return _cameraStruct.CameraPos; } set { _cameraStruct.CameraPos = value; } }
    private float _cameraRotateAngle { get { return _cameraStruct.CameraRotateAngle; } set { _cameraStruct.CameraRotateAngle = value; } }
    private float _cameraRotateSpeed { get { return _cameraStruct.CameraRotateSpeed; } set { _cameraStruct.CameraRotateSpeed = value; } }
    private PlayerCameraHold _cameraHolder { get { return _cameraStruct.CameraHolder; } set { _cameraStruct.CameraHolder = value; } }
    public bool IsVerticalCameraMove { get { return _cameraStruct.IsVerticalCameraMove; } set { _cameraStruct.IsVerticalCameraMove = value; } }
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
        public bool IsNearGround;
        public bool IsWall; // 벽 접촉 여부
        public bool CanClimbSlope; // 오를 수 있는 경사면 각도 인지 체크
    }
    [System.Serializable]
    public struct WallCheckStruct
    {
        public Transform Head;
        public Transform Foot;
    }
    [SerializeField] private CheckStruct _checkStruct;
    private Transform _groundCheckPos => _checkStruct.GroundCheckPos;
    public WallCheckStruct WallCheckPos => _checkStruct.WallCheckPos;

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
    #region 조건체크 Bool 필드
    [System.Serializable]
    public struct BoolField
    {
        public bool IsDoubleJump; // 더블점프 했음?
        public bool IsJumpAttack; // 점프공격 했음?
        public bool IsInvincible; // 무적상태임?
        public bool IsHit; // 맞음?
        public bool IsDead; // 죽음?
        public bool IsStaminaCool; // 스테미나 사용 후 쿨타임인지?
        public bool CanStaminaRecovery; // 스테미나 회복 할 수 있는지?
        public bool CantOperate; // 조작할 수 있는지?
    }
    [SerializeField]private BoolField _boolField;
    public bool IsDoubleJump { get { return _boolField.IsDoubleJump; } set { _boolField.IsDoubleJump = value; } }
    public bool IsJumpAttack { get { return _boolField.IsJumpAttack; } set { _boolField.IsJumpAttack = value; } }
    public bool IsInvincible { get { return _boolField.IsInvincible; } set { _boolField.IsInvincible = value; } }
    public bool IsHit { get { return _boolField.IsHit; } set { _boolField.IsHit = value; } }
    public bool IsDead { get { return _boolField.IsDead; } set { _boolField.IsDead = value; } }
    public bool IsStaminaCool { get { return _boolField.IsStaminaCool; } set { _boolField.IsStaminaCool = value; } }
    public bool CanStaminaRecovery { get { return _boolField.CanStaminaRecovery; } set { _boolField.CanStaminaRecovery = value; } }
    public bool CantOperate { get { return _boolField.CantOperate; } set { _boolField.CantOperate = value; TriggerCantOperate(); } }
    #endregion

    //TODO: 인스펙터 정리 필요
    public GameObject DrainField;

    public bool IsGround { get { return _checkStruct.IsGround; } set { _checkStruct.IsGround = value; } }// 지면 접촉 여부
    public bool IsNearGround { get { return _checkStruct.IsNearGround; } set { _checkStruct.IsNearGround = value; } }
    public bool IsWall { get { return _checkStruct.IsWall; } set { _checkStruct.IsWall = value; } } // 벽 접촉 여부
    public bool CanClimbSlope { get { return _checkStruct.CanClimbSlope; } set { _checkStruct.CanClimbSlope = value; } } // 오를 수 있는 경사면 각도 인지 체크

    [HideInInspector] public Collider[] OverLapColliders = new Collider[100];

    [HideInInspector] public Vector3 MoveDir;

    Quaternion _defaultMuzzlePointRot;
    private void Awake()
    {

    }

    private void Start()
    {
        Init();
        InitUIEvent();
        StartRoutine();
        InitAdditionnal();
        ChangeArmUnit(Model.NowWeapon);
        StartCoroutine(ControlMousePointer());
        //Camera.main.transform.SetParent(_cameraPos, true);
        _states[(int)CurState].Enter();
    }
    public bool IsMouseVisible;

    private void OnDisable()
    {
        ExitPlayerAdditional();
        _states[(int)CurState].Exit();
    }

    private void Update()
    {
        if (CantOperate == true)
            return;

        if (Time.timeScale == 0)
            return;

        _states[(int)CurState].Update();

        CheckAnyState();
        RotateCamera();
        ChackInput();
        UpdatePlayerAdditional();
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0)
            return;

        _states[(int)CurState].FixedUpdate();
        CheckGround();
        CheckWall();
        CheckIsNearGround();
        FixedPlayerAdditional();
    }

    private void OnDrawGizmos()
    {
        if (_states[(int)CurState] == null)
            return;
        _states[(int)CurState].OnDrawGizmos();

        DrawCheckGround();
        DrawWallCheck();
        DrawIsNearGround();
    }

    /// <summary>
    /// 상태 변경
    /// </summary>
    public void ChangeState(State state)
    {
        // 스테미나 쓰는 상태
        if (_states[(int)state].UseStamina == true)
        {
            // 사용할수 있음?(최소 스테미나)
            if (Model.CurStamina < _states[(int)state].StaminaAmount)
                return;
            // 사용가능하면 스테미나 깎음
            Model.CurStamina -= _states[(int)state].StaminaAmount;
        }

        _states[(int)CurState].Exit();
        PrevState = CurState;
        CurState = state;
        _states[(int)CurState].Enter();

        //Debug.Log(CurState);
    }

    /// <summary>
    /// 데미지 받기
    /// </summary>
    public void TakeDamage(int damage, bool isStun)
    {
        OnPlayerHitEvent?.Invoke(damage, isStun);
    }

    /// <summary>
    /// 사망
    /// </summary>
    public void Die()
    {
        OnPlayerDieEvent?.Invoke();
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
    #region 플레이어 방향 처리
    public void LookAtAttackDir()
    {
        if (IsTargetHolding == false && IsTargetToggle == false)
            LookAtCameraFoward();
        else
            LookAtTargetDir(TargetPos);
    }

    /// <summary>
    /// 카메라 방향으로 플레이어 방향 전환
    /// </summary>
    public void LookAtCameraFoward()
    {
        // 카메라 방향으로 플레이어가 바라보게
        Quaternion cameraRot = Quaternion.Euler(0, CamareArm.eulerAngles.y, 0);
        transform.rotation = cameraRot;
        // 카메라는 다시 로컬 기준 전방 방향
        if (CamareArm.parent != null)
        {
            CamareArm.localRotation = Quaternion.Euler(CamareArm.localRotation.eulerAngles.x, 0, 0);
        }
        MuzzletPoint.localRotation = _defaultMuzzlePointRot;
    }

    /// <summary>
    /// 입력한 방향을 플레이어가 바라봄
    /// </summary>
    /// <param name="moveDir"></param>
    public void LookAtMoveDir()
    {
        // 카메라 방향으로 플레이어가 바라보게
        Quaternion cameraRot = Quaternion.Euler(0, CamareArm.eulerAngles.y, 0);
        transform.rotation = cameraRot;
        // 카메라는 다시 로컬 기준 전방 방향
        if (CamareArm.parent != null)
        {
            // 카메라 흔들림 버그 잡아주는 코드
            CamareArm.localPosition = new Vector3(0, CamareArm.localPosition.y, 0);
            CamareArm.localRotation = Quaternion.Euler(CamareArm.localRotation.eulerAngles.x, 0, 0);
        }

        Quaternion cameraTempRot = CamareArm.rotation;

        // 입력한 방향쪽을 플레이어가 바라봄
        Vector3 dir = transform.forward * MoveDir.z + transform.right * MoveDir.x;
        if (dir == Vector3.zero)
        {
            if (CurState == State.Run)
                return;
            else
            {
                dir = transform.forward;
            }
        }
        transform.rotation = Quaternion.LookRotation(dir);
        CamareArm.rotation = cameraTempRot;
    }

    /// <summary>
    /// 타겟 방향을 플레이어가 바라봄
    /// </summary>
    public void LookAtTargetDir(Vector3 targetPos)
    {
        // 카메라 방향으로 플레이어가 바라보게
        Quaternion cameraRot = Quaternion.Euler(0, CamareArm.eulerAngles.y, 0);
        transform.rotation = cameraRot;
        // 카메라는 다시 로컬 기준 전방 방향
        if (CamareArm.parent != null)
        {
            // 카메라 흔들림 버그 잡아주는 코드
            CamareArm.localPosition = new Vector3(0, CamareArm.localPosition.y, 0);
            CamareArm.localRotation = Quaternion.Euler(CamareArm.localRotation.eulerAngles.x, 0, 0);
        }

        Quaternion cameraTempRot = CamareArm.rotation;
        targetPos = new Vector3(targetPos.x, targetPos.y + 2f, targetPos.z);
        // 입력한 방향쪽을 플레이어가 바라봄
        transform.LookAt(targetPos);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        CamareArm.rotation = cameraTempRot;
        MuzzletPoint.LookAt(targetPos);
    }

    /// <summary>
    ///  플레이어가 보고있는 방향으로 물리량 바꾸기
    /// </summary>
    public void ChangeVelocityPlayerFoward()
    {
        Vector3 tempVelocity = transform.forward * Rb.velocity.magnitude; // x,z 값의 물리량만 계산
        Rb.velocity = tempVelocity; // 대입
    }
    #endregion
    /// <summary>
    /// 오브젝트 줍기
    /// </summary>
    public void AddThrowObject(ThrowObject throwObject)
    {

        if (Model.CurThrowables < Model.MaxThrowables)
        {
            Model.PushThrowObject(DataContainer.GetThrowObject(throwObject.Data.ID).Data);
            Destroy(throwObject.gameObject);
        }
    }

    public void ChangeArmUnit(ArmUnit armUnit)
    {
        Model.Arm = Instantiate(armUnit);
        Model.Arm.Init(this);
    }
    public void ChangeArmUnit(GlobalGameData.AmWeapon armUnit)
    {
        Model.Arm = Instantiate(DataContainer.GetArmUnit(armUnit));
        Model.Arm.Init(this);
    }
    #region 플레이어 추가효과 관련
    /// <summary>
    /// 추가효과 추가
    /// </summary>
    public void AddAdditional(AdditionalEffect addtionalEffect)
    {
        switch (addtionalEffect.AdditionalType)
        {
            case AdditionalEffect.Type.Hit:
                if (CheckForAddAdditionalDuplication(Model.HitAdditionals, addtionalEffect as HitAdditional))
                {
                    Model.HitAdditionals.Add(addtionalEffect as HitAdditional);
                    Model.AdditionalEffects.Add(addtionalEffect);
                    // 적중효과는 배틀시스템에도 추가 등록
                    Battle.AddHitAdditionalList(addtionalEffect as HitAdditional);
                }
                break;
            case AdditionalEffect.Type.Throw:
                if (CheckForAddAdditionalDuplication(Model.ThrowAdditionals, addtionalEffect as ThrowAdditional))
                {
                    Model.ThrowAdditionals.Add(addtionalEffect as ThrowAdditional);
                    Model.AdditionalEffects.Add(addtionalEffect);
                }
                break;
            // 플레이어 추가효과는 플레이어에 종속되기 때문에 Clone을 더해줌
            case AdditionalEffect.Type.Player:
                if (CheckForAddAdditionalDuplication(Model.PlayerAdditionals, addtionalEffect as PlayerAdditional))
                {
                    PlayerAdditional instance = Instantiate(addtionalEffect as PlayerAdditional);
                    Model.PlayerAdditionals.Add(instance);
                    Model.AdditionalEffects.Add(instance);
                    instance.Init(this, addtionalEffect);
                    instance.Enter();
                }
                break;
        }
    }
    /// <summary>
    /// 추가효과 삭제
    /// </summary>
    public void RemoveAdditional(AdditionalEffect addtionalEffect)
    {
        switch (addtionalEffect.AdditionalType)
        {
            case AdditionalEffect.Type.Hit:
                if (CheckForRemoveAdditionalDuplication(Model.HitAdditionals, addtionalEffect as HitAdditional))
                {
                    Model.HitAdditionals.Remove(addtionalEffect as HitAdditional);
                    // 배틀시스템에도 적중 효과 삭제
                    Battle.RemoveHitAdditionalList(addtionalEffect as HitAdditional);
                }
                break;
            case AdditionalEffect.Type.Throw:
                if (CheckForRemoveAdditionalDuplication(Model.ThrowAdditionals, addtionalEffect as ThrowAdditional))
                {
                    Model.ThrowAdditionals.Remove(addtionalEffect as ThrowAdditional);
                }
                break;
            case AdditionalEffect.Type.Player:
                if (CheckForRemoveAdditionalDuplication(Model.PlayerAdditionals, addtionalEffect as PlayerAdditional))
                {

                    addtionalEffect.Exit();
                    Model.PlayerAdditionals.Remove(addtionalEffect as PlayerAdditional);
                }
                break;
        }
    }

    public void EnterPlayerAdditional()
    {
        foreach (PlayerAdditional playerAdditional in Model.PlayerAdditionals)
        {
            playerAdditional.Enter();
        }
    }
    public void ExitPlayerAdditional()
    {
        foreach (PlayerAdditional playerAdditional in Model.PlayerAdditionals)
        {
            playerAdditional.Exit();
        }
    }

    public void UpdatePlayerAdditional()
    {
        foreach (PlayerAdditional playerAdditional in Model.PlayerAdditionals)
        {
            playerAdditional.Update();
        }
    }

    public void FixedPlayerAdditional()
    {
        foreach (PlayerAdditional playerAdditional in Model.PlayerAdditionals)
        {
            playerAdditional.FixedUpdate();
        }
    }
    public void TriggerPlayerAdditional()
    {
        foreach (PlayerAdditional playerAdditional in Model.PlayerAdditionals)
        {
            playerAdditional.Trigger();
        }
    }
    public void TriggerFirstPlayerAdditional()
    {
        foreach (PlayerAdditional playerAdditional in Model.PlayerAdditionals)
        {
            playerAdditional.TriggerFirst();
        }
    }
    /// <summary>
    /// 추가효과 추가 시 중복 체크
    /// </summary>
    private bool CheckForAddAdditionalDuplication<T>(List<T> additinalList, T additinal) where T : AdditionalEffect
    {
        int index = additinalList.FindIndex(origin => origin.Origin.Equals(additinal.Origin));
        if (index >= additinalList.Count)
            return false;
        // 중복 시
        if (index != -1)
            return false;
        else
            return true;

    }
    /// <summary>
    /// 추가효과 삭제 시 중복 체크
    /// </summary>
    private bool CheckForRemoveAdditionalDuplication<T>(List<T> additinalList, T additinal) where T : AdditionalEffect
    {
        int index = additinalList.FindIndex(origin => origin.Origin.Equals(additinal.Origin));
        if (index >= additinalList.Count)
            return false;
        // 중복 시 (지울 수 있을 때)
        if (index != -1)
        {
            Model.AdditionalEffects.Remove(additinal);
            return true;
        }
        else
            return false;
    }
    #endregion


    /// <summary>
    /// TPS 시점 카메라 회전
    /// </summary>
    private void RotateCamera()
    {
        float angleX = InputKey.GetAxis(InputKey.MouseX);
        float angleY = default;
        // 체크시 마우스 상하도 가능
        if (IsVerticalCameraMove == true)
            angleY = InputKey.GetAxis(InputKey.MouseY);
        _cameraRotateSpeed = setting.cameraSpeed;
        Vector2 mouseDelta = new Vector2(angleX, angleY) * _cameraRotateSpeed;
        Vector3 camAngle = CamareArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;
        x = x < 180 ? Mathf.Clamp(x, -10f, 50f) : Mathf.Clamp(x, 360f - _cameraRotateAngle, 361f);
        CamareArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);

        if (IsVerticalCameraMove)
        {
            // 머즐포인트 각도조절
            MuzzletPoint.rotation = Quaternion.Euler(x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
    }
    #region 지형물 체크 로직
    /// <summary>
    /// 지면 체크
    /// </summary>
    private void CheckGround()
    {
        // 살짝위에서 쏨
        Vector3 CheckPos = _groundCheckPos.position;
        if (Physics.SphereCast(
            CheckPos,
            0.25f,
            Vector3.down,
            out RaycastHit hit,
            0.4f,
            Layer.GetLayerMaskEveryThing(),
            QueryTriggerInteraction.Ignore))
        {
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
            IsGround = false;
            CanClimbSlope = false;
        }
    }

    private void DrawCheckGround()
    {
        Gizmos.color = Color.yellow;

        Vector3 CheckPos = _groundCheckPos.position;

        if (Physics.SphereCast(CheckPos, 0.25f, Vector3.down, out RaycastHit hit, 0.4f, Layer.GetLayerMaskEveryThing(), QueryTriggerInteraction.Ignore))
        {
            Gizmos.DrawLine(CheckPos, hit.point);
            Gizmos.DrawWireSphere(CheckPos + Vector3.down * hit.distance, 0.3f);
        }
    }

    /// <summary>
    /// 지면에 가까운지 체크
    /// </summary>
    private void CheckIsNearGround()
    {
        Vector3 CheckPos = new Vector3(transform.position.x, transform.position.y + 0.31f, transform.position.z);
        if (Physics.SphereCast(CheckPos, 0.3f, Vector3.down, out RaycastHit hit, 1f, Layer.GetLayerMaskEveryThing(), QueryTriggerInteraction.Ignore))
        {
            IsNearGround = true;
        }
        else
        {
            IsNearGround = false;
        }
    }

    private void DrawIsNearGround()
    {
        Vector3 CheckPos = new Vector3(transform.position.x, transform.position.y + 0.31f, transform.position.z);
        if (Physics.SphereCast(CheckPos, 0.3f, Vector3.down, out RaycastHit hit, 1, Layer.GetLayerMaskEveryThing(), QueryTriggerInteraction.Ignore))
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(CheckPos, hit.point);
            Gizmos.DrawWireSphere(CheckPos + Vector3.down * hit.distance, 0.3f);
        }
    }

    /// <summary>
    /// 벽체크
    /// </summary>
    private void CheckWall()
    {
        int layerMask = 0;
        layerMask |= 1 << Layer.Wall;
        layerMask |= 1 << Layer.Monster;
        int hitCount = Physics.OverlapCapsuleNonAlloc(
            WallCheckPos.Foot.position,
            WallCheckPos.Head.position,
            _wallCheckDistance,
            OverLapColliders,
            layerMask);

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

        Vector3 footPos = WallCheckPos.Foot.position + transform.forward * _wallCheckDistance;
        Vector3 headPos = WallCheckPos.Head.position + transform.forward * _wallCheckDistance;

        Gizmos.DrawLine(footPos, headPos);
    }
    #endregion

    /// <summary>
    /// 스테미나 회복 코루틴
    /// </summary>
    IEnumerator RecoveryStamina()
    {
        CanStaminaRecovery = true;
        while (true)
        {
            // 초당 MaxStamina / RegainStamina 만큼 회복
            // 현재 스테미나가 꽉찼으면 더이상 회복안함
            // 만약 스테미나 사용 후 쿨타임 상태면 쿨타임만큼 회복안함
            if (CanStaminaRecovery == true)
            {
                Model.CurStamina += Model.RegainStamina * Time.deltaTime;
            }
            if (Model.CurStamina >= Model.MaxStamina)
            {
                Model.CurStamina = Model.MaxStamina;
            }

            if (IsStaminaCool == true)
            {
                IsStaminaCool = false;
                //yield return 1f.GetDelay();
                yield return Model.StaminaCoolTime.GetDelay();
            }

            yield return null;
        }
    }
    #region 키입력 관련
    /// <summary>
    /// 키입력 감지
    /// </summary>
    private void ChackInput()
    {
        float x = InputKey.GetAxisRaw(InputKey.Horizontal);
        float z = InputKey.GetAxisRaw(InputKey.Vertical);
        MoveDir = new Vector3(x, 0, z);

        if (IsTargetHolding == false && IsTargetToggle == false)
        {
            //if (Input.GetMouseButtonDown(2))
            //{
            //    //TODO: 카메라 몬스터 홀딩 기능
            //    IsTargetHolding = true;
            //    _cameraHolder.gameObject.SetActive(true);
            //}
            if (InputKey.GetButtonDown(InputKey.RockOn) && IsTargetHolding == false)
            {
                //TODO: 카메라 몬스터 홀딩 기능
                IsTargetToggle = true;
                _cameraHolder.gameObject.SetActive(true);
            }
        }
        else
        {
            //if (Input.GetMouseButtonUp(2) && IsTargetToggle == false)
            //{
            //    //TODO: 카메라 몬스터 홀딩 풀기
            //    IsTargetHolding = false;
            //    _cameraHolder.gameObject.SetActive(false);
            //}
            if (InputKey.GetButtonDown(InputKey.RockCancel) && IsTargetHolding == false)
            {
                //TODO: 카메라 몬스터 홀딩 풀기
                IsTargetToggle = false;
                _cameraHolder.gameObject.SetActive(false);
            }
        }
    }
    private void CheckAnyState()
    {
        if (IsDead == true || IsHit == true)
            return;

        if (InputKey.GetButtonDown(InputKey.Dash) && CurState != State.Dash && CurState != State.JumpDown)
        {
            ChangeState(PlayerController.State.Dash);
        }
    }
    #endregion

    #region  넉백
    /// <summary>
    /// 넉백 안함(위치 고정)
    /// </summary>
    public void DontKnockBack(Transform target)
    {
        Rigidbody targetRb = target.GetComponent<Rigidbody>();
        targetRb.velocity = new(0, targetRb.velocity.y, 0);
    }

    /// <summary>
    /// 해당 방향으로 입력 거리만큼 넉백
    /// </summary>
    public void DoKnockBack(Transform target, Vector3 dir, float distance)
    {
        Rigidbody targetRb = target.GetComponent<Rigidbody>();

        //targetRb.AddForce(dir * distance * 10f, ForceMode.Impulse);

        CoroutineHandler.StartRoutine(KnockBackRoutine(targetRb, dir, distance));
    }
    /// <summary>
    /// 공격자 중심으로 입력거리만큼 넉백
    /// </summary>
    public void DoKnockBack(Transform target, Transform attacker, float distance)
    {
        Vector3 attackerPos = new(attacker.position.x, 0, attacker.position.z);
        Vector3 targetPos = new(target.position.x, 0, target.position.z);
        Vector3 knockBackDir = targetPos - attackerPos;
        Rigidbody targetRb = target.GetComponent<Rigidbody>();


        //targetRb.AddForce(knockBackDir.normalized * distance * 10f, ForceMode.Impulse);

        CoroutineHandler.StartRoutine(KnockBackRoutine(targetRb, knockBackDir, distance));
    }

    /// <summary>
    /// 특정 지점을 중심으로 넉백
    /// </summary>
    /// <param name="target"></param>
    /// <param name="pos"></param>
    /// <param name="distance"></param>
    public void DoKnockBackFromPos(Transform target, Vector3 pos, float distance)
    {
        Vector3 attackerPos = new(pos.x, 0, pos.z);
        Vector3 targetPos = new(target.position.x, 0, target.position.z);
        Vector3 knockBackDir = targetPos - attackerPos;
        Rigidbody targetRb = target.GetComponent<Rigidbody>();

        //targetRb.AddForce(knockBackDir.normalized * distance * 10f, ForceMode.Impulse);

        CoroutineHandler.StartRoutine(KnockBackRoutine(targetRb, knockBackDir, distance));
    }

    IEnumerator KnockBackRoutine(Rigidbody targetRb, Vector3 knockBackDir, float distance)
    {
        Vector3 originPos = targetRb.position;

        targetRb.transform.LookAt(transform.position);
        targetRb.transform.rotation = Quaternion.Euler(0, targetRb.transform.eulerAngles.y, 0);
        // 타겟이 날 바라보도록
        while (true)
        {
            targetRb.transform.Translate(knockBackDir * Time.deltaTime * 30f, Space.World);

            if (Vector3.Distance(originPos, targetRb.position) > distance)
            {
                break;
            }

            Vector3 targetPos = new(targetRb.transform.position.x, targetRb.transform.position.y + 0.75f, targetRb.transform.position.z);
            if (Physics.SphereCast(targetRb.transform.position, 0.2f, knockBackDir, out RaycastHit hit, 0.3f, 1 << Layer.Wall, QueryTriggerInteraction.Ignore))
            {
                break;
            }
            yield return null;
        }
    }
    #endregion
    #region 데미지 계산
    /// <summary>
    /// 기본 스텟 데미지
    /// </summary>
    public int GetFinalDamage()
    {
        int finalDamage = 0;
        finalDamage = GetCommonDamage(finalDamage);
        return finalDamage;
    }
    /// <summary>
    /// 데미지 추가
    /// </summary>
    public int GetFinalDamage(int addtionalDamage)
    {
        int finalDamage = 0;
        // 추가 데미지
        finalDamage += addtionalDamage;
        finalDamage = GetCommonDamage(finalDamage);
        return finalDamage;
    }
    /// <summary>
    /// 데미지 배율
    /// </summary>
    public int GetFinalDamage(float multiplier)
    {
        int finalDamage = 0;
        finalDamage = GetCommonDamage(finalDamage);

        // 데미지 배율 추가
        finalDamage = (int)(finalDamage * multiplier);
        return finalDamage;
    }
    /// <summary>
    /// 추가 데미지 + 데미지 배율
    /// </summary>
    public int GetFinalDamage(int addtionalDamage, float multiplier)
    {
        int finalDamage = 0;
        // 추가 데미지
        finalDamage += addtionalDamage;
        finalDamage = GetCommonDamage(finalDamage);

        // 데미지 배율 추가
        finalDamage = (int)(finalDamage * multiplier);
        return finalDamage;
    }
    /// <summary>
    /// 공통계산 용
    /// </summary>
    private int GetCommonDamage(int finalDamage)
    {
        // 기본 스텟 데미지 
        finalDamage += Model.AttackPower;
        // 치명타 데미지
        if (Random.value < Model.CriticalChance / 100f)
            finalDamage = (int)(finalDamage * (Model.CriticalDamage / 100f));

        return finalDamage;
    }
    #endregion

    IEnumerator ControlMousePointer()
    {
        while (true)
        {
            if (Time.timeScale == 1 && IsMouseVisible == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (Time.timeScale == 0 || IsMouseVisible == true)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            yield return null;
        }
    }

    private void TriggerCantOperate()
    {
        _states[(int)CurState].TriggerCantOperate();
    }
    // 초기 설정 ============================================================================================================================================ //
    /// <summary>
    /// 초기 설정
    /// </summary>
    private void Init()
    {
        InitGetComponent();
        InitPlayerStates();

        _defaultMuzzlePointRot = MuzzletPoint.localRotation;
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
        _states[(int)State.SpecialAttack] = new SpecialAttackState(this); // 특수공격
        _states[(int)State.Jump] = new JumpState(this);                 // 점프
        _states[(int)State.DoubleJump] = new DoubleJumpState(this);     // 더블점프
        _states[(int)State.JumpAttack] = new JumpAttackState(this);     // 점프공격
        _states[(int)State.JumpDown] = new JumpDownState(this);         // 하강 공격 
        _states[(int)State.Fall] = new FallState(this);                 // 추락
        _states[(int)State.DoubleJumpFall] = new DoubleJumpFallState(this); // 더블점프 추락
        _states[(int)State.Dash] = new DashState(this);                 // 대쉬
        _states[(int)State.Drain] = new DrainState(this);               // 드레인
        _states[(int)State.Hit] = new HitState(this);                   // 피격
        _states[(int)State.Dead] = new DeadState(this);                 // 사망
    }

    /// <summary>
    /// UI이벤트 설정
    /// </summary>
    private void InitUIEvent()
    {
        PlayerPanel panel = View.Panel;

        // 투척오브젝트
        Model.CurThrowCountSubject = new Subject<int>();
        Model.CurThrowCountSubject
            .DistinctUntilChanged()
            .Subscribe(x =>  View.UpdateText(panel.ObjectCount, $"{x} / {Model.MaxThrowables}") );
        View.UpdateText(panel.ObjectCount, $"{Model.CurThrowables} / {Model.MaxThrowables}");

        // 체력
        Model.CurHpSubject
            .DistinctUntilChanged()
            .Subscribe(x => panel.BarValueController(panel.HpBar, Model.CurHp, Model.MaxHp));
        panel.BarValueController(panel.HpBar, Model.CurHp, Model.MaxHp);

        // 스테미나
        Model.CurStaminaSubject
            .DistinctUntilChanged()
            .Subscribe(x => panel.BarValueController(panel.StaminaBar, Model.CurStamina, Model.MaxStamina));
        panel.BarValueController(panel.StaminaBar, Model.CurStamina, Model.MaxStamina);

        // 특수자원
        Model.CurManaSubject
            .DistinctUntilChanged()
            .Subscribe(x => panel.MpBar.value = x);
        panel.MpBar.value = Model.CurHp;

        // 특수공격 차지
        Model.SpecialChargeGageSubject
            .DistinctUntilChanged()
            .Subscribe(x => panel.ChargingMpBar.value = x);
        panel.ChargingMpBar.value = Model.SpecialChargeGage;

        // 스테미나 차지
        Model.MaxStaminaCharge = 1;
        Model.CurStaminaChargeSubject
            .DistinctUntilChanged()
            .Subscribe(x => panel.BarValueController(panel.ChanrgeStaminaBar, Model.CurStaminaCharge, Model.MaxStaminaCharge));
        panel.BarValueController(panel.ChanrgeStaminaBar, Model.CurStaminaCharge, Model.MaxStaminaCharge);
    }

    /// <summary>
    /// 초기 겟컴포넌트 설정
    /// </summary>
    private void InitGetComponent()
    {
        Model = GetComponent<PlayerModel>();
        View = GetComponent<PlayerView>();
        Rb = GetComponent<Rigidbody>();
        Battle = GetComponent<BattleSystem>();
    }
    private void InitAdditionnal()
    {
        Model.AdditionalEffects.Clear();
        List<AdditionalEffect> tempList = new List<AdditionalEffect>();

        ProcessInitAddtional(tempList, Model.PlayerAdditionals);
        ProcessInitAddtional(tempList, Model.ThrowAdditionals);
        ProcessInitAddtional(tempList, Model.HitAdditionals);
    }
    private void ProcessInitAddtional<T>(List<AdditionalEffect> tempList, List<T> additionals) where T : AdditionalEffect
    {
        foreach (AdditionalEffect additional in additionals)
        {
            tempList.Add(additional);
        }
        additionals.Clear();
        foreach (AdditionalEffect additional in tempList)
        {
            AddAdditional(additional);
        }
        tempList.Clear();
    }

    private void StartRoutine()
    {
        StartCoroutine(RecoveryStamina());
    }


    #region 애니메이션 콜백

    public void OnTrigger()
    {
        TriggerPlayerAdditional();
        _states[(int)CurState].OnTrigger();
    }
    public void EndAnimation()
    {
        _states[(int)CurState].EndAnimation();
    }
    public void OnCombo()
    {
        _states[(int)CurState].OnCombo();
    }
    public void EndCombo()
    {
        _states[(int)CurState].EndCombo();
    }
    #endregion
}
