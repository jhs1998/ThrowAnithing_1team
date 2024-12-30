using UnityEngine;
using UnityEngine.Events;

public class HitAdditional : AdditionalEffect
{
    public IBattle Battle;
    protected Coroutine _debuffRoutine;
    [HideInInspector]public Transform transform;
    protected GameObject gameObject => transform.gameObject;
}
