using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

[CreateAssetMenu(fileName = "Balance PrevSpecial", menuName = "Arm/AttackType/Balance/PrevSpecial")]
public class BalanceSpecialAttack : ArmSpecialAttack
{
    [System.Serializable]
    struct ChargeEffect
    {
        public GameObject EffectPrefab;
        [HideInInspector] public GameObject Effect;
        public GameObject StepUpEffectPrefab;
        public GameObject ChargeEndEffect;
    }
    [System.Serializable]
    struct ChargeStruct
    {
        public float ChargeTime;
        public float ChargeMana;
        public int ObjectCount;
    }
    [System.Serializable]
    struct FirstStruct
    {
        public GameObject SpecialEffect;
        public GameObject BuffEffectPrefab;
        public GameObject BuffEndEffect;
        [HideInInspector]public GameObject BuffEffect;
        public float AttackSpeed;
        public float Duration;
    }
    [System.Serializable]
    struct SecondStruct
    {
        public SpecialObject SpecialObject;
        public float Damage;
        public float MiddleDamage;
        public float Range;
        public float MiddleRange;
    }
    [System.Serializable]
    struct ThirdStruct
    {
        public GameObject Effect;
        public GameObject BeforeEffect;
        public ElectricShockAdditonal ElectricShockOrigin;
        [HideInInspector] public ElectricShockAdditonal ElectricShock;
        public float AttackDelay;
        public Vector3 AttackOffset;
        public float Damage;
        public float MiddleDamage;
        public float Range;
        public float MiddleRange;
        public float ElectricShockDuration;
    }
    [SerializeField] private ChargeEffect _chargeEffect;
    [SerializeField] private ChargeStruct[] _charges;
    [SerializeField] private FirstStruct _first;
    [SerializeField] private SecondStruct _second;
    [SerializeField] private ThirdStruct _third;

    private List<Transform> MiddleHittargets = new List<Transform>();
    private float _maxChargeTime => _charges[_charges.Length - 1].ChargeTime;
    private float _maxChargeMana => _charges[_charges.Length - 1].ChargeMana;

    // ������ ĳ��
    private BalanceArm _balance => arm as BalanceArm;

    Coroutine _firstSpecialRoutine;

    Coroutine _checkManaUIRoutien;
    public override void Init(PlayerController player, ArmUnit arm)
    {
        base.Init(player, arm);
        View.Panel.SetChargingMpVarMaxValue(Model.MaxMana);
        for (int i = 0; i < _charges.Length; i++)
        {
            View.Panel.StepTexts[i].SetText(_charges[i].ObjectCount.GetText());
            View.Panel.SetChargingMpHandle(i, _charges[i].ChargeMana);
        }

        // ���� ȿ�� Ŭ������ ����(��ġ �����ϱ� ����)
        _third.ElectricShock = Instantiate(_third.ElectricShockOrigin);
        _third.ElectricShock.Duration = _third.ElectricShockDuration;

        // ���� ���� ���� ���� üũ
        _checkManaUIRoutien = CoroutineHandler.StartRoutine(_checkManaUIRoutien, CheckManaUIRoutine());
    }

    private void OnDestroy()
    {
        _checkManaUIRoutien = CoroutineHandler.StopRoutine(_checkManaUIRoutien);
    }

    public override void Enter()
    {
        if (_isSpecialCharge == false
            && (Model.ThrowObjectStack.Count < _charges[_index].ObjectCount || Model.CurMana < Model.ManaConsumption[0]))
        {
            _isSpecialCharge = false;
            ChangeState(Player.PrevState);
            return;
        }

        if (_isSpecialCharge == false)
        {
            ChangeState(Player.PrevState);
            _isSpecialCharge = true;
            CoroutineHandler.StartRoutine(ChargeRoutine());
        }
        else
        {
            _index--;
            if (_index < 0)
            {
                Model.SpecialChargeGage = 0;
                _isSpecialCharge = false;
                ChangeState(Player.PrevState);
                return;
            }
            SelectSpecialAttack(_index);
        }
    }
    public override void Exit()
    {
        if (_isSpecialCharge == false)
        {

        }
        else
        {

            _isSpecialCharge = false;
        }
        _index = 0;
    }
    public override void OnTrigger()
    {
        SelectTrigger(_index);
    }
    public override void EndAnimation()
    {
        ChangeState(PlayerController.State.Idle);
    }
    IEnumerator ChargeRoutine()
    {
        // ���� ����Ʈ ����
        _chargeEffect.Effect = ObjectPool.GetPool(_chargeEffect.EffectPrefab, transform);
        while (true)
        {
            ProcessCharge();
            if (InputKey.GetButtonUp(InputKey.Special))
            {
                ObjectPool.ReturnPool(_chargeEffect.Effect);

                ObjectPool.GetPool(_chargeEffect.ChargeEndEffect, transform, 2f);

                ChangeState(PlayerController.State.SpecialAttack);
                yield break;
            }
            yield return null;
        }
    }

