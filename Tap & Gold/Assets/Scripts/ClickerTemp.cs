using System.Collections.Generic;
using UnityEngine;
using System;
using static Unity.Mathematics.math;
public partial class ClickerTemp : MonoBehaviour
{
    public bool loadSavedData = true;

    /// 스프라이트 목록
    #region Sprite
    public Sprite[] artifactSprites;
    public Sprite[] volumeSprites;

    #endregion


    /// 오브젝트 목록
    #region GameObject

    /// InputBlocker
    SimpleCanvas inputBlocker;

    // Anim
    SimpleCanvas anim;
    AnimationImage anim_Prestige;

    // TopCanvas
    SimpleCanvas topCanvas;
    SimpleText topCanvas_GoldPerTapText, topCanvas_GoldPerSecText, topCanvas_GoldText;
    SimpleButton topCanvas_TapTempBuffBtn, topCanvas_SecTempBuffBtn;
    SimpleText topCanvas_TapTempBuffBtnText, topCanvas_SecTempBuffBtnText;
    SimpleButton topCanvas_VolumeButton;
    SimpleAudio topCanvas_VolumeButton_BGM;

    // MidCanvas
    SimpleCanvas midCanvas;
    SimpleText[] midCanvas_Effects;
    AnimationImage[] midCanvas_Art;
    SimpleRectTransform midCanvas_SpecialBonus;
    AnimationButton midCanvas_SpecialBonus_Button;
    

    // NavCanvas
    SimpleCanvas navCanvas;
    SimpleImage navCanvas_ChallengeNotifyMark;
    SimpleImage navCanvas_RadioButton_TapUpgrade_Icon;
    SimpleImage navCanvas_RadioButton_SecUpgrade_Icon;
    SimpleImage navCanvas_RadioButton_Artifact_Icon;
    SimpleImage navCanvas_RadioButton_Challenge_Icon;
    SimpleImage navCanvas_RadioButton_Statistics_Icon;

    // TapUpgrade
    ScrollCanvas tapUpgrade;
    SimpleButton tapUpgrade_TapUpgrade_Button; SimpleText tapUpgrade_TapUpgrade_Name, tapUpgrade_TapUpgrade_Description, tapUpgrade_TapUpgrade_Price;
    SimpleButton tapUpgrade_Skills_AutoTapButton, tapUpgrade_Skills_BonusTapButton, tapUpgrade_Skills_BonusSecButton, tapUpgrade_Skills_CoolDownButton; SimpleText tapUpgrade_Skills_AutoTapButton_Text, tapUpgrade_Skills_BonusTapButton_Text, tapUpgrade_Skills_BonusSecButton_Text, tapUpgrade_Skills_CoolDownButton_Text;
    SimpleButton tapUpgrade_AutoTapUpgrade_Button; SimpleText tapUpgrade_AutoTapUpgrade_Name, tapUpgrade_AutoTapUpgrade_Description, tapUpgrade_AutoTapUpgrade_Price;
    SimpleButton tapUpgrade_BonusTapUpgrade_Button; SimpleText tapUpgrade_BonusTapUpgrade_Name, tapUpgrade_BonusTapUpgrade_Description, tapUpgrade_BonusTapUpgrade_Price;
    SimpleButton tapUpgrade_BonusSecUpgrade_Button; SimpleText tapUpgrade_BonusSecUpgrade_Name, tapUpgrade_BonusSecUpgrade_Description, tapUpgrade_BonusSecUpgrade_Price;
    SimpleButton tapUpgrade_CoolDownUpgrade_Button; SimpleText tapUpgrade_CoolDownUpgrade_Name, tapUpgrade_CoolDownUpgrade_Description, tapUpgrade_CoolDownUpgrade_Price;

