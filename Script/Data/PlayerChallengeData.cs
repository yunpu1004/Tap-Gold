
using System;

// 플레이어의 도전과제 달성 레벨에 대한 데이터를 나타내는 클래스입니다
// 플레이어의 데이터를 이루는 하나의 요소로, PlayerData에 포함되어 저장됩니다
[Serializable]
public class PlayerChallengeData 
{
    public int holdGoldChallengeLevel;
    public int goldPerTapChallengeLevel;
    public int goldPerSecChallengeLevel;
    public int adCountChallengeLevel;
    public int playTimeChallengeLevel;
    public int tapCountChallengeLevel;
    public int prestigeCountChallengeLevel;
    public int artifactCountChallengeLevel;

    public void CopyFrom(PlayerChallengeData data)
    {
        holdGoldChallengeLevel = data.holdGoldChallengeLevel;
        goldPerTapChallengeLevel = data.goldPerTapChallengeLevel;
        goldPerSecChallengeLevel = data.goldPerSecChallengeLevel;
        adCountChallengeLevel = data.adCountChallengeLevel;
        playTimeChallengeLevel = data.playTimeChallengeLevel;
        tapCountChallengeLevel = data.tapCountChallengeLevel;
        prestigeCountChallengeLevel = data.prestigeCountChallengeLevel;
        artifactCountChallengeLevel = data.artifactCountChallengeLevel;
    }
}