using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public partial class ClickerTemp
{

# region TopCanvas
    public void ShowAdAndGetSecTempBuff()
    {
        RewardAdRequest.ShowAd(
        () => {gameState = GameState.Advertisement;},
        () => 
        {
            secTempBuffData.SetRemainedTime(30); 
            adCount++;
        },
        () =>
        {
            secTempBuff.SetDisplay(false); 
            gameState = GameState.Normal;
            Execute_UpdateChallengeScroll = true;
            Execute_UpdateTopCanvasGoldPerSecText = true;
            Execute_UpdateTopCanvasGoldPerTapText = true;
            SaveData(); 
        });
    }


    public void ShowAdAndGetTapTempBuff()
    {
        RewardAdRequest.ShowAd(
        () => {gameState = GameState.Advertisement;},
        () => 
        {
            tapTempBuffData.SetRemainedTime(30); 
            adCount++;
        },
        () =>
        {
            tapTempBuff.SetDisplay(false); 
            gameState = GameState.Normal;
            Execute_UpdateChallengeScroll = true;
            Execute_UpdateTopCanvasGoldPerSecText = true;
            Execute_UpdateTopCanvasGoldPerTapText = true;
            SaveData(); 
        });
    }

    public void ChangeVolumeOnState()
    {
        bool newState = !volumeOn;
        volumeOn = newState;
    }

# endregion

# region MidCanvas
    public void GetGoldByTap()
    {
        gold += realGoldPerTap;
        tapCount++;
        if(gold > highestGold) highestGold = gold;
        if(scrollState == ScrollState.Challenge) Execute_UpdateChallengeScroll = true;

        RectTransform rectTransform = midCanvas.rectTransformData.rectTransform;

        #if UNITY_EDITOR
        if(tap_normalizedPosList.Count == 0)
        {
            Vector2 screenPos = Input.mousePosition;
            Vector2 localPos = RectTransformUtil.ScreenToLocalPoint(rectTransform, screenPos);
            Vector2 normalizedPos = RectTransformUtil.LocalToNormalizedPoint(rectTransform, localPos);
            tap_normalizedPosList.Add(normalizedPos);
        }
        
        #elif UNITY_ANDROID
        if(tap_normalizedPosList.Count == 0)
        {
            int touchCount = Input.touchCount;
            var touch = Input.GetTouch(touchCount - 1);

            for(int i = 0; i < touchCount; i++)
            {
                if(Input.GetTouch(i).phase != TouchPhase.Ended) continue;
                Vector2 screenPos = Input.GetTouch(i).position;
                Vector2 localPos = RectTransformUtil.ScreenToLocalPoint(rectTransform, screenPos);
                Vector2 normalizedPos = RectTransformUtil.LocalToNormalizedPoint(rectTransform, localPos);
                if(normalizedPos.x < 0 || normalizedPos.x > 1 || normalizedPos.y < 0 || normalizedPos.y > 1) continue;
                tap_normalizedPosList.Add(normalizedPos);
            }
        }
        #endif
    }

# endregion


# region scroll

    public void EnableTapUpgradeScroll(bool value)
    {
        if(!value) return;
        tapUpgrade.gameObjectData.SetEnabled(true);
        secUpgrade.gameObjectData.SetEnabled(false);
        artifact.gameObjectData.SetEnabled(false);
        challenge.gameObjectData.SetEnabled(false);
        statistics.gameObjectData.SetEnabled(false);
        
        Color color = navCanvas_RadioButton_TapUpgrade_Icon.spriteData.GetColor();
        color.a = 1;
        navCanvas_RadioButton_TapUpgrade_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_SecUpgrade_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_SecUpgrade_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_Artifact_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_Artifact_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_Challenge_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_Challenge_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_Statistics_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_Statistics_Icon.spriteData.SetColor(color);

        scrollState = ScrollState.TapUpgrade;
    }

    public void EnableSecUpgradeScroll(bool value)
    {
        if(!value) return;
        tapUpgrade.gameObjectData.SetEnabled(false);
        secUpgrade.gameObjectData.SetEnabled(true);
        artifact.gameObjectData.SetEnabled(false);
        challenge.gameObjectData.SetEnabled(false);
        statistics.gameObjectData.SetEnabled(false);

        Color color = navCanvas_RadioButton_TapUpgrade_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_TapUpgrade_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_SecUpgrade_Icon.spriteData.GetColor();
        color.a = 1;
        navCanvas_RadioButton_SecUpgrade_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_Artifact_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_Artifact_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_Challenge_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_Challenge_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_Statistics_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_Statistics_Icon.spriteData.SetColor(color);

        scrollState = ScrollState.SecUpgrade;
    }

    public void EnableArtifactScroll(bool value)
    {
        if(!value) return;
        tapUpgrade.gameObjectData.SetEnabled(false);
        secUpgrade.gameObjectData.SetEnabled(false);
        artifact.gameObjectData.SetEnabled(true);
        challenge.gameObjectData.SetEnabled(false);
        statistics.gameObjectData.SetEnabled(false);

        Color color = navCanvas_RadioButton_TapUpgrade_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_TapUpgrade_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_SecUpgrade_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_SecUpgrade_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_Artifact_Icon.spriteData.GetColor();
        color.a = 1f;
        navCanvas_RadioButton_Artifact_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_Challenge_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_Challenge_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_Statistics_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_Statistics_Icon.spriteData.SetColor(color);

        scrollState = ScrollState.Artifact;
        Execute_UpdateSpecialTokenText = true;
        Execute_UpdateArtifactScroll = true;
    }

    public void EnableChallengeScroll(bool value)
    {
        if(!value) return;
        tapUpgrade.gameObjectData.SetEnabled(false);
        secUpgrade.gameObjectData.SetEnabled(false);
        artifact.gameObjectData.SetEnabled(false);
        challenge.gameObjectData.SetEnabled(true);
        statistics.gameObjectData.SetEnabled(false);

        Color color = navCanvas_RadioButton_TapUpgrade_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_TapUpgrade_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_SecUpgrade_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_SecUpgrade_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_Artifact_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_Artifact_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_Challenge_Icon.spriteData.GetColor();
        color.a = 1;
        navCanvas_RadioButton_Challenge_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_Statistics_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_Statistics_Icon.spriteData.SetColor(color);

        scrollState = ScrollState.Challenge;
        Execute_UpdateChallengeScroll = true;
    }

    public void EnableStatisticsScroll(bool value)
    {
        if(!value) return;
        tapUpgrade.gameObjectData.SetEnabled(false);
        secUpgrade.gameObjectData.SetEnabled(false);
        artifact.gameObjectData.SetEnabled(false);
        challenge.gameObjectData.SetEnabled(false);
        statistics.gameObjectData.SetEnabled(true);

        Color color = navCanvas_RadioButton_TapUpgrade_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_TapUpgrade_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_SecUpgrade_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_SecUpgrade_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_Artifact_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_Artifact_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_Challenge_Icon.spriteData.GetColor();
        color.a = 0.3f;
        navCanvas_RadioButton_Challenge_Icon.spriteData.SetColor(color);

        color = navCanvas_RadioButton_Statistics_Icon.spriteData.GetColor();
        color.a = 1;
        navCanvas_RadioButton_Statistics_Icon.spriteData.SetColor(color);

        scrollState = ScrollState.Statistics;
    }

    # endregion


# region popup

    public void EnableTapTempBuffPopup(bool value)
    {
        tapTempBuff.SetDisplay(value);
        popupState = (value) ?PopupState.TapTempBuff :PopupState.None;
    }

    public void EnableSecTempBuffPopup(bool value)
    {
        secTempBuff.SetDisplay(value);
        popupState = (value) ?PopupState.SecTempBuff :PopupState.None;
    }

    public void EnablePrestigePopup(bool value)
    {
        prestige.SetDisplay(value);
        popupState = (value) ?PopupState.Prestige :PopupState.None;
    }

    public void EnableGachaPopup(bool value)
    {
        gacha.SetDisplay(value);
        Execute_UpdateGachaPopup = value;
        popupState = (value) ?PopupState.Gacha :PopupState.None;
    }

    public void EnableSpecialBonusPopup(bool value)
    {
        midCanvas_SpecialBonus_Button.animationData.SetCurrentState("IDLE");
        midCanvas_SpecialBonus.gameObjectData.SetEnabled(false);
        specialBonus.SetDisplay(value);
        popupState = (value) ?PopupState.SpecialBonus :PopupState.None;
        if(value) Execute_UpdateSpecialBonusPopup = true;
    }

    # endregion


# region Gacha

    /// === 실행 조건 ===
    /// 1. Gacha_Button 이 클릭될 때
    /// === 실행 내용 ===
    /// 1. artifactList 에 있는 Artifact 의 buffID 를 제외한 1 ~ 15 사이의 숫자 중 하나를 랜덤으로 선택한다
    /// 2. 선택된 숫자에 해당하는 Artifact 를 artifactList 에 추가한다
    /// 3. gameState 를 GachaAnimation 으로 변경한다
    public void StartGacha() 
    {
        if(artifactList.Count == 15) return;
        if(prestigePoint < 10) return;
        prestigePoint -= 10;

        Execute_UpdateGachaPopup = true;

        var rand = new System.Random();
        

        int[] nums = new int[3];
        int[] alreadyHave = ArrayUtil.Select(artifactList.ToArray(), (x) => x.buffID);
        List<int> list = new List<int>();
        for(int i = 0; i < 5; i++)
        {
            nums[0] = i*3+1;
            nums[1] = i*3+2;
            nums[2] = i*3+3;

            var targets = ArrayUtil.Except(nums, alreadyHave);
            if(targets.Length == 0) continue;
            int target = ArrayUtil.Min(targets).value;
            list.Add(target);
        }

        /// select a random number from the list
        int random = list[rand.Next(list.Count)];
        
        string methodName = $"GetArtifactBuff{random}";
        ArtifactData buffData = typeof(ArtifactData).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static).Invoke(null, null) as ArtifactData;
        artifactList.Add(buffData);

        gameState = GameState.GachaAnimation;
        Execute_BlockUserInput = true;
        Execute_OnGachaAnimationStart = true;
    }
