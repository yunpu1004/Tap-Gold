using System;

// 시간 관련 유틸리티 클래스입니다.
public static class DateTimeUtil
{
    // DateTime의 최초 시간부터 현재까지의 총 초를 계산합니다.
    public static long GetTotalSecondsFromEarliestTime()
    {
        // DateTime의 최초 시간
        DateTime earliestTime = DateTime.MinValue;

        // 현재 시간
        DateTime currentTime = DateTime.Now;

        // 두 시간 사이의 간격 계산
        TimeSpan timeSpan = currentTime - earliestTime;

        // 총 초 계산
        double totalSeconds = timeSpan.TotalSeconds;

        // 소수점 자리 버림
        long result = (long)totalSeconds;

        return result;
    }
}