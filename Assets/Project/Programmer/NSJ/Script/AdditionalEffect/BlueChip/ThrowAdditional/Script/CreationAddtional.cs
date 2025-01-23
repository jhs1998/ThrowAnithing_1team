using UnityEngine;

[CreateAssetMenu(fileName = "Creation", menuName = "AdditionalEffect/Player/Creation")]
public class CreationAddtional : PlayerAdditional
{

    [Header("Ȯ��(%)")]
    [Range(0, 100)]
    [SerializeField] private float _probability;

    private int _prevThrowables;
    public override void Enter()
    {
        _prevThrowables = Model.CurThrowables;
    }
    public override void Update()
    {
        // ĳ���� ���ں��� ���� ���� �� Ŭ��
        if (_prevThrowables < Model.CurThrowables)
        {
            ProcessCreation();
        }

        _prevThrowables = Model.CurThrowables;

    }


    private void ProcessCreation()
    {
        // ���� �Ĺַ��� �ִ�ġ �̻��϶�
        if (Model.CurThrowables >= Model.MaxThrowables)
        {
            _prevThrowables = Model.CurThrowables;
            return;
        }
        int randomNum = Random.Range(0, 100);
        // Ȯ�������� �߰��Ĺ�
        if (randomNum <= _probability)
        {
            Player.AddThrowObject(Model.PeekThrowObject());
            _prevThrowables = Model.CurThrowables + 1;
        }
    }
}
