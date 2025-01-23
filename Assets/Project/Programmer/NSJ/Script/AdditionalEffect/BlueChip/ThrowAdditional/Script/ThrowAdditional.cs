using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAdditional : AdditionalEffect
{
    protected PlayerController Player;
    protected PlayerModel Model => Player.Model;
    protected BattleSystem Battle => Player.Battle;
    protected ThrowObject _throwObject;
    [System.Serializable]
    protected struct TargetInfo
    {
        public Transform transform;
        public float Distance;
    }

    public void Init(PlayerController player, AdditionalEffect addtional,ThrowObject throwObject)
    {
        Origin = addtional.Origin;
        Player = player;
        _throwObject = throwObject;
    }

    protected TargetInfo FindNearTarget()
    {
        TargetInfo minNearTarget = SetTargetInfo(null, 10000f);
        int nearTarhetCount = Physics.OverlapSphereNonAlloc(_throwObject.transform.position, 0.5f, Player.OverLapColliders, 1 << Layer.Monster);
        if (nearTarhetCount > 0)
        {      
            // �ֺ��� ���Ͱ� �ִ��� ��ĵ
            for (int i = 0; i < nearTarhetCount; i++)
            {
                Transform targetTransform = Player.OverLapColliders[i].transform;
                // ���ǿ� �����ϸ� �ش� Ÿ���� �Ÿ��� �Բ� ����
                TargetInfo targetInfo = SetTargetInfo(targetTransform, Vector3.Distance(_throwObject.transform.position, targetTransform.position));

                // �Ÿ��� �� �����ٸ� ��ü
                if (targetInfo.Distance < minNearTarget.Distance)
                {
                    minNearTarget = targetInfo;
                }
            }
        }
        return minNearTarget;
    }
    //TargetInfo ����
    protected TargetInfo SetTargetInfo(Transform target, float distance)
    {
        TargetInfo info = new TargetInfo();
        info.transform = target;
        info.Distance = distance;
        return info;
    }
}
