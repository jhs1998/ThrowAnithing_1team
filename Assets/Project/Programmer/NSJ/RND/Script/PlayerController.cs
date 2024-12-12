using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerModel Model;
    [HideInInspector] public PlayerView View;
    [HideInInspector] public Rigidbody Rb;

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
    [SerializeField] private CameraStruct _cameraStruct;
    public Transform CamareArm { get { return _cameraStruct.CamaraArm; } set { _cameraStruct.CamaraArm = value; } }
    private Transform _cameraPos { get { return _cameraStruct.CameraPos; } set { _cameraStruct.CameraPos = value; } }
    private float _cameraRotateSpeed { get { return _cameraStruct.CameraRotateSpeed; } set { _cameraStruct.CameraRotateSpeed = value; } }
    private bool _isVerticalCameraMove { get { return _cameraStruct.IsVerticalCameraMove;} set { _cameraStruct.IsVerticalCameraMove = value; } }
    #endregion
    public enum State { Idle, Run, MeleeAttack, Size }

    private PlayerState[] _states = new PlayerState[(int)State.Size];
    private State _curState;


    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        Camera.main.transform.SetParent(_cameraPos,true);
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

    public void ChangeState(State state)
    {
        _states[(int)_curState].Exit();
        _curState = state;
        _states[(int)_curState].Enter();
    }

    public void AttackMelee()
    {
        Debug.Log("근접공격!");
    }


    /// <summary>
    /// TPS 시점 카메라 회전
    /// </summary>
    private void RotateCamera()
    {
        float angleX = Input.GetAxis("Mouse X");
        float angleY = default;
        if (_isVerticalCameraMove == true)
            angleY = Input.GetAxis("Mouse Y");
        Vector2 mouseDelta = new Vector2(angleX, angleY) * _cameraRotateSpeed;
        Vector3 camAngle = CamareArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;
        x = x < 180 ? Mathf.Clamp(x, -10f, 50f) : Mathf.Clamp(x, 340f, 361f);
        CamareArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }


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
