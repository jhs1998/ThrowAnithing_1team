using System.Linq.Expressions;
using UnityEngine;


/// <summary>
/// ���� ��Ƽ��
/// </summary>
[CreateAssetMenu(fileName = "TripleShot",menuName = "AdditionalEffect/PrevThrow/TripleShot")]
public class TripleShot : ThrowAdditional
{

    [Range(0, 90)][SerializeField] private float _angle;

     public bool CanTripleShot = true;
    public override void Enter()
    {
        if (CanTripleShot == false)
            return;

        Shot();
    }

    private void Shot()
    {
        CanTripleShot = false;
        Vector3 originObjectRot = _throwObject.transform.eulerAngles;
        for (int i = 0; i < 2; i++)
        {
            // ���� ������ �� �⺻ ������Ʈ�� ��������
            int throwObjectID = 0;
            // �⺻ ������Ʈ�� ���� ��쿡�� �⺻ ������Ʈ�� ������ ��
           if (_throwObject.Data.ID == 0)
            {
                throwObjectID = 0;
            }
            else
            {
                throwObjectID = Model.ThrowObjectStack.Count > 0 ? Model.PopThrowObject().ID : 0;
            }

            float angleY = i == 0 ? originObjectRot.y - _angle : originObjectRot.y + _angle;
            Quaternion shotAngle = Quaternion.Euler(
               originObjectRot.x,
               angleY,
               originObjectRot.z);
    
            ThrowObject newObject = GameObject.Instantiate(DataContainer.GetThrowObject(throwObjectID), _throwObject.transform.position, shotAngle);
            newObject.Init(Player, _throwObject.CCType, _throwObject.IsBoom, _throwObject.PlayerDamage, _throwObject.ThrowAdditionals);
            _throwObject.AddChainList(newObject);

            newObject.Shoot(Player.ThrowPower);
        }
    }

}
