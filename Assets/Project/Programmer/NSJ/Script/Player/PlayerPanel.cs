using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : BaseUI
{
    public TMP_Text ThrowCount => GetUI<TMP_Text>("ThrowCountText");
    public Slider StaminaSlider => GetUI<Slider>("StaminaSlider"); 

    private void Awake()
    {
        Bind();        
    }
}
