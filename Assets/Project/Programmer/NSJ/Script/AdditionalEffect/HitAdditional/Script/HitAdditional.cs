using UnityEngine;
using UnityEngine.Events;

public class HitAdditional : AdditionalEffect
{
    public IBattle Battle;
    public Transform transform;
    protected Coroutine _debuffRoutine;
    protected GameObject gameObject => transform.gameObject;
}
