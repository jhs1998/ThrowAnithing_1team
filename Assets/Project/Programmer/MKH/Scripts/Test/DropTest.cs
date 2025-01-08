using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace MKH
{
    public class DropTest : MonoBehaviour
    {
        public DropList dropList;
        private GameObject dropPrefab;
        public GameObject particle;
        public GameObject particle1;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Drop());
            }
        }

        private void DropItem()
        {
            dropPrefab = dropList.itemList[Random.Range(0, dropList.Count)];
            GameObject obj = Instantiate(dropPrefab, transform.position, Quaternion.identity);
            Destroy(obj, 10f);
        }

        private void Pat()
        {
            GameObject particles = Instantiate(particle, transform.position, Quaternion.Euler(-90f, 0, 0));
            Destroy(particles, 0.5f);
        }

        private void End()
        {
            GameObject particles = Instantiate(particle1, transform.position, Quaternion.Euler(-90f, 0, 0));
            Destroy(particles, 1f);
        }

        IEnumerator Drop()
        {
            Pat();
            yield return new WaitForSeconds(0.2f);
            DropItem();
            yield return new WaitForSeconds(10f);
            if (dropPrefab != null)
            {
                End();
            }
        }
    }

}
