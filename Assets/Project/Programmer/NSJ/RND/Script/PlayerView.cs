using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerView : MonoBehaviour
{
    public enum Parameter { Idle, Run, MeleeAttack, MeleeCombo, ThrowAttack, ThrowCombo, Size }

    private bool _isAnimationFinish;
    public bool IsAnimationFinish
    {
        get
        {
            bool answer = _isAnimationFinish;
            if (_isAnimationFinish == true)
            {
                _isAnimationFinish = false;
            }
            return answer;
        }
        set
        {
            _isAnimationFinish = value;
        }
    }

    [SerializeField] PlayerPanel _panel;

    [SerializeField] Animator _animator;

    private int[] _animatorHashes = new int[(int)Parameter.Size];

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

    public int GetInteger(Parameter animation)
    {
        return _animator.GetInteger(_animatorHashes[(int)animation]);
    }

    /// <summary>
    /// 플레이어 애니메이션 SetBool
    /// </summary>
    public void SetBool(Parameter animation, bool value)
    {
        _animator.SetBool(_animatorHashes[(int)animation], value);
    }

    public bool GetBool(Parameter animation)
    {
        return _animator.GetBool(_animatorHashes[(int)animation]);
    }

    /// <summary>
    /// 플레이어 애니메이션 SetFloat
    /// </summary>
    public void SetFloat(Parameter animation, float value)
    {
        _animator.SetFloat(_animatorHashes[(int)animation], value);
    }
    public float GetFloat(Parameter animation)
    {
        return _animator.GetFloat(_animatorHashes[(int)animation]);
    }

    public void SetIsAnimationFinish()
    { 
        IsAnimationFinish = true;
    }

    private void Init()
    {
        _animatorHashes[(int)Parameter.Idle] = Animator.StringToHash("Idle");
        _animatorHashes[(int)Parameter.Run] = Animator.StringToHash("Run");
        _animatorHashes[(int)Parameter.MeleeCombo] = Animator.StringToHash("MeleeCombo");
        _animatorHashes[(int)Parameter.MeleeAttack] = Animator.StringToHash("MeleeAttack");
        _animatorHashes[(int)Parameter.ThrowAttack] = Animator.StringToHash("ThrowAttack");
        _animatorHashes[(int)Parameter.ThrowCombo] = Animator.StringToHash("ThrowCombo");
    }
}
