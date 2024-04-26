using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// 이 cs파일은 게임 내의 모든 버튼 이벤트를 관리합니다.
public partial class Tap_N_Gold
{

# region TopCanvas

    /// 초당 획득 골드를 증가시키는 버프를 얻기위한 광고를 시청합니다.
    /// 광고가 종료되면 게임 상태를 Normal 로 변경하고, 현재 열린 팝업을 닫습니다.
    /// 이후 30초간 초당 획득 골드가 10배가 됩니다.
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


    /// 탭당 획득 골드를 증가시키는 버프를 얻기위한 광고를 시청합니다.
    /// 광고가 종료되면 게임 상태를 Normal 로 변경하고, 현재 열린 팝업을 닫습니다.
    /// 이후 30초간 탭당 획득 골드가 10배가 됩니다.
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

    /// 볼륨을 On / Off 합니다.
    public void ChangeVolumeOnState()
    {
        bool newState = !volumeOn;
        volumeOn = newState;
    }

# endregion

# region MidCanvas

    /// 화면을 탭하여 골드를 획득할때 호출됩니다.
    public void GetGoldByTap()
    {
        /// 골드 보유량, 탭 카운트를 증가시키고, 최고 골드 보유량을 갱신합니다.
        gold += realGoldPerTap;
        tapCount++;
        if(gold > highestGold) highestGold = gold;
        if(scrollState == ScrollState.Challenge) Execute_UpdateChallengeScroll = true;

        RectTransform rectTransform = midCanvas.rectTransformData.rectTransform;

        /// 마우스 또는 터치 위치를 읽어서 이펙트 위치 리스트에 추가합니다. 
        if(tap_normalizedPosQueue.Count != 0) return;

        #if UNITY_EDITOR
        Vector2 screenPos = Input.mousePosition;
        Vector2 localPos = RectTransformUtil.ScreenToLocalPoint(rectTransform, screenPos);
        Vector2 normalizedPos = RectTransformUtil.LocalToNormalizedPoint(rectTransform, localPos);
        tap_normalizedPosQueue.Enqueue(normalizedPos);

        #elif UNITY_ANDROID
        int touchCount = Input.touchCount;
        for(int i = 0; i < touchCount; i++)
        {
            if(Input.GetTouch(i).phase != TouchPhase.Ended) continue;
            Vector2 screenPos = Input.GetTouch(i).position;
            Vector2 localPos = RectTransformUtil.ScreenToLocalPoint(rectTransform, screenPos);
            Vector2 normalizedPos = RectTransformUtil.LocalToNormalizedPoint(rectTransform, localPos);
            if(normalizedPos.x < 0 || normalizedPos.x > 1 || normalizedPos.y < 0 || normalizedPos.y > 1) continue;
            tap_normalizedPosQueue.Enqueue(normalizedPos);
        }

        #endif

        Execute_UpdateMidCanvasTouchEffectActivation = true;
    }


    

# endregion


# region scroll

    /// 탭당 골드 획득 업그레이드 스크롤을 활성화 할 때 호출됩니다.
    /// 네비게이션 바의 버튼들의 색상을 변경하고, 업그레이드 스크롤을 활성화합니다.
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

    /// 초당 골드 획득 업그레이드 스크롤을 활성화 할 때 호출됩니다.
    /// 네비게이션 바의 버튼들의 색상을 변경하고, 업그레이드 스크롤을 활성화합니다.
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

