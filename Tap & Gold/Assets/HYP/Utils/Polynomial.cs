using System.Runtime.CompilerServices;
using Unity.Mathematics;
using static Unity.Mathematics.math;

/// Polynomial은 다항함수을 계산하는 클래스입니다.
public static class Polynomial
{
    /// 다항식 한개에 대해서 float4를 계수로 하는 3차함수의 값을 반환합니다.
    /// float4의 x, y, z, w는 각각 3차, 2차, 1차, 0차 항의 계수입니다.    
    public static void CalcPoly(in float input, in float4 coef, out float result)
    {
        result = 0;
        for(int i = 0; i < 4; i++)
        {
            result += coef[3-i] * pow(input, i);
        }
    }


    /// 주어진 함수를 y축으로 대칭시킨 함수의 계수를 반환합니다.
    /// x, y, z, w는 각각 3차, 2차, 1차, 0차 항의 계수입니다.
    public static void SymPolyYAxis(in float4 coef, out float4 result)
    {
        result = coef;
        result.x *= -1;
        result.z *= -1;
    }


    /// 주어진 함수를 x축으로 대칭시킨 함수의 계수를 반환합니다.
    /// <br/>x, y, z, w는 각각 3차, 2차, 1차, 0차 항의 계수입니다.    
    public static void SymPolyXAxis(in float4 coef, out float4 result)
    {
        result = coef;
        result.x *= -1;
        result.y *= -1;
        result.z *= -1;
        result.w *= -1;
    }


    /// 주어진 함수를 원점을 중심으로 대칭시킨 함수의 계수를 반환합니다.
    /// x, y, z, w는 각각 3차, 2차, 1차, 0차 항의 계수입니다.    
    public static void SymPolyOrigin(in float4 coef, out float4 result)
    {
        result = coef;
        result.y *= -1;
        result.w *= -1;
    }


    /// 주어진 함수를 평행이동시킨 함수의 계수를 반환합니다.
    /// x, y, z, w는 각각 3차, 2차, 1차, 0차 항의 계수입니다.
    public static void MovePoly(in float4 coef, float offsetX, float offsetY, out float4 result)
    {
        var temp = float4(0);
        temp.x = coef.x;
        temp.y = coef.y - 3*coef.x*offsetX;
        temp.z = 3*coef.x*offsetX*offsetX - 2*coef.y*offsetX + coef.z;
        temp.w = coef.w + offsetY - coef.x*offsetX*offsetX*offsetX + coef.y*offsetX*offsetX - coef.z*offsetX;
        result = temp;
    }



    /// 주어진 함수를 원점을 기준으로 늘리거나 줄인 함수의 계수를 반환합니다.
    /// x, y, z, w는 각각 3차, 2차, 1차, 0차 항의 계수입니다.
    public static void ScalePoly(in float4 coef, float offsetX, float offsetY, out float4 result)
    {
        var temp = float4(0);
        temp.x = coef.x * offsetY / (offsetX * offsetX * offsetX);
        temp.y = coef.y * offsetY  / (offsetX * offsetX);
        temp.z = coef.z * offsetY  / offsetX;
        temp.w = coef.w * offsetY;
        result = temp;
    }


    /// 주어진 범위 내에서 시그모이드와 유사한 함수의 계수를 반환합니다.
    /// f(a) = -1, f(b) = 1, f((a+b)/2) = 0
    public static void SigmoidPoly(float a, float b, out float4 result)
    {
        result = float4(-0.5f, 0, 1.5f, 0);
        float scale = (b - a)/2;
        float center = (a + b)/2;
        ScalePoly(result, scale, 1, out result);
        MovePoly(result, center, 0, out result);

    }


    /// 주어진 범위 내에서 사인함수와 유사한 함수의 계수를 반환합니다.
    /// f(a) = 0, f(b) = 0, f((a+b)/2) = 0 (최대값이 1, 최소값이 -1)
    /// 이 함수는 굴곡에 해당하는 지점이 양 끝에 더 가깝게 위치합니다. (범위가 [-1, 1] 일때, f(±0.577) = ∓1)
    public static void SinPoly(float a, float b, out float4 result)
    {
        result = float4(1, 0, -1, 0) * 2.598f;
        float scale = (b - a)/2;
        float center = (a + b)/2;
        ScalePoly(result, scale, 1, out result);
        MovePoly(result, center, 0, out result);
    }


    /// 단순한 3차 함수의 계수를 반환합니다.
    public static float4 SimpleCubicPoly()
    {
        return float4(1, 0, 0, 0);
    }


    /// 단순한 2차 함수의 계수를 반환합니다.
    public static float4 SimpleQuadPoly()
    {
        return float4(0, 1, 0, 0);
    }


    /// 단순한 1차 함수의 계수를 반환합니다.
    public static float4 SimpleLinePoly()
    {
        return float4(0, 0, 1, 0);
    }


    /// 단순한 상수 함수의 계수를 반환합니다.
    public static float4 SimpleConstPoly()
    {
        return float4(0, 0, 0, 1);
    }
}