# endregion
 

# region Prestige
    public void StartPrestige()
    {
        if(GetPrestigeReward() <= 0) return;
        gameState = GameState.PrestigeAnimation;
        prestige.SetDisplay(false);
        anim.SetDisplay(true);
        anim_Prestige.gameObjectData.SetEnabled(true);
        anim_Prestige.animationData.SetCurrentState("Prestige");
        prestigeCount++;
    }
# endregion



# region TapUpgrade

    public void TapUpgrade()
    {
        if(gold >= tapUpgradeData.GetNextLevelCost())
        {
            gold -= tapUpgradeData.GetNextLevelCost();
            defaultGoldPerTap += tapUpgradeData.GetNextLevelEffect() - tapUpgradeData.GetCurrentLevelEffect();
            tapUpgradeData.level += 1;

            if(defaultGoldPerTap > highestDefaultGoldPerTap) highestDefaultGoldPerTap = defaultGoldPerTap;
            Execute_UpdateTopCanvasGoldPerTapText = true;
            Execute_UpdateTopCanvasGoldText = true;
            Execute_UpdateTapUpgradeScroll = true;
        }
    }

    public void Button_AutoTapSkill()
    {
        autoTapSkillData.ActivateSkill();
        Execute_UpdateTapUpgradeScroll = true;
    }

    public void Button_BonusTapSkill()
    {
        bonusTapSkillData.ActivateSkill();
        Execute_UpdateTapUpgradeScroll = true;
        Execute_UpdateTopCanvasGoldPerTapText = true;
    }

    public void Button_BonusSecSkill()
    {
        bonusSecSkillData.ActivateSkill();
        Execute_UpdateTapUpgradeScroll = true;
        Execute_UpdateTopCanvasGoldPerSecText = true;
    }

    public void Button_CoolDownSkill()
    {
        coolDownSkillData.ActivateSkill(this);
        Execute_UpdateTapUpgradeScroll = true;
    }

    public void Button_AutoTapSkillUpgrade()
    {
        if(prestigePoint < 2) return;
        prestigePoint -= 2;
        autoTapSkillData.SetLevel(autoTapSkillData.GetLevel() + 1);
        Execute_UpdateTapUpgradeScroll = true;
    }

    public void Button_BonusTapSkillUpgrade()
    {
        if(prestigePoint < 2) return;
        prestigePoint -= 2;
        bonusTapSkillData.SetLevel(bonusTapSkillData.GetLevel() + 1);
        Execute_UpdateTapUpgradeScroll = true;
    }

    public void Button_BonusSecSkillUpgrade()
    {
        if(prestigePoint < 2) return;
        prestigePoint -= 2;
        bonusSecSkillData.SetLevel(bonusSecSkillData.GetLevel() + 1);
        Execute_UpdateTapUpgradeScroll = true;
    }

    public void Button_CoolDownSkillUpgrade()
    {
        if(coolDownSkillData.GetLevel() >= 100) return;
        if(prestigePoint < 2) return;
        prestigePoint -= 2;
        coolDownSkillData.SetLevel(coolDownSkillData.GetLevel() + 1);
        Execute_UpdateTapUpgradeScroll = true;
    }

    # endregion


