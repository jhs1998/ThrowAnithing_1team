using BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSceneNumber : MonoBehaviour
{
    [SerializeField] public SceneField nextScene;
    [SerializeField] public SceneField[] hiddenSceneArr;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == Tag.Player)
        {
            StartCoroutine(DeleteRoutine());
        }   
    }

    IEnumerator DeleteRoutine()
    {
        yield return 3f.GetDelay();
        Destroy(gameObject);
    }
}