    private void ProcessCharge()
    {        
        // �����ð� ���
        Model.SpecialChargeGage += Time.deltaTime * 100 / _maxChargeTime;
        // �ε����� �迭 ũ�⺸�� ��������
        if (_index < _charges.Length)
        {
            // �Ҹ� ������Ʈ�� ������ ���
            if (Model.ThrowObjectStack.Count >= _charges[_index].ObjectCount)
            {
                // ���� �ð��� ���� �ܰ� ��¡ ���ǽð��� �ѱ� ���
                if (Model.SpecialChargeGage >= _charges[_index].ChargeMana)
                {
                    _index++;
                    ObjectPool.GetPool(_chargeEffect.StepUpEffectPrefab, transform, 1f);
                }
                // ���� Ư���ڿ������� �������� �� ���� ���
                else if (Model.SpecialChargeGage > Model.CurMana)
                {
                    Model.SpecialChargeGage = Model.CurMana;
                }
            }
            else
            {
                Model.SpecialChargeGage = _charges[_index - 1].ChargeMana;
            }
        }
        else
        {
            Model.SpecialChargeGage = _charges[_index - 1].ChargeMana;
        }
    }

    private void SelectSpecialAttack(int index)
    {
        Rb.velocity = Vector3.zero;
        // ���� ��뷮��ŭ ����
        Model.CurMana -= _charges[_index].ChargeMana;
        Model.SpecialChargeGage = 0;
        // ����� ������Ʈ��ŭ ���Ť�
        for (int i = 0; i < _charges[_index].ObjectCount; i++)
        {
            Model.PopThrowObject();
        }
        switch (index)
        {
            case 0:
                ProcessFirstSpecial();
                break;
            case 1:
                ProcessSecondSpecial();
                break;
            case 2:
                ProcessThirdSpecial();
                break;
        }
    }

    private void SelectTrigger(int index)
    {
        switch (index)
        {
            case 0:
                break;
            case 1:
                ThrowSpecialObject();
                break;
            case 2:
                Bombard();
                break;
        }
    }


    /// <summary>
    /// ù��° Ư��
    /// </summary>
    private void ProcessFirstSpecial()
    {
        if (_firstSpecialRoutine != null)
        {
            CoroutineHandler.StopRoutine(_firstSpecialRoutine);
            _firstSpecialRoutine = null;

            Model.AttackSpeedMultiplier -= _first.AttackSpeed;
        }
        _firstSpecialRoutine = CoroutineHandler.StartRoutine(_firstSpecialRoutine, FirstSpecialBuffRoutine());

        // ���� �ִϸ��̼��� ��� �߰�ȿ�� Ʈ���Ÿ� ������ �ߵ��ؾ� �ҵ�
        Player.TriggerPlayerAdditional();


        ObjectPool.GetPool(_first.SpecialEffect, Player.RightArmPoint, 2f);
        ObjectPool.GetPool(_first.BuffEffectPrefab, transform, _first.Duration);
        ChangeState(Player.PrevState);


    }
    IEnumerator FirstSpecialBuffRoutine()
    {
        Model.AttackSpeedMultiplier += _first.AttackSpeed;
        _balance.OnFirstSpecial = true;
        yield return _first.Duration.GetDelay();
        Model.AttackSpeedMultiplier -= _first.AttackSpeed;
        _balance.OnFirstSpecial = false;

        ObjectPool.GetPool(_first.BuffEndEffect, transform,3f);
        _firstSpecialRoutine = null;
    }
    /// <summary>
    /// �ι�° Ư��
    /// </summary>
    private void ProcessSecondSpecial()
    {
        Player.LookAtAttackDir();
        View.SetTrigger(PlayerView.Parameter.BalanceSpecial2);
    }
    /// <summary>
    /// Ư�� ��ô�� ������
    /// </summary>
    private void ThrowSpecialObject()
    {
        SpecialObject specialObject = ObjectPool.GetPool(_second.SpecialObject, _muzzlePoint.position, _muzzlePoint.rotation);
        specialObject.Init(Player, CrowdControlType.None, true, 0,Model.ThrowAdditionals);
        specialObject.InitSpecial(_second.Damage, _second.MiddleDamage, _second.Range, _second.MiddleRange);
        specialObject.Shoot(Player.ThrowPower);
    }

