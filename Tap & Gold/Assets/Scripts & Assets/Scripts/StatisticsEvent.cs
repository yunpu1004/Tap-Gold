public partial class ClickerTemp
{
    private void UpdateStatisticsScroll()
    {
        if(!Execute_UpdateStatisticsScroll) return;
        Execute_UpdateStatisticsScroll = false;

        string numericalText_defaultGoldPerTap = defaultGoldPerTap < 1000000 ? defaultGoldPerTap.ToString("N0") : defaultGoldPerTap.ToString("0.000e0");
        statistics_currentDefaultGoldPerTapText.textData.SetText($"Current : <sprite=1> {numericalText_defaultGoldPerTap} / Tap (Default)");

        string numericalText_defaultGoldPerSec = defaultGoldPerSec < 1000000 ? defaultGoldPerSec.ToString("N0") : defaultGoldPerSec.ToString("0.000e0");
        statistics_currentDefaultGoldPerSecText.textData.SetText($"Current : <sprite=1> {numericalText_defaultGoldPerSec} / Sec (Default)");

        string numericalText_highestDefaultGoldPerTap = highestDefaultGoldPerTap < 1000000 ? highestDefaultGoldPerTap.ToString("N0") : highestDefaultGoldPerTap.ToString("0.000e0");
        statistics_highestDefaultGoldPerTapText.textData.SetText($"Highest : <sprite=1> {numericalText_highestDefaultGoldPerTap} / Tap (Default)");

        string numericalText_highestDefaultGoldPerSec = highestDefaultGoldPerSec < 1000000 ? highestDefaultGoldPerSec.ToString("N0") : highestDefaultGoldPerSec.ToString("0.000e0");
        statistics_highestDefaultGoldPerSecText.textData.SetText($"Highest : <sprite=1> {numericalText_highestDefaultGoldPerSec} / Sec (Default)");

        string numericalText_highestGold = highestGold < 1000000 ? highestGold.ToString("N0") : highestGold.ToString("0.000e0");
        statistics_highestGoldText.textData.SetText($"Highest Held : <sprite=1> {numericalText_highestGold}");
    }
}
