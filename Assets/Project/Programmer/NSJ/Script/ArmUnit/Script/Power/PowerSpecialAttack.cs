using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Power PrevSpecial", menuName = "Arm/AttackType/Power/PrevSpecial")]
public class PowerSpecialAttack : ArmSpecialAttack
{
    [System.Serializable]
    struct ChargeEffectStruct
    {
        public GameObject ChargeEffect;
    }
    [System.Serializable]
    struct EffectStruct
    {
        public ChargeEffectStruct[] Effects;
        public GameObject Attack;
        public GameObject FullCharge;
        public PowerObjectEffect EffectObject;
   
    }
    [System.Serializable]
    struct ChargeStruct
    {
        public float ChargeTime;
        public float ChargeMana;
        public int ObjectCount;
        public Vector3 AttackOffset;
        public float Radius;
        public int Damage;
        public float KnockBackDistance;
    }
    [SerializeField] private ChargeStruct[] _charges;
    [SerializeField] private GameObject _specialRange;
    [SerializeField] private float _moveSpeedMultyPlier;
    [SerializeField] private EffectStruct _effect;
    private float _maxChargeTime => _charges[_charges.Length - 1].ChargeTime;
    private float _maxChargeMana => _charges[_charges.Length - 1].ChargeMana;
    private PowerObjectEffect _effectObject;
    private GameObject _instanceSpecialRange;

    private GameObject _chargeEffect;
    private Vector3 _dropPos;
    Coroutine _chargeRoutine;


    Coroutine _checkManaUIRoutien;
    public override void Init(PlayerController player, ArmUnit arm)
    {
        base.Init(player,arm);
        View.Panel.SetChargingMpVarMaxValue(Model.MaxMana);
        for (int i = 0; i < _charges.Length; i++)
        {
            _charges[i].Damage = (int)Model.PowerSpecialAttack[i];
            //View.Panel.StepTexts[i].SetText(_charges[i].ObjectCount.GetText());
            _charges[i].ChargeMana = Model.ManaConsumption[i];
            View.Panel.SetChargingMpHandle(i, _charges[i].ChargeMana);
        }
        // ���� ���� ���� ���� üũ
        _checkManaUIRoutien = CoroutineHandler.StartRoutine(_checkManaUIRoutien, CheckManaUIRoutine());
    }

