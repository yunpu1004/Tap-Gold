using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 플레이어의 데이터를 나타내는 클래스입니다
// 플레이어의 모든 데이터를 포함하고 있으며, 저장 및 로드가 가능합니다 (PlayerDataLoader.cs 참고)
[Serializable]
public class PlayerData
{
    public PlayerStatusData status;
    public PlayerInfoData info;
    public PlayerChallengeData challenge;

    public const int buffMultiplier = 10;
    public const int maxBuffDuration = 30;
    public const int maxSkillCooltime = 60;
    public const int gachaPrestigePointCost = 10;
    public const int skillLevelUpPresitgePointCost = 2;
    public const float returnRewardMultiplier = 1f;
    public const int minSecondsForReturnReward = 300;
    public const int challengePrestigePointReward = 2;


    public PlayerData()
    {
        status = new PlayerStatusData();
        info = new PlayerInfoData();
        challenge = new PlayerChallengeData();
    }
    

    public void CopyFrom(PlayerData playerData)
    {
        status.CopyFrom(playerData.status);
        info.CopyFrom(playerData.info);
        challenge.CopyFrom(playerData.challenge);
    }


    // 플레이어의 실제 탭당 골드 획득량을 반환합니다
    public double GetGoldPerTap()
    {
        double result = status.defaultGoldPerTap;

        float multiplier = 1; // 기본 텝당 골드 획득량에 곱해지는 배수 (스킬 및 아티팩트 효과)
        double plusGold = 0;  // 기본 텝당 골드 획득량에 더해지는 골드 (아티팩트 효과)

        // 획득한 아티팩트들의 효과를 반영
        for (int i = 0; i < status.artifactAcquired.Length; i++)
        {
            if(status.artifactAcquired[i])
            {
                multiplier += ArtifactData.GetArtifactData(i).tapBonus;
                plusGold += ArtifactData.GetArtifactData(i).tapPlus;
            }
        }

        // 보너스 골드 획득량 스킬 효과를 반영
        if(status.bonusGoldPerTapSkillDuration > 0)
        {
            var (bonusMultiplier, _) = SkillManager.GetBonusGoldPerTapEffect(status.bonusGoldPerTapSkillLevel);
            multiplier += bonusMultiplier;
        }

        // 최종 결과를 계산
        result *= multiplier;
        result += plusGold;

        // 탭 버프 스킬 효과를 반영 (보상형 광고 시청시 얻는 버프)
        if(status.tapBuffDuration > 0)
        {
            result *= buffMultiplier;
        }

        return result;
    }

    // 플레이어의 실제 초당 골드 획득량을 반환합니다
    public double GetGoldPerSec()
    {
        double result = status.defaultGoldPerSec;

        float multiplier = 1; // 기본 초당 골드 획득량에 곱해지는 배수 (스킬 및 아티팩트 효과)
        double plusGold = 0;  // 기본 초당 골드 획득량에 더해지는 골드 (아티팩트 효과)

        // 획득한 아티팩트들의 효과를 반영
        for (int i = 0; i < status.artifactAcquired.Length; i++)
        {
            if(status.artifactAcquired[i])
            {
                multiplier += ArtifactData.GetArtifactData(i).secBonus;
                plusGold += ArtifactData.GetArtifactData(i).secPlus;
            }
        }

        // 보너스 골드 획득량 스킬 효과를 반영
        if(status.bonusGoldPerSecSkillDuration > 0)
        {
            var (bonusMultiplier, _) = SkillManager.GetBonusGoldPerSecEffect(status.bonusGoldPerSecSkillLevel);
            multiplier += bonusMultiplier;
        }

        // 최종 결과를 계산
        result *= multiplier;
        result += plusGold;

        // 초당 버프 스킬 효과를 반영 (보상형 광고 시청시 얻는 버프)
        if(status.secBuffDuration > 0)
        {
            result *= buffMultiplier;
        }

        return result;
    }

    // 플레이어가 화면을 탭할 때 호출되는 함수입니다 (화면의 탭 영역 버튼에 연결되어 있음)
    // 탭당 골드 획득량만큼 소지 골드를 증가시키고, 플레이어 정보를 갱신합니다
    public void EarnGoldPerTap()
    {
        status.gold += GetGoldPerTap();
        info.tapCount++;
        info.highestGoldHeld = Math.Max(info.highestGoldHeld, status.gold);
    }

