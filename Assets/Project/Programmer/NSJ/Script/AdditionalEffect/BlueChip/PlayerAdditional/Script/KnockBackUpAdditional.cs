using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "KnockBackUp", menuName = "AdditionalEffect/Player/KnockBackUp")]
public class KnockBackUpAdditional : PlayerAdditional
{
    [Header("�˹� �Ÿ� ������(%)")]
    [SerializeField] private float _increaseDistance;

    public override void Enter()
    {
        Model.KnockBackDistanceMultiplier += _increaseDistance;
    }

    public override void Exit()
    {
        Model.KnockBackDistanceMultiplier -= _increaseDistance;
    }
}
