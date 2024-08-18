using System;

// 이 클래스는 Double에 관련된 유틸리티 함수를 제공합니다.
public static class DoubleUtil
{
    // Double을 자리수에 따라 적절하게 문자열로 변환합니다.
    public static string DoubleToString(double number)
    {
        if(number < 100)
        {
            return number.ToString("0.##");
        }
        else if (number < 1000000)
        {
            return Math.Truncate(number).ToString();
        }
        else
        {
            return number.ToString("0.000E+0");
        }
    }
}