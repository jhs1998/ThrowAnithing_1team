using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : UpgradeBinding
{
    Button[,] slots;

    int ho = 0;
    int ver = 0;

    private void Awake()
    {
        Bind();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        
    }

    void Slot_Selected()
    {
        float x = Input.GetAxisRaw("Vertical");
        float y = Input.GetAxisRaw("Horizontal");

        ho += (int)x;
        ver +=  (int)y;

        if (ho == 0)
            if (x < 0)
                ho = 4;
    }

    void Init()
    {
        slots = new Button[5,4];

        slots[0, 0] = GetUI<Button>("MeleeAttack");
        slots[0, 1] = GetUI<Button>("RangeAttack");
        slots[0, 2] = GetUI<Button>("MoveSpeed");
        slots[0, 3] = GetUI<Button>("Health");

        slots[1, 0] = GetUI<Button>("Stamina");
        slots[1, 1] = GetUI<Button>("AttackSpeed");
        slots[1, 2] = GetUI<Button>("Critical");
        slots[1, 3] = GetUI<Button>("GetItem");

        slots[2, 0] = GetUI<Button>("Attack");
        slots[2, 1] = GetUI<Button>("HaveObj");
        slots[2, 2] = GetUI<Button>("Armor");
        slots[2, 3] = GetUI<Button>("MpRegen");

        slots[3, 0] = GetUI<Button>("StaminaLess");
        slots[3, 1] = GetUI<Button>("RangeAttack2");
        slots[3, 2] = GetUI<Button>("MeleeAttack2");
        slots[3, 3] = GetUI<Button>("GetObj");

        slots[4, 0] = GetUI<Button>("MpLess");
        slots[4, 1] = GetUI<Button>("BloodDrain");
        slots[4, 2] = GetUI<Button>("Armor2");
        slots[4, 3] = GetUI<Button>("GetItem2");


    }
}
