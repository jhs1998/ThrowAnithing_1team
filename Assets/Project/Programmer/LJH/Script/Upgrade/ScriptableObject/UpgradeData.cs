using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade Data", menuName = "Scriptable Object/Upgrade Data", order = 0)]
public class UpgradeData : ScriptableObject
{
    [SerializeField]
    private string upgradeName;
    public string UpgradeName { get { return upgradeName; } }
    
    [SerializeField]
    private int price;
    public int Price { get { return price; } }
    
    [SerializeField]
    private string info;
    public string Info { get { return info; } }
}
