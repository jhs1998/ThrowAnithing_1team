using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerBinder : BaseBinder
{
    public GameObject[] PowerMeleeEffect;

    private void Awake()
    {
        Bind();
        InitField();
    }

    private void InitField()
    {
        PowerMeleeEffect = new GameObject[3];
        PowerMeleeEffect[0] = GetObject("PowerMelee0");
        PowerMeleeEffect[1] = GetObject("PowerMelee1");
        PowerMeleeEffect[2] = GetObject("PowerMelee2");
    }
}
