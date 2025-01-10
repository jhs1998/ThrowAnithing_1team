using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class JumpAttack : Action
{
    public SharedGameObject player; // 플레이어
    public float dis;  // 뛸 떄 자신의 위치와 플레이어의 위치 차이

    private BossEnemy enemy;
    private Vector3 playerPos;   // 이동할 플레이어의 위치

    public override void OnAwake()
    {
        enemy = GetComponent<BossEnemy>();
    }

    public override void OnStart()
    {
        playerPos = player.Value.transform.position;
        enemy.SetPlayerPos(playerPos);
    }

    public override TaskStatus OnUpdate()
    {
        dis = (transform.position - playerPos).sqrMagnitude;

        return dis <= 0.1f ? TaskStatus.Success : TaskStatus.Running;
    }
}