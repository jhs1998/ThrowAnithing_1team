using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddtionalEffect : ScriptableObject
{
    public AddtionalEffect Origin;
    public enum Type { Throw, Hit, Player }

    public Type AdditionalType;
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }

    public virtual void Trigger() { }

}
