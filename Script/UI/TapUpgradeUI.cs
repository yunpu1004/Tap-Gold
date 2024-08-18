using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 탭 업그레이드 UI를 관리하는 클래스입니다.
public class TapUpgradeUI : MonoBehaviour
{
    public TextMeshProUGUI tapUpgradeName;
    public TextMeshProUGUI tapUpgradeDesc;
    public TextMeshProUGUI tapUpgradeCost;
    public Button tapUpgradeButton;
    public Image tapUpgradeButtonImage;

    public TextMeshProUGUI autoTapUpgradeName;
    public TextMeshProUGUI autoTapUpgradeDesc;
    public Button autoTapUpgradeButton;
    public Image autoTapUpgradeButtonImage;
    public Button autoTapActivateButton;

    public TextMeshProUGUI bonusGoldPerTapUpgradeName;
    public TextMeshProUGUI bonusGoldPerTapUpgradeDesc;
    public Button bonusGoldPerTapUpgradeButton;
    public Image bonusGoldPerTapUpgradeButtonImage;
    public Button bonusGoldPerTapActivateButton;

    public TextMeshProUGUI bonusGoldPerSecUpgradeName;
    public TextMeshProUGUI bonusGoldPerSecUpgradeDesc;
    public Button bonusGoldPerSecUpgradeButton;
    public Image bonusGoldPerSecUpgradeButtonImage;
    public Button bonusGoldPerSecActivateButton;

    public TextMeshProUGUI fastCooldownUpgradeName;
    public TextMeshProUGUI fastCooldownUpgradeDesc;
    public Button fastCooldownUpgradeButton;
    public Image fastCooldownUpgradeButtonImage;
    public Button fastCooldownActivateButton;


    // 탭 업그레이드 슬롯의 텍스트를 설정합니다.
    public void SetTapUpgradeSlotText(int lv, double cost, double currentEffect, double nextEffect)
    {
        tapUpgradeName.text = $"Tap Upgrade Lv.{lv}";
        tapUpgradeDesc.text = $"<sprite=1> +{DoubleUtil.DoubleToString(currentEffect)} / Tap\n<sprite=2>  <sprite=1> +{DoubleUtil.DoubleToString(nextEffect)} / Tap";
        tapUpgradeCost.text = $"<sprite=1> {DoubleUtil.DoubleToString(cost)}";
    }

    // 탭 업그레이드 슬롯 버튼의 상호작용을 활성화/비활성화합니다.
    public void SetTapUpgradeButtonInteractable(bool isInteractable)
    {
        tapUpgradeButton.interactable = isInteractable;
        tapUpgradeButtonImage.color = isInteractable ? Color.green : Color.gray;
    }

    // 오토 탭 스킬 업그레이드 슬롯의 텍스트를 설정합니다.
    public void SetAutoTapUpgradeSlotText(int lv, double tapPerSec, int duration, double nextTapPerSec, int nextDuration)
    {
        autoTapUpgradeName.text = $"Auto Tap Lv.{lv}";
        autoTapUpgradeDesc.text = $"Auto tap <color=green><b>{DoubleUtil.DoubleToString(tapPerSec)}</b></color> / Sec for <color=orange><b>{duration}</b></color> seconds\n<sprite=2>  Auto tap <color=green><b>{DoubleUtil.DoubleToString(nextTapPerSec)}</b></color> / Sec for <color=orange><b>{nextDuration}</b></color> seconds";
    }

    // 오토 탭 스킬 업그레이드 슬롯 버튼의 상호작용을 활성화/비활성화합니다.
    public void SetAutoTapUpgradeButtonInteractable(bool isInteractable)
    {
        autoTapUpgradeButton.interactable = isInteractable;
        autoTapUpgradeButtonImage.color = isInteractable ? Color.green : Color.gray;
    }

    // 탭 보너스 골드 스킬 업그레이드 슬롯의 텍스트를 설정합니다.
    public void SetBonusGoldPerTapUpgradeSlotText(int lv, double multiplier, int duration, double nextMultiplier, int nextDuration)
    {
        bonusGoldPerTapUpgradeName.text = $"Bonus Gold Per Tap Lv.{lv}";
        bonusGoldPerTapUpgradeDesc.text = $"Get <sprite=1> <color=green><b>{DoubleUtil.DoubleToString(multiplier)}X</b></color> / Tap for <color=orange><b>{duration}</b></color> seconds\n<sprite=2>  Get <sprite=1> <color=green><b>{DoubleUtil.DoubleToString(nextMultiplier)}X</b></color> / Tap for <color=orange><b>{nextDuration}</b></color> seconds";
    }

    // 탭 보너스 골드 스킬 업그레이드 슬롯 버튼의 상호작용을 활성화/비활성화합니다.
    public void SetBonusGoldPerTapUpgradeButtonInteractable(bool isInteractable)
    {
        bonusGoldPerTapUpgradeButton.interactable = isInteractable;
        bonusGoldPerTapUpgradeButtonImage.color = isInteractable ? Color.green : Color.gray;
    }

    // 초당 보너스 골드 스킬 업그레이드 슬롯의 텍스트를 설정합니다.
    public void SetBonusGoldPerSecUpgradeSlotText(int lv, double multiplier, int duration, double nextMultiplier, int nextDuration)
    {
        bonusGoldPerSecUpgradeName.text = $"Bonus Gold Per Sec Lv.{lv}";
        bonusGoldPerSecUpgradeDesc.text = $"Get <sprite=1> <color=green><b>{DoubleUtil.DoubleToString(multiplier)}X</b></color> / Sec for <color=orange><b>{duration}</b></color> seconds\n<sprite=2>  Get <sprite=1> <color=green><b>{DoubleUtil.DoubleToString(nextMultiplier)}X</b></color> / Sec for <color=orange><b>{nextDuration}</b></color> seconds";
    }

    // 초당 보너스 골드 스킬 업그레이드 슬롯 버튼의 상호작용을 활성화/비활성화합니다.
    public void SetBonusGoldPerSecUpgradeButtonInteractable(bool isInteractable)
    {
        bonusGoldPerSecUpgradeButton.interactable = isInteractable;
        bonusGoldPerSecUpgradeButtonImage.color = isInteractable ? Color.green : Color.gray;
    }

    // 쿨다운 감소 스킬 업그레이드 슬롯의 텍스트를 설정합니다.
    public void SetFastCooldownUpgradeSlotText(int lv, double cooldown, double nextCooldown)
    {
        fastCooldownUpgradeName.text = $"Fast Cooldown Lv.{lv}";
        fastCooldownUpgradeDesc.text = $"Reduce all cooldown times by <color=green><b>{DoubleUtil.DoubleToString(cooldown*100)}%</b></color>\n<sprite=2>  Reduce all cooldown times by <color=green><b>{DoubleUtil.DoubleToString(nextCooldown*100)}%</b></color>";
    }

    // 쿨다운 감소 스킬 업그레이드 슬롯 버튼의 상호작용을 활성화/비활성화합니다.
    public void SetFastCooldownUpgradeButtonInteractable(bool isInteractable)
    {
        fastCooldownUpgradeButton.interactable = isInteractable;
        fastCooldownUpgradeButtonImage.color = isInteractable ? Color.green : Color.gray;
    }
}