using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Result : BaseUI
{
    [Inject]
    PlayerData playerData;

    //�÷��� Ÿ��
    TMP_Text timer;
    // Ŭ���� ���� ��
    TMP_Text round;
    // ���� ���� ��
    TMP_Text kill;
    // ȹ���� ����Ʈ ����Ʈ
    TMP_Text rp;
    private void Awake()
    {
        //Bind();
        //InitInputManager();
    }

    void Update()
    {
        if (InputKey.GetButtonDown(InputKey.Interaction))
            LoadingToBase.LoadScene(SceneName.LobbyScene);
    }


    void Init()
    {
        // ���� �� ���� ä������
        // ������Ÿ�Կ��� ����, ���� ����� �� ������ ���ܵΰ���
        timer = GetUI<TMP_Text>("Timer");
        round = GetUI<TMP_Text>("StageText");
        kill = GetUI<TMP_Text>("KillText");
        rp = GetUI<TMP_Text>("RpText");

    }
}
