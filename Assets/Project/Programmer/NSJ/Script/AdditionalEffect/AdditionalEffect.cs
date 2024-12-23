using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalEffect : ScriptableObject
{
    public AdditionalEffect Origin;
    public enum Type { Throw, Hit, Player }

    public Type AdditionalType;

    [Space(20)]
    public string Name;
    [TextArea]
    public string Description;

    protected bool _isTriggerFirst;

    /// <summary>
    /// 적용 시 호출
    /// </summary>
    public virtual void Enter() { }
    /// <summary>
    /// 해제 시 호출
    /// </summary>
    public virtual void Exit() { }
    /// <summary>
    /// 프레임마다 호출
    /// </summary>
    public virtual void Update() { }
    /// <summary>
    /// 0.02초마다 호출
    /// </summary>
    public virtual void FixedUpdate() { }
    /// <summary>
    /// 트리거 됬을 때만 호출
    /// </summary>
    public virtual void Trigger() { }
    /// <summary>
    /// 트리거 됬을 때 첫번째만 호출
    /// </summary>
    public virtual void TriggerFirst() { }

}
