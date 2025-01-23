using BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class MinimapCamController : MonoBehaviour
{
    //��Ŀ�� ĳ����, ������Ʈ, ���� � �����տ� �ٿ��ְ� ���̾� ó��
    [Inject]
    OptionSetting option_setting;


    GameObject player;
    [SerializeField] GameObject minimap;

    [SerializeField] GameObject minimapActCheckBox;
    [SerializeField] GameObject minimapFixCheckBox;

    [SerializeField] bool minimapAct;
    [SerializeField] bool minimapFix;

    GameObject playerCamera;

    private void Start()
    {
        Init();
        gameObject.transform.parent = null;
    }
    private void Update()
    {
        CamPos();
        CamRot();
        CamActivated();
    }

    // ���ξ��� �ھƾ���
    void CamPos()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, 10, player.transform.position.z);
    }

    // ȯ�� �������� �̴ϸ� �Ƚ��� ���������� ȣ��
    void CamRot()
    {
        minimapFix = option_setting.miniMapFixBool;

        if(!minimapFix)
        transform.eulerAngles = new Vector3(90, player.transform.eulerAngles.y, 0);

    }

    // ȯ�� �������� �̴ϸ� ��Ƽ����Ʈ ���������� ȣ��
    void CamActivated()
    {
        minimapAct = option_setting.miniMapOnBool;
        minimap.SetActive(minimapAct);
        
    }

    void Init()
    {
        player = GameObject.FindWithTag(Tag.Player);
        playerCamera = GameObject.FindWithTag(Tag.MainCamera);
    }
}
