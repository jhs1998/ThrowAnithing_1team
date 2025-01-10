using UnityEngine;

[CreateAssetMenu(fileName = "BalanceArm", menuName = "Arm/Balance")]
public class BalanceArm : ArmUnit
{
    private bool _onFirstSpecial;
    public bool OnFirstSpecial
    {
        get { return _onFirstSpecial; }
        set
        {
            _onFirstSpecial = value;
            View.SetBool(PlayerView.Parameter.OnBuff, value);
        }
    }
}
