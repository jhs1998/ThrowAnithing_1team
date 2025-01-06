using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public enum DamageType { Default, PlayerHit ,Critical , Posion, Size }

public class DamageText : BaseUI
{
    public DamageType Type;
    [System.Serializable]
    struct ColorStruct
    {
        public Color Default;
        public Color PlayerHit;
        public Color Critical;
        public Color Poison;
    }
    [SerializeField] private ColorStruct TextColor;
    [System.Serializable]
    struct TextStruct
    {
        public float FontSize;
        public float Duration;
    }
    [SerializeField] private TextStruct _noCriticalText;
    [SerializeField] private TextStruct _criticalText;
    private Color[] _textColors = new Color[(int)DamageType.Size];
    private TMP_Text text => GetUI<TMP_Text>("DamageText");
    private Vector3 _targetPos;
    private bool _isCritical;
    Coroutine _gfxRoutine;
    private void Awake()
    {
        Bind();
        Init();
    }
    private void OnEnable()
    {
        if (_gfxRoutine == null)
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
    public void SetDamageText(int damage, Transform target, bool isPlayer)
    {
        text.SetText(damage.GetText());
        _targetPos = new Vector3(
            Random.Range(target.position.x - 0.5f, target.position.x + 0.5f),
            Random.Range(target.position.y - 0.5f, target.position.y + 0.5f),
            Random.Range(target.position.z - 0.5f, target.position.z + 0.5f)
            );

        SetTextColor(DamageType.Default, _isCritical, isPlayer);
    }
    /// <summary>
    /// 데미지 수치 설정
    /// </summary>
    /// <param name="damage"></param>
    public void SetDamageText(int damage, Transform target, DamageType type, bool isCritical, bool isPlayer)
    {
        text.SetText(damage.GetText());
        _targetPos = new Vector3(
            Random.Range(target.position.x - 0.5f, target.position.x + 0.5f),
            Random.Range(target.position.y - 0.5f, target.position.y + 0.5f),
            Random.Range(target.position.z - 0.5f, target.position.z + 0.5f)
            );
        _isCritical = isCritical;
        SetTextColor(type, _isCritical, isPlayer);
    }


    IEnumerator GFXRoutine()
    {
        yield return null;
        TextStruct textStruct = _isCritical == false ? _noCriticalText : _criticalText;

        if (_isCritical == true)
        {
            text.fontSize = textStruct.FontSize + 15;
            while (true)
            {
                text.fontSize += Time.deltaTime * 120;
                if (text.fontSize > textStruct.FontSize + 25)
                    break;
                yield return null;
            }
            while (true)
            {
                text.fontSize -= Time.deltaTime * 160;
                if (text.fontSize < textStruct.FontSize)
                    break;
                yield return null;
            }
        }
        else
        {
            text.fontSize = textStruct.FontSize;
        }

        StartCoroutine(MoveUpRoutine());
        yield return textStruct.Duration.GetDelay();

        float aValue = text.color.a;
        while (true)
        {
            aValue -= Time.deltaTime * 5;
            text.color = text.color.GetColor(aValue);
            if (aValue < 0)
                break;
            yield return null;
        }

        Destroy(gameObject);
    }

    IEnumerator MoveUpRoutine()
    {
        while (true)
        {
            _targetPos.y += Time.deltaTime / 2;
            yield return null;
        }
    }

    private void SetTextColor(DamageType type,bool isCritical ,bool isPlayer)
    {
        if (isPlayer == true)
        {
           if(type == DamageType.Default)
            {
                text.color = _textColors[(int)DamageType.PlayerHit];
                return;
            }
        }
        else
        {
            if(isCritical == true)
            {
                text.color = _textColors[(int)DamageType.Critical];
                return;
            }
        }
        text.color = _textColors[(int)type];
    }

    private void Init()
    {
        _textColors[(int)DamageType.Default] = TextColor.Default;
        _textColors[(int)DamageType.PlayerHit] = TextColor.PlayerHit;
        _textColors[(int)DamageType.Critical] = TextColor.Critical;
        _textColors[(int)DamageType.Posion] = TextColor.Poison;
    }
}
