using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 화면 중앙의 탭 영역 UI를 관리하는 클래스입니다.
public class MidCanvasUI : MonoBehaviour
{
    public Camera mainCamera;
    public Canvas canvas;
    public PointerClickHandler tapArea;
    
    [HideInInspector] public TextMeshProUGUI[] textEffects;


    // 비활성화된 채로 대기중인 텍스트 오브젝트를 찾아서 활성화하고, 텍스트 효과를 재생합니다.
    public void PlayTextEffect(double earnedGold, Vector3 screenPos)
    {
        var targetEffect = textEffects.FirstOrDefault(effect => !effect.gameObject.activeSelf);
        if (targetEffect != null)
        {
            targetEffect.gameObject.SetActive(true);
            StartCoroutine(TextFadeOutEffect(targetEffect, earnedGold, screenPos));
        }
    }

    // 화면을 탭했을때 재생되는 텍스트 효과입니다.
    // 획득한 골드만큼 텍스트를 표시하고, 위로 올라가면서 점점 사라지게 하는 효과입니다.
    IEnumerator TextFadeOutEffect(TextMeshProUGUI textEffect, double earnedGold, Vector3 screenPos)
    {
        // 획득한 골드를 텍스트로 표시하고 탭한 위치로 이동
        textEffect.text = $"<sprite=1> {DoubleUtil.DoubleToString(earnedGold)}";
        textEffect.rectTransform.position = mainCamera.ScreenToWorldPoint(screenPos);
        textEffect.alpha = 1f;
        yield return null;

        // 텍스트가 위로 올라가면서 점점 사라지는 효과
        float fadeOutTime = 1.5f;
        float elapsedTime = 0f;
        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            textEffect.rectTransform.position += 0.5f * Time.deltaTime * Vector3.up;
            textEffect.alpha = 1f - elapsedTime / fadeOutTime;
            yield return null;
        }

        // 텍스트 오브젝트를 비활성화
        textEffect.gameObject.SetActive(false);
    }
}