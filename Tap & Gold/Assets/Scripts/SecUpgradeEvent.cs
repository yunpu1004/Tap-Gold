using UnityEngine;

public partial class ClickerTemp
{
    private void UpdateSecUpgradeScroll()
    {
        if(!Execute_UpdateSecUpgradeScroll) return;
        Execute_UpdateSecUpgradeScroll = false;

        for(int i = 0; i < 15; i++)
        {
            Color color = (gold >= secUpgradeList[i].GetNextLevelCost()) ? new Color(0.5f, 0.9f, 0.5f, 1) : Color.gray;
            SimpleButton btn = this.GetType().GetField($"secUpgrade{(char)(i + 65)}_Button", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this) as SimpleButton;
            SimpleText name = this.GetType().GetField($"secUpgrade{(char)(i + 65)}_Name", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this) as SimpleText;
            SimpleText description = this.GetType().GetField($"secUpgrade{(char)(i + 65)}_Description", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this) as SimpleText;
            SimpleText price = this.GetType().GetField($"secUpgrade{(char)(i + 65)}_Price", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this) as SimpleText;
            string numericalText_CurrentLevelEffect = secUpgradeList[i].GetCurrentLevelEffect() < 1000000 ? secUpgradeList[i].GetCurrentLevelEffect().ToString("N0") : secUpgradeList[i].GetCurrentLevelEffect().ToString("0.000e0");
            string numericalText_NextLevelEffect = secUpgradeList[i].GetNextLevelEffect() < 1000000 ? secUpgradeList[i].GetNextLevelEffect().ToString("N0") : secUpgradeList[i].GetNextLevelEffect().ToString("0.000e0");
            string numericalText_NextLevelCost = secUpgradeList[i].GetNextLevelCost() < 1000000 ? secUpgradeList[i].GetNextLevelCost().ToString("N0") : secUpgradeList[i].GetNextLevelCost().ToString("0.000e0");
            btn.spriteData.SetColor(color);
            name.textData.SetText($"{secUpgradeList[i].name}  Lv.{secUpgradeList[i].level}");
            description.textData.SetText($"<sprite=1> +{numericalText_CurrentLevelEffect} / Sec \n<sprite=2> <sprite=1> +{numericalText_NextLevelEffect} / Sec");
            price.textData.SetText($"<sprite=1> {numericalText_NextLevelCost}");
        }
    }   
}