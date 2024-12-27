using UnityEngine;
using UnityEngine.SceneManagement;

public class LSH_Teleport : MonoBehaviour
{
    SceneField nextStage; //스테이지 이동용 변수
    SceneField randomHiddenRoom; //비밀방 입장용 변수

    Vector3 beforeTeleportPos; //기존 씬에서 비밀방으로 텔포타기 전 위치
    Vector3 afterTeleportPos; //애디티브 씬으로 텔포탄 후 위치

    [SerializeField] bool isSceneAdditive; //T애디티브 씬이 열려있음, F아님


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "PortalHidden")
        {            
            //멀티 씬 로딩, LoadSceneMode.Additive
            randomHiddenRoom = other.GetComponent<PortalSceneNumber>().hiddenSceneArr[Random.Range(0, 2)];
            ChangeScene(randomHiddenRoom); //0함정 1몬스터 2블루칩
        }

        Debug.Log(0);   

        if (other.tag == Tag.Portal)
        {
            Debug.Log(1);   
            //다중씬 로딩중일때
            if (isSceneAdditive)
            {
                Debug.Log(2);   
                //플레이어 기존씬에 있던 자리로 돌려놓고
                transform.position = beforeTeleportPos;
                Debug.Log(3);   
                //변수 false로 바꾼 뒤
                isSceneAdditive = false;
                Debug.Log(4);   

                //열어둔 랜덤 씬을 저장한 뒤 언로드씬() 해줌
                Scene additiveScene = SceneManager.GetSceneByName(randomHiddenRoom.SceneName);
                Debug.Log(5);   
                SceneManager.UnloadScene(additiveScene);
                Debug.Log(6);   
                return;
                Debug.Log(7);   

            }
            //스테이지 이동할때
            else
            {
                Debug.Log(8);   

                //직접 씬 이동, LoadSceneMode.Single
                nextStage = other.GetComponent<PortalSceneNumber>().nextScene;
                Debug.Log(9);   
                SceneManager.LoadScene(nextStage);
                Debug.Log(10);   
                return;
                Debug.Log(11);   


            }
            Debug.Log(12);   
        }
        
        Debug.Log(13);   
        

    }

    public void ChangeScene(SceneField SceneName)
    {
        //애디티브로 다중 씬 열기
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        isSceneAdditive = true;

        //플레이어 기존씬 위치 저장
        beforeTeleportPos = transform.position;
        afterTeleportPos = new Vector3(100, 1, 100);

        //플레이어 기존씬에서 애디티브 씬으로 위치 이동
        transform.position = afterTeleportPos;

    }

}
