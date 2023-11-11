/// 이 cs 파일은 5분마다 나타나는 특별 보상 이벤트를 관리합니다.
public partial class ClickerTemp
{
    /// === 실행 조건 ===
    /// 1. 특별 보상 팝업이 활성화되었을때
    /// === 실행 내용 ===
    /// 1. 광고 시청시 얻을 특별 보상(골드)에 대한 텍스트를 업데이트
    private void UpdateSpecialBonusPopup()
    {
        if(!Execute_UpdateSpecialBonusPopup) return;
        Execute_UpdateSpecialBonusPopup = false;

        double sum_TapSec = defaultGoldPerSec + defaultGoldPerTap;
        double bonus = sum_TapSec * 2000;
        string bonusText = bonus < 1000000 ? bonus.ToString("N0") : bonus.ToString("0.000e0");
        string text = $"You can earn <sprite=1>{bonusText} by watching an AD!";
        specialBonus_Text.textData.SetText(text);
    }
}