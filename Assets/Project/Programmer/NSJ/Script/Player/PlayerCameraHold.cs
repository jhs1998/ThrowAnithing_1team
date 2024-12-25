using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class PlayerCameraHold : MonoBehaviour
{
    [SerializeField] GameObject _targetEffect;
    [SerializeField] float _angle;
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
    }

    private void Update()
    {
        if (_targetList.Count <= 0)
            return;

        if (_target == null || _target.gameObject.activeSelf == false)
        {
            _targetList.Clear();
            SetTargetList();
        }
        _player.CamareArm.LookAt(_target);
        _targetEffect.transform.position = _target.position;
    }

    /// <summary>
    /// 적 타겟팅
    /// </summary>
    private void SetTargetList()
    {
        _player.LookAtCameraFoward();

        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, 10f, _player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            // 2. 각도 내에 있는지 확인
            Vector3 source = transform.position;
            source.y = 0;
            Vector3 destination = _player.OverLapColliders[i].transform.position;
            destination.y = 0;

            Vector3 targetDir = (destination - source).normalized;
            float targetAngle = Vector3.Angle(transform.forward, targetDir); // 아크코사인 필요 (느리다)
            if (targetAngle > _angle * 0.5f)
                continue;

            TargetInfo targetInfo = SetTargetInfo(_player.OverLapColliders[i].transform, Vector3.Distance(transform.position, _player.OverLapColliders[i].transform.position));
            _targetList.Add(targetInfo);
        }
        if (_targetList.Count <= 0)
        {
            _player.IsHolding = false;
            gameObject.SetActive(false);
            return;
        }
          
        _targetList.Sort((a, b) => a.Distance.CompareTo(b.Distance));
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
