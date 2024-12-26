using UnityEngine;

public class BossEnemy : BaseEnemy
{
    [SerializeField] BossEnemyState bossState;

    public void FootStep()
    {
        Debug.Log("FootSteop()");
    }

    public void OnHitBegin()
    {
        Debug.Log("OnHitBegin()");
    }

    public void OnHitEnd()
    {
        Debug.Log("OnHitEnd()");
    }

    public void ThunderStomp()
    {
        Debug.Log("ThunderStomp()");
    }

    public void ShakingForAni()
    {
        Debug.Log("ShakingForAni()");
    }
}
