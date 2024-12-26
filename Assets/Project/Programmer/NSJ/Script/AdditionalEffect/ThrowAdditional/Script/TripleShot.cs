using UnityEngine;


/// <summary>
/// 세발 멀티샷
/// </summary>
[CreateAssetMenu(fileName = "TripleShot",menuName = "AdditionalEffect/Throw/TripleShot")]
public class TripleShot : ThrowAdditional
{
    [Range(0, 90)][SerializeField] private float _angle;

    [HideInInspector]public bool CanTripleShot = true;
    public override void Enter()
    {
       
    }

    public override void TriggerFirst()
    {
        Shot();
    }

    private void Shot()
    { 
        Vector3 originObjectRot = _throwObject.transform.eulerAngles;
        for (int i = 0; i < 2; i++)
        {
            // 점프 공격일 땐 기본 오브젝트만 던져야함
            int throwObjectID = 0;
            if (_player.CurState == PlayerController.State.JumpAttack)
            {
                throwObjectID = 0;
            }
            else
            {
                throwObjectID = _model.ThrowObjectStack.Count > 0 ? _model.PopThrowObject().ID : 0;
            }

            float angleY = i == 0 ? originObjectRot.y - _angle : originObjectRot.y + _angle;
            Quaternion shotAngle = Quaternion.Euler(
               originObjectRot.x,
               angleY,
               originObjectRot.z);

            ThrowObject throwObject = GameObject.Instantiate(DataContainer.GetThrowObject(throwObjectID), _throwObject.transform.position, shotAngle);
            throwObject.Init(_player, _model.HitAdditionals, _model.ThrowAdditionals);
            throwObject.Damage = _throwObject.Damage;
            throwObject.Shoot(_player.ThrowPower);
        }
    }

}