    /// <summary>
    /// ����° Ư��
    /// </summary>
    private void ProcessThirdSpecial()
    {
        Player.LookAtAttackDir();
        View.SetTrigger(PlayerView.Parameter.BalanceSpecial3);
    }
    /// <summary>
    /// �����ϱ�
    /// </summary>
    private void Bombard()
    {
        Player.LookAtAttackDir();
        CoroutineHandler.StartRoutine(BombardRoutine());
    }

    IEnumerator BombardRoutine()
    {
        Vector3 attackPos = new Vector3(
                transform.position.x + (Player.transform.forward.x * _third.AttackOffset.x),
                transform.position.y + 0.01f,
                transform.position.z + (Player.transform.forward.z * _third.AttackOffset.z));

        int layer = Layer.EveryThing;
        layer &= ~(Layer.Monster);

        if(Physics.Raycast(attackPos, Vector3.down, out RaycastHit hit,100f, Layer.EveryThing, QueryTriggerInteraction.Ignore))
        {
            attackPos = new Vector3(hit.point.x, hit.point.y + 0.01f, hit.point.z);
        }

        GameObject beforeEffect = ObjectPool.GetPool(_third.BeforeEffect, attackPos, Quaternion.identity);
        beforeEffect.transform.localScale *= _third.MiddleRange;

        yield return (_third.AttackDelay-0.3f).GetDelay();

        // ����ġ�� ����
        SoundManager.PlaySFX(Player.Sound.Balance.Special3Hit);
        yield return 0.3f.GetDelay();
        ObjectPool.ReturnPool(beforeEffect);
        // ���� ����
        SoundManager.PlaySFX(Player.Sound.Balance.Special3Loop);
        // ����
        for (int i = 0; i < 8; i++) 
        {
            AttackBombard(attackPos);
            GameObject effect = ObjectPool.GetPool(_third.Effect, attackPos, _third.Effect.transform.rotation, 1f);       
            effect.transform.localScale *= _third.MiddleRange;
            yield return 0.25f.GetDelay();
        }

    }

    private void AttackBombard(Vector3 attackPos)
    {
        // �߾� ���� ���� �ɷ�����
        int hitCount = Physics.OverlapSphereNonAlloc(attackPos, _third.MiddleRange, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            MiddleHittargets.Add(Player.OverLapColliders[i].transform);
        }


        hitCount = Physics.OverlapSphereNonAlloc(attackPos, _third.Range, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            int finalDamage = 0;
            bool isCritical = false;
            // �߾ӿ� �¾��� ���
            if (MiddleHittargets.Contains(Player.OverLapColliders[i].transform))
            {
                finalDamage = Player.GetFinalDamage((int)_third.MiddleDamage, out isCritical);
            }
            else
            {
                finalDamage = Player.GetFinalDamage((int)_third.Damage, out isCritical);
            }

            Battle.TargetAttack(Player.OverLapColliders[i], isCritical, finalDamage);
            Battle.TargetCrowdControl(Player.OverLapColliders[i], CrowdControlType.Stun, 1f);
            Battle.TargetDebuff(Player.OverLapColliders[i], _third.ElectricShock);
        }
        MiddleHittargets.Clear();
    }

    IEnumerator CheckManaUIRoutine()
    {
        while (true)
        {
            View.Panel.Step[0].SetActive(Model.CurMana >= _charges[0].ChargeMana && Model.CurThrowables >= _charges[0].ObjectCount);
            View.Panel.Step[1].SetActive(Model.CurMana >= _charges[1].ChargeMana && Model.CurThrowables >= _charges[1].ObjectCount);
            View.Panel.Step[2].SetActive(Model.CurMana >= _charges[2].ChargeMana && Model.CurThrowables >= _charges[2].ObjectCount);
            yield return 0.1f.GetDelay();
        }
    }
}
