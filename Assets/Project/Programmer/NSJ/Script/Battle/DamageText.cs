using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject.Asteroids;
public enum DamageType { Default }

public class DamageText : BaseUI
{
    private TMP_Text text => GetUI<TMP_Text>("DamageText");

    public DamageType Type;

    private Vector3 _targetPos;

    Coroutine _gfxRoutine;
    private void Awake()
    {
        Bind();
    }
    private void OnEnable()
    {        
        if(_gfxRoutine  == null)
        {
            _gfxRoutine = StartCoroutine(GFXRoutine());
        }
    }
    private void OnDisable()
    {
        if (_gfxRoutine != null)
        {
            StopCoroutine(_gfxRoutine);
            _gfxRoutine = null; 
        }
    }

    private void Update()
    {
        text.transform.position = Camera.main.WorldToScreenPoint(_targetPos);
    }
    private void OnDrawGizmos()
    {
       
    }
    /// <summary>
    /// 데미지 수치 설정
    /// </summary>
    /// <param name="damage"></param>
    public void SetDamageText(int damage, Transform target)
    {
        text.SetText(damage.GetText());
        _targetPos = target.position;
    }

    IEnumerator GFXRoutine()
    {
        text.fontSize = 35;
        while (true)
        {
            text.fontSize += Time.deltaTime * 120;
            if (text.fontSize > 50)
                break;
            yield return null;
        }
        while (true)
        {
            text.fontSize -= Time.deltaTime * 80;
            if (text.fontSize < 35)
                break;
            yield return null;
        }
        StartCoroutine(MoveUpRoutine());
        yield return 1f.GetDelay();

        float aValue= text.color.a; 
        while(true)
        {
            aValue -= Time.deltaTime * 3;
            text.color = text.color.GetColor(aValue);
            if (aValue < 0)
                break;
            yield return null;
        }
    }

    IEnumerator MoveUpRoutine()
    {
        while (true) 
        {
            _targetPos.y += Time.deltaTime / 2;
            yield return null;
        }
    }
}
