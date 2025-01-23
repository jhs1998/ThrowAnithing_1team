using System.Collections.Generic;
using UnityEngine;

public class BoomEnemy : MeleeEnemy
{
    [Tooltip("�� ������ ��ƼŬ")]
    [SerializeField] ParticleSystem chargeParticle;

    public void BeginCharge()
    {
        chargeParticle.Play();
    }
}
