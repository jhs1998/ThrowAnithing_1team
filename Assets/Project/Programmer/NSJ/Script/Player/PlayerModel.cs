using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;
using static GlobalPlayerStateData;

public class PlayerModel : MonoBehaviour
{
    public GlobalPlayerData GlobalData;
    public PlayerData Data;
    public ArmUnit Arm;

    public int MaxHp { get { return Data.MaxHp; } set { Data.MaxHp = value; } }
    public int CurHp { get { return Data.CurHp; } set { Data.CurHp = value; } }
    public int Defense { get { return Data.Defense; } set { Data.Defense = value; } }
    public float DamageReduction { get { return Data.DamageReduction; } set { Data.DamageReduction = value; } }
    public int AttackPower { get { return Data.AttackPower; } set { Data.AttackPower = value; } }
    public float AttackSpeed { get { return Data.AttackSpeed; } set { Data.AttackSpeed = value; _view.SetFloat(PlayerView.Parameter.AttackSpeed, Data.AttackSpeed); } }
    public float[] PowerMeleeAttack { get { return Data.PowerMeleeAttack; } set { Data.PowerMeleeAttack =value; } }
     public float[] PowerThrowAttack { get { return Data.PowerThrowAttack; } set { Data.PowerThrowAttack = value; } }
    public float[] PowerSpecialAttack { get { return Data.PowerSpecialAttack; } set { Data.PowerSpecialAttack = value; } }
    public float CriticalChance { get { return Data.CriticalChance; } set { Data.CriticalChance = value; } }
    public float CriticalDamage { get { return Data.CriticalDamage; } set { Data.CriticalDamage = value; } }
    public int MaxThrowables { get { return Data.MaxThrowables; } set { Data.MaxThrowables = value; } }
    public int CurThrowables
    {
        get { return Data.CurThrowables; }
        set
        {
            Data.CurThrowables = value;
            CurThrowCountSubject?.OnNext(Data.CurThrowables);

        }
    }
    public Subject<int> CurThrowCountSubject = new Subject<int>();

    public List<AdditionalEffect> AdditionalEffects { get { return Data.AdditionalEffects; } set { Data.AdditionalEffects = value; } } // 블루칩 모음 리스트
    public List<HitAdditional> HitAdditionals { get { return Data.HitAdditionals; } set { Data.HitAdditionals = value; } }
    public List<ThrowAdditional> ThrowAdditionals { get { return Data.ThrowAdditionals; } set { Data.ThrowAdditionals = value; } } // 공격 방법 추가효과 리스트
    public List<PlayerAdditional> PlayerAdditionals { get { return Data.PlayerAdditionals; } set { Data.PlayerAdditionals = value; } } // 플레이어 추가효과 리스트
    public List<ThrowObjectData> ThrowObjectStack { get { return Data.ThrowObjectStack; } set { Data.ThrowObjectStack = value; } }
    public float MoveSpeed { get { return Data.MoveSpeed; } set { Data.MoveSpeed = value; } } // 이동속도
    // 대쉬
    public float DashDistance { get { return Data.DashDistance; } set { Data.DashDistance = value; } }
    public int DashStamina { get { return Data.DashStamina; } set { Data.DashStamina = value; } }
    // 점프
    public float JumpPower { get { return Data.JumpPower; } set { Data.JumpPower = value; } }
    public int JumpStamina { get { return Data.JumpStamina; } set { Data.JumpStamina = value; } }
    public int DoubleJumpStamina { get { return Data.DoubleJumpStamina; } set { Data.DoubleJumpStamina = value; } }
    public int JumpDownStamina { get { return Data.JumpDownStamina; } set { Data.JumpDownStamina = value; } }
    public float MaxStamina { get { return Data.MaxStamina; } set { Data.MaxStamina = value; } } // 최대 스테미나
    public float CurStamina { get { return Data.CurStamina; } set { Data.CurStamina = value; CurStaminaSubject.OnNext(Data.CurStamina); } } // 현재 스테미나
    public Subject<float> CurStaminaSubject = new Subject<float>();
    public float RegainStamina { get { return Data.RegainStamina; } set { Data.RegainStamina = value; } } // 스테미나 초당 회복량
    public float StaminaCoolTime { get { return Data.StaminaCoolTime; } set { Data.StaminaCoolTime = value; } } // 스테미나 소진 후 쿨타임

