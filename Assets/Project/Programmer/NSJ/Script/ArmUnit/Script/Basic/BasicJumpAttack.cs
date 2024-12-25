using UnityEngine;

[CreateAssetMenu(fileName = "Basic JumpAttack", menuName = "Arm/AttackType/Basic/JumpAttack")]
public class BasicJumpAttack : ArmJumpAttack
{
    [Header("플레이어가 허공에서 살짝 튀어오르는 정도")]
    [SerializeField] private float _popValue;
    [Header("하단 공격 각도")]
    [SerializeField] private float _downAngle;
    public override void Enter()
    {
        Debug.Log("점프어택 시작");
        // 애니메이션 실행
        View.SetTrigger(PlayerView.Parameter.JumpAttack);

    }
    public override void Exit()
    {
        Player.LookAtCameraFoward();
    }

    public override void OnTrigger()
    {
        // 캐릭터 살짝 튀어오르기

        // 입력한 방향으로 플레이어 방향 전환
        Player.LookAtMoveDir();
        // 플레이어 물리량 전환
        Rb.velocity = new Vector3(Rb.velocity.x, 0, Rb.velocity.z);

        if (Player.MoveDir != Vector3.zero)
            Player.ChangeVelocityPlayerFoward();

        // 플레이어 카메라 방향 바라보기
        Player.LookAtCameraFoward();
        Rb.AddForce(Vector3.up * _popValue, ForceMode.Impulse);
        ThrowObject();
    }
    public override void EndAnimation()
    {
        ChangeState(PlayerController.State.Fall);
    }

    private void ThrowObject()
    {
        //int throwObjectID = Model.ThrowObjectStack.Count > 0 ? Model.PopThrowObject().ID : 0;
        Quaternion _muzzleRot = Quaternion.Euler(_muzzlePoint.eulerAngles.x + _downAngle, _muzzlePoint.eulerAngles.y, _muzzlePoint.eulerAngles.z);
        ThrowObject throwObject = Player.InstantiateObject(DataContainer.GetThrowObject(0), _muzzlePoint.position, _muzzleRot);
        throwObject.Init(Player, Model.HitAdditionals, Model.ThrowAdditionals);
        throwObject.Shoot(Player.ThrowPower);
        throwObject.TriggerFirstThrowAddtional();
    }
}
