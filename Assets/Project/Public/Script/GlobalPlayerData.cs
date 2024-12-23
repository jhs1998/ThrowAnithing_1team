using System.Collections.Generic;

/// <summary>
/// 글로벌 플레이어 데이터
/// 담당자: 정현수
/// 사용 시 허락 맡으시오
/// </summary>

[System.Serializable]
public partial class GlobalPlayerData
{
    // 플레이어 이름
    public string playerName;
    // 플레이어 스탯
    public int maxHp;
    public int attackDamage;
    public int speed;
    public int luck;
    // 보유 재화
    public int coin;
    // 암 유닛 선택 종류 (Balance, _power, Speed)
    public enum AmWeapon { Balance, Power, Speed }
    // 날짜와 시간
    public string saveDateTime;
    // 로비 업그레이드 체크
    // 스테이지 라운드 별 등급 확률
}

