using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerArm", menuName = "Arm/Power")]
public class PowerArm : ArmUnit
{
    [System.Serializable]
    struct InitStruct
    {
        [Header("최대 마나")]
        public float MaxMana;
    }
    [SerializeField] InitStruct _init;
    public override void Init(PlayerController player)
    {
        base.Init(player);
        Model.DashStamina = (int)(Model.GlobalStateData.dashConsumesStamina);
        Model.DashDistance = (int)(Model.GlobalStateData.dashDistance);

        Model.DrainDistance = Model.Drain.Default.DrainDistance;
        Model.DrainStamina = Model.Drain.Default.DrainStamina;

        Model.MaxMana = Model.GlobalStateData.maxMana + _init.MaxMana - Model.GlobalStateData.maxMana;


        Model.NowWeapon = GlobalGameData.AmWeapon.Power;

        InitAllType();
    }
}
