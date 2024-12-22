using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashBoom", menuName = "AdditionalEffect/Player/DashBoom")]
public class DashBoom : PlayerAdditional
{

    public override void Trigger()
    {
        if(_player.CurState == PlayerController.State.Dash)
        {
            Debug.Log("주변에 큰 데미지!");
        }
    }
}
