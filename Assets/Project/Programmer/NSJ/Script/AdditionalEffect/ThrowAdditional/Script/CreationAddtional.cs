using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "Creation", menuName = "AdditionalEffect/Player/Creation")]
public class CreationAddtional : PlayerAdditional
{
    [Header("확률(%)")]
    [Range(0, 100)]
    [SerializeField] private float _probability;

    private int _prevThrowables;
    public override void Enter()
    {
        _prevThrowables = Model.CurThrowables;
    }
    public override void Update()
    {
        // 캐싱한 숫자보다 모델의 숫자 더 클때
        if(_prevThrowables < Model.CurThrowables)
        {
            ProcessCreation();
        }
        else
        {
            _prevThrowables = Model.CurThrowables;
        }
    }


    private void ProcessCreation()
    {
        // 현재 파밍량이 최대치 이상일때
        if (Model.CurThrowables >= Model.MaxThrowables)
        {
            _prevThrowables = Model.CurThrowables;
            return;
        }

        // 확률적으로 추가파밍
        if (Random.Range(0,100) <= _probability)
        {
            Player.AddThrowObject(Model.PeekThrowObject());
            _prevThrowables = Model.CurThrowables + 1;
        }
    }
}   
