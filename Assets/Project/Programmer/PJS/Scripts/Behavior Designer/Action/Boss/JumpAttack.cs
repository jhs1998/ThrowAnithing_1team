using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class JumpAttack : Action
{
    public SharedGameObject player; // 플레이어
    public float dis;  // 뛸 떄 자신의 위치와 플레이어의 위치 차이
    public string testText;
    public Vector3 playerPos;   // 이동할 플레이어의 위치

    private BossEnemy enemy;

    public override void OnAwake()
    {
        enemy = GetComponent<BossEnemy>();
    }

    public override void OnStart()
    {
        enemy.SetPlayerPos(player.Value.transform.position);
    }

    public override TaskStatus OnUpdate()
    {
        dis = (transform.position - playerPos).sqrMagnitude;

        if (dis <= 0.1f)
        {
            testText = "적합";
            return TaskStatus.Success;
        }
        else
        {
            testText = "부적합";
            return TaskStatus.Running;
        }
    }
}