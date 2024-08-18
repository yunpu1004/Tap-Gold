using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 가챠 UI를 관리하는 클래스입니다.
public class GachaUI : MonoBehaviour
{
    public Canvas canvas;
    public Animator animator;
    public Button gachaButton;
    public Image gachaImage;

    // 가챠 UI를 표시합니다.
    public void Show()
    {
        canvas.gameObject.SetActive(true);
        animator.enabled = false;
    }

    // 가챠 UI의 이미지를 설정합니다.
    public void SetGachaImage(Sprite sprite)
    {
        gachaImage.sprite = sprite;
        gachaImage.color = Color.white;
    }

    // 가챠를 실행했을때 재생되는 애니메이션 코루틴입니다.
    public IEnumerator GachaAnimationCoroutine()
    {
        // 애니메이션을 재생하고, UI 상호작용을 끕니다.
        animator.enabled = true;
        var canvasGroup = canvas.GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        animator.SetTrigger("Gacha");
        yield return null;

        // 애니메이션이 끝날때까지 대기합니다.
        bool animationStarted = false;
        while (true)
        {
            bool isPlaying = animator.GetCurrentAnimatorStateInfo(0).IsName("Gacha");
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

        // 애니메이션이 끝나면 UI 상호작용을 켭니다.
        canvasGroup.interactable = true;
        animator.enabled = false;
    }
}