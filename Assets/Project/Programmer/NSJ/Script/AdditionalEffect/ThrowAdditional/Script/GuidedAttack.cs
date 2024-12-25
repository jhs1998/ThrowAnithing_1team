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

    Collider[] _targets = new Collider[1];
    Coroutine _guidedRoutien;
    public override void Enter()
    {
       
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
        }
        // 적 감지하면 적한테 날아감
        else
        {
            float guidedSpeed = _player.ThrowPower;

            Vector3 targetPos = new Vector3(_targets[0].transform.position.x, _targets[0].transform.position.y + 0.7f, _targets[0].transform.position.z);

            _throwObject.transform.LookAt(targetPos);

            _throwObject.Rb.velocity = _throwObject.transform.forward * guidedSpeed;
        }
    }
}