# region SecUpgrade

    public void SecUpgrade(int index)
    {
        if(gold >= secUpgradeList[index].GetNextLevelCost())
        {
            gold -= secUpgradeList[index].GetNextLevelCost();
            defaultGoldPerSec += secUpgradeList[index].GetNextLevelEffect() - secUpgradeList[index].GetCurrentLevelEffect();
            secUpgradeList[index].level += 1;

            if(defaultGoldPerSec > highestDefaultGoldPerSec) highestDefaultGoldPerSec = defaultGoldPerSec;
            Execute_UpdateTopCanvasGoldPerSecText = true;
            Execute_UpdateTopCanvasGoldText = true;
            Execute_UpdateSecUpgradeScroll = true;
        }
    }
    # endregion


# region Challenge

    public void Achieve_Challenge_HoldGold()
    {
        int Challenge_HoldGold_Target = (int)Math.Pow(10, challenge_HoldGold_Level * 2 + 5);
        bool condition_HoldGold = gold >= Challenge_HoldGold_Target;
        if(condition_HoldGold)
        {
            prestigePoint += 2;
            challenge_HoldGold_Level++;
            Execute_UpdateChallengeNotifyMark = true;
            Execute_UpdateChallengeScroll = true;
        }
    }


    public void Achieve_Challenge_GoldPerTap()
    {
        int Challenge_GoldPerTap_Target = (int)Math.Pow(10, challenge_GoldPerTap_Level * 2 + 2);
        bool condition_GoldPerTap = defaultGoldPerTap >= Challenge_GoldPerTap_Target;
        if(condition_GoldPerTap)
        {
            prestigePoint += 2;
            challenge_GoldPerTap_Level++;
            Execute_UpdateChallengeNotifyMark = true;
            Execute_UpdateChallengeScroll = true;
        }
    }


    public void Achieve_Challenge_GoldPerSec()
    {
        int Challenge_GoldPerSec_Target = (int)Math.Pow(10, challenge_GoldPerSec_Level * 2 + 2);
        bool condition_GoldPerSec = defaultGoldPerSec >= Challenge_GoldPerSec_Target;
        if(condition_GoldPerSec)
        {
            prestigePoint += 2;
            challenge_GoldPerSec_Level++;
            Execute_UpdateChallengeNotifyMark = true;
            Execute_UpdateChallengeScroll = true;
        }
    }

    
    public void Achieve_Challenge_AdCount()
    {
        int Challenge_AdCount_Target = challenge_AdCount_Level * 5 + 5;
        bool condition_AdCount = adCount >= Challenge_AdCount_Target;
        if(condition_AdCount)
        {
            prestigePoint += 2;
            challenge_AdCount_Level++;
            Execute_UpdateChallengeNotifyMark = true;
            Execute_UpdateChallengeScroll = true;
        }
    }


    public void Achieve_Challenge_PlayTime()
    {
        int Challenge_PlayTime_Target = challenge_PlayTime_Level * 1000 + 500;
        bool condition_PlayTime = totalPlayTime >= Challenge_PlayTime_Target;
        if(condition_PlayTime)
        {
            prestigePoint += 2;
            challenge_PlayTime_Level++;
            Execute_UpdateChallengeNotifyMark = true;
            Execute_UpdateChallengeScroll = true;
        }
    }


    public void Achieve_Challenge_TapCount()
    {
        int Challenge_TapCount_Target = challenge_TapCount_Level * 500 + 500;
        bool condition_TapCount = tapCount >= Challenge_TapCount_Target;
        if(condition_TapCount)
        {
            prestigePoint += 2;
            challenge_TapCount_Level++;
            Execute_UpdateChallengeNotifyMark = true;
            Execute_UpdateChallengeScroll = true;
        }
    }


    public void Achieve_Challenge_PrestigeCount()
    {
        int Challenge_PrestigeCount_Target = challenge_PrestigeCount_Level * 3 + 3;
        bool condition_PrestigeCount = prestigeCount >= Challenge_PrestigeCount_Target;
        if(condition_PrestigeCount)
        {
            prestigePoint += 2;
            challenge_PrestigeCount_Level++;
            Execute_UpdateChallengeNotifyMark = true;
            Execute_UpdateChallengeScroll = true;
        }
    }


    public void Achieve_Challenge_ArtifactCount()
    {
        int Challenge_ArtifactCount_Target = challenge_ArtifactCount_Level * 3 + 3;
        bool condition_ArtifactCount = artifactList.Count >= Challenge_ArtifactCount_Target;
        if(condition_ArtifactCount)
        {
            prestigePoint += 2;
            challenge_ArtifactCount_Level++;
            Execute_UpdateChallengeNotifyMark = true;
            Execute_UpdateChallengeScroll = true;
        }
    }
    #endregion


