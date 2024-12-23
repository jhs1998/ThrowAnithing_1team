using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MKH
{
    [Serializable] public struct EquipmentEffect
    {
        [Header("추가 공격력")]
        [SerializeField] private float mDamage;
        public float Damage { get { return mDamage; } }

        [Header("추가 방어력")]
        [SerializeField] private float mDefense;
        public float Defense { get { return mDefense; } }

        public static EquipmentEffect operator +(EquipmentEffect param1, EquipmentEffect param2)
        {
            EquipmentEffect calcedEffect;

            calcedEffect.mDamage = param1.mDamage + param2.mDamage;
            calcedEffect.mDefense = param1.mDefense + param2.mDefense;

            return calcedEffect;
        }
    }

    public class Item_Equipment : Item
    {
        [Space(50)]
        [Header("장비 아이템 효과")]
        [SerializeField] EquipmentEffect mEffect;
        public EquipmentEffect Effect { get { return mEffect; } }
    }
}
