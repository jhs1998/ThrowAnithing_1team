using System.Collections;
using UnityEngine;

/// <summary>
/// 유도 기능
/// </summary>
[CreateAssetMenu(fileName = "GuidedAttack", menuName = "AdditionalEffect/Throw/GuidedAttack")]
public class GuidedAttack : ThrowAdditional
{
    [SerializeField] private float _guidedDistance = 5f;

    private bool _isDetect;
    private Transform _ignoreTarget;
    private Transform _target;

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
        // 적 감지 안됨
        if(_isDetect == false)
        {
            // 적 감지
            int hitCount = Physics.OverlapSphereNonAlloc(_throwObject.transform.position, _guidedDistance, _targets, 1 << Layer.Monster);
            if (hitCount > 0)
            {
                _isDetect = true;
            }
            for(int i = 0; i < hitCount; i++)
            {
                if (_targets[i].transform == _ignoreTarget)
                    continue;

                _target = _targets[i].transform;
                break;
            }
        }
        // 적 감지하면 적한테 날아감
        else
        {
            float guidedSpeed = Player.ThrowPower;

            Vector3 targetPos = new Vector3(_target.position.x, _target.position.y + 1f, _target.position.z);

            _throwObject.transform.LookAt(targetPos);

            _throwObject.Rb.velocity = _throwObject.transform.forward * guidedSpeed;
        }
    }
}
