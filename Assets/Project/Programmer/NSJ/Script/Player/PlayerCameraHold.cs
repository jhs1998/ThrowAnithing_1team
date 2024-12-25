using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class PlayerCameraHold : MonoBehaviour
{
    [SerializeField] GameObject _targetEffect;
    [Range(0,360)][SerializeField] float _angle;
    [SerializeField] float _detectRange;
    private PlayerController _player;

    [System.Serializable]
    struct TargetInfo
    {
        public Transform Target;
        public float Distance;
    }

    [SerializeField] private List<TargetInfo> _targetList = new List<TargetInfo>();
    [SerializeField]private Transform _target;
    private void Awake()
    {
        _player = GetComponentInParent<PlayerController>();
        //transform.SetParent(null);
    }

    private void OnEnable()
    {
        SetTargetList();
    }

    private void OnDisable()
    {
        _targetList.Clear();
        _target = null;
        _player.IsTargetHolding = false;
        _player.IsTargetToggle = false;
    }

    private void Update()
    {
        // 타겟을 찾지 못했을때
        if (_targetList.Count <= 0)
            return;

        // 타겟이 죽었을때 (Destroy되거나 Disable됬을때)
        if (_target == null || _target.gameObject.activeSelf == false)
        {
            // 리스트를 비운 후, 다시 전방의 적을 재탐색한다
            _targetList.Clear();
            SetTargetList();
        }
        else
        {
            // 플레이어의 타겟 지점을 지정한 타겟위치로 지정
            _player.TargetPos = _target.transform.position;
            // 타겟 이펙트효과 타겟위치
            _targetEffect.transform.position = _target.position;
        }
    }

    /// <summary>
    /// 적 타겟팅
    /// </summary>
    private void SetTargetList()
    {
        // 플레이어가 앞을 바라봄
        _player.LookAtCameraFoward();
        
        // 주변에 몬스터가 있는지 스캔
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _detectRange, _player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            Transform targetTransform = _player.OverLapColliders[i].transform;
            // 2. 각도 내에 있는지 확인
            Vector3 source = transform.position;
            source.y = 0;
            Vector3 destination = targetTransform.position;
            destination.y = 0;

            Vector3 targetDir = (destination - source).normalized;
            float targetAngle = Vector3.Angle(transform.forward, targetDir); // 아크코사인 필요 (느리다)
            if (targetAngle > _angle * 0.5f)
                continue;

            // 조건에 부합하면 해당 타겟을 거리와 함께 저장
            TargetInfo targetInfo = SetTargetInfo(targetTransform, Vector3.Distance(transform.position, targetTransform.position));
            _targetList.Add(targetInfo);
        }
        // 스캔에 실패했다면 바로 꺼짐
        if (_targetList.Count <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        // 타겟을 거리순으로 정렬
        _targetList.Sort((a, b) => a.Distance.CompareTo(b.Distance));
        // 가장 가까운 적을 타겟으로 지정
        _target = _targetList[0].Target;
    }

    //TargetInfo 설정
    private TargetInfo SetTargetInfo(Transform target, float distance)
    {
        TargetInfo info = new TargetInfo();
        info.Target = target;
        info.Distance = distance;
        return info;
    }
}
