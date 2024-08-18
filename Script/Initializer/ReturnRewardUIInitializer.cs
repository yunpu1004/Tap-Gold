using UnityEngine;

// ReturnRewardUI 컴포넌트를 초기화하는 클래스입니다
public class ReturnRewardUIInitializer : MonoBehaviour
{
    private ReturnRewardUI returnRewardUI;

    // ReturnRewardUI의 이벤트 핸들러를 등록하고 버튼 클릭 이벤트를 연결합니다
    private void Awake()
    {
        returnRewardUI = FindObjectOfType<ReturnRewardUI>(true);
        GameController.instance.OnPlayerDataLoadedEvent += OnPlayerDataLoaded;
        returnRewardUI.rewardButton.onClick.AddListener(OnRewardButtonClicked);
        returnRewardUI.adRewardButton.onClick.AddListener(OnAdRewardButtonClicked);
    }

    // 플레이어 데이터가 로드될 때, 얻을 수 있는 복귀 보상을 UI에 표시합니다
    private void OnPlayerDataLoaded(PlayerData data)
    {
        var reward = data.GetExpectedReturnReward();
        if(reward > 0)
        {
            returnRewardUI.canvas.gameObject.SetActive(true);
            returnRewardUI.SetExpectedRewardText(reward);
        }
    } 

    // 보상 버튼을 클릭했을 때 실행되는 메소드입니다
    // 골드를 획득하고 UI를 비활성화합니다
    private void OnRewardButtonClicked()
    {
        GameController.instance.currentPlayerData.EarnGoldByReturn(false);
        returnRewardUI.canvas.gameObject.SetActive(false);   
    }

    // 광고 보상 버튼을 클릭했을 때 실행되는 메소드입니다
    // 광고를 시청하고 보상을 열배로 획득합니다
    private void OnAdRewardButtonClicked()
    {
        RewardAdRequest.ShowAd(
            null, 
            () => 
            {
                GameController.instance.currentPlayerData.EarnGoldByReturn(true);
                returnRewardUI.canvas.gameObject.SetActive(false);
            }, 
            null
        );
    }
}