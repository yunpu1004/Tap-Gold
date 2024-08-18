using System;

// 이 클래스는 탭 또는 초당 골드 획득 업그레이드에 관련된 메서드를 제공합니다.
public static class UpgradeManager
{
    // 탭 업그레이드의 비용을 반환합니다.
    public static double GetTapUpgradeCost(int currentLevel)
    {
        return 10 * Math.Pow(1.25, currentLevel);
    } 

    // 탭 업그레이드의 효과를 반환합니다.
    public static double GetTapUpgradeEffect(int currentLevel)
    {
        if(currentLevel == 0) return 0;
        return Math.Pow(1.24, currentLevel) + currentLevel - 1;
    }

    // 초당 골드 획득량 업그레이드의 비용을 반환합니다.
    public static double GetSecUpgradeCost(int upgradeIndex, int currentLevel)
    {
        return 10 * Math.Pow(1000, upgradeIndex) * Math.Pow(1.25, currentLevel);
    }

    // 초당 골드 획득량 업그레이드의 효과를 반환합니다.
    public static double GetSecUpgradeEffect(int upgradeIndex, int currentLevel)
    {
        if(currentLevel == 0) return 0;
        return Math.Pow(1000, upgradeIndex) * Math.Pow(1.24, currentLevel) + currentLevel;
    }
}
