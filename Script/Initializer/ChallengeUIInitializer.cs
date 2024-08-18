using System.Linq;
using UnityEngine;

// ChallengeUI 컴포넌트를 초기화하는 클래스입니다
public class ChallengeUIInitializer : MonoBehaviour
{
    private ChallengeUI challengeUI;
    
    // ChallengeUI의 버튼 클릭 이벤트를 연결하고 이벤트 핸들러를 등록합니다
    private void Awake()
    {
        challengeUI = FindObjectOfType<ChallengeUI>(true);
        GameController.instance.OnPlayerDataLoadedEvent += OnPlayerDataLoaded;
        GameController.instance.OnPlayerDataChangedEvent += OnPlayerDataChanged;

        challengeUI.holdGoldAchieveButton.onClick.AddListener(() => GameController.instance.currentPlayerData.AchieveHoldGoldChallenge());
        challengeUI.goldPerTapAchieveButton.onClick.AddListener(() => GameController.instance.currentPlayerData.AchieveGoldPerTapChallenge());
        challengeUI.goldPerSecAchieveButton.onClick.AddListener(() => GameController.instance.currentPlayerData.AchieveGoldPerSecChallenge());
        challengeUI.adCountAchieveButton.onClick.AddListener(() => GameController.instance.currentPlayerData.AchieveAdCountChallenge());
        challengeUI.playTimeAchieveButton.onClick.AddListener(() => GameController.instance.currentPlayerData.AchievePlayTimeChallenge());
        challengeUI.tapCountAchieveButton.onClick.AddListener(() => GameController.instance.currentPlayerData.AchieveTapCountChallenge());
        challengeUI.prestigeCountAchieveButton.onClick.AddListener(() => GameController.instance.currentPlayerData.AchievePrestigeCountChallenge());
        challengeUI.artifactCountAchieveButton.onClick.AddListener(() => GameController.instance.currentPlayerData.AchieveArtifactCountChallenge());
    }

    // 플레이어 데이터가 로드될 때, 각 도전과제의 정보를 UI에 반영합니다
    private void OnPlayerDataLoaded(PlayerData data)
    {
        UpdateHoldGoldChallengeUI();
        UpdateGoldPerTapChallengeUI();
        UpdateGoldPerSecChallengeUI();
        UpdateAdCountChallengeUI();
        UpdatePlayTimeChallengeUI();
        UpdateTapCountChallengeUI();
        UpdatePrestigeCountChallengeUI();
        UpdateArtifactCountChallengeUI();
    }

    // 각 도전과제의 진행 상황이 변경될 때 UI를 업데이트합니다 
    private void OnPlayerDataChanged(PlayerData prev, PlayerData cur)
    {
        if(prev.info.highestGoldHeld != cur.info.highestGoldHeld || prev.challenge.holdGoldChallengeLevel != cur.challenge.holdGoldChallengeLevel)
        {
            UpdateHoldGoldChallengeUI();
        }

        if(prev.info.highestDefaultGoldPerTap != cur.info.highestDefaultGoldPerTap || prev.challenge.goldPerTapChallengeLevel != cur.challenge.goldPerTapChallengeLevel)
        {
            UpdateGoldPerTapChallengeUI();
        }

        if(prev.info.highestDefaultGoldPerSec != cur.info.highestDefaultGoldPerSec || prev.challenge.goldPerSecChallengeLevel != cur.challenge.goldPerSecChallengeLevel)
        {
            UpdateGoldPerSecChallengeUI();
        }

        if(prev.info.adCount != cur.info.adCount || prev.challenge.adCountChallengeLevel != cur.challenge.adCountChallengeLevel)
        {
            UpdateAdCountChallengeUI();
        }

        if(prev.info.totalPlayTimeInSecond != cur.info.totalPlayTimeInSecond || prev.challenge.playTimeChallengeLevel != cur.challenge.playTimeChallengeLevel)
        {
            UpdatePlayTimeChallengeUI();
        }

        if(prev.info.tapCount != cur.info.tapCount || prev.challenge.tapCountChallengeLevel != cur.challenge.tapCountChallengeLevel)
        {
            UpdateTapCountChallengeUI();
        }

        if(prev.info.prestigeCount != cur.info.prestigeCount || prev.challenge.prestigeCountChallengeLevel != cur.challenge.prestigeCountChallengeLevel)
        {
            UpdatePrestigeCountChallengeUI();
        }

        if(prev.status.artifactAcquired.Count(a => a) != cur.status.artifactAcquired.Count(a => a) || prev.challenge.artifactCountChallengeLevel != cur.challenge.artifactCountChallengeLevel)
        {
            UpdateArtifactCountChallengeUI();
        }
    }

