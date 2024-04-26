using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

/// RectTransform의 크기와 위치 및 앵커를 조작하는 유틸리티입니다.
/// RectTransform와 부모 및 캔버스는 스케일 팩터가 반드시 1이어야 합니다.
public static class RectTransformUtil
{
    private static Camera mainCamera
    {
        get
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
            }
            return _mainCamera;
        }
    }

    private static Camera _mainCamera;


    /// 오프셋이 0이 되도록 앵커를 사이즈에 딱 맞춥니다.
    public static void SetAnchorFit(RectTransform rectTransform, RectTransform parentRectTransform)
    {
        var minMax = GetLocalNormalizedMinMax(rectTransform, parentRectTransform);

        rectTransform.anchorMin = minMax.min;
        rectTransform.anchorMax = minMax.max;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }


    /// 앵커의 최소와 최대가 같은 위치에 오도록 설정하고, 오프셋을 사이즈에 맞춥니다.
    public static void SetAnchorPos(RectTransform rectTransform, RectTransform parentRectTransform)
    {
        var posMinMax = GetLocalNormalizedMinMax(rectTransform, parentRectTransform);
        var pos = (posMinMax.min + posMinMax.max) / 2;
        var pixelMinMax = GetLocalScreenMinMax(rectTransform);
        var offset = (pixelMinMax.max - pixelMinMax.min);
        rectTransform.anchorMin = pos;
        rectTransform.anchorMax = pos;
        rectTransform.offsetMin = -offset / 2;
        rectTransform.offsetMax = offset / 2;
    }

    
    /// 최소, 최대 로컬 Normalized 위치를 반환합니다.
    /// 이 값은 Anchor 설정이 다르더라도 크기만 같다면 동일한 값을 반환합니다.
    public static (Vector2 min, Vector2 max) GetLocalNormalizedMinMax(RectTransform rectTransform, RectTransform parentRectTransform)
    {
        var rect = rectTransform.rect;
        var rectPos = (Vector2)rectTransform.localPosition;
        var pivot = rectTransform.pivot;
        var rectSize = rect.size;
        var rectLocalPixelMin = rectPos - rectSize / 2 + rectSize * (Vector2.one/2 - pivot);
        var rectLocalPixelMax = rectPos + rectSize / 2 + rectSize * (Vector2.one/2 - pivot);
        
        var parentRect = parentRectTransform.rect;
        var parentRectSize = parentRect.size;
        var parentPivot = parentRectTransform.pivot;

        var localAnchorMin = rectLocalPixelMin / parentRectSize + parentPivot;
        var localAnchorMax = rectLocalPixelMax / parentRectSize + parentPivot;

        return (localAnchorMin, localAnchorMax);
    }


    /// 최소, 최대 로컬 스크린 위치를 반환합니다. (부모 이미지 가운데가 원점)
    /// 이 값은 Anchor 설정이 다르더라도 크기만 같다면 동일한 값을 반환합니다.
    public static (Vector2 min, Vector2 max) GetLocalScreenMinMax(RectTransform rectTransform)
    {
        var rect = rectTransform.rect;
        var rectPos = (Vector2)rectTransform.localPosition;
        var pivot = rectTransform.pivot;
        var rectSize = rect.size;
        var rectLocalPixelMin = rectPos - rectSize / 2 + rectSize * (Vector2.one/2 - pivot);
        var rectLocalPixelMax = rectPos + rectSize / 2 + rectSize * (Vector2.one/2 - pivot);

        return (rectLocalPixelMin, rectLocalPixelMax);
    }


    /// 최소, 최대 월드 Normalize 위치를 반환합니다.
    /// 이 값은 Anchor 설정이 다르더라도 크기만 같다면 동일한 값을 반환합니다.
    public static (Vector2 min, Vector2 max) GetWorldNormalizedMinMax(RectTransform rectTransform)
    {
        var resolution = mainCamera.pixelRect.size;
        var rect = rectTransform.rect;
        var rectPos = (Vector2)rectTransform.position * resolution.y/2 / mainCamera.orthographicSize;
        var rectSize = rect.size;
        var pivot = rectTransform.pivot;
        var rectWorldPixelMin = rectPos - rectSize / 2 + rectSize * (Vector2.one/2 - pivot);
        var rectWorldPixelMax = rectPos + rectSize / 2 + rectSize * (Vector2.one/2 - pivot);

        var worldAnchorMin = rectWorldPixelMin / resolution + 0.5f * Vector2.one;
        var worldAnchorMax = rectWorldPixelMax / resolution + 0.5f * Vector2.one;

        return (worldAnchorMin, worldAnchorMax);
    }


    /// 최소, 최대 월드 Screen 위치를 반환합니다. (화면 가운데가 원점)
    /// 이 값은 Anchor 설정이 다르더라도 크기만 같다면 동일한 값을 반환합니다.
    public static (Vector2 min, Vector2 max) GetWorldScreenMinMax(RectTransform rectTransform)
    {
        var resolution = mainCamera.pixelRect.size;
        var rect = rectTransform.rect;
        var pivot = rectTransform.pivot;
        var rectSize = rect.size;
        var rectPos = (Vector2)rectTransform.position * resolution.y/2 / mainCamera.orthographicSize;
        
        
        var rectWorldPixelMin = rectPos - rectSize / 2 + rectSize * (Vector2.one/2 - pivot);
        var rectWorldPixelMax = rectPos + rectSize / 2 + rectSize * (Vector2.one/2 - pivot);

        return (rectWorldPixelMin, rectWorldPixelMax);
    }


    /// 월드 좌표를 스크린 좌표로 변환합니다.
    public static Vector2 WorldToScreenPoint(Vector3 worldPoint)
    {
        return RectTransformUtility.WorldToScreenPoint(mainCamera, worldPoint);
    }

    /// 스크린 좌표를 월드 좌표로 변환합니다.
    public static Vector3 ScreenToWorldPoint(Vector2 screenPoint)
    {
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, mainCamera.nearClipPlane));
        return worldPoint;
    }


    /// 스크린(픽셀) 좌표를 RectTransform의 로컬 좌표로 변환합니다.
    public static Vector2 ScreenToLocalPoint(RectTransform rectTransform, Vector2 screenPoint)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, mainCamera, out localPoint);
        return localPoint;
    }

    /// RectTransform의 로컬 좌표를 Normalized 좌표로 변환합니다.
    public static Vector2 LocalToNormalizedPoint(RectTransform rectTransform, Vector2 localPoint)
    {
        var rect = rectTransform.rect;
        var pivot = rectTransform.pivot;
        var rectSize = rect.size;
        var rectLocalPixelMin = -rectSize / 2 + rectSize * (Vector2.one/2 - pivot);
        var rectLocalPixelMax = rectSize / 2 + rectSize * (Vector2.one/2 - pivot);

        var normalizedPoint = (localPoint - rectLocalPixelMin) / (rectLocalPixelMax - rectLocalPixelMin);

        return normalizedPoint;
    }
}