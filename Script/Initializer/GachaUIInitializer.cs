using System.Collections;
using System.Linq;
using UnityEngine;

// GachaUI 컴포넌트를 초기화하는 클래스입니다
public class GachaUIInitializer : MonoBehaviour
{
    private GachaUI gachaUI;

    // GachaUI의 버튼 클릭 이벤트를 연결합니다
    private void Awake()
    {
        gachaUI = FindObjectOfType<GachaUI>(true);
        gachaUI.gachaButton.onClick.AddListener(OnGachaButtonClick);
    }

    // 가챠 버튼을 클릭했을 때 실행되는 메소드입니다
    // 가챠를 실행하고 결과를 UI에 반영합니다
    private void OnGachaButtonClick()
    {
        var playerData = GameController.instance.currentPlayerData;
        if(playerData.status.artifactAcquired.All(acquired => acquired)) return;
        if(playerData.status.prestigePoint < 10) return;
        StartCoroutine(Gacha());
    }

    // 가챠를 애니메이션을 실행하고, 랜덤으로 아티팩트를 획득하는 코루틴입니다
    IEnumerator Gacha()
    {
        yield return gachaUI.GachaAnimationCoroutine();
        var artifactAcquired = GameController.instance.currentPlayerData.GachaArtifact();
        gachaUI.SetGachaImage(artifactAcquired.sprite);
    }
}