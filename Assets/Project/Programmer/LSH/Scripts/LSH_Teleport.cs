using UnityEngine;
using UnityEngine.SceneManagement;

public class LSH_Teleport : MonoBehaviour
{
    SceneField nextStage; //스테이지 이동용 변수
    SceneField randomHiddenRoom; //비밀방 입장용 변수

    Transform beforeTeleportPos;
    Transform afterTeleportPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Portal)
        {
            nextStage = other.GetComponent<PortalSceneNumber>().nextScene;
            SceneManager.LoadScene(nextStage); //직접 씬 이동, LoadSceneMode.Single
        }
        
        if (other.tag == "PortalHidden")
        {
            //0함정 1몬스터 2블루칩
            randomHiddenRoom = other.GetComponent<PortalSceneNumber>().hiddenSceneArr[Random.Range(0,2)];
            ChangeScene(randomHiddenRoom); //멀티 씬 로딩, LoadSceneMode.Additive
        }

    }

    public void ChangeScene(SceneField SceneName)
    {
        //플레이어 기존씬 위치 저장
        beforeTeleportPos = this.transform;

        //애디티브로 씬 열기
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);

        //플레이어 기존씬에서 애디티브 씬으로 위치 이동
        Vector3.MoveTowards(transform.position, beforeTeleportPos.position, 1f);
    }

}
