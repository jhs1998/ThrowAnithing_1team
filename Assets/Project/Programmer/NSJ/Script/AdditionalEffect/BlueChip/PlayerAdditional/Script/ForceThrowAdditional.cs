using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "ForceThrow", menuName = "AdditionalEffect/Player/ForceThrow")]
public class ForceThrowAdditional : PlayerAdditional
{
    [System.Serializable]
    struct EffectStruct
    {
        public ForceThrowPanel UIPrefab;
        [HideInInspector] public ForceThrowPanel UI;
    }
    [SerializeField] DoubleDamageBuff _damageBuff;
    [Header("������ �߰� ���(%)")]
    [SerializeField] float _damageMultiplier;
    [Header("���� ī��Ʈ")]
    [SerializeField] float _attackCount;
    [SerializeField] EffectStruct _effect;

    private int m_curCount;
    private int _curCount
    {
        get { return m_curCount; }
        set
        {
            // ī��Ʈ����ŭ UI��ȭ
            m_curCount = value;
            if (value > 0)
                _effect.UI.UpdateText(value.GetText());
        }
    }

    private bool _onBuff;
    public override void Enter()
    {
        // ���� UI ����
        _effect.UI = Instantiate(_effect.UIPrefab);

        _curCount = 1;
        // ���ο� ���������� �ν��Ͻ��� ����
        _damageBuff = Instantiate(_damageBuff);
        _damageBuff.DamageMultyplier = _damageMultiplier;


        Player.OnThrowObjectResult += CheckCount;
    }
    public override void Exit()
    {
        // UI ����
        Destroy(_effect.UI.gameObject);
        Player.OnThrowObjectResult -= CheckCount;
    }

    public override void Trigger()
    {
        if (CurState != PlayerController.State.ThrowAttack)
            return;

        // ���� ���� �� ������ ���� ����
        if (_onBuff == true)
        {
            OffBuff();
        }
    }
    private void CheckCount(ThrowObject throwObject, bool hitSuccess)
    {
        // �¾��� ��
        if (hitSuccess == true)
        {
            // ü�ε� ���� ���ʷ� ����ٸ�
            if (throwObject.IsChainHit == false)
            {
                // ī��Ʈ ���
                if (_curCount < _attackCount)
                {
                    _curCount++;
                }
                // ����ī��Ʈ�� �ִ뿡 �����߰� ������ ���� ��
                if (_curCount == _attackCount && _onBuff == false)
                {
                    OnBuff();
                }
            }
        }
    }

    private void OnBuff()
    {
        Player.AddAdditional(_damageBuff);
        _onBuff = true;
    }
    private void OffBuff()
    {
        // 1������ �ڿ� ���� ����
        CoroutineHandler.StartRoutine(OffBuffRoutine());
        _curCount = 0;
        _onBuff = false;
    }
    IEnumerator OffBuffRoutine()
    {
        yield return null;
        Player.RemoveAdditional(_damageBuff);
    }
}
