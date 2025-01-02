using MKH;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PlayerModel : MonoBehaviour, IDebuff
{
    [SerializeField] private bool _isTest;
    [Inject]
    public PlayerData Data;
    [Inject]
    [HideInInspector] public GlobalPlayerStateData GlobalStateData;
    [Inject]
    [HideInInspector] public GlobalGameData GameData;

    public ArmUnit Arm;
    public int MaxHp { get { return Data.MaxHp; } set { Data.MaxHp = value; } }
    public int CurHp { get { return Data.CurHp; } set { Data.CurHp = value; CurHpSubject?.OnNext(Data.CurHp); } }
    public Subject<int> CurHpSubject = new Subject<int>();
    public int Defense { get { return Data.Defense; } set { Data.Defense = value; } }
    public float DamageReduction { get { return Data.DamageReduction; } set { Data.DamageReduction = value; } }
    public int AttackPower { get { return Data.AttackPower; } set { Data.AttackPower = value; } }
    public float AttackSpeed
    {
        get
        {
            return Data.AttackSpeed;
        }
        set { Data.AttackSpeed = value; }
    }
    public float[] PowerMeleeAttack { get { return Data.PowerMeleeAttack; } set { Data.PowerMeleeAttack = value; } }
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
    public float MoveSpeed { get { return (Data.MoveSpeed) / 20; } set { Data.MoveSpeed = value * 20f; } } // 이동속도
    // 대쉬
    public float DashDistance { get { return Data.DashDistance / 20f; } set { Data.DashDistance = value * 20f; } }
    public int DashStamina { get { return Data.DashStamina; } set { Data.DashStamina = value; } }
    // 점프
    public float JumpPower { get { return Data.JumpPower / 13f; } set { Data.JumpPower = value * 13f; } }
    public int JumpStamina { get { return Data.JumpStamina; } set { Data.JumpStamina = value; } }
    public int DoubleJumpStamina { get { return Data.DoubleJumpStamina; } set { Data.DoubleJumpStamina = value; } }
    public int JumpDownStamina { get { return Data.JumpDownStamina; } set { Data.JumpDownStamina = value; } }
    public float MaxStamina { get { return Data.MaxStamina; } set { Data.MaxStamina = value; } } // 최대 스테미나
    public float CurStamina
    {
        get { return Data.CurStamina; }
        set
        {
            float originStamina = Data.CurStamina;
            Data.CurStamina = value;
            if (originStamina > Data.CurStamina)
            {
                _player.IsStaminaCool = true;
            }
            CurStaminaSubject.OnNext(Data.CurStamina);
        }
    } // 현재 스테미나
    public Subject<float> CurStaminaSubject = new Subject<float>();
    public float RegainStamina { get { return Data.RegainStamina; } set { Data.RegainStamina = value; } } // 스테미나 초당 회복량
    public float StaminaCoolTime { get { return Data.StaminaCoolTime; } set { Data.StaminaCoolTime = value; } } // 스테미나 소진 후 쿨타임

    public float MaxStaminaCharge { get { return Data.MaxStaminaCharge; } set { Data.MaxStaminaCharge = value; } }
    public float CurStaminaCharge { get { return Data.CurStaminaCharge; } set { Data.CurStaminaCharge = value; CurStaminaChargeSubject?.OnNext(Data.CurStaminaCharge); } }
    public Subject<float> CurStaminaChargeSubject = new Subject<float>();

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

    public GlobalGameData.AmWeapon NowWeapon { get { return Data.NowWeapon; } set { Data.NowWeapon = value; } }
    public float EquipmentDropUpgrade { get { return Data.EquipmentDropUpgrade; } set { Data.EquipmentDropUpgrade = value; } }

    public float BoomRadius;
    // TODO : 인스펙터 정리 필요
    public float DrainDistance;

    public int ChargeStep;


    private PlayerView _view;
    private PlayerController _player;
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
        _player = GetComponent<PlayerController>();
        if (_isTest == true)
        {
            GlobalStateData.NewPlayerSetting();
        }
        Data.IsDead = false;
        Data.CopyGlobalPlayerData(GlobalStateData, GameData);
        JumpDownStamina = 40;
        CriticalDamage = 200;
    }

    private float prevAttackSpeed;
    private GlobalGameData.AmWeapon prevWeapon;
    void Start()
    {
        this.FixedUpdateAsObservable()
            .Where(x => prevAttackSpeed != AttackSpeed)
            .Subscribe(x => 
            {
                _view.SetFloat(PlayerView.Parameter.AttackSpeed, AttackSpeed);
                prevAttackSpeed = AttackSpeed;
                }
            );
        this.FixedUpdateAsObservable()
            .Where(x => prevWeapon != NowWeapon)
            .Subscribe(x => { _player.ChangeArmUnit(NowWeapon); prevWeapon = NowWeapon; });
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
    [System.Serializable]
    public struct HpStruct
    {
        [Header("최대 체력")]
        public int MaxHp;
        [Header("현재 체력")]
        public int CurHp;
    }

    [System.Serializable]
    public struct AttackStruct
    {
        [Header("공격력")]
        public int AttackPower;
        [Header("공격 속도")]
        public float AttackSpeed;
        [Header("암슈트-파워 근접공격력")]
        public float[] PowerMeleeAttack;
        [Header("암슈트-파워 투척공격력")]
        public float[] PowerThrowAttack;
        [Header("암슈트-파워 특수공격력")]
        public float[] PowerSpecialAttack;
    }
    [System.Serializable]
    public struct StaminaStruct
    {
        [Header("최대 스테미나")]
        public float MaxStamina; // 최대 스테미나
        [Header("현재 스테미나")]
        public float CurStamina; // 현재 스테미나
        [Header("스테미나 초당 회복량")]
        public float RegainStamina; // 스테미나 초당 회복량
        [Header("스테미나 사용 후 쿨타임")]
        public float StaminaCoolTime; // 스테미나 소진 후 쿨타임
        [Header("스테미나 소모량 (?)")]
        public float ConsumesStamina; // 스테미나 소모량
        [Header("스테미나 최대 차지 시간")]
        public float MaxStaminaCharge;
        [Header("현재 스테미나 차지 시간")]
        public float CurStaminaCharge;
    }
    [System.Serializable]
    public struct JumpStruct
    {
        [Header("점프력")]
        public float JumpPower;
        [Header("점프 스테미나")]
        public int JumpStamina;
        [Header("더블 점프 스테미나")]
        public int DoubleJumpStamina;
        [Header("하강 공격 스테미나")]
        public int JumpDownStamina;
        [Header("최대 점프 횟수")]
        public int MaxJumpCount;
        [Header("현재 점프 횟수")]
        public int CurJumpCount;
    }
    [System.Serializable]
    public struct DashStruct
    {
        [Header("대쉬 속도")]
        public float DashDistance;
        [Header("대쉬 스테미나")]
        public int DashStamina;
    }
    [System.Serializable]
    public struct SpecialStruct
    {
        [Header("최대 마나")]
        public float MaxMana;
        [Header("현재 마나")]
        public float CurMana;
        [Header("투척 공격 당 마나 회복")]
        public float[] RegainMana; // 던지기 공격당 마나 회복
        [Header("마나 소모량")]
        public float[] ManaConsumption; // 마나 소모량
        [HideInInspector] public float SpecialChargeGage;
    }
    [System.Serializable]
    public struct DefenseStruct
    {
        [Header("방어력")]
        public int Defense;
        [Header("피해 감소량")]
        [Range(0, 100)] public float DamageReduction;
    }
    [System.Serializable]
    public struct CriticalStruct
    {
        [Header("크리티컬 확률")]
        [Range(0, 100)] public float CriticalChance;
        [Header("크리티컬 데미지")]
        public float CriticalDamage;
    }
    [System.Serializable]
    public struct ThrowStruct
    {
        [Header("최대 투척물 수")]
        public int MaxThrowables;
        [Header("현재 투척물 수")]
        public int CurThrowables;
        [Header("투척물 파밍시 추가 획득 수")]
        public float GainMoreThrowables;
        [Header("투척물 리스트")]
        public List<ThrowObjectData> ThrowObjectStack;
    }
    [System.Serializable]
    public struct AdditionalStruct
    {
        [Header("특수효과 리스트")]
        public List<AdditionalEffect> AdditionalEffects; // 특수효과 리스트
        [Header("적중 시 효과 리스트")]
        public List<HitAdditional> HitAdditionals;
        [Header("투척물 효과 리스트")]
        public List<ThrowAdditional> ThrowAdditionals; // 공격 방법 추가효과 리스트
        [Header("플레이어 자체 효과 리스트")]
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
        public GlobalGameData.AmWeapon NowWeapon;
        public float MoveSpeed;
        public float DrainLife;
        public float[] MeleeAttackStamina;
        public float EquipmentDropUpgrade;
        // 상태이상 지속시간
    }
    [SerializeField] private DataStruct Data;
    public float MoveSpeed { get { return Data.MoveSpeed * (1 + EquipStatus.Speed); } set { Data.MoveSpeed = value; } }
    // 체력
    public int MaxHp { get { return Data.Hp.MaxHp + (int)EquipStatus.HP; } set { Data.Hp.MaxHp = value; } }
    public int CurHp { get { return Data.Hp.CurHp + (int)EquipStatus.HP; } set { Data.Hp.CurHp = value; } }
    // 방어력
    public int Defense { get { return Data.Defense.Defense + (int)EquipStatus.Defense; } set { Data.Defense.Defense = value; } }
    public float DamageReduction { get { return Data.Defense.DamageReduction; } set { Data.Defense.DamageReduction = value; } }

    public float DrainLife { get { return Data.DrainLife; } set { Data.DrainLife = value; } }

    // 스테미나
    public float MaxStamina { get { return Data.Stamina.MaxStamina + EquipStatus.Stemina; } set { Data.Stamina.MaxStamina = value; } }
    public float CurStamina { get { return Data.Stamina.CurStamina; } set { Data.Stamina.CurStamina = value; } }
    public float RegainStamina { get { return Data.Stamina.RegainStamina; } set { Data.Stamina.RegainStamina = value; } }
    public float StaminaCoolTime { get { return Data.Stamina.StaminaCoolTime; } set { Data.Stamina.StaminaCoolTime = value; } }
    public float ConsumesStamina { get { return Data.Stamina.ConsumesStamina; } set { Data.Stamina.ConsumesStamina = value; } }
    public float MaxStaminaCharge { get { return Data.Stamina.MaxStaminaCharge; } set { Data.Stamina.MaxStaminaCharge = value; } }
    public float CurStaminaCharge { get { return Data.Stamina.CurStaminaCharge; } set { Data.Stamina.CurStaminaCharge = value; } }
    // 특수공격
    public float MaxMana { get { return Data.Special.MaxMana + EquipStatus.Mana; } set { Data.Special.MaxMana = value; } }
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
    public int AttackPower
    {
        get
        {
            return Data.Attack.AttackPower + (int)EquipStatus.Damage;
        }
        set
        {

            Data.Attack.AttackPower = value;
        }
    }
    public float AttackSpeed { get { return Data.Attack.AttackSpeed * (1 + EquipStatus.AttackSpeed); } set { Data.Attack.AttackSpeed = value; } }
    public float[] PowerMeleeAttack { get { return Data.Attack.PowerMeleeAttack; } set { Data.Attack.PowerMeleeAttack = value; } }
    public float[] PowerThrowAttack { get { return Data.Attack.PowerThrowAttack; } set { Data.Attack.PowerThrowAttack = value; } }
    public float[] PowerSpecialAttack { get { return Data.Attack.PowerSpecialAttack; } set { Data.Attack.PowerSpecialAttack = value; } }
    // 크리티컬
    public float CriticalChance { get { return Data.Critical.CriticalChance + EquipStatus.Critical; } set { Data.Critical.CriticalChance = value; } }
    public float CriticalDamage { get { return Data.Critical.CriticalDamage; } set { Data.Critical.CriticalDamage = value; } }
    // 투척오브젝트
    public int MaxThrowables { get { return Data.Throw.MaxThrowables; } set { Data.Throw.MaxThrowables = value; } }
    public int CurThrowables { get { return Data.Throw.CurThrowables; } set { Data.Throw.CurThrowables = value; } }
    public float GainMoreThrowables { get { return Data.Throw.GainMoreThrowables; } set { Data.Throw.GainMoreThrowables = value; } }
    public List<ThrowObjectData> ThrowObjectStack { get { return Data.Throw.ThrowObjectStack; } set { Data.Throw.ThrowObjectStack = value; } }
    // 암유닛
    public GlobalGameData.AmWeapon NowWeapon { get { return Data.NowWeapon; } set { Data.NowWeapon = value; } }
    //추가효과
    public List<AdditionalEffect> AdditionalEffects { get { return Data.Additional.AdditionalEffects; } set { Data.Additional.AdditionalEffects = value; } }
    public List<HitAdditional> HitAdditionals { get { return Data.Additional.HitAdditionals; } set { Data.Additional.HitAdditionals = value; } }
    public List<ThrowAdditional> ThrowAdditionals { get { return Data.Additional.ThrowAdditionals; } set { Data.Additional.ThrowAdditionals = value; } } // 공격 방법 추가효과 리스트
    public List<PlayerAdditional> PlayerAdditionals { get { return Data.Additional.PlayerAdditionals; } set { Data.Additional.PlayerAdditionals = value; } } // 플레이어 추가효과 리스트
    public float[] MeleeAttackStamina { get { return Data.MeleeAttackStamina; } set { Data.MeleeAttackStamina = value; } }
    public float EquipmentDropUpgrade { get { return Data.EquipmentDropUpgrade + (100 * EquipStatus.EquipRate); } set { Data.EquipmentDropUpgrade = value; } }

    [HideInInspector] public bool IsDead;
    [System.Serializable]
    public struct InventoryStruct
    {
       public EquipmentInventory Inventory;
       public InventoryMain InventoryMain;
       public GameObject BlueChipChoice;
       public BlueChipPanel BlueChipPanel;
    }
    public InventoryStruct Inventory;

    public EquipmentEffect EquipStatus => Inventory.Inventory.CurrentEquipmentEffect;
    public void CopyGlobalPlayerData(GlobalPlayerStateData globalData, GlobalGameData gameData)
    {
        Data.Hp.MaxHp = (int)globalData.maxHp;
        Data.Hp.CurHp = (int)globalData.maxHp;
        Data.Attack.AttackPower = (int)globalData.commonAttack;

        Data.Attack.PowerMeleeAttack = new float[globalData.shortRangeAttack.Length];
        Data.Attack.PowerMeleeAttack[0] = (int)globalData.shortRangeAttack[0];
        Data.Attack.PowerMeleeAttack[1] = (int)globalData.shortRangeAttack[1];
        Data.Attack.PowerMeleeAttack[2] = (int)globalData.shortRangeAttack[2];

        Data.Attack.PowerThrowAttack = new float[globalData.longRangeAttack.Length];
        Data.Attack.PowerThrowAttack[0] = (int)globalData.longRangeAttack[0];
        Data.Attack.PowerThrowAttack[1] = (int)globalData.longRangeAttack[1];
        Data.Attack.PowerThrowAttack[2] = (int)globalData.longRangeAttack[2];
        Data.Attack.PowerThrowAttack[3] = (int)globalData.longRangeAttack[3];

        Data.Attack.PowerSpecialAttack = new float[globalData.specialAttack.Length];
        Data.Attack.PowerSpecialAttack[0] = (int)globalData.specialAttack[0];
        Data.Attack.PowerSpecialAttack[1] = (int)globalData.specialAttack[1];
        Data.Attack.PowerSpecialAttack[2] = (int)globalData.specialAttack[2];
        Data.Attack.AttackSpeed = globalData.attackSpeed;
        Data.MoveSpeed = globalData.movementSpeed;
        Data.Critical.CriticalChance = globalData.criticalChance;
        Data.Defense.Defense = (int)globalData.defense;
        Data.EquipmentDropUpgrade = globalData.equipmentDropUpgrade;
        Data.DrainLife = globalData.drainLife;
        Data.Stamina.MaxStamina = globalData.maxStamina;
        Data.Stamina.RegainStamina = globalData.regainStamina;
        Data.Stamina.ConsumesStamina = globalData.consumesStamina;

        Data.Special.RegainMana = new float[globalData.regainMana.Length];
        Data.Special.RegainMana[0] = globalData.regainMana[0];
        Data.Special.RegainMana[1] = globalData.regainMana[1];
        Data.Special.RegainMana[2] = globalData.regainMana[2];
        Data.Special.RegainMana[3] = globalData.regainMana[3];

        Data.Special.ManaConsumption = new float[globalData.manaConsumption.Length];
        Data.Special.ManaConsumption[0] = globalData.manaConsumption[0];
        Data.Special.ManaConsumption[1] = globalData.manaConsumption[1];
        Data.Special.ManaConsumption[2] = globalData.manaConsumption[2];
        Data.Throw.GainMoreThrowables = globalData.gainMoreThrowables;
        Data.Throw.MaxThrowables = (int)globalData.maxThrowables;
        Data.NowWeapon = gameData.nowWeapon;
        Data.Special.MaxMana = globalData.maxMana;
        Data.Jump.MaxJumpCount = (int)globalData.maxJumpCount;
        Data.Jump.JumpPower = globalData.jumpPower;
        Data.Jump.JumpStamina = (int)globalData.jumpConsumesStamina;
        Data.Jump.DoubleJumpStamina = (int)globalData.doubleJumpConsumesStamina;
        Data.Dash.DashDistance = globalData.dashDistance;
        Data.Dash.DashStamina = (int)globalData.dashConsumesStamina;

        Data.MeleeAttackStamina = new float[globalData.shortRangeAttackStamina.Length];
        Data.MeleeAttackStamina[0] = globalData.shortRangeAttackStamina[0];
        Data.MeleeAttackStamina[1] = globalData.shortRangeAttackStamina[1];
        Data.MeleeAttackStamina[2] = globalData.shortRangeAttackStamina[2];
    }

    public void ClearAdditional()
    {
        AdditionalEffects.Clear();
        HitAdditionals.Clear();
        ThrowAdditionals.Clear();
        PlayerAdditionals.Clear();
    }
}
