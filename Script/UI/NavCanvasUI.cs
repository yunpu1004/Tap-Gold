using UnityEngine;
using UnityEngine.UI;

// 화면 하단의 네비게이션 UI를 관리하는 클래스입니다.
public class NavCanvasUI : MonoBehaviour
{
    public Canvas tapUpgradeCanvas;
    public Canvas secUpgradeCanvas;
    public Canvas artifactCanvas;
    public Canvas challengeCanvas;
    public Canvas statisticsCanvas;

    public Toggle tapUpgradeToggle;
    public Toggle secUpgradeToggle;
    public Toggle artifactToggle;
    public Toggle challengeToggle;
    public Toggle statisticsToggle;


    // 탭당 골드 획득 업그레이드 탭을 활성화/비활성화 합니다
    public void SetActiveTapUpgradeCanvas(bool value)
    {
        tapUpgradeCanvas.gameObject.SetActive(value);
    }

    // 초당 골드 획득 업그레이드 탭을 활성화/비활성화 합니다
    public void SetActiveSecUpgradeCanvas(bool value)
    {
        secUpgradeCanvas.gameObject.SetActive(value);
    }

    // 아티팩트 탭을 활성화/비활성화 합니다
    public void SetActiveArtifactCanvas(bool value)
    {
        artifactCanvas.gameObject.SetActive(value);
    }

    // 도전과제 탭을 활성화/비활성화 합니다
    public void SetActiveChallengeCanvas(bool value)
    {
        challengeCanvas.gameObject.SetActive(value);
    }

    // 통계 탭을 활성화/비활성화 합니다
    public void SetActiveStatisticsCanvas(bool value)
    {
        statisticsCanvas.gameObject.SetActive(value);
    }
}