    // 게임이 시작되면 호출되는 함수입니다 (GameController.cs에서 호출)
    // 매초마다 초당 골드 획득량만큼 소지 골드를 증가시키고, 플레이어 정보를 갱신합니다
    public IEnumerator EarnGoldPerSecCoroutine()
    {
        while(true)
        {
            info.totalPlayTimeInSecond++;
            info.lastGameExitTimeInSecond = DateTimeUtil.GetTotalSecondsFromEarliestTime();
            status.gold += GetGoldPerSec();
            info.highestGoldHeld = Math.Max(info.highestGoldHeld, status.gold);
            yield return new WaitForSeconds(1);
        }
    }

    // 플레이어가 게임에 복귀했을때 얻는 골드를 계산하여 소지 골드에 추가합니다
    // isAdReward가 true이면 광고 시청으로 얻은 보상이므로 10배의 보상을 제공합니다
    // 게임 복귀시 나타나는 팝업의 버튼에 연결되어 있습니다 (ReturnRewardUIInitializer.cs 참고)
    public void EarnGoldByReturn(bool isAdReward)
    {
        double reward = GetExpectedReturnReward();
        if(isAdReward)
        {
            reward *= 10;
        }
        status.gold += reward;
    }

    // 플레이어가 게임에 복귀했을때 얻는 골드를 계산하여 반환합니다
    public double GetExpectedReturnReward()
    {
        if(info.unplayedTimeInSecond < minSecondsForReturnReward) return 0;
        double reward = info.unplayedTimeInSecond * status.defaultGoldPerSec * returnRewardMultiplier;
        return reward;
    }

    // 플레이어의 업그레이드와 소지금을 초기화하는 대가로 얻는 프레스티지 포인트를 반환합니다
    public int GetExpectedPrestigeReward()
    {
        double log = Math.Log10(status.defaultGoldPerSec + status.defaultGoldPerTap);
        return (int)log;
    }

    // 플레이어의 업그레이드와 소지금을 초기화하고, 프레스티지 포인트를 얻습니다
    public void Prestige()
    {
        int prestigePoint = GetExpectedPrestigeReward();
        status.prestigePoint += prestigePoint;
        status.gold = 0;
        status.defaultGoldPerSec = UpgradeManager.GetSecUpgradeEffect(0, 0);
        status.defaultGoldPerTap = UpgradeManager.GetTapUpgradeEffect(1);
        status.tapUpgradeLevel = 1;
        Array.Clear(status.secUpgradeLevels, 0, status.secUpgradeLevels.Length); 
        info.prestigeCount++;
    }

    // 프레스티지 포인트를 소모하여 랜덤 아티팩트를 획득합니다
    public ArtifactData GachaArtifact()
    {
        // 획득하지 않은 아티팩트의 인덱스 모두 찾아서 리스트에 저장
        var list = new List<int>();
        for (int i = 0; i < status.artifactAcquired.Length; i++)
        {
            if(!status.artifactAcquired[i])
            {
                list.Add(i);
            }
        }

        // 리스트가 비어있으면 아무것도 하지 않고 종료
        if(list.Count == 0) return null;

        // 리스트에서 랜덤으로 하나의 인덱스를 선택
        int randomIndex = UnityEngine.Random.Range(0, list.Count);
        int index = list[randomIndex];

        // 해당 인덱스의 아티팩트를 획득처리
        status.artifactAcquired[index] = true;

        // 프레스티지 포인트를 소모
        status.prestigePoint -= gachaPrestigePointCost;

        // 해당 아티팩트 데이터를 반환
        return ArtifactData.GetArtifactData(index);
    }



    // 탭 업그레이드 레벨을 1 증가시키고, 골드와 능력치, 기록을 갱신합니다
    public void LevelUpTapUpgrade()
    {
        double cost = UpgradeManager.GetTapUpgradeCost(status.tapUpgradeLevel);
        double currentEffect = UpgradeManager.GetTapUpgradeEffect(status.tapUpgradeLevel);
        double nextEffect = UpgradeManager.GetTapUpgradeEffect(status.tapUpgradeLevel + 1);
        double increase = nextEffect - currentEffect;
        
        if(status.gold < cost) return;

        status.gold -= cost;
        status.tapUpgradeLevel++;
        status.defaultGoldPerTap += increase;
        info.highestDefaultGoldPerTap = Math.Max(info.highestDefaultGoldPerTap, status.defaultGoldPerTap);
    }

