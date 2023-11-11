# if UNITY_EDITOR
using UnityEditor;
# endif
using UnityEngine;

public static class ResolutionUtil
{
    public static Vector2 GetResolution()
    {
        # if UNITY_EDITOR
        string[] res = UnityStats.screenRes.Split('x');
        var result = new Vector2(int.Parse(res[0]), int.Parse(res[1]));
        if(result.Equals(Vector2.zero))
        {
            var rect = Camera.main.pixelRect;
            result = new Vector2(rect.width, rect.height);
        }
        return result;
        # else
        return Camera.main.pixelRect.size;
        # endif
    }
}
