using UnityEngine;
using UnityEngine.SceneManagement;

public class LSH_Teleport : MonoBehaviour
{
    int travelNum;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Portal")
        {
            travelNum = other.GetComponent<PortalSceneNumber>().SceneNum;                        
            SceneManager.LoadScene(travelNum);
        }
        
        
    }

    public void ChangeScene(int SceneNum)
    {
        SceneManager.LoadScene("Test02_Trap");
    }

}
