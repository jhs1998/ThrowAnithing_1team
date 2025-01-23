using BehaviorDesigner.Runtime;

public struct BossSkillState
{
    public int damage;    // ���ݷ�
    public float range;   // ��Ÿ�
    public float coolTime;    // ��Ÿ��
    public SharedBool atkAble;    // ���ݰ��� ����
}

public struct GlobalState
{
    public SharedBool Able;      // ��� ����
    public SharedFloat coolTime; // ��Ÿ��
}
