
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 도전과제 UI를 관리하는 클래스입니다.
public class ChallengeUI : MonoBehaviour
{
    public TextMeshProUGUI holdGoldDesc; 
    public ValueBar holdGoldValueBar;
    public Button holdGoldAchieveButton;

    public TextMeshProUGUI goldPerTapDesc;
    public ValueBar goldPerTapValueBar;
    public Button goldPerTapAchieveButton;

    public TextMeshProUGUI goldPerSecDesc;
    public ValueBar goldPerSecValueBar;
    public Button goldPerSecAchieveButton;

    public TextMeshProUGUI adCountDesc;
    public ValueBar adCountValueBar;
    public Button adCountAchieveButton;

    public TextMeshProUGUI playTimeDesc;
    public ValueBar playTimeValueBar;
    public Button playTimeAchieveButton;

    public TextMeshProUGUI tapCountDesc;
    public ValueBar tapCountValueBar;
    public Button tapCountAchieveButton;

    public TextMeshProUGUI prestigeCountDesc;
    public ValueBar prestigeCountValueBar;
    public Button prestigeCountAchieveButton;

    public TextMeshProUGUI artifactCountDesc;
    public ValueBar artifactCountValueBar;
    public Button artifactCountAchieveButton;


    // 소지 골드 도전과제 UI를 설정합니다.
    public void SetHoldGoldChallengeUI(string desc , double goal, double current)
    {
        holdGoldDesc.text = desc;
        holdGoldValueBar.currentValue = current;
        holdGoldValueBar.maxValue = goal;
    }

    // 탭당 골드 획득량 도전과제 UI를 설정합니다.
    public void SetGoldPerTapChallengeUI(string desc , double goal, double current)
    {
        goldPerTapDesc.text = desc;
        goldPerTapValueBar.currentValue = current;
        goldPerTapValueBar.maxValue = goal;
    }

    // 초당 골드 획득량 도전과제 UI를 설정합니다.
    public void SetGoldPerSecChallengeUI(string desc , double goal, double current)
    {
        goldPerSecDesc.text = desc;
        goldPerSecValueBar.currentValue = current;
        goldPerSecValueBar.maxValue = goal;
    }

    // 광고 시청 횟수 도전과제 UI를 설정합니다.
    public void SetAdCountChallengeUI(string desc , int goal, int current)
    {
        adCountDesc.text = desc;
        adCountValueBar.currentValue = current;
        adCountValueBar.maxValue = goal;
    }

    // 플레이 시간 도전과제 UI를 설정합니다.
    public void SetPlayTimeChallengeUI(string desc , int goal, int current)
    {
        playTimeDesc.text = desc;
        playTimeValueBar.currentValue = current;
        playTimeValueBar.maxValue = goal;
    }

    // 탭 횟수 도전과제 UI를 설정합니다.
    public void SetTapCountChallengeUI(string desc , int goal, int current)
    {
        tapCountDesc.text = desc;
        tapCountValueBar.currentValue = current;
        tapCountValueBar.maxValue = goal;
    }

    // 프레스티지 횟수 도전과제 UI를 설정합니다.
    public void SetPrestigeCountChallengeUI(string desc , int goal, int current)
    {
        prestigeCountDesc.text = desc;
        prestigeCountValueBar.currentValue = current;
        prestigeCountValueBar.maxValue = goal;
    }

    // 아티팩트 획득 횟수 도전과제 UI를 설정합니다.
    public void SetArtifactCountChallengeUI(string desc , int goal, int current)
    {
        artifactCountDesc.text = desc;
        artifactCountValueBar.currentValue = current;
        artifactCountValueBar.maxValue = goal;
    }
}