    // SecUpgrade
    ScrollCanvas secUpgrade;
    SimpleButton secUpgradeA_Button; SimpleText secUpgradeA_Name, secUpgradeA_Description, secUpgradeA_Price;
    SimpleButton secUpgradeB_Button; SimpleText secUpgradeB_Name, secUpgradeB_Description, secUpgradeB_Price;
    SimpleButton secUpgradeC_Button; SimpleText secUpgradeC_Name, secUpgradeC_Description, secUpgradeC_Price;
    SimpleButton secUpgradeD_Button; SimpleText secUpgradeD_Name, secUpgradeD_Description, secUpgradeD_Price;
    SimpleButton secUpgradeE_Button; SimpleText secUpgradeE_Name, secUpgradeE_Description, secUpgradeE_Price;
    SimpleButton secUpgradeF_Button; SimpleText secUpgradeF_Name, secUpgradeF_Description, secUpgradeF_Price;
    SimpleButton secUpgradeG_Button; SimpleText secUpgradeG_Name, secUpgradeG_Description, secUpgradeG_Price;
    SimpleButton secUpgradeH_Button; SimpleText secUpgradeH_Name, secUpgradeH_Description, secUpgradeH_Price;
    SimpleButton secUpgradeI_Button; SimpleText secUpgradeI_Name, secUpgradeI_Description, secUpgradeI_Price;
    SimpleButton secUpgradeJ_Button; SimpleText secUpgradeJ_Name, secUpgradeJ_Description, secUpgradeJ_Price;
    SimpleButton secUpgradeK_Button; SimpleText secUpgradeK_Name, secUpgradeK_Description, secUpgradeK_Price;
    SimpleButton secUpgradeL_Button; SimpleText secUpgradeL_Name, secUpgradeL_Description, secUpgradeL_Price;
    SimpleButton secUpgradeM_Button; SimpleText secUpgradeM_Name, secUpgradeM_Description, secUpgradeM_Price;
    SimpleButton secUpgradeN_Button; SimpleText secUpgradeN_Name, secUpgradeN_Description, secUpgradeN_Price;
    SimpleButton secUpgradeO_Button; SimpleText secUpgradeO_Name, secUpgradeO_Description, secUpgradeO_Price;
 
    // Artifact
    ScrollCanvas artifact;
    SimpleText artifact_prestigePointText;
    SimpleImage[] artifact_Artifacts;

    // Challenge
    ScrollCanvas challenge;
    SimpleText challenge_HoldGold_Description, challenge_HoldGold_RewardText; ValueBar challenge_HoldGold_ValueBar; SimpleButton challenge_HoldGold_Button;
    SimpleText challenge_GoldPerTap_Description, challenge_GoldPerTap_RewardText; ValueBar challenge_GoldPerTap_ValueBar; SimpleButton challenge_GoldPerTap_Button;
    SimpleText challenge_GoldPerSec_Description, challenge_GoldPerSec_RewardText; ValueBar challenge_GoldPerSec_ValueBar; SimpleButton challenge_GoldPerSec_Button;
    SimpleText challenge_AdCount_Description, challenge_AdCount_RewardText; ValueBar challenge_AdCount_ValueBar; SimpleButton challenge_AdCount_Button;
    SimpleText challenge_PlayTime_Description, challenge_PlayTime_RewardText; ValueBar challenge_PlayTime_ValueBar; SimpleButton challenge_PlayTime_Button;
    SimpleText challenge_TapCount_Description, challenge_TapCount_RewardText; ValueBar challenge_TapCount_ValueBar; SimpleButton challenge_TapCount_Button;
    SimpleText challenge_PrestigeCount_Description, challenge_PrestigeCount_RewardText; ValueBar challenge_PrestigeCount_ValueBar; SimpleButton challenge_PrestigeCount_Button;
    SimpleText challenge_ArtifactCount_Description, challenge_ArtifactCount_RewardText; ValueBar challenge_ArtifactCount_ValueBar; SimpleButton challenge_ArtifactCount_Button;

    // Statistics
    ScrollCanvas statistics;
    SimpleText statistics_currentDefaultGoldPerTapText, statistics_currentDefaultGoldPerSecText, statistics_highestDefaultGoldPerTapText, statistics_highestDefaultGoldPerSecText, statistics_highestGoldText;

    // TapTempBuff
    PopupCanvas tapTempBuff;
    SimpleButton tapTempBuff_ShowAD;

    // SecTempBuff
    PopupCanvas secTempBuff;
    SimpleButton secTempBuff_ShowAD;

