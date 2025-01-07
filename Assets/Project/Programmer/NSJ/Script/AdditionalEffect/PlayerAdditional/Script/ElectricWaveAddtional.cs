using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElectricWave", menuName = "AdditionalEffect/Player/ElectricWave")]
public class ElectricWaveAddtional : PlayerAdditional
{
    [Header("발동 시간 간격")]
    [SerializeField] private float _intervalTime;
    [Header("데미지")]
    [SerializeField] private int _damage;
    [Header("범위")]
    [SerializeField] private float _range;
}
