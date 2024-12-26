using System.Collections.Generic;
using UniRx;
using UnityEngine;
using static PlayerData;

public class PlayerModel : MonoBehaviour
{
    public GlobalPlayerData GlobalData;
    public PlayerData Data;

    public ArmUnit Arm;
    public int Hp { get { return Data.Hp; } set { Data.Hp = value; } }
    public int Defense { get { return Data.Defense; } set { Data.Defense = value; } }
    public int Damage { get { return Data.Damage; } set { Data.Damage = value; } }
    public int MaxThrowCount { get { return Data.MaxThrowCount; } set { Data.MaxThrowCount = value; } }
    public int CurThrowCount
    {
        get { return Data.CurThrowCount; }
        set
        {
            Data.CurThrowCount = value;
            CurThrowCountSubject?.OnNext(Data.CurThrowCount);

        }
    }
    public Subject<int> CurThrowCountSubject = new Subject<int>();

    public List<AdditionalEffect> AdditionalEffects { get { return Data.AdditionalEffects; } set { Data.AdditionalEffects = value; } } // 블루칩 모음 리스트
    public List<HitAdditional> HitAdditionals { get { return Data.HitAdditionals; } set { Data.HitAdditionals = value; } }
    public List<ThrowAdditional> ThrowAdditionals { get { return Data.ThrowAdditionals; } set { Data.ThrowAdditionals = value; } } // 공격 방법 추가효과 리스트
    public List<PlayerAdditional> PlayerAdditionals { get { return Data.PlayerAdditionals; } set { Data.PlayerAdditionals = value; } } // 플레이어 추가효과 리스트
    public List<ThrowObjectData> ThrowObjectStack { get { return Data.ThrowObjectStack; } set { Data.ThrowObjectStack = value; } }
    public float MoveSpeed { get { return Data.MoveSpeed; } set { Data.MoveSpeed = value; } } // 이동속도

    // TODO : 인스펙터 정리 필요
    public float DashPower; // 대쉬 속도
    // TODO : 인스펙터 정리 필요
    public float JumpPower; // 점프력
    [System.Serializable]
    public struct StaminaStruct
    {
        public float MaxStamina; // 최대 스테미나
        public float CurStamina; // 현재 스테미나
        public float StaminaRecoveryPerSecond; // 스테미나 초당 회복량
        public float StaminaCoolTime; // 스테미나 소진 후 쿨타임
    }
    [SerializeField] public StaminaStruct Stamina;
    public float MaxStamina { get { return Stamina.MaxStamina; } set { Stamina.MaxStamina = value; } } // 최대 스테미나
    public float CurStamina { get { return Stamina.CurStamina; } set { Stamina.CurStamina = value; CurStaminaSubject.OnNext(Stamina.CurStamina); } } // 현재 스테미나
    public Subject<float> CurStaminaSubject = new Subject<float>();
    public float StaminaRecoveryPerSecond { get { return Stamina.StaminaRecoveryPerSecond; } set { Stamina.StaminaRecoveryPerSecond = value; } } // 스테미나 초당 회복량
    public float StaminaCoolTime { get { return Stamina.StaminaCoolTime; } set { Stamina.StaminaCoolTime = value; } } // 스테미나 소진 후 쿨타임