    public float MaxMana { get { return Data.MaxMana; } set { Data.MaxMana = value; } } // 최대 특수자원
    public float CurMana // 현재 특수 자원
    {
        get { return Data.CurMana; }
        set
        {
            Data.CurMana = value;
            // 현재 특수공격 자원이 최대치를 넘길 수 없음
            if (Data.CurMana > Data.MaxMana)
            {
                Data.CurMana = Data.MaxMana;
            }
            else if (Data.CurMana < 0)
            {
                Data.CurMana = 0;
            }
            CurSpecialGageSubject.OnNext(Data.CurMana);
        }
    }
    public Subject<float> CurSpecialGageSubject = new Subject<float>();
    public float[] RegainMana { get { return Data.RegainMana; } set { Data.RegainMana = value; } } // 특수자원 회복량
    public float SpecialChargeGage // 특수공격 차지량
    {
        get { return Data.SpecialChargeGage; }
        set
        {
            Data.SpecialChargeGage = value;
            if (Data.SpecialChargeGage > 1)
                SpecialChargeGage = 1;
            SpecialChargeGageSubject.OnNext(Data.SpecialChargeGage);
        }
    }
    public Subject<float> SpecialChargeGageSubject = new Subject<float>();

    public float[] MeleeAttackStamina { get { return Data.MeleeAttackStamina; } set { Data.MeleeAttackStamina = value; } }



    public float BoomRadius;
    // TODO : 인스펙터 정리 필요
    public float DrainDistance;

    public int ChargeStep;


    private PlayerView _view;
    public void PushThrowObject(ThrowObjectData throwObjectData)
    {
        ThrowObjectStack.Add(throwObjectData);
        CurThrowables++;
    }

    public ThrowObjectData PopThrowObject()
    {
        CurThrowables--;
        ThrowObjectData data = ThrowObjectStack[CurThrowables];
        ThrowObjectStack.RemoveAt(CurThrowables);
        return data;
    }
    public ThrowObjectData PeekThrowObject()
    {
        ThrowObjectData data = ThrowObjectStack[CurThrowables - 1];
        return data;
    }

    private void Awake()
    {
        _view = GetComponent<PlayerView>();
    }
    // TODO : 일단 젠젝트 실패, 싱글톤으로 구현 후 이후에 리팩토링 
    private void Start()
    {
        //Data = DataContainer.Instance.PlayerData;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            AttackSpeed++;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            AttackSpeed--;
        }
    }
    #region 콤보 관련 폐기된 코드
    //[System.Serializable]
    //public struct MeleeStruct
    //{
    //    [HideInInspector] public int ComboCount;
    //    public MeleeAttackStruct[] PowerMeleeAttack;
    //}
    //[System.Serializable]
    //public struct MeleeAttackStruct
    //{
    //    public float Range;
    //    [Range(0, 360)] public float Angle;
    //    [Range(0, 5)] public float DamageMultiplier;
    //}
    //[Header("근접공격 관련 필드")]
    //[SerializeField] public MeleeStruct Melee;
    //public int MeleeComboCount
    //{
    //    get { return Melee.ComboCount; }
    //    set
    //    {
    //        Melee.ComboCount = value;
    //        if (Melee.ComboCount >= Melee.PowerMeleeAttack.Length)
    //        {
    //            Melee.ComboCount = 0;
    //        }
    //    }
    //}
    //public float Range => Melee.PowerMeleeAttack[Melee.ComboCount].Range;
    //public float Angle => Melee.PowerMeleeAttack[Melee.ComboCount].Angle;
    //public float DamageMultiplier => Melee.PowerMeleeAttack[Melee.ComboCount].DamageMultiplier;
    #endregion
}


public partial class GlobalPlayerData
{

}


public partial class PlayerData
{
    [Inject]
    private GlobalPlayerStateData _globalData;
    [System.Serializable]
    public struct HpStruct
    {
        public int MaxHp;
        public int CurHp;
    }

