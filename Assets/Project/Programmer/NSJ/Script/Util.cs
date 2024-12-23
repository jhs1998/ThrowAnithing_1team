using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.Hardware;
using UnityEngine;


public static partial class Util
{
    public static StringBuilder Sb = new StringBuilder();

    private static Dictionary<float, WaitForSeconds> _delayDic = new Dictionary<float, WaitForSeconds>();

    /// <summary>
    /// 코루틴 딜레이 WaitForSeconds 가져오기
    /// </summary>
    public static WaitForSeconds GetDelay(this float delay)
    {
        if(_delayDic.ContainsKey(delay) == false)
        {
            _delayDic.Add(delay, new WaitForSeconds(delay));
        }
        
        return _delayDic[delay];
    }

    /// <summary>
    /// TMP를 위한 StringBuilder 반환 함수
    /// </summary>
    public static StringBuilder GetText<T>(this T value)
    {
        Sb.Clear();
        Sb.Append(value);
        return Sb;
    }

}
