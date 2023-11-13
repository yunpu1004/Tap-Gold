using System;
using UnityEngine;

/// 이 cs파일은 도전과제 스크롤의 UI를 관리합니다.
public partial class Tap_N_Gold
{
 
    /// === 실행 조건 ===
    /// 1. 하단 메뉴에서 Challenge 버튼을 눌렀을때
    /// 2. Challenge 스크롤이 활성화 된 상태에서 게임 내부 데이터가 변경될때
    /// 3. 도전과제 달성 버튼을 눌러서 다음 레벨의 도전과제로 넘어갈때
    /// 4. 게임을 시작할때
    /// === 실행 내용 ===
    /// 1. 각 도전과제의 달성 여부를 확인
    /// 2. 만약 하나라도 달성되었다면 도전과제의 달성을 알려주는 UI를 활성화
    /// 3. 달성한 도전과제가 없다면 UI를 비활성화
    private void UpdateChallengeScroll()
    {
        if(!Execute_UpdateChallengeScroll) return;
        Execute_UpdateChallengeScroll = false;

        /// Challenge_HoldGold
        double Challenge_HoldGold_Target = Math.Pow(10, challenge_HoldGold_Level * 2 + 5);
        bool condition_HoldGold = gold >= Challenge_HoldGold_Target;
        challenge_HoldGold_Button.SetInteractable(condition_HoldGold);
        challenge_HoldGold_Button.SetRaycastTarget(condition_HoldGold);
        challenge_HoldGold_Button.spriteData.SetColor(condition_HoldGold ? new Color(0.5f, 0.9f, 0.5f, 1) : Color.gray);
        string numericalText_Gold = Challenge_HoldGold_Target < 1000000 ? Challenge_HoldGold_Target.ToString("N0") : Challenge_HoldGold_Target.ToString("0.000e0");
        challenge_HoldGold_Description.textData.SetText($"Hold <sprite=1> {numericalText_Gold}");
        challenge_HoldGold_ValueBar.SetBarValue((float)Math.Round(gold / Challenge_HoldGold_Target, 2));
        challenge_HoldGold_RewardText.textData.SetText($"<sprite=0> 2");

        /// Challenge_GoldPerTap
        double Challenge_GoldPerTap_Target = Math.Pow(10, challenge_GoldPerTap_Level * 2 + 2);
        bool condition_GoldPerTap = defaultGoldPerTap >= Challenge_GoldPerTap_Target;
        challenge_GoldPerTap_Button.SetInteractable(condition_GoldPerTap);
        challenge_GoldPerTap_Button.SetRaycastTarget(condition_GoldPerTap);
        challenge_GoldPerTap_Button.spriteData.SetColor(condition_GoldPerTap ? new Color(0.5f, 0.9f, 0.5f, 1) : Color.gray);
        string numericalText_TapGold = Challenge_GoldPerTap_Target < 1000000 ? Challenge_GoldPerTap_Target.ToString("N0") : Challenge_GoldPerTap_Target.ToString("0.000e0");
        challenge_GoldPerTap_Description.textData.SetText($"Reach <sprite=1> {numericalText_TapGold} / Tap (Default)");
        challenge_GoldPerTap_ValueBar.SetBarValue((float)Math.Round(defaultGoldPerTap / Challenge_GoldPerTap_Target, 2));
        challenge_GoldPerTap_RewardText.textData.SetText($"<sprite=0> 2");

        /// Challenge_GoldPerSec
        double Challenge_GoldPerSec_Target = Math.Pow(10, challenge_GoldPerSec_Level * 2 + 2);
        bool condition_GoldPerSec = defaultGoldPerSec >= Challenge_GoldPerSec_Target;
        challenge_GoldPerSec_Button.SetInteractable(condition_GoldPerSec);
        challenge_GoldPerSec_Button.SetRaycastTarget(condition_GoldPerSec);
        challenge_GoldPerSec_Button.spriteData.SetColor(condition_GoldPerSec ? new Color(0.5f, 0.9f, 0.5f, 1) : Color.gray);
        string numericalText_SecGold = Challenge_GoldPerSec_Target < 1000000 ? Challenge_GoldPerSec_Target.ToString("N0") : Challenge_GoldPerSec_Target.ToString("0.000e0");
        challenge_GoldPerSec_Description.textData.SetText($"Reach <sprite=1> {numericalText_SecGold} / Sec (Default)");
        challenge_GoldPerSec_ValueBar.SetBarValue((float)Math.Round(defaultGoldPerSec / Challenge_GoldPerSec_Target, 2));
        challenge_GoldPerSec_RewardText.textData.SetText($"<sprite=0> 2");
        
        /// Challenge_AdCount
        int Challenge_AdCount_Target = challenge_AdCount_Level * 5 + 5;
        bool condition_AdCount = adCount >= Challenge_AdCount_Target;
        challenge_AdCount_Button.SetInteractable(condition_AdCount);
        challenge_AdCount_Button.SetRaycastTarget(condition_AdCount);
        challenge_AdCount_Button.spriteData.SetColor(condition_AdCount ? new Color(0.5f, 0.9f, 0.5f, 1) : Color.gray);
        challenge_AdCount_Description.textData.SetText($"Watch {Challenge_AdCount_Target} Ads");
        challenge_AdCount_ValueBar.SetBarValue(adCount / (float)Challenge_AdCount_Target);
        challenge_AdCount_RewardText.textData.SetText($"<sprite=0> 2");

        /// Challenge_PlayTime
        int Challenge_PlayTime_Minute_Target = challenge_PlayTime_Level * 30 + 30;
        int Challenge_PlayTime_Minute = totalPlayTime / 60;
        bool condition_PlayTime = Challenge_PlayTime_Minute >= Challenge_PlayTime_Minute_Target;
        challenge_PlayTime_Button.SetInteractable(condition_PlayTime);
        challenge_PlayTime_Button.SetRaycastTarget(condition_PlayTime);
        challenge_PlayTime_Button.spriteData.SetColor(condition_PlayTime ? new Color(0.5f, 0.9f, 0.5f, 1) : Color.gray);
        challenge_PlayTime_Description.textData.SetText($"Play Game For {Challenge_PlayTime_Minute_Target} Minutes");
        challenge_PlayTime_ValueBar.SetBarValue(Challenge_PlayTime_Minute / (float)Challenge_PlayTime_Minute_Target);
        challenge_PlayTime_RewardText.textData.SetText($"<sprite=0> 2");

        /// Challenge_TapCount
        int Challenge_TapCount_Target = challenge_TapCount_Level * 500 + 500;
        bool condition_TapCount = tapCount >= Challenge_TapCount_Target;
        challenge_TapCount_Button.SetInteractable(condition_TapCount);
        challenge_TapCount_Button.SetRaycastTarget(condition_TapCount);
        challenge_TapCount_Button.spriteData.SetColor(condition_TapCount ? new Color(0.5f, 0.9f, 0.5f, 1) : Color.gray);
        challenge_TapCount_Description.textData.SetText($"Tap {Challenge_TapCount_Target} Times");
        challenge_TapCount_ValueBar.SetBarValue(tapCount / (float)Challenge_TapCount_Target);
        challenge_TapCount_RewardText.textData.SetText($"<sprite=0> 2");

        /// Challenge_PrestigeCount
        int Challenge_PrestigeCount_Target = challenge_PrestigeCount_Level * 3 + 3;
        bool condition_PrestigeCount = prestigeCount >= Challenge_PrestigeCount_Target;
        challenge_PrestigeCount_Button.SetInteractable(condition_PrestigeCount);
        challenge_PrestigeCount_Button.SetRaycastTarget(condition_PrestigeCount);
        challenge_PrestigeCount_Button.spriteData.SetColor(condition_PrestigeCount ? new Color(0.5f, 0.9f, 0.5f, 1) : Color.gray);
        challenge_PrestigeCount_Description.textData.SetText($"Prestige {Challenge_PrestigeCount_Target} Times");
        challenge_PrestigeCount_ValueBar.SetBarValue(prestigeCount / (float)Challenge_PrestigeCount_Target);
        challenge_PrestigeCount_RewardText.textData.SetText($"<sprite=0> 2");

        /// Challenge_ArtifactCount
        int Challenge_ArtifactCount_Target = challenge_ArtifactCount_Level * 3 + 3;
        bool condition_ArtifactCount = artifactList.Count >= Challenge_ArtifactCount_Target;

        if(artifactList.Count == 15 && challenge_ArtifactCount_Level >= 5)
        {
            challenge_ArtifactCount_Button.SetInteractable(false);
            challenge_ArtifactCount_Button.SetRaycastTarget(false);
            challenge_ArtifactCount_Button.spriteData.SetColor(Color.gray);
            challenge_ArtifactCount_Description.textData.SetText($"You have all the artifacts.");
            challenge_ArtifactCount_ValueBar.SetBarValue(1);
            challenge_ArtifactCount_RewardText.textData.SetText($"<sprite=0> 2");
        }
        else
        {
            challenge_ArtifactCount_Button.SetInteractable(condition_ArtifactCount);
            challenge_ArtifactCount_Button.SetRaycastTarget(condition_ArtifactCount);
            challenge_ArtifactCount_Button.spriteData.SetColor(condition_ArtifactCount ? new Color(0.5f, 0.9f, 0.5f, 1) : Color.gray);
            challenge_ArtifactCount_Description.textData.SetText($"Acquire {Challenge_ArtifactCount_Target} Artifacts");
            challenge_ArtifactCount_ValueBar.SetBarValue(artifactList.Count / (float)Challenge_ArtifactCount_Target);
            challenge_ArtifactCount_RewardText.textData.SetText($"<sprite=0> 2");
        }
    }
}
