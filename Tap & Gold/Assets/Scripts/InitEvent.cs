using System;
using System.Reflection;
using UnityEngine;

public partial class ClickerTemp
{
    [LateInitialize]
    public static void Init()
    {
        var instance = GameObject.FindObjectOfType<ClickerTemp>();
        SetAllField(instance);
        SetAllComponent(instance);
        SetAllEventData(instance);
        SetInitEvent(instance);
    }



    private static void SetAllComponent(ClickerTemp instance)
    {
        // InputBlocker
        instance.inputBlocker = ComponentManager.GetComponent<SimpleCanvas>("InputBlocker");

        // Anim
        instance.anim = ComponentManager.GetComponent<SimpleCanvas>("Anim");
        instance.anim_Prestige = ComponentManager.GetComponent<AnimationImage>("Anim/Prestige");

        // TopCanvas
        instance.topCanvas = ComponentManager.GetComponent<SimpleCanvas>("TopCanvas");
        instance.topCanvas_GoldPerTapText = ComponentManager.GetComponent<SimpleText>("TopCanvas/Panel/UserStatus/GoldPerTapText");
        instance.topCanvas_GoldPerSecText = ComponentManager.GetComponent<SimpleText>("TopCanvas/Panel/UserStatus/GoldPerSecText");
        instance.topCanvas_GoldText = ComponentManager.GetComponent<SimpleText>("TopCanvas/Panel/MoneyStatus/GoldText");
        instance.topCanvas_TapTempBuffBtn = ComponentManager.GetComponent<SimpleButton>("TopCanvas/Panel/TapTempBuffBtn");
        instance.topCanvas_SecTempBuffBtn = ComponentManager.GetComponent<SimpleButton>("TopCanvas/Panel/SecTempBuffBtn");
        instance.topCanvas_TapTempBuffBtnText = ComponentManager.GetComponent<SimpleText>("TopCanvas/Panel/TapTempBuffBtn/Text");
        instance.topCanvas_SecTempBuffBtnText = ComponentManager.GetComponent<SimpleText>("TopCanvas/Panel/SecTempBuffBtn/Text");
        instance.topCanvas_VolumeButton = ComponentManager.GetComponent<SimpleButton>("TopCanvas/Panel/VolumeButton");
        instance.topCanvas_VolumeButton_BGM = ComponentManager.GetComponent<SimpleAudio>("TopCanvas/Panel/VolumeButton/BGM");

        // MidCanvas
        instance.midCanvas = ComponentManager.GetComponent<SimpleCanvas>("MidCanvas");
        instance.midCanvas_Effects = new SimpleText[20];
        for (int i = 0; i < 20; i++)
        {
            string effect = $"MidCanvas/Panel/Effect_{i + 1:D2}";
            instance.midCanvas_Effects[i] = ComponentManager.GetComponent<SimpleText>(effect);
        }

        instance.midCanvas_Art = new AnimationImage[8];
        for (int i = 0; i < 8; i++)
        {
            string art = $"MidCanvas/Panel/Art/{i}";
            instance.midCanvas_Art[i] = ComponentManager.GetComponent<AnimationImage>(art);
            var target = instance.midCanvas_Art[i];
            Action<string, string> action = (string before, string current) =>
            {
                if(before == "FadeOut" && current == "IDLE")
                {
                    target.gameObjectData.SetEnabled(false);
                }
            };
            instance.midCanvas_Art[i].eventData.AddAnimationChangedEvent(action);
        }

        instance.midCanvas_SpecialBonus = ComponentManager.GetComponent<SimpleRectTransform>("MidCanvas/Panel/SpecialBonus");
        instance.midCanvas_SpecialBonus_Button = ComponentManager.GetComponent<AnimationButton>("MidCanvas/Panel/SpecialBonus/Button");

        // NavCanvas
        instance.navCanvas = ComponentManager.GetComponent<SimpleCanvas>("NavCanvas");
        instance.navCanvas_ChallengeNotifyMark = ComponentManager.GetComponent<SimpleImage>("NavCanvas/Panel/ChallengeNotifyMark");
        instance.navCanvas_RadioButton_TapUpgrade_Icon = ComponentManager.GetComponent<SimpleImage>("NavCanvas/Panel/RadioButton/TapUpgrade/Icon");
        instance.navCanvas_RadioButton_SecUpgrade_Icon = ComponentManager.GetComponent<SimpleImage>("NavCanvas/Panel/RadioButton/SecUpgrade/Icon");
        instance.navCanvas_RadioButton_Artifact_Icon = ComponentManager.GetComponent<SimpleImage>("NavCanvas/Panel/RadioButton/Artifact/Icon");
        instance.navCanvas_RadioButton_Challenge_Icon = ComponentManager.GetComponent<SimpleImage>("NavCanvas/Panel/RadioButton/Challenge/Icon");
        instance.navCanvas_RadioButton_Statistics_Icon = ComponentManager.GetComponent<SimpleImage>("NavCanvas/Panel/RadioButton/Statistics/Icon");

        // TapUpgrade
        instance.tapUpgrade = ComponentManager.GetComponent<ScrollCanvas>("TapUpgrade");
        instance.tapUpgrade_TapUpgrade_Button = ComponentManager.GetComponent<SimpleButton>("TapUpgrade/Panel/Viewport/Content/TapUpgrade/Button");
        instance.tapUpgrade_TapUpgrade_Name = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/TapUpgrade/Name");
        instance.tapUpgrade_TapUpgrade_Description = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/TapUpgrade/Description");
        instance.tapUpgrade_TapUpgrade_Price = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/TapUpgrade/Button/Price");

        instance.tapUpgrade_Skills_AutoTapButton = ComponentManager.GetComponent<SimpleButton>("TapUpgrade/Panel/Viewport/Content/Skills/AutoTapButton");
        instance.tapUpgrade_Skills_BonusTapButton = ComponentManager.GetComponent<SimpleButton>("TapUpgrade/Panel/Viewport/Content/Skills/BonusTapButton");
        instance.tapUpgrade_Skills_BonusSecButton = ComponentManager.GetComponent<SimpleButton>("TapUpgrade/Panel/Viewport/Content/Skills/BonusSecButton");
        instance.tapUpgrade_Skills_CoolDownButton = ComponentManager.GetComponent<SimpleButton>("TapUpgrade/Panel/Viewport/Content/Skills/CoolDownButton");
        instance.tapUpgrade_Skills_AutoTapButton_Text = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/Skills/AutoTapButton/Text");
        instance.tapUpgrade_Skills_BonusTapButton_Text = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/Skills/BonusTapButton/Text");
        instance.tapUpgrade_Skills_BonusSecButton_Text = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/Skills/BonusSecButton/Text");
        instance.tapUpgrade_Skills_CoolDownButton_Text = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/Skills/CoolDownButton/Text");

        instance.tapUpgrade_AutoTapUpgrade_Button = ComponentManager.GetComponent<SimpleButton>("TapUpgrade/Panel/Viewport/Content/AutoTapUpgrade/Button");
        instance.tapUpgrade_AutoTapUpgrade_Name = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/AutoTapUpgrade/Name");
        instance.tapUpgrade_AutoTapUpgrade_Description = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/AutoTapUpgrade/Description");
        instance.tapUpgrade_AutoTapUpgrade_Price = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/AutoTapUpgrade/Button/Price");

        instance.tapUpgrade_BonusTapUpgrade_Button = ComponentManager.GetComponent<SimpleButton>("TapUpgrade/Panel/Viewport/Content/BonusTapUpgrade/Button");
        instance.tapUpgrade_BonusTapUpgrade_Name = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/BonusTapUpgrade/Name");
        instance.tapUpgrade_BonusTapUpgrade_Description = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/BonusTapUpgrade/Description");
        instance.tapUpgrade_BonusTapUpgrade_Price = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/BonusTapUpgrade/Button/Price");

        instance.tapUpgrade_BonusSecUpgrade_Button = ComponentManager.GetComponent<SimpleButton>("TapUpgrade/Panel/Viewport/Content/BonusSecUpgrade/Button");
        instance.tapUpgrade_BonusSecUpgrade_Name = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/BonusSecUpgrade/Name");
        instance.tapUpgrade_BonusSecUpgrade_Description = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/BonusSecUpgrade/Description");
        instance.tapUpgrade_BonusSecUpgrade_Price = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/BonusSecUpgrade/Button/Price");

        instance.tapUpgrade_CoolDownUpgrade_Button = ComponentManager.GetComponent<SimpleButton>("TapUpgrade/Panel/Viewport/Content/CoolDownUpgrade/Button");
        instance.tapUpgrade_CoolDownUpgrade_Name = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/CoolDownUpgrade/Name");
        instance.tapUpgrade_CoolDownUpgrade_Description = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/CoolDownUpgrade/Description");
        instance.tapUpgrade_CoolDownUpgrade_Price = ComponentManager.GetComponent<SimpleText>("TapUpgrade/Panel/Viewport/Content/CoolDownUpgrade/Button/Price");

        // SecUpgrade
        instance.secUpgrade = ComponentManager.GetComponent<ScrollCanvas>("SecUpgrade");
        for (int i = 0; i < 15; i++)
        {
            string buttonName = "SecUpgrade/Panel/Viewport/Content/SecUpgrade" + (char)(i + 65) + "/Button";
            string nameName = "SecUpgrade/Panel/Viewport/Content/SecUpgrade" + (char)(i + 65) + "/Name";
            string descriptionName = "SecUpgrade/Panel/Viewport/Content/SecUpgrade" + (char)(i + 65) + "/Description";
            string priceName = "SecUpgrade/Panel/Viewport/Content/SecUpgrade" + (char)(i + 65) + "/Button/Price";

            instance.GetType().GetField("secUpgrade" + (char)(i + 65) + "_Button", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(instance, ComponentManager.GetComponent<SimpleButton>(buttonName));
            instance.GetType().GetField("secUpgrade" + (char)(i + 65) + "_Name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(instance, ComponentManager.GetComponent<SimpleText>(nameName));
            instance.GetType().GetField("secUpgrade" + (char)(i + 65) + "_Description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(instance, ComponentManager.GetComponent<SimpleText>(descriptionName));
            instance.GetType().GetField("secUpgrade" + (char)(i + 65) + "_Price", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(instance, ComponentManager.GetComponent<SimpleText>(priceName));
        }

        // Artifact
        instance.artifact = ComponentManager.GetComponent<ScrollCanvas>("Artifact");
        instance.artifact_prestigePointText = ComponentManager.GetComponent<SimpleText>("Artifact/Panel/Viewport/Content/PrestigePoint/Text");
        instance.artifact_Artifacts = new SimpleImage[15];
        for(int i = 0; i < 15; i++)
        {
            string artifact = $"Artifact/Panel/Viewport/Content/Artifact_{i + 1:D2}";
            string name = $"{artifact}/Name";
            string desc = $"{artifact}/Description";
            string methodName = $"GetArtifactBuff{i+1}";
            instance.artifact_Artifacts[i] = ComponentManager.GetComponent<SimpleImage>(artifact);
            ArtifactData buffData = typeof(ArtifactData).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static).Invoke(null, null) as ArtifactData;
            ComponentManager.GetComponent<SimpleText>(name).textData.SetText(buffData.name);
            ComponentManager.GetComponent<SimpleText>(desc).textData.SetText(buffData.description);
        }

        // Challenge
        instance.challenge = ComponentManager.GetComponent<ScrollCanvas>("Challenge");

        instance.challenge_HoldGold_Description = ComponentManager.GetComponent<SimpleText>("Challenge/Panel/Viewport/Content/Challenge_HoldGold/Description");
        instance.challenge_HoldGold_ValueBar = ComponentManager.GetComponent<ValueBar>("Challenge/Panel/Viewport/Content/Challenge_HoldGold/ValueBar");
        instance.challenge_HoldGold_Button = ComponentManager.GetComponent<SimpleButton>("Challenge/Panel/Viewport/Content/Challenge_HoldGold/Button");
        instance.challenge_HoldGold_RewardText = ComponentManager.GetComponent<SimpleText>("Challenge/Panel/Viewport/Content/Challenge_HoldGold/Button/Reward");

        instance.challenge_GoldPerTap_Description = ComponentManager.GetComponent<SimpleText>("Challenge/Panel/Viewport/Content/Challenge_GoldPerTap/Description");
        instance.challenge_GoldPerTap_ValueBar = ComponentManager.GetComponent<ValueBar>("Challenge/Panel/Viewport/Content/Challenge_GoldPerTap/ValueBar");
        instance.challenge_GoldPerTap_Button = ComponentManager.GetComponent<SimpleButton>("Challenge/Panel/Viewport/Content/Challenge_GoldPerTap/Button");
        instance.challenge_GoldPerTap_RewardText = ComponentManager.GetComponent<SimpleText>("Challenge/Panel/Viewport/Content/Challenge_GoldPerTap/Button/Reward");

        instance.challenge_GoldPerSec_Description = ComponentManager.GetComponent<SimpleText>("Challenge/Panel/Viewport/Content/Challenge_GoldPerSec/Description");
        instance.challenge_GoldPerSec_ValueBar = ComponentManager.GetComponent<ValueBar>("Challenge/Panel/Viewport/Content/Challenge_GoldPerSec/ValueBar");
        instance.challenge_GoldPerSec_Button = ComponentManager.GetComponent<SimpleButton>("Challenge/Panel/Viewport/Content/Challenge_GoldPerSec/Button");
        instance.challenge_GoldPerSec_RewardText = ComponentManager.GetComponent<SimpleText>("Challenge/Panel/Viewport/Content/Challenge_GoldPerSec/Button/Reward");

        instance.challenge_AdCount_Description = ComponentManager.GetComponent<SimpleText>("Challenge/Panel/Viewport/Content/Challenge_AdCount/Description");
        instance.challenge_AdCount_ValueBar = ComponentManager.GetComponent<ValueBar>("Challenge/Panel/Viewport/Content/Challenge_AdCount/ValueBar");
        instance.challenge_AdCount_Button = ComponentManager.GetComponent<SimpleButton>("Challenge/Panel/Viewport/Content/Challenge_AdCount/Button");
        instance.challenge_AdCount_RewardText = ComponentManager.GetComponent<SimpleText>("Challenge/Panel/Viewport/Content/Challenge_AdCount/Button/Reward");

        instance.challenge_PlayTime_Button = ComponentManager.GetComponent<SimpleButton>("Challenge/Panel/Viewport/Content/Challenge_PlayTime/Button");
        instance.challenge_PlayTime_ValueBar = ComponentManager.GetComponent<ValueBar>("Challenge/Panel/Viewport/Content/Challenge_PlayTime/ValueBar");
        instance.challenge_PlayTime_Description = ComponentManager.GetComponent<SimpleText>("Challenge/Panel/Viewport/Content/Challenge_PlayTime/Description");
        instance.challenge_PlayTime_RewardText = ComponentManager.GetComponent<SimpleText>("Challenge/Panel/Viewport/Content/Challenge_PlayTime/Button/Reward");

        instance.challenge_TapCount_Button = ComponentManager.GetComponent<SimpleButton>("Challenge/Panel/Viewport/Content/Challenge_TapCount/Button");
        instance.challenge_TapCount_ValueBar = ComponentManager.GetComponent<ValueBar>("Challenge/Panel/Viewport/Content/Challenge_TapCount/ValueBar");
        instance.challenge_TapCount_Description = ComponentManager.GetComponent<SimpleText>("Challenge/Panel/Viewport/Content/Challenge_TapCount/Description");
        instance.challenge_TapCount_RewardText = ComponentManager.GetComponent<SimpleText>("Challenge/Panel/Viewport/Content/Challenge_TapCount/Button/Reward");

        instance.challenge_PrestigeCount_Description = ComponentManager.GetComponent<SimpleText>("Challenge/Panel/Viewport/Content/Challenge_PrestigeCount/Description");
        instance.challenge_PrestigeCount_ValueBar = ComponentManager.GetComponent<ValueBar>("Challenge/Panel/Viewport/Content/Challenge_PrestigeCount/ValueBar");
        instance.challenge_PrestigeCount_Button = ComponentManager.GetComponent<SimpleButton>("Challenge/Panel/Viewport/Content/Challenge_PrestigeCount/Button");
        instance.challenge_PrestigeCount_RewardText = ComponentManager.GetComponent<SimpleText>("Challenge/Panel/Viewport/Content/Challenge_PrestigeCount/Button/Reward");

        instance.challenge_ArtifactCount_Description = ComponentManager.GetComponent<SimpleText>("Challenge/Panel/Viewport/Content/Challenge_ArtifactCount/Description");
        instance.challenge_ArtifactCount_ValueBar = ComponentManager.GetComponent<ValueBar>("Challenge/Panel/Viewport/Content/Challenge_ArtifactCount/ValueBar");
        instance.challenge_ArtifactCount_Button = ComponentManager.GetComponent<SimpleButton>("Challenge/Panel/Viewport/Content/Challenge_ArtifactCount/Button");
        instance.challenge_ArtifactCount_RewardText = ComponentManager.GetComponent<SimpleText>("Challenge/Panel/Viewport/Content/Challenge_ArtifactCount/Button/Reward");

        // Statistics
        instance.statistics = ComponentManager.GetComponent<ScrollCanvas>("Statistics");
        instance.statistics_currentDefaultGoldPerTapText = ComponentManager.GetComponent<SimpleText>("Statistics/Panel/Viewport/Content/CurrentDefaultGoldPerTap/Text");
        instance.statistics_currentDefaultGoldPerSecText = ComponentManager.GetComponent<SimpleText>("Statistics/Panel/Viewport/Content/CurrentDefaultGoldPerSec/Text");
        instance.statistics_highestDefaultGoldPerTapText = ComponentManager.GetComponent<SimpleText>("Statistics/Panel/Viewport/Content/HighestDefaultGoldPerTap/Text");
        instance.statistics_highestDefaultGoldPerSecText = ComponentManager.GetComponent<SimpleText>("Statistics/Panel/Viewport/Content/HighestDefaultGoldPerSec/Text");
        instance.statistics_highestGoldText = ComponentManager.GetComponent<SimpleText>("Statistics/Panel/Viewport/Content/HighestGold/Text");

        // TapTempBuff
        instance.tapTempBuff = ComponentManager.GetComponent<PopupCanvas>("TapTempBuff");
        instance.tapTempBuff_ShowAD = ComponentManager.GetComponent<SimpleButton>("TapTempBuff/Panel/TapTempBuffShowAD");

        // SecTempBuff
        instance.secTempBuff = ComponentManager.GetComponent<PopupCanvas>("SecTempBuff");
        instance.secTempBuff_ShowAD = ComponentManager.GetComponent<SimpleButton>("SecTempBuff/Panel/SecTempBuffShowAD");

        // Prestige
        instance.prestige = ComponentManager.GetComponent<PopupCanvas>("Prestige");
        instance.prestige_Text = ComponentManager.GetComponent<SimpleText>("Prestige/Panel/Text");
        instance.prestige_Button = ComponentManager.GetComponent<SimpleButton>("Prestige/Panel/Button");

        // Gacha
        instance.gacha = ComponentManager.GetComponent<PopupCanvas>("Gacha");
        instance.gacha_Image = ComponentManager.GetComponent<SimpleImage>("Gacha/Panel/Image");
        instance.gacha_AnimImage = ComponentManager.GetComponent<AnimationImage>("Gacha/Panel/AnimImage");
        instance.gacha_Text = ComponentManager.GetComponent<SimpleText>("Gacha/Panel/Text");
        instance.gacha_Button = ComponentManager.GetComponent<SimpleButton>("Gacha/Panel/Button");

        // Returning
        instance.returning = ComponentManager.GetComponent<PopupCanvas>("Returning");
        instance.returning_Text = ComponentManager.GetComponent<SimpleText>("Returning/Panel/Text");

        // Special Bonus
        instance.specialBonus = ComponentManager.GetComponent<PopupCanvas>("SpecialBonus");
        instance.specialBonus_Text = ComponentManager.GetComponent<SimpleText>("SpecialBonus/Panel/Text");
    }


    private static void SetAllField(ClickerTemp instance)
    {
        /// 게임 데이터 초기화
        var saveData = (instance.loadSavedData) ?PlayerSavedData.LoadPlayerData() : PlayerSavedData.CreateDefaultInstance();
        instance.tapTempBuffData = TempBuffData.GetTapTempBuffDataInstance();
        instance.secTempBuffData = TempBuffData.GetSecTempBuffDataInstance();
        instance.artifactList = ArtifactData.GetArtifactList(saveData.artifactList);
        instance.tapUpgradeData = saveData.tapUpgradeData;
        instance.secUpgradeList = saveData.secUpgradeList;

        instance.gameState = GameState.Normal;
        instance.scrollState = ScrollState.TapUpgrade;
        instance.popupState = PopupState.None;

        instance.volumeOn = saveData.volumeOn;
        instance._lastSec = (int)AppData.totalDeltaTime;
        instance.currentTicks = DateTime.Now.Ticks;
        instance.returningRewardGold = 0;
        instance.defaultGoldPerTap = saveData.defaultGoldPerTap;
        instance.defaultGoldPerSec = saveData.defaultGoldPerSec;
        instance.gold = saveData.gold;
        instance.returningRewardGold = saveData.returningRewardGold;
        instance.prestigePoint = saveData.prestigePoint;

        instance.adCount = saveData.adCount;
        instance.totalPlayTime = saveData.playTime;
        instance.tapCount = saveData.tapCount;
        instance.prestigeCount = saveData.prestigeCount;

        instance.challenge_HoldGold_Level = saveData.challenge_HoldGold_Level;
        instance.challenge_TapCount_Level = saveData.challenge_TapCount_Level;
        instance.challenge_GoldPerTap_Level = saveData.challenge_GoldPerTap_Level;
        instance.challenge_GoldPerSec_Level = saveData.challenge_GoldPerSec_Level;
        instance.challenge_PlayTime_Level = saveData.challenge_PlayTime_Level;
        instance.challenge_AdCount_Level = saveData.challenge_AdCount_Level;
        instance.challenge_PrestigeCount_Level = saveData.challenge_PrestigeCount_Level;
        instance.challenge_ArtifactCount_Level = saveData.challenge_ArtifactCount_Level;

        instance.highestDefaultGoldPerSec = saveData.highestDefaultGoldPerSec;
        instance.highestDefaultGoldPerTap = saveData.highestDefaultGoldPerTap;
        instance.highestGold = saveData.highestGold;

        /// calculate returning reward
        var currentDateTime = new DateTime(instance.currentTicks);
        var saveDateTime = new DateTime(saveData.ticks);
        var timeSpan = (int)(currentDateTime - saveDateTime).TotalSeconds;
        if(timeSpan > 1)
        {
            instance.popupState = PopupState.Returning;
            var coef = (instance.defaultGoldPerSec + instance.defaultGoldPerTap) / 2;
            instance.returningRewardGold += Math.Floor(coef * timeSpan * returningRewardRate);
        }

        instance.autoTapSkillData = saveData.autoTapSkillData;
        instance.bonusTapSkillData = saveData.bonusTapSkillData;
        instance.bonusSecSkillData = saveData.bonusSecSkillData;
        instance.coolDownSkillData = saveData.coolDownSkillData;
    }


    private static void SetAllEventData(ClickerTemp instance)
    {
        // TopCanvas
        instance.topCanvas_GoldPerTapText.eventData.AddDataUpdateEvent(instance.UpdateTopCanvasGoldPerTapText);
        instance.topCanvas_GoldPerSecText.eventData.AddDataUpdateEvent(instance.UpdateTopCanvasGoldPerSecText);
        instance.topCanvas_GoldText.eventData.AddDataUpdateEvent(instance.UpdateTopCanvasGoldText);
        instance.topCanvas_TapTempBuffBtn.eventData.AddDataUpdateEvent(instance.UpdateTopCanvasTapTempBuffBtn);
        instance.topCanvas_SecTempBuffBtn.eventData.AddDataUpdateEvent(instance.UpdateTopCanvasSecTempBuffBtn);
        instance.topCanvas_VolumeButton.eventData.AddDataUpdateEvent(instance.UpdateTopCanvasVolumeBtn);

        // MidCanvas
        instance.midCanvas.eventData.AddDataUpdateEvent(instance.UpdateMidCanvasTouchEffect);
        instance.midCanvas.eventData.AddDataUpdateEvent(instance.UpdateMidCanvasArtAnimation);
        instance.midCanvas_SpecialBonus.eventData.AddDataUpdateEvent(instance.UpdateMidCanvasSpecialBonus);

        // NavCanvas
        instance.navCanvas.eventData.AddDataUpdateEvent(instance.UpdateChallengeNotifyMark);

        // TapUpgrade
        instance.tapUpgrade.eventData.AddDataUpdateEvent(instance.UpdateTapUpgradeScroll);

        // SecUpgrade
        instance.secUpgrade.eventData.AddDataUpdateEvent(instance.UpdateSecUpgradeScroll);

        // Artifact
        instance.artifact.eventData.AddDataUpdateEvent(instance.UpdateArtifactScroll);
        instance.artifact_prestigePointText.eventData.AddDataUpdateEvent(instance.UpdateSpecialTokenText);

        // Challenge
        instance.challenge.eventData.AddDataUpdateEvent(instance.UpdateChallengeScroll);

        // Statistics
        instance.statistics.eventData.AddDataUpdateEvent(instance.UpdateStatisticsScroll);

        // Prestige
        instance.prestige_Text.eventData.AddDataUpdateEvent(instance.UpdatePrestigePopup);

        // Anim
        instance.anim.eventData.AddDataUpdateEvent(instance.BlockUserInput);
        instance.anim.eventData.AddDataUpdateEvent(instance.AllowUserInput);

        // Gacha
        instance.gacha.eventData.AddDataUpdateEvent(instance.UpdateGachaPopup);
        instance.gacha.eventData.AddDataUpdateEvent(instance.OnGachaAnimationStart);
        instance.gacha.eventData.AddDataUpdateEvent(instance.OnGachaAnimationEnd);

        // Returning
        instance.returning.eventData.AddDataUpdateEvent(instance.DisplayReturningPopup);

        // Special Bonus
        instance.specialBonus.eventData.AddDataUpdateEvent(instance.UpdateSpecialBonusPopup);
    }



    private static void SetInitEvent(ClickerTemp instance)
    {
        instance.Execute_UpdateTopCanvasGoldPerTapText = true;
        instance.Execute_UpdateTopCanvasGoldPerSecText = true;
        instance.Execute_UpdateTopCanvasGoldText = true;
        instance.Execute_UpdateTopCanvasVolumeBtn = true;
        instance.Execute_UpdateSpecialTokenText = true;
        instance.Execute_UpdateArtifactScroll = true;
        instance.Execute_UpdatePrestigeRewardText = true;
        instance.Execute_UpdateChallengeNotifyMark = true;
        instance.Execute_UpdateStatisticsScroll = true;
        instance.Execute_UpdateMidCanvasArtAnimation = true;
        if(instance.returningRewardGold > 0) instance.Execute_DisplayReturningPopup = true;
    }
}
