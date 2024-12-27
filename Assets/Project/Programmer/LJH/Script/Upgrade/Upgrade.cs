using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : UpgradeBinding
{
    Button[,] slots;
    Image[,] slotImages;

    int ho = 0;
    int ver = 0;

    Coroutine slotCo;
    Coroutine buttonCo;
    float inputDelay = 0.25f;

    //Comment : Infomation > name
    [SerializeField] TMP_Text itemName;
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemInfo;


    //Comment : for Test
    [SerializeField] int usedCost;

    int costLimit1 = 5000;
    int costLimit2 = 30000;
    int costLimit3 = 80000;
    int costLimit4 = 200000;

    // Comment : Cost Tier
    int tier;

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
        if (slotCo == null)
            slotCo = StartCoroutine(Slot_Selected());

        //Comment : For test
        if (Input.GetButtonDown("Interaction"))
        {
            slots[ver, ho].onClick.Invoke();
        }
    }

    //Comment : if usedCost greater than costLimit Method
    void TierCal()
    {
        tier = 1;
        if (usedCost > costLimit1)
            tier = 2;
        if (usedCost > costLimit2)
            tier = 3;
        if (usedCost > costLimit3)
            tier = 4;
        if (usedCost > costLimit4)
            tier = 5;
    }

    void SlotLimit()
    {
        TierCal();

        for (int i = tier; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                //Todo : Change Color
                slots[i, j].GetComponent<Image>().color = new(0.1f, 0, 0.2f);
            }
        }

    }


    //Comment : ½½·Ô ÀÌµ¿ ÇÔ¼ö
    IEnumerator Slot_Selected()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = -Input.GetAxisRaw("Vertical");

        ho += (int)x;
        ver += (int)y;

        if (ho == -1)
        {
            ho = 3;
        }
        if (ho == 4)
        {
            ho = 0;
        }
        if (ver == -1)
        {
            ver = tier - 1;
        }
        if (ver == tier)
        {
            ver = 0;
        }

        // Comment : Other Buttons color Reset
        ColorReset();
        // Comment : Selected Button color Changed

        slots[ver, ho].GetComponent<Image>().color = new(0.7f, 0.7f, 0.1f);

        itemName.text = slots[ver, ho].name;
        itemImage.sprite = slotImages[ver, ho].sprite;
        itemInfo.text = slots[ver, ho].name;

        SlotLimit();
        yield return inputDelay.GetDelay();
        slotCo = null;

    }


    // Comment : for Test
    public void Â¥ÀÜ()
    {
        Debug.Log("Â¥ÀÜ");


    }

    void ColorReset()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                slots[i, j].GetComponent<Image>().color = new(0.2f, 0.25f, 0.6f);
            }
        }
    }

    void Init()
    {
        slots = new Button[5, 4];
        slotImages = new Image[5, 4];

        slots[0, 0] = GetUI<Button>("MeleeAttack");
        slots[0, 1] = GetUI<Button>("RangeAttack");
        slots[0, 2] = GetUI<Button>("MovementSpeed");
        slots[0, 3] = GetUI<Button>("Health");

        slots[1, 0] = GetUI<Button>("Stamina");
        slots[1, 1] = GetUI<Button>("AttackSpeed");
        slots[1, 2] = GetUI<Button>("CriticalChance");
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

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                slotImages[i, j] = slots[i, j].transform.GetChild(0).GetComponent<Image>();
            }
        }
    }
}