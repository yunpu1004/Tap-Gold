using UnityEngine;

// NavCanvasUI 컴포넌트를 초기화하는 클래스입니다
public class NavCanvasUIInitializer : MonoBehaviour
{
    private NavCanvasUI navCanvasUI;

    // NavCanvasUI의 토글 버튼 이벤트를 연결합니다
    private void Awake() 
    {
        navCanvasUI = FindObjectOfType<NavCanvasUI>(true);
        navCanvasUI.tapUpgradeToggle.onValueChanged.AddListener((value) => navCanvasUI.SetActiveTapUpgradeCanvas(value));
        navCanvasUI.secUpgradeToggle.onValueChanged.AddListener((value) => navCanvasUI.SetActiveSecUpgradeCanvas(value));
        navCanvasUI.artifactToggle.onValueChanged.AddListener((value) => navCanvasUI.SetActiveArtifactCanvas(value));
        navCanvasUI.challengeToggle.onValueChanged.AddListener((value) => navCanvasUI.SetActiveChallengeCanvas(value));
        navCanvasUI.statisticsToggle.onValueChanged.AddListener((value) => navCanvasUI.SetActiveStatisticsCanvas(value));
    }
}