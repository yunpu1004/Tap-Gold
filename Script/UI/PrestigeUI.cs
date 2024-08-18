using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 프레스티지 UI를 관리하는 클래스입니다.
public class PrestigeUI : MonoBehaviour
{
    public Canvas canvas;
    public GameObject panel;
    public TextMeshProUGUI expectedPrestigePoint;
    public Animator animator;
    public Button prestigeButton;

    // 프레스티지시 얻을 수 있는 포인트에 대한 텍스트를 설정합니다
    public void SetExpectedPrestigePoint(int prestigePoint)
    {
        expectedPrestigePoint.text = $"Expected Prestige : <sprite=0> {prestigePoint}";
    }

    // 프레스티지 애니메이션을 실행합니다
    public IEnumerator PrestigeAnimationCoroutine()
    {
        // 애니메이션을 재생하고, UI 상호작용을 비활성화
        var canvasGroup = canvas.GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        panel.SetActive(false);
        animator.SetTrigger("Prestige");
        yield return null;

        // 애니메이션이 끝날때까지 대기
        bool animationStarted = false;
        while (true)
        {
            bool isPlaying = animator.GetCurrentAnimatorStateInfo(0).IsName("Prestige");
            if (isPlaying)
            {
                animationStarted = true;
            }
            else if (animationStarted)
            {
                break;
            }
            yield return null;
        }

        // 애니메이션이 끝나면 UI 상호작용을 활성화
        canvasGroup.interactable = true;
        panel.SetActive(true);
        canvas.gameObject.SetActive(false);
    }
}