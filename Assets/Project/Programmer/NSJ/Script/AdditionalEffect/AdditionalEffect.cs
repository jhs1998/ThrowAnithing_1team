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

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }

    public virtual void Trigger() { }

}
