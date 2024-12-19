using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerView : MonoBehaviour
{
    public PlayerPanel Panel;
    public enum Parameter
    {
        Idle,
        Run,
        MeleeAttack,
        MeleeCombo,
        ThrowAttack,
        ThrowCombo,
        Jump,
        Fall,
        Landing,
        Dash,
        Size
    }

    #region 애니메이션 관련 이벤트
    public event UnityAction OnThrowAttackEvent;
    public event UnityAction OnMeleeAttackEvent;
    public event UnityAction OnJumpEvent;
    #endregion

    private Dictionary<Parameter, bool> _isAnimFinishDic = new Dictionary<Parameter, bool>();

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


    [SerializeField] Animator _animator;

    private int[] _animatorHashes = new int[(int)Parameter.Size];

    private void Awake()
    {
        Init();
    }
    // 애니메이션 =========================================================================================================//
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

    public bool GetIsAnimFinish(Parameter parameter)
    {
        if (_isAnimFinishDic.ContainsKey(parameter) ==false)
        {
            _isAnimFinishDic.Add(parameter, false);
        }

        if (_isAnimFinishDic[parameter] == true)
        {
            _isAnimFinishDic[parameter] = false;
            return true;
        }
        else
            return false;

    }

    /// <summary>
    /// 점프 타이밍에 호출
    /// </summary>
    public void OnJump()
    {
        OnJumpEvent?.Invoke();
    }


    public void OnThrowAttack()
    {
        OnThrowAttackEvent?.Invoke();
    }

    public void OnMeleeAttack()
    {
        OnMeleeAttackEvent?.Invoke();
    }
    #region SetIsAnimFinish
    public void SetIsThrowAttackFinish()
    {
        if (_isAnimFinishDic.ContainsKey(Parameter.ThrowAttack) ==false)
        {
            _isAnimFinishDic.Add(Parameter.ThrowAttack, false);
        }
        _isAnimFinishDic[Parameter.ThrowAttack] = true;
    }
    public void SetIsMeleeAttackFinish()
    {
        if (_isAnimFinishDic.ContainsKey(Parameter.MeleeAttack) == false)
        {
            _isAnimFinishDic.Add(Parameter.MeleeAttack, false);
        }
        _isAnimFinishDic[Parameter.MeleeAttack] = true;
    }

    public void SetIsDashFinish()
    {
        if (_isAnimFinishDic.ContainsKey(Parameter.Dash) == false)
        {
            _isAnimFinishDic.Add(Parameter.Dash, false);
        }
        _isAnimFinishDic[Parameter.Dash] = true;
    }
    public void SetIsLandingFinish()
    {
        if (_isAnimFinishDic.ContainsKey(Parameter.Landing) == false)
        {
            _isAnimFinishDic.Add(Parameter.Landing, false);
        }
        _isAnimFinishDic[Parameter.Landing] = true;
    }
    #endregion
    // UI ================================================================================================================//

    public void UpdateText(TMP_Text target, string text)
    {
        target.SetText(text);
    }

    private void Init()
    {
        _animatorHashes[(int)Parameter.Idle] = Animator.StringToHash("Idle");
        _animatorHashes[(int)Parameter.Run] = Animator.StringToHash("Run");
        _animatorHashes[(int)Parameter.MeleeCombo] = Animator.StringToHash("MeleeCombo");
        _animatorHashes[(int)Parameter.MeleeAttack] = Animator.StringToHash("MeleeAttack");
        _animatorHashes[(int)Parameter.ThrowAttack] = Animator.StringToHash("ThrowAttack");
        _animatorHashes[(int)Parameter.ThrowCombo] = Animator.StringToHash("ThrowCombo");
        _animatorHashes[(int)Parameter.Jump] = Animator.StringToHash("Jump");
        _animatorHashes[(int)Parameter.Fall] = Animator.StringToHash("Fall");
        _animatorHashes[(int)Parameter.Landing] = Animator.StringToHash("Landing");
        _animatorHashes[(int)Parameter.Dash] = Animator.StringToHash("Dash");
    }
}
