using System;

/// 이 cs파일은 도전과제 스크롤의 UI를 관리합니다.
public partial class Tap_N_Gold
{
    /// === 실행 조건 ===
    /// 1. 게임 데이터가 변경될때
    /// 2. 도전과제 달성 버튼을 눌러서 다음 레벨의 도전과제로 넘어갈때
    /// === 실행 내용 ===
    /// 1. 각 도전과제의 달성 여부를 확인
    /// 2. 만약 하나라도 달성되었다면 도전과제의 달성을 알려주는 UI를 활성화
    /// 3. 달성한 도전과제가 하나도 없다면 UI를 비활성화
    private void UpdateChallengeNotifyMark()
    {
        if(!Execute_UpdateChallengeNotifyMark) return;
        Execute_UpdateChallengeNotifyMark = false;

        /// Challenge_HoldGold
        double Challenge_HoldGold_Target = Math.Pow(10, challenge_HoldGold_Level * 2 + 5);
        bool condition_HoldGold = gold >= Challenge_HoldGold_Target;

        /// Challenge_GoldPerTap
        double Challenge_GoldPerTap_Target = Math.Pow(10, challenge_GoldPerTap_Level * 2 + 2);
        bool condition_GoldPerTap = defaultGoldPerTap >= Challenge_GoldPerTap_Target;

        /// Challenge_GoldPerSec
        double Challenge_GoldPerSec_Target = Math.Pow(10, challenge_GoldPerSec_Level * 2 + 2);
        bool condition_GoldPerSec = defaultGoldPerSec >= Challenge_GoldPerSec_Target;

        /// Challenge_AdCount
        int Challenge_AdCount_Target = challenge_AdCount_Level * 5 + 5;
        bool condition_AdCount = adCount >= Challenge_AdCount_Target;

        /// Challenge_PlayTime
        int Challenge_PlayTime_Minute_Target = challenge_PlayTime_Level * 30 + 30;
        int Challenge_PlayTime_Minute = totalPlayTime / 60;
        bool condition_PlayTime = Challenge_PlayTime_Minute >= Challenge_PlayTime_Minute_Target;

        /// Challenge_TapCount
        int Challenge_TapCount_Target = challenge_TapCount_Level * 500 + 500;
        bool condition_TapCount = tapCount >= Challenge_TapCount_Target;

        /// Challenge_PrestigeCount
        int Challenge_PrestigeCount_Target = challenge_PrestigeCount_Level * 3 + 3;
        bool condition_PrestigeCount = prestigeCount >= Challenge_PrestigeCount_Target;

        /// Challenge_ArtifactCount
        int Challenge_ArtifactCount_Target = challenge_ArtifactCount_Level * 3 + 3;
        bool condition_ArtifactCount = artifactList.Count >= Challenge_ArtifactCount_Target;

        bool condition = condition_HoldGold || condition_GoldPerTap || condition_GoldPerSec || condition_AdCount || condition_PlayTime || condition_TapCount || condition_PrestigeCount || condition_ArtifactCount;
        navCanvas_ChallengeNotifyMark.gameObjectData.SetEnabled(condition);
    }
}