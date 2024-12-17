using Assets.Project.Programmer.NSJ.RND.Script.State;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerState : BaseState
{
    protected PlayerController Player;

    protected GameObject gameobject;
    protected Transform transform;
    protected PlayerModel Model;
    protected PlayerView View;
    protected Rigidbody Rb;
    public PlayerState(PlayerController controller)
    {
        Player = controller;
        gameobject = controller.gameObject;
        transform = controller.transform;
        Model = controller.Model;
        View = controller.View;
        Rb = controller.Rb;
    }
}
