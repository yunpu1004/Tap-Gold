using UnityEngine;

/// 이 cs파일은 초당 골드 업그레이드 스크롤의 UI를 관리합니다.
public partial class ClickerTemp
{
    /// === 실행 조건 ===
    /// 1. 초당 골드 업그레이드 스크롤이 활성화되었을때
    /// === 실행 내용 ===
    /// 1. 초당 골드 업그레이드 스크롤의 각 업그레이드 설명 텍스트와 비용 텍스트를 업데이트
    /// 2. 초당 골드 업그레이드 스크롤의 각 업그레이드 버튼의 색상을 업데이트
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