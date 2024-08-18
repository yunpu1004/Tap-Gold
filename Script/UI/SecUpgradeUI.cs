using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 초당 골드 획득 업그레이드 UI를 관리하는 클래스입니다.
public class SecUpgradeUI : MonoBehaviour
{
    public Transform content;
    public (TextMeshProUGUI name, TextMeshProUGUI desc, TextMeshProUGUI price, Image buttonImage, Button button)[] secUpgradeSlots;

    // 초당 골드 획득 업그레이드 슬롯의 텍스트를 설정합니다.
    public void SetUpgradeSlotText(int idx, int lv, double cost, double currentEffect, double nextEffect)
    {
        secUpgradeSlots[idx].name.text = $"Sec Upgrade Lv.{lv}";
        secUpgradeSlots[idx].desc.text = $"<sprite=1> +{DoubleUtil.DoubleToString(currentEffect)} / Sec\n<sprite=2>  <sprite=1> +{DoubleUtil.DoubleToString(nextEffect)} / Sec";
        secUpgradeSlots[idx].price.text = $"<sprite=1> {DoubleUtil.DoubleToString(cost)}";
    }

    // 초당 골드 획득 업그레이드 슬롯의 버튼의 상호작용을 활성화/비활성화합니다.
    public void SetUpgradeSlotButtonInteractable(int idx, bool interactable)
    {
        secUpgradeSlots[idx].buttonImage.color = interactable ? Color.green : Color.gray;
        secUpgradeSlots[idx].button.interactable = interactable;
    }
}