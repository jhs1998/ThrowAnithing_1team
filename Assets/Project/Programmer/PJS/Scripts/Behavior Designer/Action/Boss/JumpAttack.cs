using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class JumpAttack : BossAction
{
    public SharedGameObject player; // �÷��̾�
    public float dis;  // �� �� �ڽ��� ��ġ�� �÷��̾��� ��ġ ����

    private Vector3 playerPos;   // �̵��� �÷��̾��� ��ġ

    public override void OnStart()
    {
        playerPos = player.Value.transform.position;
        bossEnemy.SetPlayerPos(playerPos);
    }

    public override TaskStatus OnUpdate()
    {
        dis = (transform.position - playerPos).sqrMagnitude;

        return dis <= 0.1f ? TaskStatus.Success : TaskStatus.Running;
    }
}