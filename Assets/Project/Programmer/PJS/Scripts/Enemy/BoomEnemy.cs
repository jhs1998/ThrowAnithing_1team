using UnityEngine;

public class BoomEnemy : MeleeEnemy
{
    [SerializeField] ParticleSystem chargeParticle;

    public void BeginCharge()
    {
        chargeParticle.Play();
    }
}
