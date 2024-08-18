using UnityEngine;

// StatisticsUI 컴포넌트를 초기화하는 클래스입니다
public class StatisticsUIInitializer : MonoBehaviour
{
    private StatisticsUI statisticsUI;

    // StatisticsUI의 이벤트 핸들러를 등록합니다
    private void Awake() 
    {
        statisticsUI = FindObjectOfType<StatisticsUI>(true);
        GameController.instance.OnPlayerDataLoadedEvent += OnPlayerDataLoaded;
        GameController.instance.OnPlayerDataChangedEvent += OnPlayerDataChanged;
    }

    // 플레이어 데이터가 로드될 때, 통계 정보를 UI에 표시합니다
    private void OnPlayerDataLoaded(PlayerData data)
    {
        statisticsUI.SetCurrentDefaultGoldPerTapText(data.status.defaultGoldPerTap);
        statisticsUI.SetCurrentDefaultGoldPerSecText(data.status.defaultGoldPerSec);
        statisticsUI.SetHighestDefaultGoldPerTapText(data.info.highestDefaultGoldPerTap);
        statisticsUI.SetHighestDefaultGoldPerSecText(data.info.highestDefaultGoldPerSec);
        statisticsUI.SetHighestGoldHeldText(data.info.highestGoldHeld);
    }

    // 플레이어 데이터가 변경될 때, 통계 정보를 UI에 업데이트합니다
    private void OnPlayerDataChanged(PlayerData prev, PlayerData cur)
    {
        if (cur.status.defaultGoldPerTap != prev.status.defaultGoldPerTap)
        {
            statisticsUI.SetCurrentDefaultGoldPerTapText(cur.status.defaultGoldPerTap);
        }

        if (cur.status.defaultGoldPerSec != prev.status.defaultGoldPerSec)
        {
            statisticsUI.SetCurrentDefaultGoldPerSecText(cur.status.defaultGoldPerSec);
        }

        if (cur.info.highestDefaultGoldPerTap != prev.info.highestDefaultGoldPerTap)
        {
            statisticsUI.SetHighestDefaultGoldPerTapText(cur.info.highestDefaultGoldPerTap);
        }

        if (cur.info.highestDefaultGoldPerSec != prev.info.highestDefaultGoldPerSec)
        {
            statisticsUI.SetHighestDefaultGoldPerSecText(cur.info.highestDefaultGoldPerSec);
        }

        if (cur.info.highestGoldHeld != prev.info.highestGoldHeld)
        {
            statisticsUI.SetHighestGoldHeldText(cur.info.highestGoldHeld);
        }
    }
}