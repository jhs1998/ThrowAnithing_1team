using Assets.Project.Programmer.NSJ.RND.Script.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : BaseState
{
    PlayerController _player;

    public PlayerState(PlayerController controller)
    {
        _player = controller;
    }
}
