public partial class ClickerTemp
{
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