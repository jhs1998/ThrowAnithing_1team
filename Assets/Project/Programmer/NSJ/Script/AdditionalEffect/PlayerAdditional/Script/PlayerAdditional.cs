using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdditional : AdditionalEffect
{
    protected PlayerController Player;
    protected Transform transform => Player.transform;
    protected Rigidbody Rb => Player.Rb;
    protected PlayerModel Model => Player.Model;

    public void Init(PlayerController player, AdditionalEffect addtional)
    {
        Origin = addtional.Origin;
        Player = player;
    }
}
