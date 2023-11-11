using static Unity.Mathematics.math;

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

    public float GetRemainedTime()
    {
        return remainedTime;
    }

    public void SetRemainedTime(float value)
    {
        remainedTime = max(0, value);
    }

    public bool GetIsBuffOn()
    {
        return remainedTime > 0;
    }

    public static TempBuffData GetTapTempBuffDataInstance()
    {
        return new TempBuffData("TapTempBuff", 10f, 1f, 0);
    }

    public static TempBuffData GetSecTempBuffDataInstance()
    {
        return new TempBuffData("SecTempBuff", 1f, 10f, 0);
    }
}