    /// 아티팩트 스크롤을 활성화 할 때 호출됩니다.
    /// 네비게이션 바의 버튼들의 색상을 변경하고, 업그레이드 스크롤을 활성화합니다.
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
        Execute_UpdatePrestigePointText = true;
        Execute_UpdateArtifactScroll = true;
    }

    /// 도전과제 스크롤을 활성화 할 때 호출됩니다.
    /// 네비게이션 바의 버튼들의 색상을 변경하고, 업그레이드 스크롤을 활성화합니다.
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

    /// 통계 스크롤을 활성화 할 때 호출됩니다.
    /// 네비게이션 바의 버튼들의 색상을 변경하고, 업그레이드 스크롤을 활성화합니다.
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

    /// 탭당 얻는 골드 버프 팝업을 활성화 할 때 호출됩니다.
    public void EnableTapTempBuffPopup(bool value)
    {
        tapTempBuff.SetDisplay(value);
        popupState = (value) ?PopupState.TapTempBuff :PopupState.None;
    }

    /// 초당 얻는 골드 버프 팝업을 활성화 할 때 호출됩니다.
    public void EnableSecTempBuffPopup(bool value)
    {
        secTempBuff.SetDisplay(value);
        popupState = (value) ?PopupState.SecTempBuff :PopupState.None;
    }

    /// 프레스티지 팝업을 활성화 할 때 호출됩니다.
    public void EnablePrestigePopup(bool value)
    {
        prestige.SetDisplay(value);
        popupState = (value) ?PopupState.Prestige :PopupState.None;
    }

    /// 유물 가챠 팝업을 활성화 할 때 호출됩니다.
    public void EnableGachaPopup(bool value)
    {
        gacha.SetDisplay(value);
        Execute_UpdateGachaPopup = value;
        popupState = (value) ?PopupState.Gacha :PopupState.None;
    }

    /// 광고 시청으로 인한 골드 획득 팝업을 활성화 할 때 호출됩니다.
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


    /// 유물 가챠 버튼을 클릭할 때 호출됩니다.
    public void StartGacha() 
    {
        /// 유물을 15개 모두 얻었거나, 프레스티지 포인트가 10보다 작으면 가챠를 시작할 수 없습니다.
        if(artifactList.Count == 15) return;
        if(prestigePoint < 10) return;

        /// 프레스티지 포인트를 10 감소시키고, 가챠 팝업을 업데이트 합니다.
        prestigePoint -= 10;
        Execute_UpdateGachaPopup = true;

        /// 보유하지 않은 유물중에서 현재 단계에서 얻을 수 있는 유물을 랜덤으로 선택합니다.
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

        int random = list[rand.Next(list.Count)];
        string methodName = $"GetArtifactBuff{random}";
        ArtifactData buffData = typeof(ArtifactData).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static).Invoke(null, null) as ArtifactData;
        artifactList.Add(buffData);
        
        /// gameState 를 GachaAnimation 으로 변경합니다
        gameState = GameState.GachaAnimation;
        Execute_BlockUserInput = true;
        Execute_OnGachaAnimationStart = true;
    }
# endregion
 