    // 초당 업그레이드 레벨을 1 증가시키고, 골드와 능력치, 기록을 갱신합니다
    public void LevelUpSecUpgrade(int index)
    {
        double cost = UpgradeManager.GetSecUpgradeCost(index, status.secUpgradeLevels[index]);
        double currentEffect = UpgradeManager.GetSecUpgradeEffect(index, status.secUpgradeLevels[index]);
        double nextEffect = UpgradeManager.GetSecUpgradeEffect(index, status.secUpgradeLevels[index] + 1);
        double increase = nextEffect - currentEffect;
        
        if(status.gold < cost) return;

        status.gold -= cost;
        status.secUpgradeLevels[index]++;
        status.defaultGoldPerSec += increase;
        info.highestDefaultGoldPerSec = Math.Max(info.highestDefaultGoldPerSec, status.defaultGoldPerSec);
    }

    // 자동 탭 스킬 레벨을 1 증가시키고, 프레스티지 포인트를 소모합니다
    public void LevelUpAutoTapSkill()
    {
        if(status.prestigePoint < skillLevelUpPresitgePointCost) return;
        status.prestigePoint -= skillLevelUpPresitgePointCost;
        status.autoTapSkillLevel++;
    }

    // 탭당 골드 보너스 스킬 레벨을 1 증가시키고, 프레스티지 포인트를 소모합니다
    public void LevelUpBonusGoldPerTapSkill()
    {
        if(status.prestigePoint < skillLevelUpPresitgePointCost) return;
        status.prestigePoint -= skillLevelUpPresitgePointCost;
        status.bonusGoldPerTapSkillLevel++;
    }

    // 초당 골드 보너스 스킬 레벨을 1 증가시키고, 프레스티지 포인트를 소모합니다
    public void LevelUpBonusGoldPerSecSkill()
    {
        if(status.prestigePoint < skillLevelUpPresitgePointCost) return;
        status.prestigePoint -= skillLevelUpPresitgePointCost;
        status.bonusGoldPerSecSkillLevel++;
    }

    // 쿨타임 감소 스킬 레벨을 1 증가시키고, 프레스티지 포인트를 소모합니다
    public void LevelUpFastCooldownSkill()
    {
        if(status.prestigePoint < skillLevelUpPresitgePointCost) return;
        status.prestigePoint -= skillLevelUpPresitgePointCost;
        status.fastCooldownSkillLevel++;
    }



    // 자동 탭 스킬을 활성화하고, 지속시간 동안 탭당 골드를 획득합니다
    // 지속시간이 끝나면 쿨타임을 시작합니다
    public IEnumerator ActivateAutoTapSkillCoroutine()
    {
        if(status.autoTapSkillLevel == 0) yield break;
        if(status.autoTapSkillCooltime > 0) yield break;
        if(status.autoTapSkillDuration > 0) yield break;

        // 자동 탭 스킬의 효과를 가져옴
        var (tapPerSec, duration) = SkillManager.GetAutoTapEffect(status.autoTapSkillLevel);

        // 지속시간 동안 탭당 골드를 획득
        status.autoTapSkillDuration = duration;
        while(status.autoTapSkillDuration > 0)
        {
            status.autoTapSkillDuration--;
            status.gold += GetGoldPerTap() * tapPerSec;
            yield return new WaitForSeconds(1);
        }

        // 지속시간이 끝나면 쿨타임을 시작
        status.autoTapSkillCooltime = maxSkillCooltime;
        while(status.autoTapSkillCooltime > 0)
        {
            status.autoTapSkillCooltime--;
            yield return new WaitForSeconds(1);
        }
    }

    // 탭당 골드 보너스 스킬을 활성화하고, 지속시간 동안 탭당 골드를 획득합니다
    public IEnumerator ActivateBonusGoldPerTapSkillCoroutine()
    {
        if(status.bonusGoldPerTapSkillLevel == 0) yield break;
        if(status.bonusGoldPerTapSkillCooltime > 0) yield break;
        if(status.bonusGoldPerTapSkillDuration > 0) yield break;

        // 탭당 골드 보너스 스킬의 효과를 가져옴
        var (_, duration) = SkillManager.GetBonusGoldPerTapEffect(status.bonusGoldPerTapSkillLevel);

        // 지속시간 동안 탭당 골드를 획득
        status.bonusGoldPerTapSkillDuration = duration;
        while(status.bonusGoldPerTapSkillDuration > 0)
        {
            status.bonusGoldPerTapSkillDuration--;
            yield return new WaitForSeconds(1);
        }

        // 지속시간이 끝나면 쿨타임을 시작
        status.bonusGoldPerTapSkillCooltime = maxSkillCooltime;
        while(status.bonusGoldPerTapSkillCooltime > 0)
        {
            status.bonusGoldPerTapSkillCooltime--;
            yield return new WaitForSeconds(1);
        }
    }

