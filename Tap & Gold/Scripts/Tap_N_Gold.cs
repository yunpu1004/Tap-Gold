using System.Collections.Generic;
using UnityEngine;
using System;
using static Unity.Mathematics.math;

/// 이 cs파일은 게임의 데이터, 업데이트 흐름을 관리합니다.
/// 게임에 필요한 모든 객체, 데이터, 플래그 등은 이 파일에 선언되어 있습니다.
public partial class Tap_N_Gold : MonoBehaviour
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
    

    /// 1초마다 1씩 증가하는 변수입니다.
    /// 값이 변경될때마다 아래의 작업을 합니다.
    ///     1. 총 플레이 시간을 1초 증가
    ///     2. 초당 얻는 골드를 획득
    ///     3. 모든 버프, 스킬의 지속시간과 쿨타임을 1초 감소
    ///     4. 변경된 데이터에 대한 UI 업데이트
    ///     5. 5분마다 광고 보상 팝업을 활성화
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


    /// 플레이어가 화면을 터치할때마다 실제로 얻는 골드입니다.
    /// 탭당 기본 골드 획득량에 버프, 스킬, 아티팩트 등의 보너스를 적용한 값입니다.
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

    /// 플레이어의 탭당 기본 골드 획득량입니다.
    /// 업그레이드로 인하여 이 값이 변경될때마다 관련된 UI를 업데이트 합니다.
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

    /// 플레이어가 1초마다 실제로 얻는 골드입니다.
    /// 초당 기본 골드 획득량에 버프, 스킬, 아티팩트 등의 보너스를 적용한 값입니다.
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


    /// 플레이어의 초당 기본 골드 획득량입니다.
    /// 업그레이드로 인하여 이 값이 변경될때마다 관련된 UI를 업데이트 합니다.
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


    /// 플레이어가 보유한 골드입니다.
    /// 골드가 변경될때마다 관련된 UI를 업데이트 합니다.
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


    /// 플레이어의 볼륨 설정입니다.
    /// 볼륨 설정이 변경될때마다 볼륨 아이콘을 업데이트 합니다.
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


    /// 플레이어의 프레스티지 포인트입니다.
    /// 프레스티지 포인트가 변경될때마다 관련된 UI를 업데이트 합니다.
    public int _prestigePoint;
    public int prestigePoint
    {
        get{return _prestigePoint;}
        set
        {
            if(value == _prestigePoint) return;
            Execute_UpdatePrestigePointText = true;
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
    private bool Execute_UpdatePrestigePointText = false;
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
    private bool Execute_UpdateMidCanvasTouchEffectActivation = false;
    private bool Execute_UpdateMidCanvasTouchEffectFadeOut = false;
    private bool Execute_UpdateMidCanvasArtAnimation = false;
    private bool Execute_UpdateMidCanvasSpecialBonus = false;
    private bool Execute_UpdateSpecialBonusPopup = false;
    #endregion


    /// 배경이미지 전환용 데이터 목록
    #region Effect Data
    private double[] midCanvasArtTiming = {1, 1e4, 1e12, 1e20, 1e28, 1e36, 1e44, 1e52};
    private Queue<Vector2> tap_normalizedPosQueue  = new ();
    #endregion



    /// 이 게임의 메인 업데이트 함수입니다.
    /// GameState -> ScrollState -> PopupState 순으로 업데이트를 진행합니다.
    void Update()
    {
        UpdateGameState();
        UpdateScrollState();
        lastSec = (int)AppData.totalDeltaTime;
    }


    /// GameState 업데이트 함수입니다.
    private void UpdateGameState()
    {
        var before = _gameState.before;
        var current = _gameState.current;

        /// 게임 상태가 노말일때
        if(current == GameState.Normal)
        {
            /// 스킬의 지속시간 및 쿨타임을 갱신시키고, 탭 이펙트를 업데이트 합니다.
            if(before == GameState.Normal)
            {
                autoTapSkillData.UpdateData(this);
                bonusTapSkillData.UpdateData();
                bonusSecSkillData.UpdateData();
                coolDownSkillData.UpdateData();
                Execute_UpdateMidCanvasTouchEffectFadeOut = true;
            }
        }


        /// 게임 상태가 프레스티지 애니메이션일때
        else if(current == GameState.PrestigeAnimation)
        {
            /// 프레스티지 애니메이션이 재생중일때
            if(before == GameState.PrestigeAnimation)
            {
                /// 프레스티지 애니메이션이 종료되어서 IDLE 상태가 되었는지 확인
                if(anim_Prestige.animationData.GetCurrentStateChanged() && anim_Prestige.animationData.IsIDLE())
                {
                    /// 프레스티지 애니메이션을 비활성화하고, 프레스티지 포인트를 획득합니다.
                    anim.SetDisplay(false);
                    prestigePoint += GetPrestigeReward();

                    /// 버프, 업그레이드, 보유골드를 초기화하고 게임 상태를 노말로 변경합니다.
                    secTempBuffData.SetRemainedTime(0);
                    tapTempBuffData.SetRemainedTime(0);
                    defaultGoldPerTap = 1;
                    defaultGoldPerSec = 5;
                    gold = 0;
                    tapUpgradeData = UpgradeData.GetTapUpgradeData();
                    secUpgradeList = UpgradeData.GetSecUpgradeList();
                    gameState = GameState.Normal;

                    /// 관련 UI를 업데이트 합니다.
                    Execute_UpdateTopCanvasSecTempBuffBtn = true;
                    Execute_UpdateTopCanvasTapTempBuffBtn = true;
                    Execute_UpdateTopCanvasGoldPerSecText = true;
                    Execute_UpdateTopCanvasGoldPerTapText = true;
                    Execute_UpdateChallengeScroll = true;
                    Execute_UpdateChallengeNotifyMark = true;
                    Execute_UpdateTapUpgradeScroll = true;
                    Execute_UpdateSecUpgradeScroll = true;
                    Execute_UpdateMidCanvasArtAnimation = true;
                    
                }
            }
        }


        /// 게임 상태가 가챠 애니메이션일때
        else if(current == GameState.GachaAnimation)
        {
            /// 가챠 애니메이션이 재생중일때
            if(before == GameState.GachaAnimation)
            {
                /// 가챠 애니메이션이 종료되어서 IDLE 상태가 되었는지 확인
                if(gacha_AnimImage.animationData.IsIDLE()) 
                {
                    /// 게임 상태를 노말로 변경하고, 관련 UI를 업데이트 합니다.
                    gameState = GameState.Normal;
                    Execute_AllowUserInput = true;
                    Execute_OnGachaAnimationEnd = true;
                    Execute_UpdateArtifactScroll = true;
                    Execute_UpdateChallengeNotifyMark = true;
                    Execute_UpdateTopCanvasGoldPerSecText = true;
                    Execute_UpdateTopCanvasGoldPerTapText = true;
                    Execute_UpdateTopCanvasGoldPerSecText = true;
                    Execute_UpdateTopCanvasGoldPerTapText = true;
                }
            }
        }


        else if(current == GameState.Advertisement)
        {
            
        }

        /// 현재 프레임의 GameState 를 기록합니다. 
        _gameState.before = _gameState.current;
    }



    /// ScrollState 업데이트 함수입니다.
    private void UpdateScrollState()
    {
        var before = _scrollState.before;
        var current = _scrollState.current;

        /// 탭 업그레이드 스크롤이 활성화 되있으면 탭 업그레이드 스크롤을 업데이트 합니다.
        if(current == ScrollState.TapUpgrade)
        {
            if(before != ScrollState.TapUpgrade)
            {
                /// 이번 프레임에 스크롤을 전환했다면 현재 열린 스크롤의 UI를 업데이트 합니다.
                Execute_UpdateTapUpgradeScroll = true;
            }
            else
            {
                /// 스크롤 전환이 없는 경우 입니다.
            }
        }

        /// 초당 업그레이드 스크롤이 활성화 되있으면 초당 업그레이드 스크롤을 업데이트 합니다.
        else if(current == ScrollState.SecUpgrade)
        {
            if(before != ScrollState.SecUpgrade)
            {
                Execute_UpdateSecUpgradeScroll = true;
            }
            else
            {

            }
        }
        
        else if(current == ScrollState.Artifact)
        {
            if(before != ScrollState.Artifact)
            {

            }
            else
            {

            }
        }


        else if(current == ScrollState.Challenge)
        {
            if(before != ScrollState.Challenge)
            {

            }
            else
            {

            }
        }


        else if(current == ScrollState.Statistics)
        {
            if(before != ScrollState.Statistics)
            {

            }
            else
            {

            }
        }

        _scrollState.before = _scrollState.current;
    }


    /// 게임의 데이터를 저장합니다.
    private void SaveData()
    {
        PlayerSavedData.SavePlayerData(this);
    }


    /// 프레스티지 보상을 계산합니다.
    private int GetPrestigeReward()
    {
        int log = (int)(log10(defaultGoldPerTap + defaultGoldPerSec) / 2);
        int reward = (int)(pow(log, 1.25));
        reward = (int)ArtifactData.PrestigeBonus(artifactList, reward);
        reward = max(reward - 8, 0);
        return reward;
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