# region Returning

    public void Returning_GetReward()
    {
        gold += returningRewardGold;
        popupState = PopupState.None;
        returning.SetDisplay(false);
        Execute_UpdateTopCanvasGoldText = true;
        returningRewardGold = 0;
        SaveData();
    }

    public void Returning_GetReward10X()
    {
        RewardAdRequest.ShowAd(
        () => {gameState = GameState.Advertisement;},
        () => 
        {
            gold += returningRewardGold * 9;
            adCount++;
        },
        () =>
        {
            gold += returningRewardGold;
            returningRewardGold = 0;
            popupState = PopupState.None;
            returning.SetDisplay(false);
            Execute_UpdateTopCanvasGoldText = true;
            gameState = GameState.Normal;
            SaveData();
        });
    }

# endregion


#region Special Bonus

    public void ShowSpecialBonusAd()
    {
        RewardAdRequest.ShowAd(
        () => {gameState = GameState.Advertisement;},
        () => 
        {
            double sum_TapSec = defaultGoldPerSec + defaultGoldPerTap;
            double bonus = sum_TapSec * 2000;
            gold += bonus;
            adCount++;
        },
        () =>
        {
            specialBonus.SetDisplay(false);
            popupState = PopupState.None;
            gameState = GameState.Normal;
            SaveData();
        });
    }
    
#endregion

}