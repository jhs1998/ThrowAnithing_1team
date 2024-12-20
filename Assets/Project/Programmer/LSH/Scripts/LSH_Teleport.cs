using UnityEngine;
using UnityEngine.SceneManagement;

public class LSH_Teleport : MonoBehaviour
{
    int travelNum;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Portal")
        {
            Debug.Log("1.닿긴하니?");
            travelNum = other.GetComponent<PortalSceneNumber>().SceneNum;                        
            Debug.Log(travelNum);
            SceneManager.LoadScene(travelNum);
        }
        //플레이어가 닿는 애들마다 이름 조사할수도 없고
        //닿으면 걔가 갖고있는 정보 int값을 가져와서
        //그 씬으로 넘어가게 하면 좋을거같은데
        //하긴 지금은 플레이어가 넘기든 다른 물체가 넘기든
        //주도권과 상관없으니까 누가 넘겨도 상관없겠구나

        
    }

    //이걸 포탈쪽 숫자랑 어떻게 연결하지?????
    public void ChangeScene(int SceneNum)
    {
        SceneManager.LoadScene("Test02_Trap");
    }

}
