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

    private int _mosterLayer;
    Collider[] _targets = new Collider[1];
    Coroutine _guidedRoutien;
    public override void Enter()
    {
        _mosterLayer = LayerMask.NameToLayer("Monster");
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {
        if(_isDetect == false)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(_throwObject.transform.position, _guidedDistance, _targets, 1 << _mosterLayer);
            if (hitCount > 0)
            {

                _isDetect = true;
            }
        }
        else
        {
            float guidedSpeed = _player.ThrowPower;

            Vector3 targetPos = new Vector3(_targets[0].transform.position.x, _targets[0].transform.position.y, _targets[0].transform.position.z);

            _throwObject.transform.LookAt(targetPos);

            _throwObject.Rb.velocity = _throwObject.transform.forward * guidedSpeed;
        }


    }

    /// <summary>
    /// 유도기능
    /// </summary>
    /// <returns></returns>
    IEnumerator GuidedRoutine()
    {
        while (true)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(_throwObject.transform.position, _guidedDistance, _targets, 1 << _mosterLayer);

            if (hitCount > 0)
            {
                float guidedSpeed = _player.ThrowPower;
                while (true)
                {
                    Vector3 targetPos = new Vector3(_targets[0].transform.position.x, _targets[0].transform.position.y, _targets[0].transform.position.z);

                    _throwObject.transform.LookAt(targetPos);

                    _throwObject.Rb.velocity = _throwObject.transform.forward * guidedSpeed;

                    //_throwObject.transform.position = Vector3.MoveTowards(
                    //   _throwObject.transform.position,
                    //   targetPos,
                    //   _throwObject.Rb.velocity.magnitude * Time.deltaTime);
                    yield return 0.02f.GetDelay();
                }
            }

            yield return 0.02f.GetDelay();
        }
    }
}
