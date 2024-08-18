using TMPro;
using UnityEngine;

// 통계 UI를 관리하는 클래스입니다.
public class StatisticsUI : MonoBehaviour
{
    public TextMeshProUGUI currentDefaultGoldPerTapText;
    public TextMeshProUGUI currentDefaultGoldPerSecText;
    public TextMeshProUGUI highestDefaultGoldPerTapText;
    public TextMeshProUGUI highestDefaultGoldPerSecText;
    public TextMeshProUGUI highestGoldHeldText;

    // 현재 탭당 골드 획득량 텍스트를 설정합니다.
    public void SetCurrentDefaultGoldPerTapText(double value)
    {
        currentDefaultGoldPerTapText.text = $"Current : <sprite=1> {DoubleUtil.DoubleToString(value)} / Tap (Default)";
    }

    // 현재 초당 골드 획득량 텍스트를 설정합니다.
    public void SetCurrentDefaultGoldPerSecText(double value)
    {
        currentDefaultGoldPerSecText.text = $"Current : <sprite=1> {DoubleUtil.DoubleToString(value)} / Sec (Default)";
    }

    // 최고 탭당 골드 획득량 텍스트를 설정합니다.
    public void SetHighestDefaultGoldPerTapText(double value)
    {
        highestDefaultGoldPerTapText.text = $"Highest : <sprite=1> {DoubleUtil.DoubleToString(value)} / Tap (Default)";
    }

    // 최고 초당 골드 획득량 텍스트를 설정합니다.
    public void SetHighestDefaultGoldPerSecText(double value)
    {
        highestDefaultGoldPerSecText.text = $"Highest : <sprite=1> {DoubleUtil.DoubleToString(value)} / Sec (Default)";
    }

    // 최고 보유 골드량 텍스트를 설정합니다.
    public void SetHighestGoldHeldText(double value)
    {
        highestGoldHeldText.text = $"Highest Held : <sprite=1> {DoubleUtil.DoubleToString(value)}";
    }
}