# region Prestige
    /// 프레스티지 버튼을 클릭할 때 호출됩니다.
    /// 프레스티지 포인트를 얻고, 애니메이션을 재생합니다.
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

    /// 탭 업그레이드 버튼을 클릭할 때 호출됩니다.
    /// 탭 레벨을 1 증가시키고, 골드를 감소시키고, 탭당 획득 골드를 증가시킵니다.
    /// 이후 탭당 얻는 골드 텍스트와 골드 보유 텍스트, 업그레이드 버튼 색상을 업데이트 합니다.
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

    /// 오토 탭 스킬 버튼을 클릭할 때 호출됩니다.
    /// 오토 탭 스킬을 활성화 시키고, 스킬 버튼을 업데이트 합니다.
    public void Button_AutoTapSkill()
    {
        autoTapSkillData.ActivateSkill();
        Execute_UpdateTapUpgradeScroll = true;
    }

    /// 탭당 보너스 스킬 버튼을 클릭할 때 호출됩니다.
    /// 탭당 보너스 스킬을 활성화 시키고, 스킬 버튼을 업데이트 합니다.
    public void Button_BonusTapSkill()
    {
        bonusTapSkillData.ActivateSkill();
        Execute_UpdateTapUpgradeScroll = true;
        Execute_UpdateTopCanvasGoldPerTapText = true;
    }

    /// 초당 보너스 스킬 버튼을 클릭할 때 호출됩니다.
    /// 초당 보너스 스킬을 활성화 시키고, 스킬 버튼을 업데이트 합니다.
    public void Button_BonusSecSkill()
    {
        bonusSecSkillData.ActivateSkill();
        Execute_UpdateTapUpgradeScroll = true;
        Execute_UpdateTopCanvasGoldPerSecText = true;
    }

    /// 쿨다운 스킬 버튼을 클릭할 때 호출됩니다.
    /// 쿨다운 스킬을 활성화 시키고, 스킬 버튼을 업데이트 합니다.
    public void Button_CoolDownSkill()
    {
        coolDownSkillData.ActivateSkill(this);
        Execute_UpdateTapUpgradeScroll = true;
    }

    /// 오토 탭 스킬 버튼을 업그레이드 할 때 호출됩니다.
    /// 프레스티지 포인트 2를 소모하고, 오토 탭 스킬 레벨을 1 증가시킵니다.
    public void Button_AutoTapSkillUpgrade()
    {
        if(prestigePoint < 2) return;
        prestigePoint -= 2;
        autoTapSkillData.SetLevel(autoTapSkillData.GetLevel() + 1);
        Execute_UpdateTapUpgradeScroll = true;
    }

    /// 탭당 보너스 스킬 버튼을 업그레이드 할 때 호출됩니다.
    /// 프레스티지 포인트 2를 소모하고, 탭당 보너스 스킬 레벨을 1 증가시킵니다.
    public void Button_BonusTapSkillUpgrade()
    {
        if(prestigePoint < 2) return;
        prestigePoint -= 2;
        bonusTapSkillData.SetLevel(bonusTapSkillData.GetLevel() + 1);
        Execute_UpdateTapUpgradeScroll = true;
    }

    /// 초당 보너스 스킬 버튼을 업그레이드 할 때 호출됩니다.
    /// 프레스티지 포인트 2를 소모하고, 초당 보너스 스킬 레벨을 1 증가시킵니다.
    public void Button_BonusSecSkillUpgrade()
    {
        if(prestigePoint < 2) return;
        prestigePoint -= 2;
        bonusSecSkillData.SetLevel(bonusSecSkillData.GetLevel() + 1);
        Execute_UpdateTapUpgradeScroll = true;
    }

    /// 쿨다운 스킬 버튼을 업그레이드 할 때 호출됩니다.
    /// 프레스티지 포인트 2를 소모하고, 쿨다운 스킬 레벨을 1 증가시킵니다.
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

    /// 초당 골드 획득 업그레이드 버튼을 클릭할 때 호출됩니다.
    /// 항목에 맞는 업그레이드 레벨을 1 증가시키고, 골드를 감소시키고, 초당 획득 골드를 증가시킵니다.
    /// 이후 초당 얻는 골드 텍스트와 골드 보유 텍스트, 업그레이드 버튼 색상을 업데이트 합니다.
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

    /// 보유 골드 도전과제 버튼을 클릭할 때 호출됩니다.
    /// 보상으로 프레스티지 포인트를 획득하고, 도전과제를 다음 레벨로 변경합니다.
    /// 이후 도전과제 스크롤을 업데이트 합니다.
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

    /// 탭당 골드 도전과제 버튼을 클릭할 때 호출됩니다.
    /// 보상으로 프레스티지 포인트를 획득하고, 도전과제를 다음 레벨로 변경합니다.
    /// 이후 도전과제 스크롤을 업데이트 합니다.
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

    /// 초당 골드 도전과제 버튼을 클릭할 때 호출됩니다.
    /// 보상으로 프레스티지 포인트를 획득하고, 도전과제를 다음 레벨로 변경합니다.
    /// 이후 도전과제 스크롤을 업데이트 합니다.
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

    /// 광고 시청 도전과제 버튼을 클릭할 때 호출됩니다.
    /// 보상으로 프레스티지 포인트를 획득하고, 도전과제를 다음 레벨로 변경합니다.
    /// 이후 도전과제 스크롤을 업데이트 합니다.    
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

    /// 게임 플레이 시간 도전과제 버튼을 클릭할 때 호출됩니다.
    /// 보상으로 프레스티지 포인트를 획득하고, 도전과제를 다음 레벨로 변경합니다.
    /// 이후 도전과제 스크롤을 업데이트 합니다.
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

    /// 탭 횟수 도전과제 버튼을 클릭할 때 호출됩니다.
    /// 보상으로 프레스티지 포인트를 획득하고, 도전과제를 다음 레벨로 변경합니다.
    /// 이후 도전과제 스크롤을 업데이트 합니다.
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

    /// 프레스티지 횟수 도전과제 버튼을 클릭할 때 호출됩니다.
    /// 보상으로 프레스티지 포인트를 획득하고, 도전과제를 다음 레벨로 변경합니다.
    /// 이후 도전과제 스크롤을 업데이트 합니다.
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

    /// 유물 획득 도전과제 버튼을 클릭할 때 호출됩니다.
    /// 보상으로 프레스티지 포인트를 획득하고, 도전과제를 다음 레벨로 변경합니다.
    /// 이후 도전과제 스크롤을 업데이트 합니다.
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

    /// 미접속 보상 버튼을 클릭할 때 호출됩니다.
    /// 미접속 보상 팝업을 닫고, 골드를 획득합니다.
    public void Returning_GetReward()
    {
        gold += returningRewardGold;
        popupState = PopupState.None;
        returning.SetDisplay(false);
        Execute_UpdateTopCanvasGoldText = true;
        returningRewardGold = 0;
        SaveData();
    }

    /// 미접속 보상 10배 버튼을 클릭할 때 호출됩니다.
    /// 광고가 종료되면 미접속 보상 팝업을 닫고, 골드를 획득합니다.
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

    /// 광고를 시청하고 골드를 얻는 버튼을 클릭할 때 호출됩니다.
    /// 광고가 종료되면 광고 시청으로 인한 골드 획득 팝업을 닫고, 골드를 획득합니다.
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