
public interface IHit
{
    int TakeDamage(int damage, bool isIgnoreDef);

    void TakeCrowdControl(CrowdControlType type);
}
