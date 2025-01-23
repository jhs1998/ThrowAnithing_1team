using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class RadiantPoint : MonoBehaviour
{
    [Inject]
    GlobalGameData gameData;

    [SerializeField] TMP_Text radiantPointText;

    private void Update()
    {
        RadiantPoints();
    }

    private void RadiantPoints()
    {
        radiantPointText.text = $"����Ʈ ����Ʈ\n{gameData.coin.ToString()}";
    }

}
