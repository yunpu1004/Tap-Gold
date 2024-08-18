using System;

// 플레이어의 스테이터스(골드, 프레스티지 포인트, 스킬 레벨 등)에 대한 데이터를 나타내는 클래스입니다
// 플레이어의 데이터를 이루는 하나의 요소로, PlayerData에 포함되어 저장됩니다
[Serializable]
public class PlayerStatusData 
{
    public double gold;
    public int prestigePoint;
    public bool[] artifactAcquired;

    public double defaultGoldPerTap;
    public int tapUpgradeLevel;

    public double defaultGoldPerSec;
    public int[] secUpgradeLevels;

    public int autoTapSkillLevel;
    public int autoTapSkillCooltime;
    public int autoTapSkillDuration;
    public int bonusGoldPerTapSkillLevel;
    public int bonusGoldPerTapSkillCooltime;
    public int bonusGoldPerTapSkillDuration;
    public int bonusGoldPerSecSkillLevel;
    public int bonusGoldPerSecSkillCooltime;
    public int bonusGoldPerSecSkillDuration;
    public int fastCooldownSkillLevel;
    public int fastCooldownSkillCooltime;

    public int tapBuffDuration;
    public int secBuffDuration;

    public PlayerStatusData()
    {
        artifactAcquired = new bool[15];
        defaultGoldPerTap = UpgradeManager.GetTapUpgradeEffect(1);
        defaultGoldPerSec = UpgradeManager.GetSecUpgradeEffect(0, 0);
        tapUpgradeLevel = 1;
        secUpgradeLevels = new int[15];
    }

    public void CopyFrom(PlayerStatusData data)
    {
        gold = data.gold;
        prestigePoint = data.prestigePoint;
        Array.Copy(data.artifactAcquired, artifactAcquired, artifactAcquired.Length);
        defaultGoldPerTap = data.defaultGoldPerTap;
        tapUpgradeLevel = data.tapUpgradeLevel;
        defaultGoldPerSec = data.defaultGoldPerSec;
        Array.Copy(data.secUpgradeLevels, secUpgradeLevels, secUpgradeLevels.Length);
        autoTapSkillLevel = data.autoTapSkillLevel;
        autoTapSkillCooltime = data.autoTapSkillCooltime;
        autoTapSkillDuration = data.autoTapSkillDuration;
        bonusGoldPerTapSkillLevel = data.bonusGoldPerTapSkillLevel;
        bonusGoldPerTapSkillCooltime = data.bonusGoldPerTapSkillCooltime;
        bonusGoldPerTapSkillDuration = data.bonusGoldPerTapSkillDuration;
        bonusGoldPerSecSkillLevel = data.bonusGoldPerSecSkillLevel;
        bonusGoldPerSecSkillCooltime = data.bonusGoldPerSecSkillCooltime;
        bonusGoldPerSecSkillDuration = data.bonusGoldPerSecSkillDuration;
        fastCooldownSkillLevel = data.fastCooldownSkillLevel;
        fastCooldownSkillCooltime = data.fastCooldownSkillCooltime;
        tapBuffDuration = data.tapBuffDuration;
        secBuffDuration = data.secBuffDuration;
    }

}