using System.Collections;
using UnityEngine;

public class DrainState : PlayerState
{
    private GameObject _drainField => Player.DrainField;
    private float _drainDistance => Model.DrainDistance * 2;

    private Vector3 _drainStartPos;
    private Collider[] _overlapColliders = new Collider[150];
    private Coroutine _drainRoutine;
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

        if (_drainRoutine == null)
        {
            _drainRoutine = CoroutineHandler.StartRoutine(DrainRoutine());
        }
    }
    private float _curDrainDistance;
    public override void Update()
    {
        // 플레이어 위치 고정
        transform.position = _drainStartPos;

        CheckInput();

        float curDistance = _drainField.transform.localScale.x;
        // 크기가 점점 커지도록
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
        if (_drainRoutine != null)
        {
            CoroutineHandler.StopRoutine(_drainRoutine);
            _drainRoutine = null;
        }

        View.SetBool(PlayerView.Parameter.Drain, false);
        Player.DrainField.SetActive(false);
    }
    public override void OnTrigger()
    {
        Player.DrainField.SetActive(false);
        if (_drainRoutine != null) 
        {
            CoroutineHandler.StopRoutine(_drainRoutine);
            _drainRoutine = null;
        }

        _curDrainDistance = 0;
    }

    public override void EndAnimation()
    {
        ChangeState(PlayerController.State.Idle);
    }

    IEnumerator DrainRoutine()
    {
        while (true)
        {
            _curDrainDistance += Time.deltaTime * 10;
            if (_curDrainDistance > Model.DrainDistance)
                _curDrainDistance = Model.DrainDistance;
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _curDrainDistance, _overlapColliders);
            for (int i = 0; i < hitCount; i++)
            {
                if (_overlapColliders[i].tag != Tag.Trash && _overlapColliders[i].tag != Tag.Item)
                    continue;

                _overlapColliders[i].transform.position = Vector3.MoveTowards(_overlapColliders[i].transform.position, transform.position, 5f * Time.deltaTime);
            }
            yield return null;
        }
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, _curDrainDistance);
    }


    private void CheckInput()
    {
        //드레인 키를 뗐을 때
        if (InputKey.GetButtonUp(InputKey.Drain))
        {
            View.SetBool(PlayerView.Parameter.Drain, false);
        }
    }
}