    // Prestige
    PopupCanvas prestige;
    SimpleText prestige_Text;
    SimpleButton prestige_Button;


    // Gacha
    PopupCanvas gacha;
    SimpleImage gacha_Image;
    AnimationImage gacha_AnimImage;
    SimpleText gacha_Text;
    SimpleButton gacha_Button;

    // Returning
    PopupCanvas returning;
    SimpleText returning_Text;

    // Special Bonus
    PopupCanvas specialBonus;
    SimpleText specialBonus_Text;


    #endregion


    /// 데이터 목록
    #region Data
    public const float returningRewardRate = 0.03f;

    public (GameState before, GameState current) _gameState;
    public GameState gameState 
    {
        get{return _gameState.current;}
        set
        {
            _gameState.current = value;
        }
    }


    public (ScrollState before, ScrollState current) _scrollState;
    public ScrollState scrollState 
    {
        get{return _scrollState.current;}
        set
        {
            _scrollState.current = value;
        }
    }


    public (PopupState before, PopupState current) _popupState;
    public PopupState popupState 
    {
        get{return _popupState.current;}
        set
        {
            _popupState.current = value;
        }
    }
    

    public int _lastSec;
    public int lastSec
    {
        get{return _lastSec;}
        set
        {
            if(value == _lastSec) return;
            int delta = value - _lastSec;
            _lastSec = value;
            currentTicks = DateTime.Now.Ticks;
            totalPlayTime += 1;

            SaveData();

            if(gameState != GameState.Normal) return;
            
            bool isTapTempBuffOn = tapTempBuffData.GetIsBuffOn();
            bool isSecTempBuffOn = secTempBuffData.GetIsBuffOn();

            if(isTapTempBuffOn) 
            {
                tapTempBuffData.SetRemainedTime(tapTempBuffData.GetRemainedTime() - delta);
            }

            if(isSecTempBuffOn) 
            {
                secTempBuffData.SetRemainedTime(secTempBuffData.GetRemainedTime() - delta);
            }

            gold += realGoldPerSec;
            if(gold > highestGold) highestGold = gold;       

            Execute_UpdateTopCanvasTapTempBuffBtn = true;
            Execute_UpdateTopCanvasSecTempBuffBtn = true;
            Execute_UpdateTopCanvasGoldPerTapText = true;
            Execute_UpdateTopCanvasGoldPerSecText = true;
            if((_lastSec % 300 == 30 || _lastSec % 300 == 45) && popupState != PopupState.SpecialBonus && RewardAdRequest.CanShowAd()) Execute_UpdateMidCanvasSpecialBonus = true;

        }
    }

    public long currentTicks;

    public double returningRewardGold;

    public double realGoldPerTap
    {
        get
        {
            double tapBonusSum = 0;
            double tapPlusSum = 0;
            foreach(ArtifactData buff in artifactList)
            {
                tapBonusSum += buff.tapBonus;
                tapPlusSum += buff.tapPlus;
            }
            double temp = defaultGoldPerTap * (1 + tapBonusSum) + tapPlusSum;
            if(tapTempBuffData.GetIsBuffOn()) { temp *= tapTempBuffData.tapBonus; }
            if(bonusTapSkillData.IsOnActivated()) temp *= bonusTapSkillData.GetBonusPerTap();
            return temp;
        }
    }

    public double _defaultGoldPerTap;
    public double defaultGoldPerTap
    {
        get{return _defaultGoldPerTap;}
        set
        {
            if(value == _defaultGoldPerTap) return;
            Execute_UpdatePrestigeRewardText = true;
            Execute_UpdateChallengeNotifyMark = true;
            Execute_UpdateStatisticsScroll = true;
            Execute_UpdateMidCanvasArtAnimation = true;
            _defaultGoldPerTap = value;
        }
    }


    public double realGoldPerSec
    {
        get
        {
            double secBonusSum = 0;
            double secPlusSum = 0;
            foreach(ArtifactData buff in artifactList)
            {
                secBonusSum += buff.secBonus;
                secPlusSum += buff.secPlus;
            }
            double temp =  defaultGoldPerSec * (1 + secBonusSum) + secPlusSum;
            if(secTempBuffData.GetIsBuffOn()) { temp *= secTempBuffData.secBonus; }
            if(bonusSecSkillData.IsOnActivated()) temp *= bonusSecSkillData.GetBonusPerSec();
            return temp;
        }
    }

