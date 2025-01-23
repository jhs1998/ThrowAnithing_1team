using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalEffect : ScriptableObject
{
    public Sprite Image;
    public AdditionalEffect Origin;
    public enum Type { Throw, Hit, Player }

    public Type AdditionalType;

    [Space(20)]
    public string Name;
    [TextArea]
    public string Description;


    /// <summary>
    /// ���� �� ȣ��
    /// </summary>
    public virtual void Enter() { }
    /// <summary>
    /// ���� �� ȣ��
    /// </summary>
    public virtual void Exit() { }
    /// <summary>
    /// ���� ���� �� ȣ��
    /// </summary>
    public virtual void EnterState() { }
    /// <summary>
    /// ���� ���� �� ȣ��
    /// </summary>
    public virtual void ExitState() { }
    /// <summary>
    /// �����Ӹ��� ȣ��
    /// </summary>
    public virtual void Update() { }

    public virtual void LateUpdate()
    {

    }

    /// <summary>
    /// 0.02�ʸ��� ȣ��
    /// </summary>
    public virtual void FixedUpdate() { }
    /// <summary>
    /// Ʈ���� ���� ���� ȣ��
    /// </summary>
    public virtual void Trigger() { }
}
