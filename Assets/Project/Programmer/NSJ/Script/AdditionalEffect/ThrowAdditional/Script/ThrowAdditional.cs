using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAdditional : AdditionalEffect
{
    protected PlayerController Player;
    protected PlayerModel Model => Player.Model;

    protected ThrowObject _throwObject;

    public void Init(PlayerController player, AdditionalEffect addtional,ThrowObject throwObject)
    {
        Origin = addtional.Origin;
        Player = player;
        _throwObject = throwObject;
    }
}
