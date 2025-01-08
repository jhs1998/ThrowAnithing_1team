using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "AdditionalEffect/Hit/Poison")]
public class PoizonAdditional : HitAdditional
{
    [System.Serializable]
    struct EffectStrcut
    {
        public GameObject EffectPrefab;
        [HideInInspector] public ParticleSystem Effect;
        public float EffectDuration;
    }
    [Range(0,1)]public float DamageMultiplier;
    [SerializeField] EffectStrcut _effect;

    public override void Enter()
    {
        Debug.Log($"{gameObject.name} µ¶");

        if (_debuffRoutine == null)
        {
            CreateEffect();
            _debuffRoutine = CoroutineHandler.StartRoutine(PoisonRoutine());
        }
    }

    public override void Exit()
    {
        if (_debuffRoutine != null)
        {
            CoroutineHandler.StopRoutine(_debuffRoutine);
            _debuffRoutine = null;
        }
    }

    IEnumerator PoisonRoutine()
    {
        _remainDuration = Duration;
        int damage = (int)(Battle.Debuff.MaxHp * 0.05f);
        while (_remainDuration > 0)
        {
            yield return 1f.GetDelay();
            CoroutineHandler.StartRoutine(EffectRoutine());
            Battle.TakeDamage(damage, CrowdControlType.None, true);
            _remainDuration--;         
        }
        Battle.EndDebuff(this);
    }
    
    private void CreateEffect()
    {
        _effect.Effect = Instantiate(_effect.EffectPrefab, transform).GetComponent<ParticleSystem>();
        _effect.Effect.transform.position = Battle.HitPoint.position;
        _effect.Effect.Stop();
    }
    IEnumerator EffectRoutine()
    {
        _effect.Effect.Play();
        yield return _effect.EffectDuration.GetDelay();
        _effect.Effect.Stop();
        if (_remainDuration <= 0)
        {
            Destroy(_effect.Effect, 0.2f);
        }
    }
}
