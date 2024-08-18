using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 화면 상단의 UI를 관리하는 클래스입니다.
public class TopCanvasUI : MonoBehaviour
{
    public TextMeshProUGUI goldPerTapText;
    public TextMeshProUGUI goldPerSecText;
    public TextMeshProUGUI goldText;
    public Image volumeIcon;
    public Button volumeButton;
    public Button showRewardAdButton_TapBuff;
    public Button showRewardAdButton_SecBuff;
    public GameObject tapBuffPanel;
    public GameObject secBuffPanel;

    // 볼륨 아이콘을 설정합니다.
    public void SetVolumeIcon(bool value)
    {
        if(value)
        {
            volumeIcon.sprite = Resources.Load<Sprite>("Image/UI/VolumeOn");
        }
        else
        {
            volumeIcon.sprite = Resources.Load<Sprite>("Image/UI/VolumeOff");
        }
    }

    // 소지 골드의 텍스트를 설정합니다.
    public void SetGoldText(double gold)
    {
        string goldStr = $"<sprite=1> {DoubleUtil.DoubleToString(gold)}";
        goldText.text = goldStr;
    }

    // 탭당 골드 획득량 텍스트를 설정합니다.
    public void SetGoldPerTapText(double goldPerSec)
    {
        string goldPerTapStr = $"<sprite=1> {DoubleUtil.DoubleToString(goldPerSec)} / Tap";
        goldPerTapText.text = goldPerTapStr;
    }

    // 초당 골드 획득량 텍스트를 설정합니다.
    public void SetGoldPerSecText(double goldPerSec)
    {
        string goldPerSecStr = $"<sprite=1> {DoubleUtil.DoubleToString(goldPerSec)} / Sec";
        goldPerSecText.text = goldPerSecStr;
    }
}
