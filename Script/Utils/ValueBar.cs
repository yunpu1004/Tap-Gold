using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ValueBar 클래스는 UI에 값을 표시하는 바를 나타냅니다.
// 값에 따라 바의 길이와 텍스트를 업데이트합니다.
public class ValueBar : MonoBehaviour
{
    public Image bar;                   // fill.amount를 조절할 Image 컴포넌트
    public TextMeshProUGUI valueText;   // 백분율 표시를 위한 TextMeshProUGUI 컴포넌트
    private double _maxValue;           // 최대값
    private double _currentValue;       // 현재값


    // 최대값을 설정하고 바를 업데이트합니다.
    public double maxValue
    {
        get => _maxValue;
        set
        {
            _maxValue = value;
            UpdateBar();
        }
    }

    // 현재값을 설정하고 바를 업데이트합니다.
    public double currentValue
    {
        get => _currentValue;
        set
        {
            _currentValue = value;
            UpdateBar();
        }
    }

    // 최대값과 현재값에 따라 바를 업데이트합니다.
    private void UpdateBar()
    {
        // 최대값이 0인 경우, 바를 가득 채우고 텍스트를 "Max Value is 0"으로 설정
        if(_maxValue == 0)
        {
            bar.fillAmount = 1;
            valueText.text = "Max Value is 0";
            return;
        }

        // 바의 fillAmount를 설정하고, 텍스트를 백분율로 표시
        else
        {
            bar.fillAmount = (float)(_currentValue / _maxValue);
            valueText.text = $"{bar.fillAmount * 100 : 0}%";
        }
    }
}