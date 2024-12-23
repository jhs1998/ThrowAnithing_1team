using UnityEngine;

public class DrainState : PlayerState
{
    private GameObject _drainField => Player.DrainField;
    private float _drainDistance => Model.DrainDistance * 2;

    private Vector3 _drainStartPos;

    public DrainState(PlayerController controller) : base(controller)
    {
        _drainField.SetActive(false);
    }

    public override void Enter()
    {
        Player.Rb.velocity = Vector3.zero;
        View.SetBool(PlayerView.Parameter.Drain, true);
        _drainField.SetActive(true);
        _drainField.transform.localScale = new Vector3(0, _drainField.transform.localScale.y, 0);

        // 플레이어 위치 고정시킬 좌표 캐싱
        _drainStartPos = transform.position;
    }

    public override void Update()
    {
        // 플레이어 위치 고정
        transform.position = _drainStartPos;

        CheckInput();

        // 크기가 점점 커지도록
        float curDistance = _drainField.transform.localScale.x;
        if (curDistance < _drainDistance)
        {
            _drainField.transform.localScale = new Vector3(
                curDistance + _drainDistance * Time.deltaTime,
                _drainField.transform.localScale.y,
                curDistance + _drainDistance * Time.deltaTime);
        }

    }

    public override void Exit()
    {
        View.SetBool(PlayerView.Parameter.Drain, false);
        Player.DrainField.SetActive(false);
    }
    public override void OnTrigger()
    {
        Player.DrainField.SetActive(false);
    }

    public override void EndAnimation()
    {
        ChangeState(PlayerController.State.Idle);
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, Model.DrainDistance);
    }

    private void CheckInput()
    {
        //드레인 키를 뗐을 때
        if (Input.GetKeyUp(KeyCode.Z))
        {
            View.SetBool(PlayerView.Parameter.Drain, false);
        }
    }
}
