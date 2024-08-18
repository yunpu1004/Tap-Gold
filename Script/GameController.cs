using System;
using UnityEngine;

// 게임의 전반적인 흐름을 제어하는 클래스입니다.
// 싱글톤 패턴을 사용하여 GameController의 인스턴스를 유일하게 유지합니다.
public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }
    public event Action<PlayerData> OnPlayerDataLoadedEvent;
    public event Action<PlayerData, PlayerData> OnPlayerDataChangedEvent;
    public PlayerData previousPlayerData;
    public PlayerData currentPlayerData;


    // Awake에서 GameController의 인스턴스를 설정하고, 플레이어 데이터를 로드합니다.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        currentPlayerData = PlayerDataLoader.LoadPlayerData();
        currentPlayerData.NotifyGameStart();
        previousPlayerData.CopyFrom(currentPlayerData);
    }

    // 플레이어 데이터를 로드하고, 초당 골드 획득 코루틴을 시작합니다.
    private void Start() 
    {
        OnPlayerDataLoadedEvent?.Invoke(currentPlayerData);
        StartCoroutine(currentPlayerData.EarnGoldPerSecCoroutine());
    }

    // 현재 데이터를 이전 데이터와 비교하여 적절한 이벤트를 발생시킵니다.
    // 이후, 이전 데이터에 현재 데이터를 복사하고, 현재 데이터를 저장합니다.
    private void LateUpdate() 
    {
        OnPlayerDataChangedEvent?.Invoke(previousPlayerData, currentPlayerData);
        previousPlayerData.CopyFrom(currentPlayerData);
        PlayerDataLoader.SavePlayerData(currentPlayerData);
    }
}