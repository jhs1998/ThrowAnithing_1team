using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdditional : AdditionalEffect
{
    protected PlayerController _player;
    protected PlayerModel _model => _player.Model;

    public void Init(PlayerController player, AdditionalEffect addtional)
    {
        Origin = addtional.Origin;
        _player = player;
    }
}
