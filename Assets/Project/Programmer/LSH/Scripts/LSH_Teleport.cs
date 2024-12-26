using UnityEngine;
using UnityEngine.SceneManagement;

public class LSH_Teleport : MonoBehaviour
{
    SceneField travelNum;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Portal)
        {
            travelNum = other.GetComponent<PortalSceneNumber>().SceneName;                        
            SceneManager.LoadScene(travelNum);
        }
        
        
    }

    public void ChangeScene(SceneField SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

}
