using UnityEngine;
[CreateAssetMenu(fileName = "SplitThrowable", menuName = "AdditionalEffect/PrevThrow/SplitThrowable")]
public class SplitThrowableAdditional : ThrowAdditional
{

    [Header("���� ���ط�(%)")]
    [SerializeField] private float _reductionDamage;
    [Header("���� ������Ʈ ��� �п� ������Ʈ ũ��(%)")]
    [SerializeField] private float _decreaseSize;

    public override void Trigger()
    {
        // �����Ҽ������� ����ó��
        if (_throwObject.CanAttack == false)
            return;

        // Ŭ�е� ����ó��
        if (_throwObject.IsClone == true)
            return;


        SplitObject();
    }

    private void SplitObject()
    {

        TargetInfo nearTarget = FindNearTarget();


        // �ΰ� ����
        // �ϳ��� ���� �߻� �ϳ��� ������ �߻�
        for (int i = 0; i < 2; i++)
        {
            // ����
            ThrowObject newObject = Instantiate(DataContainer.GetThrowObject(_throwObject.Data.ID), _throwObject.transform.position, _throwObject.transform.rotation);
            // �� ����
            newObject.Init(Player, _throwObject.CCType, _throwObject.IsBoom,_throwObject.PlayerDamage,_throwObject.ThrowAdditionals);

            newObject.ObjectDamage = (int)(newObject.ObjectDamage * (_reductionDamage / 100f));
            newObject.PlayerDamage = (int)(newObject.PlayerDamage * (_reductionDamage / 100f));

            // ������ ����
            Vector3 newObjScale = newObject.transform.localScale;
            newObject.transform.localScale *= _decreaseSize / 100;
            // ���� ��� ����(���� ����� ��, �̹� ���� ��)
            newObject.IgnoreTargets.Add(nearTarget.transform.gameObject);

            // Ŭ�� ����
            newObject.IsClone = true;
            _throwObject.AddChainList(newObject);

            // �¿� �п� 
            float splitAngle = i == 0 ? 90 : -90;
            Vector3 newObjEuler = newObject.transform.eulerAngles;
            newObject.transform.rotation = Quaternion.Euler(newObjEuler.x, newObjEuler.y + splitAngle, newObjEuler.z);

            newObject.Shoot(Player.ThrowPower);

        }

    }
}
