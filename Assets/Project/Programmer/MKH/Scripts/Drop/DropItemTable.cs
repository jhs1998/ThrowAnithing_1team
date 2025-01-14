using BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    public class DropItemTable : ScriptableObject
    {
        [Header("DropList 스크립터블오브젝트")]
        [SerializeField] public DropList[] dropLists;

        [Header("아이템 삭제 대기 시간")]
        [SerializeField] public float _destroyItemTime;

        public virtual DropItemTable DropListTable1(GameObject obj, Vector3 pos, Quaternion rot)
        {
            return null;
        }

        public virtual DropItemTable DropListTable2(GameObject obj, Vector3 pos, Quaternion rot)
        {
            return null;
        }
    }
}
