using BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MinimapCamController : MonoBehaviour
{
    //마커는 캐릭터, 오브젝트, 몬스터 등에 프리팹에 붙여넣고 레이어 처리

    [SerializeField] GameObject player;
    [SerializeField] GameObject minimap;

    [SerializeField] bool minimapAct;
    [SerializeField] bool minimapFix;
    private void Update()
    {
        CamPos();
        CamRot();
        CamActivated();
    }

    // 메인씬에 박아야함
    void CamPos()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, 10, player.transform.position.z);
    }

    // 환경 설정에서 미니맵 픽스드 누를때마다 호출
    void CamRot()
    {
        if(!minimapFix)
        transform.eulerAngles = new Vector3(90, player.transform.eulerAngles.y, 0);

    }

    // 환경 설정에서 미니맵 액티베이트 누를때마다 호출
    void CamActivated()
    {
        
        minimap.SetActive(minimapAct);
        
    }
}
