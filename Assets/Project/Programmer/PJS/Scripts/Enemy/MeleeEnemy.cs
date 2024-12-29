using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using TMPro;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    [SerializeField] CapsuleCollider attakArm;

    private bool _canAttack;

    public void BeginAttack()
    {
        _canAttack = true;
    }

    public void EndAttack()
    {
        _canAttack = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == Tag.Player && _canAttack == true)
        {
            IHit hit = other.transform.GetComponent<IHit>();
            hit.TakeDamage(Damage, true);
        }
    }
}
