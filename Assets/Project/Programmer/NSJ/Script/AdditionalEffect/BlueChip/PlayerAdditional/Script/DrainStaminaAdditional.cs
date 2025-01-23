using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DrainStamina", menuName = "AdditionalEffect/Player/DrainStamina")]
public class DrainStaminaAdditional : PlayerAdditional
{
    [Header("���� ���׹̳�(%)")]
    [SerializeField] private float _decreaseStamina;

    // ���ҽ�Ų �� ĳ��
    private float _decreaseAmount;
    public override void Enter()
    {
        // ���ҽ�ų �Ҹ� ���׹̳��� ���
        _decreaseAmount = Model.DrainStamina * (_decreaseStamina / 100);
        // ����
        Model.DrainStamina -= _decreaseAmount;
    }
    public override void Exit()
    {
        // ���ҽ�Ų ��ŭ �ٽ� ����
        Model.DrainStamina += _decreaseAmount;
    }
}