    [System.Serializable]
    public struct AttackStruct
    {
        public int AttackPower;
        public float AttackSpeed;
        public float[] PowerMeleeAttack;
        public float[] PowerThrowAttack;
        public float[] PowerSpecialAttack;
    }
    [System.Serializable]
    public struct StaminaStruct
    {
        public float MaxStamina; // 최대 스테미나
        public float CurStamina; // 현재 스테미나
        public float RegainStamina; // 스테미나 초당 회복량
        public float StaminaCoolTime; // 스테미나 소진 후 쿨타임
        public float ConsumesStamina; // 스테미나 소모량
    }
    [System.Serializable]
    public struct JumpStruct
    {
        public float JumpPower;
        public int JumpStamina;
        public int DoubleJumpStamina;
        public int JumpDownStamina;
        public int MaxJumpCount;
        public int CurJumpCount;
    }
    [System.Serializable]
    public struct DashStruct
    {
        public float DashDistance;
        public int DashStamina;
    }
    [System.Serializable]
    public struct SpecialStruct
    {
        public float MaxMana;
        public float CurMana;
        public float[] RegainMana; // 던지기 공격당 마나 회복
        public float[] ManaConsumption; // 마나 소모량
        [HideInInspector] public float SpecialChargeGage;
    }
    [System.Serializable]
    public struct DefenseStruct
    {
        public int Defense;
        [Range(0, 100)] public float DamageReduction;
    }
    [System.Serializable]
    public struct CriticalStruct
    {
        [Range(0, 100)] public float CriticalChance;
        public float CriticalDamage;
    }
    [System.Serializable]
    public struct ThrowStruct
    {
        public int MaxThrowables;
        public int CurThrowables;
        public float GainMoreThrowables;
        public List<ThrowObjectData> ThrowObjectStack;
    }
    [System.Serializable]
    public struct AdditionalStruct
    {
        public List<AdditionalEffect> AdditionalEffects; // 특수효과 리스트
        public List<HitAdditional> HitAdditionals;
        public List<ThrowAdditional> ThrowAdditionals; // 공격 방법 추가효과 리스트
        public List<PlayerAdditional> PlayerAdditionals; // 플레이어 추가효과 리스트
    }
    [System.Serializable]
    public struct DataStruct
    {
        public HpStruct Hp;
        public DefenseStruct Defense;
        public StaminaStruct Stamina;
        public SpecialStruct Special;
        public JumpStruct Jump;
        public DashStruct Dash;
        public AttackStruct Attack;
        public CriticalStruct Critical;
        public ThrowStruct Throw;
        public AdditionalStruct Additional;
        public GlobalPlayerStateData.AmWeapon NowWeapon;
        public float MoveSpeed;
        public float DrainLife;
        public float[] MeleeAttackStamina;
        public float EquipmentDropUpgrade;
        // 상태이상 지속시간
    }
    [SerializeField] private DataStruct Data;
    public float MoveSpeed { get { return Data.MoveSpeed; } set { Data.MoveSpeed = value; } }
    // 체력
    public int MaxHp { get { return Data.Hp.MaxHp; } set { Data.Hp.MaxHp = value; } }
    public int CurHp { get { return Data.Hp.CurHp; } set { Data.Hp.CurHp = value; } }
    // 방어력
    public int Defense { get { return Data.Defense.Defense; } set { Data.Defense.Defense = value; } }
    public float DamageReduction { get { return Data.Defense.DamageReduction; } set { Data.Defense.DamageReduction = value; } }

    public float DrainLife { get { return Data.DrainLife; } set { Data.DrainLife = value; } }

