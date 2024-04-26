using UnityEngine;

/// 이 cs파일은 탭 업그레이드 스크롤의 UI를 관리합니다.
public partial class Tap_N_Gold
{
    /// === 실행 조건 ===
    /// 1. 탭 업그레이드 스크롤이 활성화되었을때
    /// === 실행 내용 ===
    /// 1. 탭 업그레이드 항목의 업그레이드 설명을 업데이트하고, 소유 골드와 비용을 비교해 버튼 색상을 변경
    /// 2. 스킬 업그레이드 항목의 업그레이드 설명을 업데이트하고, 소유 프레스티지 포인트와 비용을 비교해 버튼 색상을 변경
    /// 3. 스킬의 남은 시간을 표시하고, 쿨타임이 끝나면 버튼 색상을 변경
    private void UpdateTapUpgradeScroll()
    {
        if(!Execute_UpdateTapUpgradeScroll) return;
        Execute_UpdateTapUpgradeScroll = false;

        // 탭 업그레이드 연동
        Color color = (gold >= tapUpgradeData.GetNextLevelCost()) ? new Color(0.5f, 0.9f, 0.5f, 1) : Color.gray;
        string numericalText_CurrentLevelEffect = tapUpgradeData.GetCurrentLevelEffect() < 1000000 ? tapUpgradeData.GetCurrentLevelEffect().ToString("N0") : tapUpgradeData.GetCurrentLevelEffect().ToString("0.000e0");
        string numericalText_NextLevelEffect = tapUpgradeData.GetNextLevelEffect() < 1000000 ? tapUpgradeData.GetNextLevelEffect().ToString("N0") : tapUpgradeData.GetNextLevelEffect().ToString("0.000e0");
        string numericalText_NextLevelCost = tapUpgradeData.GetNextLevelCost() < 1000000 ? tapUpgradeData.GetNextLevelCost().ToString("N0") : tapUpgradeData.GetNextLevelCost().ToString("0.000e0");
        tapUpgrade_TapUpgrade_Button.spriteData.SetColor(color);
        tapUpgrade_TapUpgrade_Name.textData.SetText($"{tapUpgradeData.name}  Lv.{tapUpgradeData.level}");
        tapUpgrade_TapUpgrade_Description.textData.SetText($"<sprite=1> +{numericalText_CurrentLevelEffect} / Tap \n<sprite=2> <sprite=1> +{numericalText_NextLevelEffect} / Tap");
        tapUpgrade_TapUpgrade_Price.textData.SetText($"<sprite=1> {numericalText_NextLevelCost}");


        // 스킬 업그레이드 연동
        color = prestigePoint >= 2 ? new Color(0.5f, 0.9f, 0.5f, 1) : Color.gray;

        tapUpgrade_AutoTapUpgrade_Button.spriteData.SetColor(color);
        tapUpgrade_AutoTapUpgrade_Name.textData.SetText($"Auto Tap  Lv.{autoTapSkillData.GetLevel()}");
        tapUpgrade_AutoTapUpgrade_Description.textData.SetText(autoTapSkillData.GetUpgradeDesc());

        tapUpgrade_BonusTapUpgrade_Button.spriteData.SetColor(color);
        tapUpgrade_BonusTapUpgrade_Name.textData.SetText($"Bonus Tap  Lv.{bonusTapSkillData.GetLevel()}");
        tapUpgrade_BonusTapUpgrade_Description.textData.SetText(bonusTapSkillData.GetUpgradeDesc());

        tapUpgrade_BonusSecUpgrade_Button.spriteData.SetColor(color);
        tapUpgrade_BonusSecUpgrade_Name.textData.SetText($"Bonus Sec  Lv.{bonusSecSkillData.GetLevel()}");
        tapUpgrade_BonusSecUpgrade_Description.textData.SetText(bonusSecSkillData.GetUpgradeDesc());

        tapUpgrade_CoolDownUpgrade_Button.spriteData.SetColor(color);
        tapUpgrade_CoolDownUpgrade_Name.textData.SetText($"Cool Down  Lv.{coolDownSkillData.GetLevel()}");
        tapUpgrade_CoolDownUpgrade_Description.textData.SetText(coolDownSkillData.GetUpgradeDesc());


        // 스킬 남은 시간 및 쿨타임 연동
        Color autoTapTextColor = autoTapSkillData.IsOnActivated() ? Color.blue : Color.black;
        int autoTapTextTime = autoTapSkillData.IsOnActivated() ? (int)autoTapSkillData.GetRemainedTime() : (int)autoTapSkillData.remainedCoolTime;
        string autoTapTextString = (!autoTapSkillData.IsActivatable()) ? autoTapTextTime.ToString() : "";
        tapUpgrade_Skills_AutoTapButton_Text.textData.SetText(autoTapTextString);
        tapUpgrade_Skills_AutoTapButton_Text.textData.SetColor(autoTapTextColor);

        Color bonusTapTextColor = bonusTapSkillData.IsOnActivated() ? Color.blue : Color.black;
        int bonusTapTextTime = bonusTapSkillData.IsOnActivated() ? (int)bonusTapSkillData.GetRemainedTime() : (int)bonusTapSkillData.remainedCoolTime;
        string bonusTapTextString = (!bonusTapSkillData.IsActivatable()) ? bonusTapTextTime.ToString() : "";
        tapUpgrade_Skills_BonusTapButton_Text.textData.SetText(bonusTapTextString);
        tapUpgrade_Skills_BonusTapButton_Text.textData.SetColor(bonusTapTextColor);

        Color bonusSecTextColor = bonusSecSkillData.IsOnActivated() ? Color.blue : Color.black;
        int bonusSecTextTime = bonusSecSkillData.IsOnActivated() ? (int)bonusSecSkillData.GetRemainedTime() : (int)bonusSecSkillData.remainedCoolTime;
        string bonusSecTextString = (!bonusSecSkillData.IsActivatable()) ? bonusSecTextTime.ToString() : "";
        tapUpgrade_Skills_BonusSecButton_Text.textData.SetText(bonusSecTextString);
        tapUpgrade_Skills_BonusSecButton_Text.textData.SetColor(bonusSecTextColor);

        Color coolDownTextColor = Color.black;
        int coolDownTextTime = (int)coolDownSkillData.remainedCoolTime;
        string coolDownTextString = (!coolDownSkillData.IsActivatable()) ? coolDownTextTime.ToString() : "";
        tapUpgrade_Skills_CoolDownButton_Text.textData.SetText(coolDownTextString);
        tapUpgrade_Skills_CoolDownButton_Text.textData.SetColor(coolDownTextColor);
    }
}
