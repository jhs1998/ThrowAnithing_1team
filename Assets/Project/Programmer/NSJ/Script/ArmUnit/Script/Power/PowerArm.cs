using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerArm", menuName = "Arm/Power")]
public class PowerArm : ArmUnit
{
    public override void Init(PlayerController player)
    {
        base.Init(player);
        Model.NowWeapon = GlobalGameData.AmWeapon.Power;

        Model.DashStamina = (int)(Model.GlobalStateData.dashConsumesStamina);
        Model.DashDistance = (int)(Model.GlobalStateData.dashDistance);

        Model.DrainDistance = Model.Drain.Default.DrainDistance;
        Model.DrainStamina = Model.Drain.Default.DrainStamina;
    }
}
