using System.Collections;
using UnityEngine;

/// <summary>
/// ���� ���
/// </summary>
[CreateAssetMenu(fileName = "GuidedAttack", menuName = "AdditionalEffect/PrevThrow/GuidedAttack")]
public class GuidedAttack : ThrowAdditional
{
    [Header("���� ���� ����")]
    [SerializeField] private float _guidedDistance = 5f;

    private bool _isDetect;
    private Transform _ignoreTarget;
    private Collider _target;

    Collider[] _targets = new Collider[5];
    Coroutine _guidedRoutien;
    public override void Enter()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(_throwObject.transform.position, 0.5f, _targets, 1 << Layer.Monster);
        if (hitCount > 0) 
        {
            _ignoreTarget = _targets[0].transform;
        }
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {
        // �� ���� �ȵ�
        if(_isDetect == false)
        {
            // �� ����
            int hitCount = Physics.OverlapSphereNonAlloc(_throwObject.transform.position, _guidedDistance, _targets, 1 << Layer.Monster);
            if (hitCount > 0)
            {
                _isDetect = true;
            }
            for(int i = 0; i < hitCount; i++)
            {
                if (_targets[i].transform == _ignoreTarget)
                    continue;

                _target = _targets[i];
                break;
            }
        }
        // �� �����ϸ� ������ ���ư�
        else
        {
            float guidedSpeed = Player.ThrowPower;

            Vector3 targetPos = new Vector3(_target.transform.position.x, _target.transform.position.y + 1f, _target.transform.position.z);

            _throwObject.transform.LookAt(targetPos);

            _throwObject.Rb.velocity = _throwObject.transform.forward * guidedSpeed;

            if(_target.transform == null || _target.gameObject.activeSelf == false || _target.enabled == false)
            {
                _isDetect = false;
            }
        }
    }
}
