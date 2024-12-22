using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdditional : AddtionalEffect
{
    protected PlayerController _player;
    protected PlayerModel _model => _player.Model;

    public void Init(PlayerController player, AddtionalEffect addtional)
    {
        Origin = addtional.Origin;
        _player = player;
    }
}
