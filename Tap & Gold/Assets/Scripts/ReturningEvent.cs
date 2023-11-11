public partial class ClickerTemp
{
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