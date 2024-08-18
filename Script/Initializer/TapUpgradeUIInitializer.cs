using UnityEngine;

// TapUpgradeUI 컴포넌트를 초기화하는 클래스입니다
public class TapUpgradeUIInitializer : MonoBehaviour
{
    private TapUpgradeUI tapUpgradeUI;

    // TapUpgradeUI의 이벤트 핸들러를 등록하고 버튼 클릭 이벤트를 연결합니다
    private void Awake() 
    {
        tapUpgradeUI = FindObjectOfType<TapUpgradeUI>(true);

        GameController.instance.OnPlayerDataChangedEvent += OnPlayerDataChanged;
        GameController.instance.OnPlayerDataLoadedEvent += OnPlayerDataLoaded;

        var cur = GameController.instance.currentPlayerData;

        tapUpgradeUI.tapUpgradeButton.onClick.AddListener(() => cur.LevelUpTapUpgrade());
        tapUpgradeUI.autoTapUpgradeButton.onClick.AddListener(() => cur.LevelUpAutoTapSkill());
        tapUpgradeUI.bonusGoldPerTapUpgradeButton.onClick.AddListener(() => cur.LevelUpBonusGoldPerTapSkill());
        tapUpgradeUI.bonusGoldPerSecUpgradeButton.onClick.AddListener(() => cur.LevelUpBonusGoldPerSecSkill());
        tapUpgradeUI.fastCooldownUpgradeButton.onClick.AddListener(() => cur.LevelUpFastCooldownSkill());

        tapUpgradeUI.autoTapActivateButton.onClick.AddListener(() => GameController.instance.StartCoroutine(cur.ActivateAutoTapSkillCoroutine()));
        tapUpgradeUI.bonusGoldPerTapActivateButton.onClick.AddListener(() => GameController.instance.StartCoroutine(cur.ActivateBonusGoldPerTapSkillCoroutine()));
        tapUpgradeUI.bonusGoldPerSecActivateButton.onClick.AddListener(() => GameController.instance.StartCoroutine(cur.ActivateBonusGoldPerSecSkillCoroutine()));
        tapUpgradeUI.fastCooldownActivateButton.onClick.AddListener(() => GameController.instance.StartCoroutine(cur.ActivateFastCooldownSkillCoroutine()));
    }

    // 플레이어 데이터가 로드될 때, 탭 업그레이드 정보를 UI에 반영합니다
    private void OnPlayerDataLoaded(PlayerData data)
    {
        UpdateTapUpgradeSlotText();
        UpdateAutoTapUpgradeSlotText();
        UpdateBonusGoldPerTapUpgradeSlotText();
        UpdateBonusGoldPerSecUpgradeSlotText();
        UpdateFastCooldownUpgradeSlotText();

        UpdateTapUpgradeButtonInteractable();
        UpdateAutoTapUpgradeButtonInteractable();
        UpdateBonusGoldPerTapUpgradeButtonInteractable();
        UpdateBonusGoldPerSecUpgradeButtonInteractable();
        UpdateFastCooldownUpgradeButtonInteractable();
    }

    // 탭 업그레이드 정보가 변경될 때 UI를 업데이트합니다
    private void OnPlayerDataChanged(PlayerData prev, PlayerData cur)
    {
        if(prev.status.tapUpgradeLevel != cur.status.tapUpgradeLevel)
        {
            UpdateTapUpgradeSlotText();
        }

        if(prev.status.autoTapSkillLevel != cur.status.autoTapSkillLevel)
        {
            UpdateAutoTapUpgradeSlotText();
        }

        if(prev.status.bonusGoldPerTapSkillLevel != cur.status.bonusGoldPerTapSkillLevel)
        {
            UpdateBonusGoldPerTapUpgradeSlotText();
        }

        if(prev.status.bonusGoldPerSecSkillLevel != cur.status.bonusGoldPerSecSkillLevel)
        {
            UpdateBonusGoldPerSecUpgradeSlotText();
        }

        if(prev.status.fastCooldownSkillLevel != cur.status.fastCooldownSkillLevel)
        {
            UpdateFastCooldownUpgradeSlotText();
        }

        if(prev.status.gold != cur.status.gold)
        {
            UpdateTapUpgradeButtonInteractable();
        }

        if(prev.status.prestigePoint != cur.status.prestigePoint)
        {
            UpdateAutoTapUpgradeButtonInteractable();
            UpdateBonusGoldPerTapUpgradeButtonInteractable();
            UpdateBonusGoldPerSecUpgradeButtonInteractable();
            UpdateFastCooldownUpgradeButtonInteractable();
        }
    }

    // 탭 업그레이드 슬롯의 텍스트를 업데이트합니다
    private void UpdateTapUpgradeSlotText()
    {
        int lv = GameController.instance.currentPlayerData.status.tapUpgradeLevel;
        double cost = UpgradeManager.GetTapUpgradeCost(lv);
        double currentEffect = UpgradeManager.GetTapUpgradeEffect(lv);
        double nextEffect = UpgradeManager.GetTapUpgradeEffect(lv + 1);
        tapUpgradeUI.SetTapUpgradeSlotText(lv, cost, currentEffect, nextEffect);
    }

