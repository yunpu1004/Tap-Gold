using System;

// 도전과제의 목표치와 설명을 반환하는 메서드를 제공하는 클래스입니다.
public static class ChallengeManager
{
    // 소지 골드 도전과제의 레벨에 맞는 목표치를 반환합니다.
    public static double GetHoldGoldChallengeGoal(int level)
    {
        return 1000 * Math.Pow(1000, level);
    }

    // 탭당 골드 획득량 도전과제의 레벨에 맞는 목표치를 반환합니다.
    public static double GetGoldPerTapChallengeGoal(int level)
    {
        return 1000 * Math.Pow(1000, level);
    }

    // 초당 골드 획득량 도전과제의 레벨에 맞는 목표치를 반환합니다.
    public static double GetGoldPerSecChallengeGoal(int level)
    {
        return 1000 * Math.Pow(1000, level);
    }

    // 광고 시청 횟수 도전과제의 레벨에 맞는 목표치를 반환합니다.
    public static int GetAdCountChallengeGoal(int level)
    {
        return 3 * (level + 1);
    }

    // 플레이 시간 도전과제의 레벨에 맞는 목표치를 반환합니다.
    public static int GetPlayTimeChallengeGoal(int level)
    {
        return 3600 * (level + 1);
    }

    // 탭 횟수 도전과제의 레벨에 맞는 목표치를 반환합니다.
    public static int GetTapCountChallengeGoal(int level)
    {
        return 300 * (level + 1);
    }

    // 프레스티지 횟수 도전과제의 레벨에 맞는 목표치를 반환합니다.
    public static int GetPrestigeCountChallengeGoal(int level)
    {
        return 2 * (level + 1);
    }

    // 유물 획득 횟수 도전과제의 레벨에 맞는 목표치를 반환합니다.
    public static int GetArtifactCountChallengeGoal(int level)
    {
        return 2 * (level + 1);
    }

    // 소지 골드 도전과제의 설명을 반환합니다.
    public static string GetHoldGoldChallengeDesc(int level)
    {
        return $"Hold {DoubleUtil.DoubleToString(GetHoldGoldChallengeGoal(level))} gold";
    }

    // 탭당 골드 획득량 도전과제의 설명을 반환합니다.
    public static string GetGoldPerTapChallengeDesc(int level)
    {
        return $"Achieve {DoubleUtil.DoubleToString(GetGoldPerTapChallengeGoal(level))} gold per tap";
    }

    // 초당 골드 획득량 도전과제의 설명을 반환합니다.
    public static string GetGoldPerSecChallengeDesc(int level)
    {
        return $"Achieve {DoubleUtil.DoubleToString(GetGoldPerSecChallengeGoal(level))} gold per sec";
    }

    // 광고 시청 횟수 도전과제의 설명을 반환합니다.
    public static string GetAdCountChallengeDesc(int level)
    {
        return $"Watch {GetAdCountChallengeGoal(level)} ads";
    }

    // 플레이 시간 도전과제의 설명을 반환합니다.
    public static string GetPlayTimeChallengeDesc(int level)
    {
        return $"Play for {GetPlayTimeChallengeGoal(level)} seconds";
    }

    // 탭 횟수 도전과제의 설명을 반환합니다.
    public static string GetTapCountChallengeDesc(int level)
    {
        return $"Tap {GetTapCountChallengeGoal(level)} times";
    }

    // 프레스티지 횟수 도전과제의 설명을 반환합니다.
    public static string GetPrestigeCountChallengeDesc(int level)
    {
        return $"Prestige {GetPrestigeCountChallengeGoal(level)} times";
    }

    // 유물 획득 횟수 도전과제의 설명을 반환합니다.
    public static string GetArtifactCountChallengeDesc(int level)
    {
        return $"Acquire {GetArtifactCountChallengeGoal(level)} artifacts";
    }
}
