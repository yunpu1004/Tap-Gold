public partial class ClickerTemp
{
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