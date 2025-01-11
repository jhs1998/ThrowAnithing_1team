using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ForceThrow", menuName = "AdditionalEffect/Player/ForceThrow")]
public class ForceThrowAdditional : PlayerAdditional
{
    [SerializeField] DoubleDamageBuff _damageBuff;
    [Header("데미지 추가 배수(%)")]
    [SerializeField] float _damageMultiplier;
    [Header("공격 카운트")]
    [SerializeField] float _attackCount;

    private int _curCount;
    private bool _onBuff;
    public override void Enter()
    {
        _curCount = 1;
        // 새로운 데미지버프 인스턴스로 변경
        _damageBuff = Instantiate(_damageBuff);
        _damageBuff.DamageMultyplier = _damageMultiplier;


        Player.OnThrowObjectResult += CheckCount;
    }
    public override void Exit()
    {
        Player.OnThrowObjectResult -= CheckCount;
    }

    public override void Trigger()
    {
        if (CurState != PlayerController.State.ThrowAttack)
            return;

        // 버프 있을 때 던지면 버프 해제
        if (_onBuff == true)
        {       
            OffBuff();
        }
    }
    private void CheckCount(ThrowObject throwObject, bool hitSuccess)
    {
        // 맞았을 때
        if(hitSuccess == true)
        {
            // 체인된 것중 최초로 맞췄다면
            if(throwObject.IsChainHit == false)
            {
                // 카운트 상승
                if (_curCount < _attackCount)
                {
                    _curCount++;
                }
                // 공격카운트가 최대에 도달했고 버프가 없을 때
                if (_curCount == _attackCount&& _onBuff == false)
                {
                    OnBuff();
                }
            }
        }
    }

    private void OnBuff()
    {
        Player.AddAdditional(_damageBuff);
        _onBuff = true;
    }
    private void OffBuff()
    {
        // 1프레임 뒤에 버프 삭제
        CoroutineHandler.StartRoutine(OffBuffRoutine());
        _curCount = 0;
        _onBuff = false;
    }
    IEnumerator OffBuffRoutine()
    {
        yield return null;
        Player.RemoveAdditional(_damageBuff);  
    }
}
