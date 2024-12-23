using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAdditional : AdditionalEffect
{
    protected PlayerController _player;
    protected PlayerModel _model => _player.Model;

    protected ThrowObject _throwObject;

    public void Init(PlayerController player, AdditionalEffect addtional,ThrowObject throwObject)
    {
        Origin = addtional.Origin;
        _player = player;
        _throwObject = throwObject;
    }
}
