using System;
using UnityEngine;

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

    public double GetCurrentLevelEffect()
    {
        if(level == 0) return 0;
        return Math.Truncate(defaultEffect * Math.Pow(1.2, level - 1) + level - 1);
    }

    public double GetNextLevelEffect()
    {
        return Math.Truncate(defaultEffect * Math.Pow(1.2, level) + level);
    }

    public double GetNextLevelCost()
    {
        return Math.Truncate(defaultCost * Math.Pow(1.2, level));
    }


    public static UpgradeData GetTapUpgradeData()
    {
        return new UpgradeData("Tap Upgrade", 1e0, 1e1, 1);
    }

    public static UpgradeData[] GetSecUpgradeList()
    {
        var array = new UpgradeData[15];

        array[0] = new UpgradeData("Sec Upgrade A", 5e0, 1e1, 1);
        array[1] = new UpgradeData("Sec Upgrade B", 5e3, 1e4);
        array[2] = new UpgradeData("Sec Upgrade C", 5e6, 1e7);
        array[3] = new UpgradeData("Sec Upgrade D", 5e9, 1e10);
        array[4] = new UpgradeData("Sec Upgrade E", 5e12, 1e13);
        array[5] = new UpgradeData("Sec Upgrade F", 5e15, 1e16);
        array[6] = new UpgradeData("Sec Upgrade G", 5e18, 1e19);
        array[7] = new UpgradeData("Sec Upgrade H", 5e21, 1e22);
        array[8] = new UpgradeData("Sec Upgrade I", 5e24, 1e25);
        array[9] = new UpgradeData("Sec Upgrade J", 5e27, 1e28);
        array[10] = new UpgradeData("Sec Upgrade K", 5e30, 1e31);
        array[11] = new UpgradeData("Sec Upgrade L", 5e33, 1e34);
        array[12] = new UpgradeData("Sec Upgrade M", 5e36, 1e37);
        array[13] = new UpgradeData("Sec Upgrade N", 5e39, 1e40);
        array[14] = new UpgradeData("Sec Upgrade O", 5e42, 1e43);

        return array;
    }

}