using System;
using System.Collections.Generic;

/// <summary>
/// 글로벌 플레이어 스텟 데이터
/// 담당자: 정현수
/// 사용 시 허락 맡으시오
/// </summary>

[System.Serializable]
public class GlobalPlayerStateData
{
    // GlobalGameData의 로비 특성 강화 단계 확인 후 플레이어의 스탯 변동
    // 굳이 json으로 저장할 필요 없이 업그레이드 단계를 저장하여 그에 따라 스탯 적용
    // 로비 업그레이드 로직은 따로 제작

    // 플레이어 스탯
    // 최대 체력 
    public int maxHp;
    // 근거리 공격력
    public int shortRangeAttack;
    // 원거리 공격력
    public int longRangeAttack;
    // 공격 속도
    public int attackSpeed;
    // 이동 속도
    public int movementSpeed;
    // 크리티컬 확률
    public int criticalChance;
    // 방어력 
    public int defense;
    // 장비 획득 확률
    public int equipmentDrop;
    // 생명력 흡수
    public int luck;
    //  공속 이속 크확 방어력 체력 장비 획득
    // 생명력 흡수 스테미나 최대치 증가 스테 회복 증가 
    // 마나 회복 증가 마나 소모량 감소 스테 소모 감소
    // 투척물 추가 획득 확률 증가, 보유 투척물 제한 증가
    // 암 유닛 선택 종류 (Balance, _power, Speed)
    public enum AmWeapon { Balance, Power, Speed }
}
