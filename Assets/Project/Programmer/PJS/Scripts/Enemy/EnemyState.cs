using UnityEngine;

[System.Serializable]
public class EnemyState
{
    [Range(50, 300)] public int MaxHp;  // ü��
    [Range(0, 20)] public int Atk;       // ���ݷ�
    [Range(0, 10)] public float Def;    // ����
    [Range(0, 10)] public float Speed;    // �̵� �ӵ�
    [Range(0, 10)] public float AtkDelay;   // ���� �ӵ�
    [Range(0, 10)] public float AttackDis;  // ���� ��Ÿ�
    [Range(0, 10)] public float TraceDis;   // �ν� ��Ÿ�
}

[System.Serializable]
public class EliteEnemyState
{
    [Range(500, 1000)] public int MaxHp;  // ü��
    [Range(0, 20)] public int Atk;       // ���ݷ�
    [Range(0, 20)] public float Def;    // ����
    [Range(0, 10)] public float Speed;    // �̵� �ӵ�
    [Range(0, 10)] public float AtkDelay;   // ���� �ӵ�
    [Range(0, 10)] public float AttackDis;  // ���� ��Ÿ�
    [Range(0, 10)] public float TraceDis;   // �ν� ��Ÿ�
}

[System.Serializable]
public class BossEnemyState
{
    [Range(1000, 5000)] public int MaxHp;  // ü��
    [Range(10, 20)] public int Atk;       // ���ݷ�
    [Range(0, 10)] public float Def;    // ����
    [Range(0, 10)] public float Speed;    // �̵� �ӵ�
    [Range(0, 10)] public float AtkDelay;   // ���� �ӵ�
    [Range(0, 10)] public float AttackDis;  // ���� ��Ÿ�
    [Range(0, 10)] public float TraceDis;   // �ν� ��Ÿ�
}
