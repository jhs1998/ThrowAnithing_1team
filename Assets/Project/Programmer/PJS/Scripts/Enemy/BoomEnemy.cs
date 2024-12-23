using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEnemy : BaseEnemy
{
    [SerializeField] List<GameObject> objs = new List<GameObject>();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, state.AttackDis);
            foreach (Collider collider in colliders)
            {
                Debug.Log(collider.gameObject);
                IHit hit = collider.transform.GetComponent<IHit>();
                if(hit != null)
                    hit.TakeDamage(0);
            }
        }
    }
}