    // 초당 골드 보너스 스킬을 활성화하고, 지속시간 동안 초당 골드를 획득합니다
    public IEnumerator ActivateBonusGoldPerSecSkillCoroutine()
    {
        if(status.bonusGoldPerSecSkillLevel == 0) yield break;
        if(status.bonusGoldPerSecSkillCooltime > 0) yield break;
        if(status.bonusGoldPerSecSkillDuration > 0) yield break;

        // 초당 골드 보너스 스킬의 효과를 가져옴
        var (_, duration) = SkillManager.GetBonusGoldPerSecEffect(status.bonusGoldPerSecSkillLevel);

        // 지속시간 동안 초당 골드를 획득
        status.bonusGoldPerSecSkillDuration = duration;
        while(status.bonusGoldPerSecSkillDuration > 0)
        {
            status.bonusGoldPerSecSkillDuration--;
            yield return new WaitForSeconds(1);
        }

        // 지속시간이 끝나면 쿨타임을 시작
        status.bonusGoldPerSecSkillCooltime = maxSkillCooltime;
        while(status.bonusGoldPerSecSkillCooltime > 0)
        {
            status.bonusGoldPerSecSkillCooltime--;
            yield return new WaitForSeconds(1);
        }
    }

    // 쿨타임 감소 스킬을 활성화하고, 지속시간 동안 스킬의 쿨타임을 감소시킵니다
    public IEnumerator ActivateFastCooldownSkillCoroutine()
    {
        if(status.fastCooldownSkillLevel == 0) yield break;
        if(status.fastCooldownSkillCooltime > 0) yield break;
        
        // 쿨타임 감소 스킬의 효과를 가져옴
        var coolDownEffect = SkillManager.GetFastCooldownEffect(status.fastCooldownSkillLevel);

        // 스킬 발동시 다른 스킬의 쿨타임을 감소시킴
        status.autoTapSkillCooltime -= (int)(maxSkillCooltime * coolDownEffect);
        status.bonusGoldPerTapSkillCooltime -= (int)(maxSkillCooltime * coolDownEffect);
        status.bonusGoldPerSecSkillCooltime -= (int)(maxSkillCooltime * coolDownEffect);

        // 스킬들의 쿨타임을 음수로 만드는 것을 방지
        status.autoTapSkillCooltime = Math.Max(status.autoTapSkillCooltime, 0);
        status.bonusGoldPerTapSkillCooltime = Math.Max(status.bonusGoldPerTapSkillCooltime, 0);
        status.bonusGoldPerSecSkillCooltime = Math.Max(status.bonusGoldPerSecSkillCooltime, 0);

        // 지속시간 동안 쿨타임을 감소시킴
        status.fastCooldownSkillCooltime = maxSkillCooltime;
        while(status.fastCooldownSkillCooltime > 0)
        {
            status.fastCooldownSkillCooltime--;
            yield return new WaitForSeconds(1);
        }
    }

    // 지속시간 동안 탭당 골드 획득량을 증가시킵니다
    // 이 메소드는 보상형 광고 시청시 호출됩니다
    public IEnumerator ActivateTapBuffCoroutine()
    {
        status.tapBuffDuration = maxBuffDuration;

        while(status.tapBuffDuration > 0)
        {
            status.tapBuffDuration--;
            yield return new WaitForSeconds(1);
        }
    }

    // 지속시간 동안 초당 골드 획득량을 증가시킵니다
    // 이 메소드는 보상형 광고 시청시 호출됩니다
    public IEnumerator ActivateSecBuffCoroutine()
    {
        status.secBuffDuration = maxBuffDuration;

        while(status.secBuffDuration > 0)
        {
            status.secBuffDuration--;
            yield return new WaitForSeconds(1);
        }
    }

    

