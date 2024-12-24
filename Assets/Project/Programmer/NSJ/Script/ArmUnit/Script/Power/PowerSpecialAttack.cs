using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "_power Special", menuName = "Arm/AttackType/_power/Special")]
public class PowerSpecialAttack : ArmSpecialAttack
{
    [System.Serializable]
    struct ChargeStruct
    {
        public GameObject DropObject;
        public Vector3 DropSize;
        public float ChargeTime;
        public int ObjectCount;
        public Vector3 AttackOffset;
        public int Damage;
        public float Range;
    }
    [SerializeField] private ChargeStruct[] _charges;
    [SerializeField] private GameObject _specialRange;

    private int _index;
    private float _maxChargeTime => _charges[_charges.Length - 1].ChargeTime;
    private int _triggerIndex;
    private GameObject _instanceDropObject;
    private GameObject _instanceSpecialRange;
    private Vector3 _dropPos;
    Coroutine _chargeRoutine;
    public override void Enter()
    { 
        if (Model.ThrowObjectStack.Count < _charges[_index].ObjectCount)
        {
            EndAnimation();
            return;
        }


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
        if (_instanceDropObject != null)
        {
            Destroy(_instanceDropObject);
        }
        if(_instanceSpecialRange != null)
        {
            Destroy( _instanceSpecialRange);
        }
        #endregion

        Model.SpecialChargeGage = 0;
        _index = 0;
        _triggerIndex = 0;
    }
    public override void Update()
    {
        Player.Rb.velocity = Vector3.zero;
        if (_instanceDropObject != null)
        {
            _instanceDropObject.transform.localPosition = Vector3.zero;
        }
    }
    public override void OnTrigger()
    {
        if (_triggerIndex == 0)
        {
            Player.LookAtCameraFoward();
            _triggerIndex++;
        }
        else
        {
            AttackSpecial();
        }
    }
    public override void EndAnimation()
    {
        ChangeState(PlayerController.State.Idle);
    }
    IEnumerator ChargeRoutine()
    {
        while (true)
        {
            ProcessCharge();

            if (Input.GetButtonUp("Fire2"))
            {
                Model.SpecialChargeGage = 0;
                if (_index != 0)
                {
                    _index--;                
                    View.SetTrigger(PlayerView.Parameter.ChargeEnd);                
                }
                else
                {
                    View.SetTrigger(PlayerView.Parameter.ChargeCancel);
                    ChangeState(PlayerController.State.Idle);
                }
                _chargeRoutine = null;
                break;
            }
            yield return null;
        }
    }
    private void ProcessCharge()
    {
        if (_instanceDropObject != null)
        {
            _dropPos = new Vector3(
                transform.position.x + (Player.CamareArm.forward.x * _charges[_index - 1].AttackOffset.x),
                transform.position.y + 0.01f,
                transform.position.z + (Player.CamareArm.forward.z * _charges[_index - 1].AttackOffset.z));
            _instanceSpecialRange.transform.position = _dropPos;
        }

            // �����ð� ���
            Model.SpecialChargeGage += Time.deltaTime / _maxChargeTime;
        // �ε����� �迭 ũ�⺸�� ��������
        if (_index < _charges.Length)
        {
            // �Ҹ� ������Ʈ�� ������ ���
            if (Model.ThrowObjectStack.Count >= _charges[_index].ObjectCount)
            {
                // ���� �ð��� ���� �ܰ� ��¡ ���ǽð��� �ѱ� ���
                if (Model.SpecialChargeGage >= _charges[_index].ChargeTime / _maxChargeTime)
                {
                    // ������ �׷���
                    CreateSpecialObject();

                    CreateSpecialRange();

                    _index++;
                }
                // ���� Ư���ڿ������� �������� �� ���� ���
                else if (Model.SpecialChargeGage > Model.CurSpecialGage / Model.MaxSpecialGage)
                {
                    Model.SpecialChargeGage = Model.CurSpecialGage / Model.MaxSpecialGage;
                }
            }
            else
            {
                Model.SpecialChargeGage = _index == 0 ? 0 : _charges[_index - 1].ChargeTime / _maxChargeTime;
            }
        }
    }

    private void CreateSpecialObject()
    {
        if (_instanceDropObject != null)
            Destroy(_instanceDropObject);
        _instanceDropObject = Instantiate(_charges[_index].DropObject, Player.ArmPoint);
        _instanceDropObject.transform.localScale = _charges[_index].DropSize;
    }
    private void CreateSpecialRange()
    {
        if (_instanceDropObject != null)
            Destroy(_instanceSpecialRange);
        // ���ݹ��� �׷���
        _dropPos = new Vector3(
              transform.position.x + (Player.CamareArm.forward.x * _charges[_index].AttackOffset.x),
              transform.position.y + 0.01f,
              transform.position.z + (Player.CamareArm.forward.z * _charges[_index].AttackOffset.z));
        _instanceSpecialRange = Instantiate(_specialRange, _dropPos, Quaternion.identity);
        // ũ�� ����
        _instanceSpecialRange.transform.localScale = new Vector3(
            _charges[_index].Range * 2,
            _instanceSpecialRange.transform.localScale.y,
            _charges[_index].Range * 2);
    }
    private void AttackSpecial()
    {
        int finalDamage = Model.Damage + _charges[_index].Damage;
        // ���� �� ������ ������
        int hitCount = Physics.OverlapSphereNonAlloc(_dropPos, _charges[_index].Range, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            IHit hitable = Player.OverLapColliders[i].gameObject.GetComponent<IHit>();
            hitable.TakeDamage(finalDamage);
        }
        // ���� ��뷮��ŭ ����
        Model.CurSpecialGage -= (_charges[_index].ChargeTime / _maxChargeTime) * Model.MaxSpecialGage;
        // ����� ������Ʈ��ŭ ����
        for (int i = 0; i < _charges[_index].ObjectCount; i++) 
        {
            Model.PopThrowObject();
        }

        Destroy(_instanceSpecialRange);
    }
}