    // 자동 탭 스킬 슬롯의 텍스트를 업데이트합니다
    private void UpdateAutoTapUpgradeSlotText()
    {
        int lv = GameController.instance.currentPlayerData.status.autoTapSkillLevel;
        var (tapPerSec, duration) = SkillManager.GetAutoTapEffect(lv);
        var (nextTapPerSec, nextDuration) = SkillManager.GetAutoTapEffect(lv + 1);
        tapUpgradeUI.SetAutoTapUpgradeSlotText(lv, tapPerSec, duration, nextTapPerSec, nextDuration);
    }

    // 탭당 보너스 골드 스킬 슬롯의 텍스트를 업데이트합니다
    private void UpdateBonusGoldPerTapUpgradeSlotText()
    {
        int lv = GameController.instance.currentPlayerData.status.bonusGoldPerTapSkillLevel;
        var (multiplier, duration) = SkillManager.GetBonusGoldPerTapEffect(lv);
        var (nextMultiplier, nextDuration) = SkillManager.GetBonusGoldPerTapEffect(lv + 1);
        tapUpgradeUI.SetBonusGoldPerTapUpgradeSlotText(lv, multiplier, duration, nextMultiplier, nextDuration);
    }

    // 초당 보너스 골드 스킬 슬롯의 텍스트를 업데이트합니다
    private void UpdateBonusGoldPerSecUpgradeSlotText()
    {
        int lv = GameController.instance.currentPlayerData.status.bonusGoldPerSecSkillLevel;
        var (multiplier, duration) = SkillManager.GetBonusGoldPerSecEffect(lv);
        var (nextMultiplier, nextDuration) = SkillManager.GetBonusGoldPerSecEffect(lv + 1);
        tapUpgradeUI.SetBonusGoldPerSecUpgradeSlotText(lv, multiplier, duration, nextMultiplier, nextDuration);
    }

    // 쿨다운 감소 스킬 슬롯의 텍스트를 업데이트합니다
    private void UpdateFastCooldownUpgradeSlotText()
    {
        int lv = GameController.instance.currentPlayerData.status.fastCooldownSkillLevel;
        var cooldown = SkillManager.GetFastCooldownEffect(lv);
        var nextCooldown = SkillManager.GetFastCooldownEffect(lv + 1);
        tapUpgradeUI.SetFastCooldownUpgradeSlotText(lv, cooldown, nextCooldown);
    }

    // 탭 업그레이드 버튼의 상호작용 여부를 업데이트합니다
    private void UpdateTapUpgradeButtonInteractable()
    {
        int lv = GameController.instance.currentPlayerData.status.tapUpgradeLevel;
        double currentHeldGold = GameController.instance.currentPlayerData.status.gold;
        bool isInteractable = currentHeldGold >= UpgradeManager.GetTapUpgradeCost(lv);
        tapUpgradeUI.SetTapUpgradeButtonInteractable(isInteractable);
    }

    // 자동 탭 스킬 버튼의 상호작용 여부를 업데이트합니다
    private void UpdateAutoTapUpgradeButtonInteractable()
    {
        int currentHeldPrestigePoint = GameController.instance.currentPlayerData.status.prestigePoint;
        bool isInteractable = currentHeldPrestigePoint >= PlayerData.skillLevelUpPresitgePointCost;
        tapUpgradeUI.SetAutoTapUpgradeButtonInteractable(isInteractable);
    }

    // 탭당 보너스 골드 스킬 버튼의 상호작용 여부를 업데이트합니다
    private void UpdateBonusGoldPerTapUpgradeButtonInteractable()
    {
        int currentHeldPrestigePoint = GameController.instance.currentPlayerData.status.prestigePoint;
        bool isInteractable = currentHeldPrestigePoint >= PlayerData.skillLevelUpPresitgePointCost;
        tapUpgradeUI.SetBonusGoldPerTapUpgradeButtonInteractable(isInteractable);
    }

    // 초당 보너스 골드 스킬 버튼의 상호작용 여부를 업데이트합니다
    private void UpdateBonusGoldPerSecUpgradeButtonInteractable()
    {
        int currentHeldPrestigePoint = GameController.instance.currentPlayerData.status.prestigePoint;
        bool isInteractable = currentHeldPrestigePoint >= PlayerData.skillLevelUpPresitgePointCost;
        tapUpgradeUI.SetBonusGoldPerSecUpgradeButtonInteractable(isInteractable);
    }

    // 쿨다운 감소 스킬 버튼의 상호작용 여부를 업데이트합니다
    private void UpdateFastCooldownUpgradeButtonInteractable()
    {
        int currentHeldPrestigePoint = GameController.instance.currentPlayerData.status.prestigePoint;
        bool isInteractable = currentHeldPrestigePoint >= PlayerData.skillLevelUpPresitgePointCost;
        tapUpgradeUI.SetFastCooldownUpgradeButtonInteractable(isInteractable);
    }
}