using Unity.Mathematics;
using UnityEngine;
using static Unity.Mathematics.math;

public class TweenUtil
{
    public static float Linear(float start, float end, float progress)
    {
        if(progress < 0 || progress > 1)
        {
            Debug.Log("progress must be between 0 and 1");
            return start;
        }
        
        return start + (end - start) * progress;
    }

    public static float2 Linear(float2 start, float2 end, float progress)
    {
        if(progress < 0 || progress > 1)
        {
            Debug.Log("progress must be between 0 and 1");
            return start;
        }
        
        return start + (end - start) * progress;
    }


    public static float Sin(float start, float end, float progress)
    {
        if(progress < 0 || progress > 1)
        {
            Debug.Log("progress must be between 0 and 1");
            return start;
        }
        
        return start + (end - start) * (1 - cos(progress * PI)) / 2;
    }

    public static float2 Sin(float2 start, float2 end, float progress)
    {
        if(progress < 0 || progress > 1)
        {
            Debug.Log("progress must be between 0 and 1");
            return start;
        }
        
        return start + (end - start) * (1 - cos(progress * PI)) / 2;
    }


    public static float Ease(float start, float end, float progress)
    {
        if(progress < 0 || progress > 1)
        {
            Debug.Log("progress must be between 0 and 1");
            return start;
        }
        
        return start + (end - start) * (progress - sin(progress * 2 * PI) / (2 * PI));
    }

    public static float2 Ease(float2 start, float2 end, float progress)
    {
        if(progress < 0 || progress > 1)
        {
            Debug.Log("progress must be between 0 and 1");
            return start;
        }
        
        return start + (end - start) * (progress - sin(progress * 2 * PI) / (2 * PI));
    }


    public static float EaseBack(float start, float end, float progress, float strength = 1.70158f)
    {
        if(progress < 0 || progress > 1)
        {
            Debug.Log("progress must be between 0 and 1");
            return start;
        }
        
        progress -= 1;
        return start + (end - start) * (progress * progress * ((strength + 1) * progress + strength) + 1);
    }

    public static float2 EaseBack(float2 start, float2 end, float progress, float strength = 1.70158f)
    {
        if(progress < 0 || progress > 1)
        {
            Debug.Log("progress must be between 0 and 1");
            return start;
        }
        
        progress -= 1;
        return start + (end - start) * (progress * progress * ((strength + 1) * progress + strength) + 1);
    }
}
