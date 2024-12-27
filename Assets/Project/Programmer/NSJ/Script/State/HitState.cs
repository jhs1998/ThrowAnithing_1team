using UnityEngine;

public class HitState : PlayerState
{
    public HitState(PlayerController controller) : base(controller)
    {
        Player.OnPlayerHitEvent += TakeDamage;
    }


    public override void Enter()
    {
        // 움직임 멈춤
        Rb.velocity = new Vector3(0, Rb.velocity.y, 0);

        // 플레이어 무적상태
        Player.IsInvincible = true;

        View.SetTrigger(PlayerView.Parameter.Hit);
    }

    public override void Exit()
    {
        // 플레이어 무적 해제
        Player.IsInvincible = false;

        if (Player.IsGround == true)
        {
            // 점프 중이었을때를 위한 조건 해제
            Player.IsJumpAttack = false;
            Player.IsDoubleJump = false;
        }
    }

    public override void EndAnimation()
    {
        ChangeState(PlayerController.State.Idle);
    }

    private void TakeDamage(int damage, bool isStun)
    {
        if (Player.IsInvincible == true)
            return;

        // 방어력 계산
        int finalDamage = damage - Model.Defense;
        // 받는 피해 감소 계산
        finalDamage = (int)(finalDamage * (1 - Model.DamageReduction/100f));
        // 0보다 작으면 값을 0으로 고정 시킴
        finalDamage = finalDamage <= 0 ? 0 : finalDamage;

        // 입은 데미지가 없을때 Hit 상태 안들어감
        if (finalDamage == 0)
            return;

        Model.CurHp -= finalDamage;


        if (Model.CurHp > 0)
        {
            // 경직 없음
            if (isStun == false)
                return;
            ChangeState(PlayerController.State.Hit);
        }
        else
        {
            // Die
            Player.Die();
        }
    }

}
