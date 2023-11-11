using static Unity.Mathematics.math;

/// 이 클래스는 보상형 광고 시청시 얻는 버프를 관리합니다.
[System.Serializable]
public class TempBuffData
{
    public readonly string name;
    public readonly float tapBonus;
    public readonly float secBonus;
    private float remainedTime;


    private TempBuffData(string name, float tapBonus, float secBonus, float remainedTime)
    {
        this.name = name;
        this.tapBonus = tapBonus;
        this.secBonus = secBonus;
        this.remainedTime = remainedTime;
    }

    /// 버프의 남은 시간을 반환합니다.
    public float GetRemainedTime()
    {
        return remainedTime;
    }

    /// 버프의 남은 시간을 설정합니다.
    public void SetRemainedTime(float value)
    {
        remainedTime = max(0, value);
    }

    /// 버프가 활성화되어있는지 확인합니다.
    public bool GetIsBuffOn()
    {
        return remainedTime > 0;
    }

    /// 탭 버프 객체를 생성합니다.
    public static TempBuffData GetTapTempBuffDataInstance()
    {
        return new TempBuffData("TapTempBuff", 10f, 1f, 0);
    }

    /// 초당 골드 버프 객체를 생성합니다.
    public static TempBuffData GetSecTempBuffDataInstance()
    {
        return new TempBuffData("SecTempBuff", 1f, 10f, 0);
    }
}