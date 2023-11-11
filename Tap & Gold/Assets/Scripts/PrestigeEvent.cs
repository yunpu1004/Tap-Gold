
/// 이 cs파일은 프레스티지 이벤트를 관리합니다.
public partial class ClickerTemp
{
    /// === 실행 조건 ===
    /// 1. 프레스티지 팝업이 활성화되었을때
    /// === 실행 내용 ===
    /// 1. 프레스티지 보상 텍스트를 업데이트
    private void UpdatePrestigePopup()
    {
        if(Execute_UpdatePrestigeRewardText)
        {
            prestige_Text.textData.SetText($"Expected Reward : <sprite=0> {GetPrestigeReward()}");
            prestige_Button.SetInteractable(GetPrestigeReward() > 0);
            Execute_UpdatePrestigeRewardText = false;
        }
    }
}