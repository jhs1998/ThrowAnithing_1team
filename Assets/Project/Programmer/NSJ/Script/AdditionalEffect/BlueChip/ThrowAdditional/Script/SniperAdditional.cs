using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sniper", menuName = "AdditionalEffect/PrevThrow/Sniper")]
public class SniperAdditional : ThrowAdditional
{
    [Header("�ִ� ������ ������(%)")]
    [SerializeField] private float _maxIncreaseDamage;
    [Header("�ִ� ���������� �Ÿ�")]
    [SerializeField] private float _maxDistance;

    Vector3 _startPos;
    Vector3 _endPos;

    public override void Enter()
    {
        // �������� ĳ��
        _startPos = _throwObject.transform.position;
    }

    public override void Trigger()
    {
        // ���� �����Ҷ���
        if (_throwObject.CanAttack == false)
            return;
        // ������ ĳ��
        _endPos = _throwObject.transform.position;
        AmplifyDamage();
    }

    private void AmplifyDamage()
    {
        // ��������, ������ �Ÿ� ����
        float distance = Vector3.Distance(_startPos, _endPos);
        // �ִ�Ÿ��� �� ��� 1 �ʰ��� ��� 1�� ����
        float distancePerMax = distance / _maxDistance <= 1 ? distance / _maxDistance : 1 ;
        // �����ؾ��� ������ ���
        float increaseDamage = _maxIncreaseDamage * distancePerMax;
 
        _throwObject.DamageMultyPlier += increaseDamage;
    }
}
