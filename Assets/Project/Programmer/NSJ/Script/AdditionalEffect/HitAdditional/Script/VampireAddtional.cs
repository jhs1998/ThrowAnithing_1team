using UnityEngine;

[CreateAssetMenu(fileName = "Vampire", menuName = "AdditionalEffect/Hit/Vampire")]
public class VampireAddtional : HitAdditional
{
    [Header("피해 흡혈량(%)")]
    [SerializeField] private float _lifeDrainAmount;
    public override void Enter()
    {
        DrainLife();
        Battle.EndDebuff(this);
    }

    private void DrainLife()
    {
        if (_isCritical == false)
            return;

        int lifeAmount = (int)(_damage * (_lifeDrainAmount / 100));
        // TODO : 체력이 올라가는 기능이 필요함
        // 힐을 하게 하는 인터페이스가 또 필요할 듯
        Debug.Log($"{lifeAmount} 만큼 회복. 아직 기능 없음");
    }
}
