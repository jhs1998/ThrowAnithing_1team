using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public static partial class Tag
{
    public const string Untagged = "Untagged";
    public const string Respawn = "Respawn";
    public const string Finish = "Finish";
    public const string EditorOnly = "EditorOnly";
    public const string MainCamera = "MainCamara";
    public const string Player = "Player";
    public const string GameController = "GameController";
    public const string Monster = "Monster";
    public const string Item = "Item";
    public const string Portal = "Portal";
    public const string Trash = "Trash";
    public const string BlueChip = "BlueChip";
    public const string PortalHidden = "PortalHidden";
}
public static partial class Layer
{
    public static int Default => LayerEnum.Default.GetLayer();  
    public static int TransparentFX => LayerEnum.TransparentFX.GetLayer();
    public static int IgnoreRaycast => LayerEnum.IgnoreRaycast.GetLayer();
    public static int Water => LayerEnum.Water.GetLayer();
    public static int UI => LayerEnum.UI.GetLayer();
    public static int Wall => LayerEnum.Wall.GetLayer();
    public static int Player => LayerEnum.Player.GetLayer();
    public static int Monster => LayerEnum.Monster.GetLayer();
    public static int ThrowObject => LayerEnum.ThrowObject.GetLayer();
    public enum LayerEnum
    {
        Default,
        TransparentFX,
        IgnoreRaycast,
        Water,
        UI,
        Wall,
        Player,
        Monster,
        ThrowObject
    }

    private static Dictionary<LayerEnum, int> _layerDic = new Dictionary<LayerEnum, int>();

    public static int GetLayerMaskEveryThing()
    {
        int everyThing = 0;
        everyThing |= 1 << Default;
        everyThing |= 1 << TransparentFX;
        everyThing |= 1 << IgnoreRaycast;
        everyThing |= 1 << Water;
        everyThing |= 1 << UI;
        everyThing |= 1 << Wall;
        everyThing |= 1 << Player;
        everyThing |= 1 << Monster;
        everyThing |= 1 << ThrowObject;

        return everyThing;
    }
    private static int GetLayer(this LayerEnum layer)
    {
        if (_layerDic.ContainsKey(layer) == false)
        {
            _layerDic.Add(layer, LayerMask.NameToLayer($"{layer}"));
        }
        return _layerDic[layer];
    }


}
