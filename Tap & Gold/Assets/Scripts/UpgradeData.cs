using System;
using UnityEngine;

/// 이 클래스는 탭당 또는 초당 골드 획득 업그레이드 데이터를 관리합니다.
[System.Serializable]
public class UpgradeData
{
    public string name;
    [SerializeField] private double defaultEffect;
    [SerializeField] private double defaultCost;
    public int level;

    public UpgradeData(string name, double defaultEffect, double defaultCost, int level = 0)
    {
        this.name = name;
        this.defaultCost = defaultCost;
        this.defaultEffect = defaultEffect;
        this.level = level;
    }


    /// 현재 레벨의 효과를 반환합니다.
    public double GetCurrentLevelEffect()
    {
        if(level == 0) return 0;
        return Math.Truncate(defaultEffect * Math.Pow(1.2, level - 1) + level - 1);
    }

    /// 다음 레벨의 효과를 반환합니다.
    public double GetNextLevelEffect()
    {
        return Math.Truncate(defaultEffect * Math.Pow(1.2, level) + level);
    }

    /// 다음 레벨의 비용을 반환합니다.
    public double GetNextLevelCost()
    {
        return Math.Truncate(defaultCost * Math.Pow(1.2, level));
    }

    /// 탭당 골드 획득 업그레이드 객체를 생성합니다.
    public static UpgradeData GetTapUpgradeData()
    {
        return new UpgradeData("Tap Upgrade", 1e0, 1e1, 1);
    }

    /// 초당 골드 획득 업그레이드 객체 리스트를 생성합니다.
    public static UpgradeData[] GetSecUpgradeList()
    {
        var array = new UpgradeData[15];

        for (int i = 0; i < array.Length; i++)
        {
            char upgradeLetter = (char)('A' + i);
            double effect = 5 * Math.Pow(10, i * 3);
            double cost = effect * 2;
            int level = (i == 0) ? 1 : 0;

            array[i] = new UpgradeData($"Sec Upgrade {upgradeLetter}", effect, cost, level);
        }

        return array;
    }

}