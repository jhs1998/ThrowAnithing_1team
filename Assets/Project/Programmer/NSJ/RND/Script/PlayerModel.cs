using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] public float MoveSpeed;


    [System.Serializable]
    public struct MeleeStruct
    {
        [HideInInspector] public int ComboCount;
        public int Damage;
        public MeleeAttackStruct[] MeleeAttack;
    }
    [System.Serializable]
   public struct MeleeAttackStruct
    {
        public float Range;
        [Range(0, 360)] public float Angle;
        [Range(0, 5)] public float DamageMultiplier;
    }
    [Header("근접공격 관련 필드")]
    [SerializeField] public MeleeStruct Melee;
    public int MeleeComboCount
    {
        get { return Melee.ComboCount; }
        set
        {
            Melee.ComboCount = value;
            if (Melee.ComboCount >= Melee.MeleeAttack.Length)
            {
                Melee.ComboCount = 0;
            }
        }
    }
    public float Range => Melee.MeleeAttack[Melee.ComboCount].Range;
    public float Angle => Melee.MeleeAttack[Melee.ComboCount].Angle;
    public float DamageMultiplier => Melee.MeleeAttack[Melee.ComboCount].DamageMultiplier;
    public int MeleeDamage { get { return Melee.Damage; } set { Melee.Damage = value; } }

    [System.Serializable]
    public struct ThrowStruct
    {
        public int Damage;
        public float BoomRadius;
    }
    [Header("투척 공격 관련 필드")]
    [SerializeField] public ThrowStruct Throw;
    public int ThrowDamage { get { return Throw.Damage; } set { Throw.Damage = value; } }
    public float BoomRadius {  get { return BoomRadius; } set { BoomRadius = value; } }

    [Space(10)]
    public List<HitAdditional> HitAdditionals;

}