    // 소지금이 목표치를 달성했다면 도전과제 레벨을 1 증가시키고 프레스티지 포인트를 지급합니다
    public void AchieveHoldGoldChallenge()
    {
        double goal = ChallengeManager.GetHoldGoldChallengeGoal(challenge.holdGoldChallengeLevel);
        if(info.highestGoldHeld >= goal)
        {
            challenge.holdGoldChallengeLevel++;
            status.prestigePoint += challengePrestigePointReward;
        }
    }

    // 탭당 골드 획득량이 목표치를 달성했다면 도전과제 레벨을 1 증가시키고 프레스티지 포인트를 지급합니다
    public void AchieveGoldPerTapChallenge()
    {
        double goal = ChallengeManager.GetGoldPerTapChallengeGoal(challenge.goldPerTapChallengeLevel);
        if(info.highestDefaultGoldPerTap >= goal)
        {
            challenge.goldPerTapChallengeLevel++;
            status.prestigePoint += challengePrestigePointReward;
        }
    }

    // 초당 골드 획득량이 목표치를 달성했다면 도전과제 레벨을 1 증가시키고 프레스티지 포인트를 지급합니다
    public void AchieveGoldPerSecChallenge()
    {
        double goal = ChallengeManager.GetGoldPerSecChallengeGoal(challenge.goldPerSecChallengeLevel);
        if(info.highestDefaultGoldPerSec >= goal)
        {
            challenge.goldPerSecChallengeLevel++;
            status.prestigePoint += challengePrestigePointReward;
        }
    }

    // 광고 시청 횟수가 목표치를 달성했다면 도전과제 레벨을 1 증가시키고 프레스티지 포인트를 지급합니다
    public void AchieveAdCountChallenge()
    {
        int goal = ChallengeManager.GetAdCountChallengeGoal(challenge.adCountChallengeLevel);
        if(info.adCount >= goal)
        {
            challenge.adCountChallengeLevel++;
            status.prestigePoint += challengePrestigePointReward;
        }
    }

    // 플레이 시간이 목표치를 달성했다면 도전과제 레벨을 1 증가시키고 프레스티지 포인트를 지급합니다
    public void AchievePlayTimeChallenge()
    {
        int goal = ChallengeManager.GetPlayTimeChallengeGoal(challenge.playTimeChallengeLevel);
        if(info.totalPlayTimeInSecond >= goal)
        {
            challenge.playTimeChallengeLevel++;
            status.prestigePoint += challengePrestigePointReward;
        }
    }

    // 탭 횟수가 목표치를 달성했다면 도전과제 레벨을 1 증가시키고 프레스티지 포인트를 지급합니다
    public void AchieveTapCountChallenge()
    {
        int goal = ChallengeManager.GetTapCountChallengeGoal(challenge.tapCountChallengeLevel);
        if(info.tapCount >= goal)
        {
            challenge.tapCountChallengeLevel++;
            status.prestigePoint += challengePrestigePointReward;
        }
    }

    // 프레스티지 횟수가 목표치를 달성했다면 도전과제 레벨을 1 증가시키고 프레스티지 포인트를 지급합니다
    public void AchievePrestigeCountChallenge()
    {
        int goal = ChallengeManager.GetPrestigeCountChallengeGoal(challenge.prestigeCountChallengeLevel);
        if(info.prestigeCount >= goal)
        {
            challenge.prestigeCountChallengeLevel++;
            status.prestigePoint += challengePrestigePointReward;
        }
    }

    // 보유한 아티팩트 갯수가 목표치를 달성했다면 도전과제 레벨을 1 증가시키고 프레스티지 포인트를 지급합니다
    public void AchieveArtifactCountChallenge()
    {
        int goal = ChallengeManager.GetArtifactCountChallengeGoal(challenge.artifactCountChallengeLevel);
        int count = status.artifactAcquired.Count(a => a);
        if(count >= goal)
        {
            challenge.artifactCountChallengeLevel++;
            status.prestigePoint += challengePrestigePointReward;
        }
    }


    // 게임이 시작되면 호출되는 함수입니다 (GameController.cs에서 호출)
    // 미접속 시간을 계산하고 난 다음, 게임 시작 및 종료 시간을 갱신합니다
    public void NotifyGameStart()
    {
        info.lastGameStartTimeInSecond = DateTimeUtil.GetTotalSecondsFromEarliestTime();
        info.unplayedTimeInSecond = info.lastGameStartTimeInSecond - info.lastGameExitTimeInSecond;
        info.lastGameExitTimeInSecond = DateTimeUtil.GetTotalSecondsFromEarliestTime();
    }
}