    [System.Serializable]
    public struct SpecialStruct
    {
        public float MaxSpecialGage;
        public float CurSpecialGage;
        public float SpecialRecoveryAmount;
        public float SpecialChargeGage;
    }
    [SerializeField] public SpecialStruct Special;
    public float MaxSpecialGage { get { return Special.MaxSpecialGage; } set { Special.MaxSpecialGage = value; } } // 최대 특수자원
    public float CurSpecialGage // 현재 특수 자원
    {
        get { return Special.CurSpecialGage; }
        set
        {
            Special.CurSpecialGage = value;
            // 현재 특수공격 자원이 최대치를 넘길 수 없음
            if (Special.CurSpecialGage > Special.MaxSpecialGage)
            {
                Special.CurSpecialGage = Special.MaxSpecialGage;
            }
            else if(Special.CurSpecialGage < 0)
            {
                Special.CurSpecialGage = 0;
            }
            CurSpecialGageSubject.OnNext(Special.CurSpecialGage);
        }
    }
    public Subject<float> CurSpecialGageSubject = new Subject<float>();
    public float SpecialRecoveryAmount { get { return Special.SpecialRecoveryAmount; } set { Special.SpecialRecoveryAmount = value; } } // 특수자원 회복량
    public float SpecialChargeGage // 특수공격 차지량
    {
        get { return Special.SpecialChargeGage; }
        set
        {
            Special.SpecialChargeGage = value;
            if (Special.SpecialChargeGage > 1)
                SpecialChargeGage = 1;
            SpecialChargeGageSubject.OnNext(Special.SpecialChargeGage);
        }
    }
    public Subject<float> SpecialChargeGageSubject = new Subject<float>();


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
    public float BoomRadius { get { return Throw.BoomRadius; } set { Throw.BoomRadius = value; } }

    // TODO : 인스펙터 정리 필요
    public float DrainDistance;


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
    public ThrowObjectData PeekThrowObject()
    {
        ThrowObjectData data = ThrowObjectStack[CurThrowCount - 1];
        return data;
    }

    // TODO : 일단 젠젝트 실패, 싱글톤으로 구현 후 이후에 리팩토링 
    private void Start()
    {
        //Data = DataContainer.Instance.PlayerData;
    }
}


public partial class GlobalPlayerData
{

}
public partial class PlayerData
{
    [System.Serializable]
    public struct NSJTest
    {
        public float MoveSpeed;
        public int Hp;
        public int Defense;
        public int Damage;
        public int MaxThrowCount;
        public int CurThrowCount;
        public List<ThrowObjectData> ThrowObjectStack;
        public List<AdditionalEffect> AdditionalEffects; // 블루칩 모음 리스트
        public List<HitAdditional> HitAdditionals;
        public List<ThrowAdditional> ThrowAdditionals; // 공격 방법 추가효과 리스트
        public List<PlayerAdditional> PlayerAdditionals; // 플레이어 추가효과 리스트
    }
    [SerializeField] private NSJTest _NSJTest;
    public float MoveSpeed { get { return _NSJTest.MoveSpeed; } set { _NSJTest.MoveSpeed = value; } }
    public int Hp { get { return _NSJTest.Hp; } set { _NSJTest.Hp = value; } }
    public int Defense { get { return _NSJTest.Defense; } set { _NSJTest.Defense = value; } }
    public int Damage { get { return _NSJTest.Damage; } set { _NSJTest.Damage = value; } }
    public int MaxThrowCount { get { return _NSJTest.MaxThrowCount; } set { _NSJTest.MaxThrowCount = value; } }
    public int CurThrowCount { get { return _NSJTest.CurThrowCount; } set { _NSJTest.CurThrowCount = value; } }
    public List<ThrowObjectData> ThrowObjectStack { get { return _NSJTest.ThrowObjectStack; } set { _NSJTest.ThrowObjectStack = value; } }
    public List<AdditionalEffect> AdditionalEffects { get { return _NSJTest.AdditionalEffects; } set { _NSJTest.AdditionalEffects = value; } }
    public List<HitAdditional> HitAdditionals { get { return _NSJTest.HitAdditionals; } set { _NSJTest.HitAdditionals = value; } }
    public List<ThrowAdditional> ThrowAdditionals { get { return _NSJTest.ThrowAdditionals; } set { _NSJTest.ThrowAdditionals = value; } } // 공격 방법 추가효과 리스트
    public List<PlayerAdditional> PlayerAdditionals { get { return _NSJTest.PlayerAdditionals; } set { _NSJTest.PlayerAdditionals = value; } } // 플레이어 추가효과 리스트
}
