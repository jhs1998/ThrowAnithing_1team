using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] public float MoveSpeed;


    [System.Serializable]
    public struct MeleeStruct
    {
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
    public float DamageMultiplier => Melee.MeleeAttack[Melee.ComboCount].Damage;

    public int Damage;

}
