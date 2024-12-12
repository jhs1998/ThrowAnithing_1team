using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public enum Parameter { MoveSpeed, MeleeAttack, Size}

    [SerializeField] PlayerPanel _panel;

    [SerializeField] Animator _animator;

    private int[] _animatorHashes = new int[(int)Parameter.Size];

    int _idleHash = Animator.StringToHash("Idle");
    int _runHash = Animator.StringToHash("Run");
    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 플레이어 애니메이션 SetTrigger
    /// </summary>
    public void SetTrigger(Parameter animation)
    {
        _animator.SetTrigger(_animatorHashes[(int)animation]);
    }
    
    /// <summary>
    /// 플레이어 애니메이션 SetInteger
    /// </summary>
    public void SetInteger(Parameter animation, int value)
    {
        _animator.SetInteger(_animatorHashes[(int)animation], value);
    }

    /// <summary>
    /// 플레이어 애니메이션 SetBool
    /// </summary>
    public void SetBool(Parameter animation, bool value) 
    {
        _animator.SetBool(_animatorHashes[(int)animation], value);
    }

    /// <summary>
    /// 플레이어 애니메이션 SetFloat
    /// </summary>
    public void SetFloat(Parameter animation, float value) 
    {
        _animator.SetFloat(_animatorHashes[(int)animation], value);
    }

    private void Init()
    {
        _animatorHashes[(int)Parameter.MoveSpeed] = Animator.StringToHash("MoveSpeed");
        _animatorHashes[(int)Parameter.MeleeAttack] = Animator.StringToHash("MeleeAttack");
    }
}
