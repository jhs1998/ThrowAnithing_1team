using UnityEngine;
using UnityEngine.Events;

public class HitAdditional : AdditionalEffect
{
    public IBattle Battle;
    public float Duration;
    [HideInInspector]public float RemainDuraiton;
    protected Coroutine _debuffRoutine;
    [HideInInspector]public Transform transform;
    protected GameObject gameObject => transform.gameObject;
}
