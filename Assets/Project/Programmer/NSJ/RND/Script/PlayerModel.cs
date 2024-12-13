using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] public float MoveSpeed;


    [System.Serializable]
    public struct MeleeStruct
    {
        public int MaxComboCount;
        [HideInInspector] public int ComboCount;
        public MeleeAttackStruct[] MeleeAttack;
    }
    [System.Serializable]
    public struct MeleeAttackStruct
    {
        public float Range;
        [Range(0, 360)] public float Angle;
        [Range(0, 5)] public float Damage;
    }
    [SerializeField] public MeleeStruct Melee;
    public int MaxComboCount { get { return Melee.MaxComboCount; } }
    public int ComboCount
    {
        get { return Melee.ComboCount; }
        set
        {
            Melee.ComboCount = value;
            if (Melee.ComboCount >= Melee.MaxComboCount)
            {
                Melee.ComboCount = 0;
            }
        }
    }
    public float Range => Melee.MeleeAttack[Melee.ComboCount].Range;
    public float Angle => Melee.MeleeAttack[Melee.ComboCount].Angle;
    public float DamageMultiplier => Melee.MeleeAttack[Melee.ComboCount].Damage;

    public int Damage;

}
