using System.Collections;
using UnityEngine;

// PrestigeUI 컴포넌트를 초기화하는 클래스입니다
public class PrestigeUIInitializer : MonoBehaviour
{
    private PrestigeUI prestigeUI;
    private ArtifactUI artifactUI;

    // PrestigeUI와 ArtifactUI의 버튼 클릭 이벤트를 연결합니다
    // ArtifactUI의 버튼을 눌렀을 때 PrestigeUI가 활성화되므로, 여기서 연결합니다
    private void Awake() 
    {
        prestigeUI = FindObjectOfType<PrestigeUI>(true);
        artifactUI = FindObjectOfType<ArtifactUI>(true);

        prestigeUI.prestigeButton.onClick.AddListener(OnPrestigeButtonClick);
        artifactUI.prestigeOpenButton.onClick.AddListener(OnPrestigeOpenButtonClick);
    }

    // 프레스티지 버튼을 눌렀을 때 실행되는 메소드입니다
    private void OnPrestigeButtonClick()
    {
        StartCoroutine(Prestige());
    }

    // 프레스티지 애니메이션을 실행하고나면, 프레스티지를 수행합니다
    IEnumerator Prestige()
    {
        yield return prestigeUI.PrestigeAnimationCoroutine();
        GameController.instance.currentPlayerData.Prestige();
    }
    
    // 프레스티지 열기 버튼을 눌렀을 때 실행되는 메소드입니다
    // UI를 활성화 하고, 프레스티지 실행시 얻을 수 있는 포인트를 표시합니다
    private void OnPrestigeOpenButtonClick()
    {
        prestigeUI.canvas.gameObject.SetActive(true);
        int prestigePoint = GameController.instance.currentPlayerData.GetExpectedPrestigeReward();
        prestigeUI.SetExpectedPrestigePoint(prestigePoint);
    }
}