    public double _defaultGoldPerSec;
    public double defaultGoldPerSec
    {
        get{return _defaultGoldPerSec;}
        set
        {
            if(value == _defaultGoldPerSec) return;
            Execute_UpdatePrestigeRewardText = true;
            Execute_UpdateChallengeNotifyMark = true;
            Execute_UpdateStatisticsScroll = true;
            Execute_UpdateMidCanvasArtAnimation = true;
            _defaultGoldPerSec = value;
        }
    }

    public double _gold;
    public double gold
    {
        get{return _gold;}
        set
        {
            if(value == _gold) return;
            Execute_UpdateTopCanvasGoldText = true;
            Execute_UpdateTapUpgradeScroll = true;
            Execute_UpdateSecUpgradeScroll = true;
            Execute_UpdateChallengeNotifyMark = true;
            _gold = value;
        }
    }

    public bool _volumeOn;
    public bool volumeOn
    {
        get{return _volumeOn;}
        set
        {
            if(value == _volumeOn) return;
            _volumeOn = value;
            Execute_UpdateTopCanvasVolumeBtn = true;
        }
    }

    public int _prestigePoint;
    public int prestigePoint
    {
        get{return _prestigePoint;}
        set
        {
            if(value == _prestigePoint) return;
            Execute_UpdateSpecialTokenText = true;
            Execute_UpdateChallengeNotifyMark = true;
            _prestigePoint = value;
        }
    }

    public TempBuffData tapTempBuffData, secTempBuffData;
    public List<ArtifactData> artifactList;
    public UpgradeData tapUpgradeData;
    public UpgradeData[] secUpgradeList;
    public AutoTapSkillData autoTapSkillData; 
    public BonusTapSkillData bonusTapSkillData; 
    public BonusSecSkillData bonusSecSkillData; 
    public CoolDownSkillData coolDownSkillData;
    #endregion


    /// 통계 및 업적 전용 데이터 목록
    #region Stat Data
    public int challenge_HoldGold_Level;
    public int challenge_TapCount_Level;
    public int challenge_GoldPerTap_Level;
    public int challenge_GoldPerSec_Level;
    public int challenge_AdCount_Level;
    public int challenge_PlayTime_Level;
    public int challenge_PrestigeCount_Level;
    public int challenge_ArtifactCount_Level;

    public double _highestDefaultGoldPerTap;
    public double highestDefaultGoldPerTap
    {
        get{return _highestDefaultGoldPerTap;}
        set
        {
            if(value == _highestDefaultGoldPerTap) return;
            Execute_UpdateStatisticsScroll = true;
            Execute_UpdateChallengeNotifyMark = true;
            _highestDefaultGoldPerTap = value;
        }
    }

    public double _highestDefaultGoldPerSec;
    public double highestDefaultGoldPerSec
    {
        get{return _highestDefaultGoldPerSec;}
        set
        {
            if(value == _highestDefaultGoldPerSec) return;
            Execute_UpdateStatisticsScroll = true;
            Execute_UpdateChallengeNotifyMark = true;
            _highestDefaultGoldPerSec = value;
        }
    }

    public double _highestGold;
    public double highestGold
    {
        get{return _highestGold;}
        set
        {
            if(value == _highestGold) return;
            Execute_UpdateStatisticsScroll = true;
            Execute_UpdateChallengeNotifyMark = true;
            _highestGold = value;
        }
    }

    public int _adCount;
    public int adCount
    {
        get{return _adCount;}
        set
        {
            if(value == _adCount) return;
            Execute_UpdateChallengeNotifyMark = true;
            _adCount = value;
        }
    }

    public int _totalPlayTime;
    public int totalPlayTime
    {
        get{return _totalPlayTime;}
        set
        {
            if(value == _totalPlayTime) return;
            Execute_UpdateChallengeNotifyMark = true;
            _totalPlayTime = value;
        }
    }

