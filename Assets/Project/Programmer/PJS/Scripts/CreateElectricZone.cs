using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateElectricZone : MonoBehaviour
{
    [SerializeField] int range;
    [SerializeField] GameObject electricZone;

    private void Start()
    {
        /*for (int i = 0; i < range; i++)
        {
            Vector3 dic = new Vector3(transform.position.x, transform.position.y, transform.position.z + i);
            GameObject obj = Instantiate(electricZone, dic, transform.rotation);
            obj.transform.parent = transform;
        }*/

        GameObject obj = Instantiate(electricZone, transform.position, transform.rotation);
        obj.transform.parent = transform;

        Destroy(gameObject, 3f);
    }
}
