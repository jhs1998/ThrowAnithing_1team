using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CushionAttack", menuName = "AdditionalEffect/PrevThrow/CushionAttack")]
public class CushionAttack : ThrowAdditional
{

    [Header("ƨ�� Ƚ��")]
    [SerializeField] int _maxCushionCount;
    [Header("ƨ�� �Ÿ�")]
    [SerializeField] float _CushionDistance;
    [Range(0, 100)]
    [Header("������ ���ҷ� ( % )")]
    [SerializeField] float _ReductionDamage;
    [Space(10)]
    [SerializeField] private int _cushionCount;

    private List<TargetInfo> _targetList = new List<TargetInfo>();
    private TargetInfo _target;
    public override void Trigger()
    {
        if (_throwObject.CanAttack == false)
            return;
        // �ִ� ƨ�� Ƚ���� �� Ŭ���� �۵�
        if (_cushionCount < _maxCushionCount - 1)
        {
            ProcessCushionAttack();
        }
    }

    private void ProcessCushionAttack()
    {
        _targetList.Clear();
        int hitCount = Physics.OverlapSphereNonAlloc(_throwObject.transform.position, _CushionDistance, Player.OverLapColliders, 1 << Layer.Monster, QueryTriggerInteraction.Ignore);

        // �ֺ��� ���Ͱ� �ִ��� ��ĵ
        for (int i = 0; i < hitCount; i++)
        {
            Transform targetTransform = Player.OverLapColliders[i].transform;

            // ���ǿ� �����ϸ� �ش� Ÿ���� �Ÿ��� �Բ� ����
            TargetInfo targetInfo = SetTargetInfo(targetTransform, Vector3.Distance(_throwObject.transform.position, targetTransform.position));

            _targetList.Add(targetInfo);
        }

        // ��ĵ�� �����ߴٸ� �ٷ� ����
        if (_targetList.Count <= 1)
            return;

        // Ÿ���� �Ÿ������� ����
        _targetList.Sort((a, b) => a.Distance.CompareTo(b.Distance));
        // ���� ����� ��(���� �� ����)�� Ÿ������ ����
        _target = _targetList[1];
        _cushionCount++;
        // �ش� ���������� ����
        _throwObject.IgnoreTargets.Clear();
        _throwObject.IgnoreTargets.Add(_targetList[0].transform.gameObject);
        //ThrowObject newObject = Instantiate(_throwObject, _throwObject.transform.position, _throwObject.transform.rotation);
        //_throwObject = newObject;

        ThrowObject newObject = Instantiate(DataContainer.GetThrowObject(_throwObject.Data.ID), _throwObject.transform.position, _throwObject.transform.rotation);
        newObject.Init(Player, _throwObject.CCType, _throwObject.IsBoom,_throwObject.PlayerDamage, _throwObject.ThrowAdditionals);
        newObject.IgnoreTargets = _throwObject.IgnoreTargets;

        // Ŭ�� ����
        newObject.IsClone = true;
        _throwObject.AddChainList(newObject);
        

        // ������ ����
        newObject.ObjectDamage = (int)(newObject.ObjectDamage * (_ReductionDamage / 100f));
        newObject.PlayerDamage = (int)(newObject.PlayerDamage * (_ReductionDamage / 100f));

        // �� ��ġ Ž��
        // ����
        Vector3 targetPos = new(_target.transform.position.x, _target.transform.position.y + 1.5f, _target.transform.position.z);
        newObject.transform.LookAt(targetPos);
        newObject.Shoot(Player.ThrowPower);
    }
}
