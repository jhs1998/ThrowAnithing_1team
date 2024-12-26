using UnityEngine;
using UnityEngine.SceneManagement;

public class LSH_Teleport : MonoBehaviour
{
    SceneField travelName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Portal)
        {
            travelName = other.GetComponent<PortalSceneNumber>().SceneName;                        
            SceneManager.LoadScene(travelName);
        }
        
        
    }

    public void ChangeScene(SceneField SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

}
