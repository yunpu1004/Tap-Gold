/// 이 cs 파일은 접속 보상 이벤트를 관리합니다.
public partial class Tap_N_Gold
{
    /// === 실행 조건 ===
    /// 1. 게임 접속후 보상 팝업이 활성화되었을때
    /// === 실행 내용 ===
    /// 1. 접속 보상 텍스트를 업데이트
    private void DisplayReturningPopup()
    {
        if(!Execute_DisplayReturningPopup) return;
        Execute_DisplayReturningPopup = false;

        returning.SetDisplay(true);
        string numericalText_returningRewardGold = returningRewardGold < 1000000 ? returningRewardGold.ToString("N0") : returningRewardGold.ToString("0.000e0");
        string text = $"You earned <sprite=1> {numericalText_returningRewardGold} while you were disconnected.";
        returning_Text.textData.SetText(text);
    }
}