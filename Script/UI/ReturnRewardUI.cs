using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 게임 복귀시 표시되는 UI를 관리하는 클래스입니다.
public class ReturnRewardUI : MonoBehaviour
{
    public Canvas canvas;
    public TextMeshProUGUI rewardText;
    public Button rewardButton;
    public Button adRewardButton;

    // 복귀 보상 텍스트를 설정합니다.
    public void SetExpectedRewardText(double reward)
    {
        rewardText.text = $"You earned <sprite=1> {DoubleUtil.DoubleToString(reward)} while you were disconnected.";
    }
}