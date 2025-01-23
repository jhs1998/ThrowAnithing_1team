using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Oblivion", menuName = "AdditionalEffect/Player/Oblivion")]
public class OblivionAddtional : PlayerAdditional
{
    [Header("Ȯ��(%)")]
    [Range(0, 100)]
    [SerializeField] private float _probability;
    public override void Trigger()
    {
        if (Player.CurState != PlayerController.State.SpecialAttack)
            return;

        RecoverMana();
    }

    public void RecoverMana()
    {
        // Ȯ�������� �� ������ŭ �ٽ� ȸ��
        if(Random.Range(0,100) <= _probability)
        {
            Model.CurMana += Model.ManaConsumption[Model.ChargeStep];
        }
    }
}
