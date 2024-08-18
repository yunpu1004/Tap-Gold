using UnityEngine;

// 플레이어 데이터를 로드하고 저장하는 클래스입니다.
public static class PlayerDataLoader
{
    private const string PlayerDataKey = "PlayerData";

    // 플레이어 데이터를 저장합니다.
    public static void SavePlayerData(PlayerData playerData)
    {
        // PlayerData를 JSON 문자열로 직렬화
        string json = JsonUtility.ToJson(playerData);
        // JSON 문자열을 PlayerPrefs에 저장
        PlayerPrefs.SetString(PlayerDataKey, json);
        // 변경사항을 즉시 저장
        PlayerPrefs.Save();
    }

    // 플레이어 데이터를 불러옵니다.
    public static PlayerData LoadPlayerData()
    {
        // 저장된 PlayerData가 있는지 확인
        if (PlayerPrefs.HasKey(PlayerDataKey))
        {
            // JSON 문자열로 저장된 PlayerData를 가져옴
            string json = PlayerPrefs.GetString(PlayerDataKey);
            // JSON 문자열을 PlayerData 구조체로 역직렬화
            return JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            // 저장된 데이터가 없으면 기본값을 반환
            return new PlayerData();
        }
    }

    // 플레이어 데이터를 삭제합니다.
    public static void ClearPlayerData()
    {
        // PlayerDataKey로 저장된 데이터를 삭제
        PlayerPrefs.DeleteKey(PlayerDataKey);
        // 변경사항을 즉시 저장
        PlayerPrefs.Save();
    }
}