    private void OnDestroy()
    {
        _checkManaUIRoutien = CoroutineHandler.StopRoutine(_checkManaUIRoutien);
    }
    public override void Enter()
    {
        if (Model.ThrowObjectStack.Count < _charges[_index].ObjectCount || Model.CurMana < Model.ManaConsumption[0])
        {
            ChangeState(Player.PrevState);
            return;
        }
        Player.Rb.velocity = Vector3.zero;

        // ��¡ ��� ����
        View.SetTrigger(PlayerView.Parameter.PowerSpecial);
        if (_chargeRoutine == null)
        {
            _chargeRoutine = CoroutineHandler.StartRoutine(ChargeRoutine());
        }
    }
    public override void Exit()
    {
        if (_chargeRoutine != null)
        {
            CoroutineHandler.StopRoutine(_chargeRoutine);
            _chargeRoutine = null;
        }


        #region ���ݿ� ��ȯ�ߴ� �׷��� ������Ʈ ����
        if (_effectObject != null)
        {
            _effectObject.End();
            ObjectPool.ReturnPool(_effectObject);
            _effectObject = null;
        }
        if (_instanceSpecialRange != null)
        {
            ObjectPool.ReturnPool(_instanceSpecialRange);
            _instanceSpecialRange = null;
        }
        if (_chargeEffect != null)
        {
            ObjectPool.ReturnPool(_chargeEffect);
        }
        #endregion
        // ���� ����
        Player.StopSFX();

        Model.SpecialChargeGage = 0;
        _index = 0;
        Player.IsInvincible = false;
    }
    public override void Update()
    {

    }
    public override void OnTrigger()
    {
        if (_chargeEffect != null)
        {
            ObjectPool.ReturnPool(_chargeEffect);
        }
        AttackSpecial();
    }
    public override void EndAnimation()
    {
        ChangeState(PlayerController.State.Idle);
    }
    IEnumerator ChargeRoutine()
    {
        // �տ� ������Ʈ ���̴� ���� ����Ʈ ����
        CreateSpecialObject();

        // ���� ����
        Player.PlaySFX(Player.Sound.Power.Charge);
        while (true)
        {
            Move();
            ProcessCharge();

            if (InputKey.GetButtonUp(InputKey.Special))
            {
                Model.SpecialChargeGage = 0;


                // ���� ���� ����
                if (_index != 0)
                {
                    _index--;
                    Player.LookAtAttackDir();
                    View.SetTrigger(PlayerView.Parameter.ChargeEnd);

                    Player.StopSFX();
                }
                else
                {
                    View.SetTrigger(PlayerView.Parameter.ChargeCancel);
                    ChangeState(PlayerController.State.Idle);
                }
                _chargeRoutine = null;
                // ĳ���� �ӽ� ����
                Player.IsInvincible = true;
                break;
            }
            yield return null;
        }
    }
    private void ProcessCharge()
    {
        // �÷��̾� ���ݹ��� ��� �ٶ󺸱�
        Player.LookAtAttackDir();
        // ���ݹ��� ��ġ ���
        if (_index > 0)
        {
            _dropPos = new Vector3(
                transform.position.x + (Player.transform.forward.x * _charges[_index - 1].AttackOffset.x),
                transform.position.y + 0.01f,
                transform.position.z + (Player.transform.forward.z * _charges[_index - 1].AttackOffset.z));
            _instanceSpecialRange.transform.position = _dropPos;
        }
        // ������ ȿ�� �� ����ٴϱ�
        if (_effectObject != null)
        {
            _effectObject.transform.position = Player.RightArmPoint.position;
            _effectObject.transform.rotation = Quaternion.LookRotation(transform.forward);
        }

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
                    // ������ �׷���
                    CreateSpecialObject();

                    CreateSpecialRange();

                    CreateChargeEffect();
                    _index++;
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

    private void CreateSpecialObject()
    {
        if(_effectObject == null)
        {
            _effectObject = ObjectPool.GetPool(_effect.EffectObject, Player.RightArmPoint.position, _effect.EffectObject.transform.rotation);
            _effectObject.transform.SetParent(Player.RightArmPoint);
        }
        else
        {
            _effectObject.Next();
        }
    }
    private void CreateSpecialRange()
    {
        //if (_effectObject != null)
        //    Destroy(_instanceSpecialRange);
        // ���ݹ��� �׷���
        _dropPos = new Vector3(
              transform.position.x + (Player.CamareArm.forward.x * _charges[_index].AttackOffset.x),
              transform.position.y + 0.01f,
              transform.position.z + (Player.CamareArm.forward.z * _charges[_index].AttackOffset.z));


        if (_instanceSpecialRange == null)
        {
            _instanceSpecialRange = ObjectPool.GetPool(_specialRange, _dropPos, Quaternion.identity);
        }
        // ũ�� ����
        _instanceSpecialRange.transform.localScale = new Vector3(
            _charges[_index].Radius * 2,
            _instanceSpecialRange.transform.localScale.y,
            _charges[_index].Radius * 2);
    }

    private void CreateChargeEffect()
    {
        if (_chargeEffect != null)
        {
            ObjectPool.ReturnPool(_chargeEffect);
        }
        _chargeEffect = ObjectPool.GetPool(_effect.Effects[_index].ChargeEffect, Player.RightArmPoint);
    }
    private void AttackSpecial()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(_dropPos, _charges[_index].Radius, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            int finalDamage = Player.GetFinalDamage(_charges[_index].Damage, out bool isCritical);
            // ���� �� ������ ������

            // ������ �ֱ�
            Battle.TargetAttackWithDebuff(Player.OverLapColliders[i], isCritical, finalDamage,   false);
            // Ǯ���� ��
            if(_index == _charges.Length - 1)
            {
                Battle.TargetCrowdControl(Player.OverLapColliders[i], CrowdControlType.Stun, 1);
            }
            else
            {
                Battle.TargetCrowdControl(Player.OverLapColliders[i], CrowdControlType.Stiff);
            }


            // �˹� �����ϸ� �˹�
            if (_charges[_index].KnockBackDistance > 0)
                Player.DoKnockBack(Player.OverLapColliders[i].transform, transform, _charges[_index].KnockBackDistance);
        }
        // ���� ��뷮��ŭ ����
        Model.CurMana -= _charges[_index].ChargeMana;
        // ����� ������Ʈ��ŭ ����
        for (int i = 0; i < _charges[_index].ObjectCount; i++)
        {
            Model.PopThrowObject();
        }

        
        // Ÿ������ ����Ʈ ����
        GameObject attackEffect = ObjectPool.GetPool(_effect.Attack, _dropPos, Quaternion.identity, 2f);
        attackEffect.transform.localScale = Util.GetPos(_charges[_index].Radius/2);
        // Ǯ������ �߰� ����Ʈ ����
        if (_index == _charges.Length - 1) 
        {
            ObjectPool.GetPool(_effect.FullCharge, _dropPos, Quaternion.identity, 2f);
        }
        ObjectPool.ReturnPool(_instanceSpecialRange);
        ObjectPool.ReturnPool(_effectObject,0.5f);

        // ���� ����
        SoundManager.PlaySFX(Player.Sound.Power.SpecialHit);

        _instanceSpecialRange = null;
    }

    private void Move()
    {
        if (Player.MoveDir == Vector3.zero)
            return;
        Player.LookAtMoveDir();

        // �÷��̾� �̵�
        // ���� �ְ� ���� �ε����� ���� ���¿����� �̵�
        if (Player.IsGround == false && Player.IsWall == true)
            return;
        Vector3 originRb = Rb.velocity;
        Vector3 velocityDir = transform.forward * (Model.MoveSpeed * _moveSpeedMultyPlier);
        Rb.velocity = new Vector3(velocityDir.x, originRb.y, velocityDir.z);
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
