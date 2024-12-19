using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerModel : MonoBehaviour
{   
    public PlayerData Data;

    public float MoveSpeed { get { return Data.MoveSpeed; }  set{ Data.MoveSpeed = value; } }
    public float DashPower;
    public float JumpPower;
    public float MaxStamina;
    public float CurStamina;
    public int Damage { get { return Data.Damage; } set { Data.Damage = value; } }
    public int MaxThrowCount { get { return Data.MaxThrowCount; } set { Data.MaxThrowCount = value; } }
    public int CurThrowCount { get { return Data.CurThrowCount; } 
        set 
        { 
            Data.CurThrowCount = value;
            CurThrowCountSubject?.OnNext(Data.CurThrowCount);

        } }
    public Subject<int> CurThrowCountSubject = new Subject<int>();
    public List<HitAdditional> HitAdditionals { get { return Data.HitAdditionals; } set { Data.HitAdditionals= value; } }
    public List<ThrowObjectData> ThrowObjectStack { get { return Data.ThrowObjectStack; } set { Data.ThrowObjectStack = value; } }


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

    [System.Serializable]
    public struct ThrowStruct
    {
        public float BoomRadius;
    }
    [Header("투척 공격 관련 필드")]
    [SerializeField] public ThrowStruct Throw;
    public float BoomRadius {  get { return Throw.BoomRadius; } set { Throw.BoomRadius = value; } }


    public void PushThrowObject(ThrowObjectData throwObjectData)
    {
        ThrowObjectStack.Add(throwObjectData);
        CurThrowCount++;
    }

    public ThrowObjectData PopThrowObject()
    {  
        CurThrowCount--;
        ThrowObjectData data = ThrowObjectStack[CurThrowCount];
        ThrowObjectStack.RemoveAt(CurThrowCount);
        return data;
    }

    // TODO : 일단 젠젝트 실패, 싱글톤으로 구현 후 이후에 리팩토링 
    private void Start()
    {
        Data= DataContainer.Instance.PlayerData;
    }
}

[System.Serializable]
public class PlayerData
{
    public float MoveSpeed;

    public int Damage;

    public int MaxThrowCount;

    public int CurThrowCount;

    public List<ThrowObjectData> ThrowObjectStack;

    public List<HitAdditional> HitAdditionals;
}