    public int _tapCount;
    public int tapCount
    {
        get{return _tapCount;}
        set
        {
            if(value == _tapCount) return;
            Execute_UpdateChallengeNotifyMark = true;
            _tapCount = value;
        }
    }

    public int _prestigeCount;
    public int prestigeCount
    {
        get{return _prestigeCount;}
        set
        {
            if(value == _prestigeCount) return;
            Execute_UpdateChallengeNotifyMark = true;
            _prestigeCount = value;
        }
    }
    #endregion


    /// DataUpdate 이벤트 실행 여부 목록
    #region Execute_Boolean
    private bool Execute_AllowUserInput = false;
    private bool Execute_BlockUserInput = false;
    private bool Execute_UpdateSpecialTokenText = false;
    private bool Execute_UpdatePrestigeRewardText = false;
    private bool Execute_UpdateSecUpgradeScroll = false;
    private bool Execute_UpdateTapUpgradeScroll = false;
    private bool Execute_UpdateStatisticsScroll = false;
    private bool Execute_UpdateTopCanvasGoldPerTapText = false;
    private bool Execute_UpdateTopCanvasGoldPerSecText = false;
    private bool Execute_UpdateTopCanvasGoldText = false;
    private bool Execute_UpdateTopCanvasTapTempBuffBtn = false;
    private bool Execute_UpdateTopCanvasSecTempBuffBtn = false;
    private bool Execute_UpdateTopCanvasVolumeBtn = false;
    private bool Execute_UpdateArtifactScroll = false;
    private bool Execute_UpdateGachaPopup = false;
    private bool Execute_OnGachaAnimationStart = false;
    private bool Execute_OnGachaAnimationEnd = false;
    private bool Execute_UpdateChallengeNotifyMark = false;
    private bool Execute_UpdateChallengeScroll = false;
    private bool Execute_DisplayReturningPopup = false;
    private bool Execute_UpdateMidCanvasTouchEffect = false;
    private bool Execute_UpdateMidCanvasArtAnimation = false;
    private bool Execute_UpdateMidCanvasSpecialBonus = false;
    private bool Execute_UpdateSpecialBonusPopup = false;
    #endregion


    #region Effect Data
    private double[] midCanvasArtTiming = {1, 1e4, 1e12, 1e20, 1e28, 1e36, 1e44, 1e52};
    private List<Vector2> tap_normalizedPosList  = new List<Vector2>();
    #endregion


    void Update()
    {
        UpdateGameState();
        UpdateScrollState();
        UpdatePopupState();
        lastSec = (int)AppData.totalDeltaTime;
    }


    private void UpdateGameState()
    {
        var before = _gameState.before;
        var current = _gameState.current;

        if(current == GameState.Normal)
        {
            if(before == GameState.Normal)
            {
                autoTapSkillData.UpdateData(this);
                bonusTapSkillData.UpdateData();
                bonusSecSkillData.UpdateData();
                coolDownSkillData.UpdateData();
                StartTapEffect();
                Execute_UpdateMidCanvasTouchEffect = true;
            }
        }


        else if(current == GameState.PrestigeAnimation)
        {
            if(before == GameState.PrestigeAnimation)
            {
                if(anim_Prestige.animationData.GetCurrentStateChanged() && anim_Prestige.animationData.IsIDLE())
                {
                    anim.SetDisplay(false);
                    prestigePoint += GetPrestigeReward();
                    secTempBuffData.SetRemainedTime(0);
                    tapTempBuffData.SetRemainedTime(0);

                    defaultGoldPerTap = 1;
                    defaultGoldPerSec = 5;
                    gold = 0;

                    tapUpgradeData = UpgradeData.GetTapUpgradeData();
                    secUpgradeList = UpgradeData.GetSecUpgradeList();

                    Execute_UpdateTopCanvasSecTempBuffBtn = true;
                    Execute_UpdateTopCanvasTapTempBuffBtn = true;
                    Execute_UpdateTopCanvasGoldPerSecText = true;
                    Execute_UpdateTopCanvasGoldPerTapText = true;
                    Execute_UpdateChallengeScroll = true;
                    Execute_UpdateChallengeNotifyMark = true;
                    Execute_UpdateTapUpgradeScroll = true;
                    Execute_UpdateSecUpgradeScroll = true;
                    Execute_UpdateMidCanvasArtAnimation = true;
                    gameState = GameState.Normal;
                }
            }
        }


        else if(current == GameState.GachaAnimation)
        {
            if(before == GameState.GachaAnimation)
            {
                if(gacha_AnimImage.animationData.IsIDLE()) // 애니메이션 종료 조건
                {
                    Execute_AllowUserInput = true;
                    Execute_OnGachaAnimationEnd = true;
                    Execute_UpdateArtifactScroll = true;
                    Execute_UpdateChallengeNotifyMark = true;
                    Execute_UpdateTopCanvasGoldPerSecText = true;
                    Execute_UpdateTopCanvasGoldPerTapText = true;
                    Execute_UpdateTopCanvasGoldPerSecText = true;
                    Execute_UpdateTopCanvasGoldPerTapText = true;
                    gameState = GameState.Normal;
                }
            }
        }


        else if(current == GameState.Advertisement)
        {
            
        }


        _gameState.before = _gameState.current;
    }


