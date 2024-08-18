using TMPro;
using UnityEngine;
using UnityEngine.UI;

// SecUpgradeUI 컴포넌트를 초기화하는 클래스입니다
public class SecUpgradeUIInitializer : MonoBehaviour
{
    private SecUpgradeUI secUpgradeUI;

    // SecUpgradeUI의 필드를 초기화하고 이벤트 핸들러와 버튼 클릭 이벤트를 연결합니다
    private void Awake()
    {
        secUpgradeUI = FindObjectOfType<SecUpgradeUI>(true);
        secUpgradeUI.secUpgradeSlots = new (TextMeshProUGUI name, TextMeshProUGUI desc, TextMeshProUGUI price, Image buttonImage, Button button)[secUpgradeUI.content.childCount];

        for (int i = 0; i < secUpgradeUI.content.childCount; i++)
        {
            Transform slot = secUpgradeUI.content.GetChild(i);
            TextMeshProUGUI name = slot.Find("Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI desc = slot.Find("Description").GetComponent<TextMeshProUGUI>();
            Button button = slot.Find("Button").GetComponent<Button>();
            Image buttonImage = button.GetComponent<Image>();
            TextMeshProUGUI price = slot.Find("Button/Price").GetComponent<TextMeshProUGUI>();
            secUpgradeUI.secUpgradeSlots[i] = (name, desc, price, buttonImage, button);

            int idx = i;
            button.onClick.AddListener(() => GameController.instance.currentPlayerData.LevelUpSecUpgrade(idx));
        }

        GameController.instance.OnPlayerDataLoadedEvent += OnPlayerDataLoaded;
        GameController.instance.OnPlayerDataChangedEvent += OnPlayerDataChanged;
    }

    // 플레이어 데이터가 로드될 때, 초당 골드 획득 업그레이드 정보를 UI에 반영합니다
    private void OnPlayerDataLoaded(PlayerData data)
    {
        for(int i = 0; i < secUpgradeUI.secUpgradeSlots.Length; i++)
        {
            UpdateUpgradeSlotText(i);
            UpdateUpgradeSlotButton(i);
        }
    }

    // 초당 골드 획득 업그레이드 정보가 변경될 때 UI를 업데이트합니다
    private void OnPlayerDataChanged(PlayerData prev, PlayerData cur)
    {
        for(int i = 0; i < secUpgradeUI.secUpgradeSlots.Length; i++)
        {
            if(prev.status.secUpgradeLevels[i] != cur.status.secUpgradeLevels[i])
            {
                UpdateUpgradeSlotText(i);
            }
            
            if(prev.status.gold != cur.status.gold)
            {
                UpdateUpgradeSlotButton(i);
            }
        }
    }

    // 업그레이드 슬롯의 텍스트를 업데이트합니다
    private void UpdateUpgradeSlotText(int idx)
    {
        int lv = GameController.instance.currentPlayerData.status.secUpgradeLevels[idx];
        double cost = UpgradeManager.GetSecUpgradeCost(idx, lv);
        double currentEffect = UpgradeManager.GetSecUpgradeEffect(idx, lv);
        double nextEffect = UpgradeManager.GetSecUpgradeEffect(idx, lv + 1);
        secUpgradeUI.SetUpgradeSlotText(idx, lv, cost, currentEffect, nextEffect);
    }

    // 업그레이드 슬롯의 버튼의 상호작용 여부를 업데이트합니다
    private void UpdateUpgradeSlotButton(int idx)
    {
        int lv = GameController.instance.currentPlayerData.status.secUpgradeLevels[idx];
        double gold = GameController.instance.currentPlayerData.status.gold;
        double cost = UpgradeManager.GetSecUpgradeCost(idx, lv);
        secUpgradeUI.SetUpgradeSlotButtonInteractable(idx, gold >= cost);
    }
}