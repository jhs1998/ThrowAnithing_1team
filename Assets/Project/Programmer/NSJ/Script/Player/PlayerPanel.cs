using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : BaseUI
{
    public TMP_Text ObjectCount => GetUI<TMP_Text>("ObjectCountText");

    public Slider HpBar => GetUI<Slider>("HpBar");
    public Slider StaminaBar => GetUI<Slider>("StaminaBar");
    public Slider MpBar => GetUI<Slider>("MpBar");
    public Slider ChargingMpBar => GetUI<Slider>("ChargingMpBar");
    private void Awake()
    {
        Bind();
    }

    public void BarValueController(Slider bar,float curValue,float maxValue)
    {
        float per;
        per = curValue / maxValue;
        bar.value = per;
    }
}
