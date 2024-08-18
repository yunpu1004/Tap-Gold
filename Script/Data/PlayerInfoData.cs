
using System;

// 플레이어의 각종 기록에 대한 데이터를 나타내는 클래스입니다
// 플레이어의 데이터를 이루는 하나의 요소로, PlayerData에 포함되어 저장됩니다
[Serializable]
public class PlayerInfoData
{
    public double highestDefaultGoldPerTap;
    public double highestDefaultGoldPerSec;
    public double highestGoldHeld;
    public int adCount;
    public int tapCount;
    public int prestigeCount;
    public long totalPlayTimeInSecond;
    public long lastGameStartTimeInSecond;
    public long lastGameExitTimeInSecond;
    public long unplayedTimeInSecond;

    public PlayerInfoData()
    {
        lastGameStartTimeInSecond = DateTimeUtil.GetTotalSecondsFromEarliestTime();
        lastGameExitTimeInSecond = DateTimeUtil.GetTotalSecondsFromEarliestTime();
        highestDefaultGoldPerTap = UpgradeManager.GetSecUpgradeEffect(0, 0);
        highestDefaultGoldPerSec = UpgradeManager.GetTapUpgradeEffect(1);
    }

    public void CopyFrom(PlayerInfoData data)
    {
        highestDefaultGoldPerTap = data.highestDefaultGoldPerTap;
        highestDefaultGoldPerSec = data.highestDefaultGoldPerSec;
        highestGoldHeld = data.highestGoldHeld;
        adCount = data.adCount;
        tapCount = data.tapCount;
        prestigeCount = data.prestigeCount;
        totalPlayTimeInSecond = data.totalPlayTimeInSecond;
        lastGameStartTimeInSecond = data.lastGameStartTimeInSecond;
        lastGameExitTimeInSecond = data.lastGameExitTimeInSecond;
        unplayedTimeInSecond = data.unplayedTimeInSecond;
    }
}