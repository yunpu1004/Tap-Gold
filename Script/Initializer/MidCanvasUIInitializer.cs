using TMPro;
using UnityEngine;

// MidCanvasUI 컴포넌트를 초기화하는 클래스입니다
public class MidCanvasUIInitializer : MonoBehaviour
{
    private MidCanvasUI midCanvasUI;
    
    // MidCanvasUI의 필드를 초기화하고 버튼 클릭 이벤트를 연결합니다
    private void Awake() 
    {
        midCanvasUI = FindObjectOfType<MidCanvasUI>(true);
        var effectsParent = midCanvasUI.canvas.transform.Find("Panel/EffectsParent");
        midCanvasUI.textEffects = new TextMeshProUGUI[effectsParent.childCount];
        for (int i = 0; i < effectsParent.childCount; i++)
        {
            midCanvasUI.textEffects[i] = effectsParent.GetChild(i).GetComponent<TextMeshProUGUI>();
        }
        midCanvasUI.tapButton.onClick.AddListener(OnTapButtonClick);
    }

    // 탭 버튼을 클릭했을 때 실행되는 메소드입니다
    // 골드를 획득하고 이펙트를 재생합니다
    private void OnTapButtonClick()
    {
        // 골드 획득
        GameController.instance.currentPlayerData.EarnGoldPerTap();

        // 이펙트 재생
        Vector3 touchPos = Input.mousePosition;
        touchPos.z = 10f;
        double goldPerTap = GameController.instance.currentPlayerData.GetGoldPerTap();
        midCanvasUI.PlayTextEffect(goldPerTap, touchPos);
    }
}