    // 스테미나
    public float MaxStamina { get { return Data.Stamina.MaxStamina; } set { Data.Stamina.MaxStamina = value; } }
    public float CurStamina { get { return Data.Stamina.CurStamina; } set { Data.Stamina.CurStamina = value; } }
    public float RegainStamina { get { return Data.Stamina.RegainStamina; } set { Data.Stamina.RegainStamina = value; } }
    public float StaminaCoolTime { get { return Data.Stamina.StaminaCoolTime; } set { Data.Stamina.StaminaCoolTime = value; } }
    public float ConsumesStamina { get { return Data.Stamina.ConsumesStamina; } set { Data.Stamina.ConsumesStamina = value; } }
    // 특수공격
    public float MaxMana { get { return Data.Special.MaxMana; } set { Data.Special.MaxMana = value; } }
    public float CurMana { get { return Data.Special.CurMana; } set { Data.Special.CurMana = value; } }
    public float[] RegainMana { get { return Data.Special.RegainMana; } set { Data.Special.RegainMana = value; } }
    public float[] ManaConsumption { get { return Data.Special.ManaConsumption; } set { Data.Special.ManaConsumption = value; } }
    public float SpecialChargeGage { get { return Data.Special.SpecialChargeGage; } set { Data.Special.SpecialChargeGage = value; } }
    // 점프
    public float JumpPower { get { return Data.Jump.JumpPower; } set { Data.Jump.JumpPower = value; } }
    public int JumpStamina { get { return Data.Jump.JumpStamina; } set { Data.Jump.JumpStamina = value; } }
    public int DoubleJumpStamina { get { return Data.Jump.DoubleJumpStamina; } set { Data.Jump.DoubleJumpStamina = value; } }
    public int JumpDownStamina { get { return Data.Jump.JumpDownStamina; } set { Data.Jump.JumpDownStamina = value; } }
    public int MaxJumpCount { get { return Data.Jump.MaxJumpCount; } set { Data.Jump.MaxJumpCount = value; } }
    public int CurJumpCount { get { return Data.Jump.CurJumpCount; } set { Data.Jump.CurJumpCount = value; } }
    // 대쉬
    public float DashDistance { get { return Data.Dash.DashDistance; } set { Data.Dash.DashDistance = value; } }
    public int DashStamina { get { return Data.Dash.DashStamina; } set { Data.Dash.DashStamina = value; } }

    // 공격
    public int AttackPower { get { return Data.Attack.AttackPower; } set { Data.Attack.AttackPower = value; } }
    public float AttackSpeed { get { return Data.Attack.AttackSpeed; } set { Data.Attack.AttackSpeed = value; } }
    public float[] PowerMeleeAttack { get { return Data.Attack.PowerMeleeAttack; } set { Data.Attack.PowerMeleeAttack = value; } }
    public float[] PowerThrowAttack { get { return Data.Attack.PowerThrowAttack; } set { Data.Attack.PowerThrowAttack = value; } }
    public float[] PowerSpecialAttack { get { return Data.Attack.PowerSpecialAttack; } set { Data.Attack.PowerSpecialAttack = value; } }
    // 크리티컬
    public float CriticalChance { get { return Data.Critical.CriticalChance; } set { Data.Critical.CriticalChance = value; } }
    public float CriticalDamage { get { return Data.Critical.CriticalDamage; } set { Data.Critical.CriticalDamage = value; } }
    // 투척오브젝트
    public int MaxThrowables { get { return Data.Throw.MaxThrowables; } set { Data.Throw.MaxThrowables = value; } }
    public int CurThrowables { get { return Data.Throw.CurThrowables; } set { Data.Throw.CurThrowables = value; } }
    public float GainMoreThrowables { get { return Data.Throw.GainMoreThrowables; } set { Data.Throw.GainMoreThrowables = value; } }
    public List<ThrowObjectData> ThrowObjectStack { get { return Data.Throw.ThrowObjectStack; } set { Data.Throw.ThrowObjectStack = value; } }
    // 암유닛
    public GlobalPlayerStateData.AmWeapon NowWeapon { get { return Data.NowWeapon; } set { Data.NowWeapon = value; } }
    //추가효과
    public List<AdditionalEffect> AdditionalEffects { get { return Data.Additional.AdditionalEffects; } set { Data.Additional.AdditionalEffects = value; } }
    public List<HitAdditional> HitAdditionals { get { return Data.Additional.HitAdditionals; } set { Data.Additional.HitAdditionals = value; } }
    public List<ThrowAdditional> ThrowAdditionals { get { return Data.Additional.ThrowAdditionals; } set { Data.Additional.ThrowAdditionals = value; } } // 공격 방법 추가효과 리스트
    public List<PlayerAdditional> PlayerAdditionals { get { return Data.Additional.PlayerAdditionals; } set { Data.Additional.PlayerAdditionals = value; } } // 플레이어 추가효과 리스트
    public float[] MeleeAttackStamina { get { return Data.MeleeAttackStamina; } set { Data.MeleeAttackStamina = value; } }
    public float EquipmentDropUpgrade { get { return Data.EquipmentDropUpgrade; } set { Data.EquipmentDropUpgrade = value; } }




