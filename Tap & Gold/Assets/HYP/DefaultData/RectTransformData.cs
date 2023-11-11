using Unity.Mathematics;
using UnityEngine;

/// RectTransformData는 RectTransform Component에 대한 정보를 담고 있습니다.
public class RectTransformData : DefaultData
{
    public RectTransform panelRectTransform;
    [HideInInspector] public RectTransform rectTransform;
    private EnumFlag<DragType> dragType;
    private ValueFlag<float2> anchoredPos;
    private ValueFlag<float2> anchorMin;
    private ValueFlag<float2> anchorMax;
    private ValueFlag<float2> offSetMin;
    private ValueFlag<float2> offSetMax;
    private ValueFlag<float2> scale;
    

    public override void OnInit()
    {
        rectTransform = (GetComponent<Canvas>()) ?panelRectTransform :GetComponent<RectTransform>(); 
        anchoredPos = new (rectTransform.anchoredPosition);
        anchorMin = new (rectTransform.anchorMin);
        anchorMax = new (rectTransform.anchorMax);
        offSetMin = new (rectTransform.offsetMin);
        offSetMax = new (rectTransform.offsetMax);
        scale = new (((float3)(rectTransform.localScale)).xy);
        dragType = new (DragType.None);
        ComponentManager.AddComponent(hierarchyName, this);
    }


    public float2 GetAnchoredPos()
    {
        return anchoredPos.value;
    }

    public void SetAnchoredPos(in float2 value)
    {
        anchoredPos.value = value;
    }

    public float2 GetAnchorMin()
    {
        return anchorMin.value;
    }

    public void SetAnchorMin(in float2 value)
    {
        anchorMin.value = value;
    }

    public float2 GetAnchorMax()
    {
        return anchorMax.value;
    }

    public void SetAnchorMax(in float2 value)
    {
        anchorMax.value = value;
    }

    public float2 GetAnchorPos()
    {
        return (anchorMin.value + anchorMax.value) / 2;
    }

    public void SetAnchorPos(in float2 value)
    {
        var delta = value - GetAnchorPos();
        anchorMin.value += delta;
        anchorMax.value += delta;
    }

    public float2 GetOffSetMin()
    {
        return offSetMin.value;
    }

    public void SetOffSetMin(in float2 value)
    {
        offSetMin.value = value;
    }

    public float2 GetOffSetMax()
    {
        return offSetMax.value;
    }

    public void SetOffSetMax(in float2 value)
    {
        offSetMax.value = value;
    }

    public float2 GetSizeScale()
    {
        return scale.value;
    }

    public void SetSizeScale(in float2 value)
    {
        scale.value = value;
    }


    /// DragType을 반환합니다.
    public DragType GetDragType()
    {
        return dragType.value;
    }

    /// DragType을 변경합니다.
    public void SetDragType(in DragType value)
    {
        dragType.value = value;
    }



    public bool IsChanged()
    {
        return anchoredPos.isChanged || anchorMin.isChanged || anchorMax.isChanged || offSetMin.isChanged || offSetMax.isChanged || scale.isChanged || dragType.isChanged;
    }

    public void Sync()
    {
        if(anchoredPos.isChanged)
        {
            rectTransform.anchoredPosition = anchoredPos.value;
            anchoredPos.isChanged = false;
        }

        if(anchorMin.isChanged)
        {
            rectTransform.anchorMin = anchorMin.value;
            anchorMin.isChanged = false;
        }

        if(anchorMax.isChanged)
        {
            rectTransform.anchorMax = anchorMax.value;
            anchorMax.isChanged = false;
        }

        if(offSetMin.isChanged)
        {
            rectTransform.offsetMin = offSetMin.value;
            offSetMin.isChanged = false;
        }

        if(offSetMax.isChanged)
        {
            rectTransform.offsetMax = offSetMax.value;
            offSetMax.isChanged = false;
        }

        if(scale.isChanged)
        {
            rectTransform.localScale = new Vector3(scale.value.x, scale.value.y, rectTransform.localScale.z);
            scale.isChanged = false;
        }
        
        dragType.isChanged = false;
    }
}