    private void UpdateScrollState()
    {
        var before = _scrollState.before;
        var current = _scrollState.current;

        if(current == ScrollState.TapUpgrade)
        {
            Execute_UpdateTapUpgradeScroll = true;
        }


        else if(current == ScrollState.SecUpgrade)
        {
            Execute_UpdateSecUpgradeScroll = true;
        }


        else if(current == ScrollState.Artifact)
        {
            if(before == ScrollState.Artifact)
            {

            }
        }


        else if(current == ScrollState.Challenge)
        {
            if(before == ScrollState.Challenge)
            {

            }
        }


        else if(current == ScrollState.Statistics)
        {
           
        }

        _scrollState.before = _scrollState.current;
    }


    private void UpdatePopupState()
    {
        var before = _popupState.before;
        var current = _popupState.current;

        if(current == PopupState.None)
        {
            
        }


        else if(current == PopupState.TapTempBuff)
        {
            
        }


        else if(current == PopupState.SecTempBuff)
        {
            
        }


        else if(current == PopupState.Prestige)
        {
           
        }


        else if(current == PopupState.Gacha)
        {
           
        }

        _popupState.before = _popupState.current;
    }


    private void SaveData()
    {
        PlayerSavedData.SavePlayerData(this);
    }


    private int GetPrestigeReward()
    {
        int log = (int)(log10(defaultGoldPerTap + defaultGoldPerSec) / 2);
        int reward = (int)(pow(log, 1.25));
        reward = (int)ArtifactData.PrestigeBonus(artifactList, reward);
        reward = max(reward - 8, 0);
        return reward;
    }

    private void StartTapEffect()
    {
        int count = tap_normalizedPosList.Count;
        if(count == 0) return;

        for (int i = 0; i < midCanvas_Effects.Length; i++)
        {
            if(midCanvas_Effects[i].gameObjectData.GetEnabled()) continue;
            count--;
            midCanvas_Effects[i].gameObjectData.SetEnabled(true);
            midCanvas_Effects[i].rectTransformData.SetAnchorPos(tap_normalizedPosList[count]);
            var color = midCanvas_Effects[i].textData.GetColor();
            color.a = 1;
            midCanvas_Effects[i].textData.SetColor(color);
            string numericalText_RealGoldPerTap = realGoldPerTap < 1000000 ? realGoldPerTap.ToString("N0") : realGoldPerTap.ToString("0.000e0");
            midCanvas_Effects[i].textData.SetText($"+<sprite=1> {numericalText_RealGoldPerTap}");
            if(count == 0) break;
        }

        tap_normalizedPosList.Clear();
        
    }
}


[System.Serializable]
public enum GameState
{
    Normal,
    PrestigeAnimation,
    GachaAnimation,
    Advertisement,
}

[System.Serializable]
public enum ScrollState
{
    TapUpgrade,
    SecUpgrade,
    Artifact,
    Challenge,
    Statistics
}

[System.Serializable]
public enum PopupState
{
    None,
    TapTempBuff,
    SecTempBuff,
    Prestige,
    Gacha,
    Returning,
    SpecialBonus,
}