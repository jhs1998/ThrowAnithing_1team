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
        // 체력의 의한 패턴 변경
        // 1페이즈 - 일렉트릭 아머
        // 2페이즈 - 레이지 스톰
        Debug.Log("ThunderStomp()");
    }

    public void DieEff()
    {
        Debug.Log("DieEff()");
    }

    public void ShakingForAni()
    {
        Debug.Log("ShakingForAni()");
    }

    public void Shooting()
    {
        Debug.Log("Shooting()");
    }
}