    public void CopyGlobalPlayerData()
    {
        Data.Hp.MaxHp = (int)_globalData.maxHp;
        Data.Attack.AttackPower = (int)_globalData.commonAttack;
        Data.Attack.PowerMeleeAttack[0] = (int)_globalData.shortRangeAttack[0];
        Data.Attack.PowerMeleeAttack[1] = (int)_globalData.shortRangeAttack[1];
        Data.Attack.PowerMeleeAttack[2] = (int)_globalData.shortRangeAttack[2];
        Data.Attack.PowerThrowAttack[0] = (int)_globalData.longRangeAttack[0];
        Data.Attack.PowerThrowAttack[1] = (int)_globalData.longRangeAttack[1];
        Data.Attack.PowerThrowAttack[2] = (int)_globalData.longRangeAttack[2];
        Data.Attack.PowerThrowAttack[3] = (int)_globalData.longRangeAttack[3];
        Data.Attack.PowerSpecialAttack[0] = (int)_globalData.specialAttack[0];
        Data.Attack.PowerSpecialAttack[1] = (int)_globalData.specialAttack[1];
        Data.Attack.PowerSpecialAttack[2] = (int)_globalData.specialAttack[2];
        Data.Attack.AttackSpeed = _globalData.attackSpeed;
        Data.MoveSpeed = _globalData.movementSpeed;
        Data.Critical.CriticalChance = _globalData.criticalChance;
        Data.Defense.Defense = (int)_globalData.defense;
        Data.EquipmentDropUpgrade = _globalData.equipmentDropUpgrade;
        Data.DrainLife = _globalData.drainLife;
        Data.Stamina.MaxStamina = _globalData.maxStamina;
        Data.Stamina.RegainStamina = _globalData.regainStamina;
        Data.Stamina.ConsumesStamina = _globalData.consumesStamina;
        Data.Special.RegainMana[0] = _globalData.regainMana[0];
        Data.Special.RegainMana[1] = _globalData.regainMana[1];
        Data.Special.RegainMana[2] =  _globalData.regainMana[2];
        Data.Special.RegainMana[3] =  _globalData.regainMana[3];
        Data.Special.ManaConsumption[0] = _globalData.manaConsumption[0];
        Data.Special.ManaConsumption[1] = _globalData.manaConsumption[1];
        Data.Special.ManaConsumption[2] = _globalData.manaConsumption[2];
        Data.Throw.GainMoreThrowables = _globalData.gainMoreThrowables;
        Data.Throw.MaxThrowables = (int)_globalData.maxThrowables;
        Data.NowWeapon = _globalData.nowWeapon;
        Data.Special.MaxMana = _globalData.maxMana;
        Data.Jump.MaxJumpCount = (int)_globalData.maxJumpCount;
        Data.Jump.JumpPower = _globalData.jumpPower;
        Data.Jump.JumpStamina = (int)_globalData.jumpConsumesStamina;
        Data.Jump.DoubleJumpStamina =(int)_globalData.doubleJumpConsumesStamina;
        Data.Dash.DashDistance = _globalData.dashDistance;
        Data.Dash.DashStamina = (int)_globalData.dashConsumesStamina;
        Data.MeleeAttackStamina[0] = _globalData.shortRangeAttackStamina[0];
        Data.MeleeAttackStamina[1] = _globalData.shortRangeAttackStamina[1];
        Data.MeleeAttackStamina[2] = _globalData.shortRangeAttackStamina[2];
    }
}
