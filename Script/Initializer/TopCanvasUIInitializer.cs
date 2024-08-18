using UnityEngine;

// TopCanvasUI 컴포넌트를 초기화하는 클래스입니다
public class TopCanvasUIInitializer : MonoBehaviour
{    
    private TopCanvasUI topCanvasUI;

    // TopCanvasUI의 이벤트 핸들러를 등록하고 버튼 클릭 이벤트를 연결합니다
    private void Awake()
    {
        topCanvasUI = FindObjectOfType<TopCanvasUI>(true);
        GameController.instance.OnPlayerDataLoadedEvent += OnPlayerDataLoaded;
        GameController.instance.OnPlayerDataChangedEvent += OnPlayerDataChanged;
        topCanvasUI.volumeButton.onClick.AddListener(OnVolumeButtonClicked);
        topCanvasUI.showRewardAdButton_TapBuff.onClick.AddListener(ShowRewardAdButton_TapBuffClicked);
        topCanvasUI.showRewardAdButton_SecBuff.onClick.AddListener(ShowRewardAdButton_SecBuffClicked);
    }

    // 플레이어 데이터가 로드될 때, 골드와 탭/초당 골드 획득량 정보를 UI에 표시합니다
    private void OnPlayerDataLoaded(PlayerData data)
    {
        topCanvasUI.SetGoldText(data.status.gold);
        topCanvasUI.SetGoldPerTapText(data.GetGoldPerTap());
        topCanvasUI.SetGoldPerSecText(data.GetGoldPerSec());
    }

    // 플레이어 데이터가 변경될 때, 골드와 탭/초당 골드 획득량 정보를 UI에 업데이트합니다
    private void OnPlayerDataChanged(PlayerData prev, PlayerData cur)
    {
        // 골드가 변경되었을 때, UI를 업데이트함
        if(cur.status.gold != prev.status.gold)
        {
            topCanvasUI.SetGoldText(cur.status.gold);
        }

        // 탭당 골드 획득량이 변경되었을 때, UI를 업데이트함
        if(cur.GetGoldPerTap() != prev.GetGoldPerTap())
        {
            topCanvasUI.SetGoldPerTapText(cur.GetGoldPerTap());
        }

        // 초당 골드 획득량이 변경되었을 때, UI를 업데이트함
        if(cur.GetGoldPerSec() != prev.GetGoldPerSec())
        {
            topCanvasUI.SetGoldPerSecText(cur.GetGoldPerSec());
        }
    }

    // 볼륨 버튼을 클릭했을 때 실행되는 메소드입니다
    // 볼륨을 켜거나 끄며, 아이콘을 변경합니다
    private void OnVolumeButtonClicked()
    {
        bool volumeState = AudioManager.GetVolumeState();
        AudioManager.SetVolumeState(!volumeState);
        topCanvasUI.SetVolumeIcon(!volumeState);
    }

    // 탭 버프 광고 버튼을 클릭했을 때 실행되는 메소드입니다
    // 광고를 시청하고 탭당 골드 획득량 증가 버프를 활성화합니다
    private void ShowRewardAdButton_TapBuffClicked()
    {
        RewardAdRequest.ShowAd(
            null,
            () => 
            {
                var currentPlayerData = GameController.instance.currentPlayerData;
                GameController.instance.StartCoroutine(currentPlayerData.ActivateTapBuffCoroutine());
            },
            () => topCanvasUI.tapBuffPanel.SetActive(false)
        );
    }
    
    // 초당 버프 광고 버튼을 클릭했을 때 실행되는 메소드입니다
    // 광고를 시청하고 초당 골드 획득량 증가 버프를 활성화합니다
    private void ShowRewardAdButton_SecBuffClicked()
    {
        RewardAdRequest.ShowAd(
            null,
            () => 
            {
                var currentPlayerData = GameController.instance.currentPlayerData;
                GameController.instance.StartCoroutine(currentPlayerData.ActivateSecBuffCoroutine());
            },
            () => topCanvasUI.secBuffPanel.SetActive(false)
        );
    }
}