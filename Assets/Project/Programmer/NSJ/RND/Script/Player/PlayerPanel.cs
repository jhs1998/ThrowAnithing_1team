using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPanel : BaseUI
{
    public TMP_Text ThrowCount => GetUI<TMP_Text>("ThrowCountText");

    private void Awake()
    {
        Bind();        
    }
}
