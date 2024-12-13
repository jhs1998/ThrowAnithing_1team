using BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 코루틴 대리 실행 클래스
/// </summary>
public class CoroutineHandler : MonoBehaviour
{
    public static CoroutineHandler Instance;

    private void Awake()
    {
        InitSingleTon();
    }

    /// <summary>
    /// 코루틴 실행
    /// </summary>
    public static Coroutine StartRoutine(IEnumerator enumerator)
    {
        return Instance.StartCoroutine(enumerator);
    }

    /// <summary>
    /// 코루틴 중지
    /// </summary>
    public static void StopRoutine(IEnumerator enumerator)
    {
        Instance.StopCoroutine(enumerator);
    }



    private void InitSingleTon()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
