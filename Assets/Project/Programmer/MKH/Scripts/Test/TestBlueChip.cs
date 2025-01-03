using TMPro;
using UnityEngine;

public class TestBlueChip : MonoBehaviour
{
    [SerializeField] public AdditionalEffect Effect;
    [SerializeField] TMP_Text nameText;

    Vector3 pos = new Vector3(0, 1, 0);
    private float range = 0.2f;
    private float speed = 2f;

    private void Start()
    {
        nameText.SetText(Effect.Name.GetText());
        pos = transform.position;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + 180, 0);


        Vector3 v = pos;
        v.y += range * Mathf.Sin(Time.time * speed);
        transform.position = v;

    }

    public void SetBlueChip(AdditionalEffect additional)
    {
        Effect = additional;
        nameText.SetText(Effect.Name.GetText());
    }


}
