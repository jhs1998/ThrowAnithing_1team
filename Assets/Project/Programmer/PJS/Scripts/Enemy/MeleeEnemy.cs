using Assets.Project.Programmer.NSJ.RND.Script;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    [SerializeField] CapsuleCollider attakArm;

    public void BeginAttack()
    {
        attakArm.isTrigger = true;
    }

    public void EndAttack()
    {
        attakArm.isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == Tag.Player)
        {
            IHit hit = other.transform.GetComponent<IHit>();
            hit.TakeDamage(Damage);
        }
    }
}
