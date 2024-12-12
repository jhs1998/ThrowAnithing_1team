using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public enum Animation { Idle, Run, MeleeAttack, Size}

    [SerializeField] PlayerPanel _panel;

    [SerializeField] Animator _animator;

    private int[] _animatorHashes = new int[(int)Animation.Size];

    int _idleHash = Animator.StringToHash("Idle");
    int _runHash = Animator.StringToHash("Run");
    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 플레이어 애니메이션 SetTrigger
    /// </summary>
    public void SetTrigger(Animation animation)
    {
        _animator.SetTrigger((int)animation);
    }
    
    public void SetInteger(Animation animation, int value)
    {
        _animator.SetInteger((int)animation, value);
    }

    private void Init()
    {
        _animatorHashes[(int)Animation.Idle] = Animator.StringToHash("Idle");
        _animatorHashes[(int)Animation.Run] = Animator.StringToHash("Run");
        _animatorHashes[(int)Animation.MeleeAttack] = Animator.StringToHash("MeleeAttack");
    }
}
