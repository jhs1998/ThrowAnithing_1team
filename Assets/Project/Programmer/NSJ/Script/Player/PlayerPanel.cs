using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : BaseUI
{
    public TMP_Text ThrowCount => GetUI<TMP_Text>("ThrowCountText");
    public Slider StaminaSlider => GetUI<Slider>("StaminaSlider");
    public Slider SpecialGageSlider => GetUI<Slider>("SpecialGageSlider");
    public Slider SpecialChargeSlider => GetUI<Slider>("SpecialChargeSlider");
    private void Awake()
    {
        Bind();        
    }
}
