// 스킬의 효과와 지속시간을 반환하는 메서드를 제공하는 클래스입니다.
public static class SkillManager
{
    // 자동 탭의 효과와 지속시간을 반환합니다.
    public static (int tapPerSec, int duration) GetAutoTapEffect(int level)
    {
        return (level, level + 10);
    }

    // 탭당 골드 획득량 증가 효과와 지속시간을 반환합니다.
    public static (float multiplier, int duration) GetBonusGoldPerTapEffect(int level)
    {
        return (0.1f * level, level + 10);
    }

    // 초당 골드 획득량 증가 효과와 지속시간을 반환합니다.
    public static (float multiplier, int duration) GetBonusGoldPerSecEffect(int level)
    {
        return (0.1f * level, level + 10);
    }

    // 쿨타임 감소 효과를 반환합니다.
    public static float GetFastCooldownEffect(int level)
    {
        return 0.01f * level;
    }
}
