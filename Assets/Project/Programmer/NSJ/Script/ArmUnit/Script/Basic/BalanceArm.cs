using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "BalanceArm" ,menuName = "Arm/Balance")]
public class BalanceArm : ArmUnit
{
    [System.Serializable]
    struct InitStruct
    { 
        [Header("대쉬 스테미나(%)-파워모드가 기본")]
        public float DashStamina;
        [Header("대쉬 거리(%)")]
         public float DashDistance;
        [Header("드레인 스테미나(%)")]
        public float DrainStamina;
        [Header("드레인 거리(%)")]
        public float DrainDistance;
        [Header("최대 마나")]
        public float MaxMana;
    }
    [SerializeField] private InitStruct _init;

    public override void Init(PlayerController player)
    {
        base.Init(player);
        Model.DashStamina = (int)(Model.GlobalStateData.dashConsumesStamina * (_init.DashStamina / 100f));
        Model.DashDistance = (int)(Model.GlobalStateData.dashDistance * (_init.DashDistance / 100f));

        Model.DrainDistance = Model.Drain.Default.DrainDistance * (_init.DrainDistance /100f);
        Model.DrainStamina = Model.Drain.Default.DrainStamina * (_init.DrainStamina / 100f);

        Model.MaxMana = Model.GlobalStateData.maxMana + _init.MaxMana - Model.GlobalStateData.maxMana;


        Model.NowWeapon = GlobalGameData.AmWeapon.Balance;

        InitAllType();
    }
}