    // 소지금 도전과제 정보를 UI에 반영합니다
    private void UpdateHoldGoldChallengeUI()
    {
        PlayerData data = GameController.instance.currentPlayerData;
        string desc = ChallengeManager.GetHoldGoldChallengeDesc(data.challenge.holdGoldChallengeLevel);
        double goal = ChallengeManager.GetHoldGoldChallengeGoal(data.challenge.holdGoldChallengeLevel);
        double current = data.info.highestGoldHeld;
        challengeUI.SetHoldGoldChallengeUI(desc, goal, current);
    }

    // 탭당 획득 골드 도전과제 정보를 UI에 반영합니다
    private void UpdateGoldPerTapChallengeUI()
    {
        PlayerData data = GameController.instance.currentPlayerData;
        string desc = ChallengeManager.GetGoldPerTapChallengeDesc(data.challenge.goldPerTapChallengeLevel);
        double goal = ChallengeManager.GetGoldPerTapChallengeGoal(data.challenge.goldPerTapChallengeLevel);
        double current = data.info.highestDefaultGoldPerTap;
        challengeUI.SetGoldPerTapChallengeUI(desc, goal, current);
    }

    // 초당 획득 골드 도전과제 정보를 UI에 반영합니다
    private void UpdateGoldPerSecChallengeUI()
    {
        PlayerData data = GameController.instance.currentPlayerData;
        string desc = ChallengeManager.GetGoldPerSecChallengeDesc(data.challenge.goldPerSecChallengeLevel);
        double goal = ChallengeManager.GetGoldPerSecChallengeGoal(data.challenge.goldPerSecChallengeLevel);
        double current = data.info.highestDefaultGoldPerSec;
        challengeUI.SetGoldPerSecChallengeUI(desc, goal, current);
    }

    // 광고 시청 횟수 도전과제 정보를 UI에 반영합니다
    private void UpdateAdCountChallengeUI()
    {
        PlayerData data = GameController.instance.currentPlayerData;
        string desc = ChallengeManager.GetAdCountChallengeDesc(data.challenge.adCountChallengeLevel);
        int goal = ChallengeManager.GetAdCountChallengeGoal(data.challenge.adCountChallengeLevel);
        int current = data.info.adCount;
        challengeUI.SetAdCountChallengeUI(desc, goal, current);
    }

    // 플레이 시간 도전과제 정보를 UI에 반영합니다
    private void UpdatePlayTimeChallengeUI()
    {
        PlayerData data = GameController.instance.currentPlayerData;
        string desc = ChallengeManager.GetPlayTimeChallengeDesc(data.challenge.playTimeChallengeLevel);
        int goal = ChallengeManager.GetPlayTimeChallengeGoal(data.challenge.playTimeChallengeLevel);
        int current = (int)data.info.totalPlayTimeInSecond;
        challengeUI.SetPlayTimeChallengeUI(desc, goal, current);
    }

    // 탭 횟수 도전과제 정보를 UI에 반영합니다
    private void UpdateTapCountChallengeUI()
    {
        PlayerData data = GameController.instance.currentPlayerData;
        string desc = ChallengeManager.GetTapCountChallengeDesc(data.challenge.tapCountChallengeLevel);
        int goal = ChallengeManager.GetTapCountChallengeGoal(data.challenge.tapCountChallengeLevel);
        int current = data.info.tapCount;
        challengeUI.SetTapCountChallengeUI(desc, goal, current);
    }

    // 프레스티지 횟수 도전과제 정보를 UI에 반영합니다
    private void UpdatePrestigeCountChallengeUI()
    {
        PlayerData data = GameController.instance.currentPlayerData;
        string desc = ChallengeManager.GetPrestigeCountChallengeDesc(data.challenge.prestigeCountChallengeLevel);
        int goal = ChallengeManager.GetPrestigeCountChallengeGoal(data.challenge.prestigeCountChallengeLevel);
        int current = data.info.prestigeCount;
        challengeUI.SetPrestigeCountChallengeUI(desc, goal, current);
    }

    // 아티팩트 획득 횟수 도전과제 정보를 UI에 반영합니다
    private void UpdateArtifactCountChallengeUI()
    {
        PlayerData data = GameController.instance.currentPlayerData;
        string desc = ChallengeManager.GetArtifactCountChallengeDesc(data.challenge.artifactCountChallengeLevel);
        int goal = ChallengeManager.GetArtifactCountChallengeGoal(data.challenge.artifactCountChallengeLevel);
        int current = data.status.artifactAcquired.Count(a => a);
        challengeUI.SetArtifactCountChallengeUI(desc, goal, current);
    }
}