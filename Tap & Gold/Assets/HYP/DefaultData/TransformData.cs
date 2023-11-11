using Unity.Mathematics;
using UnityEngine;
using System;


/// TransformData는 Transform Component에 대한 정보를 담고 있습니다.
public class TransformData : DefaultData
{
    private Transform tr;
    private EnumFlag<DragType> dragType;
    private ValueFlag<float3> localPosition;
    private ValueFlag<float> localRotation;
    private ValueFlag<float3> localScale;


    public override void OnInit()
    {
        if(GetComponent<ObjectData>() != null)
        {
            dragType = new (DragType.None);
            tr = GetComponent<Transform>();
            localPosition = new (transform.localPosition);
            localRotation = new (transform.localEulerAngles.z);
            localScale = new (transform.localScale);
        }
        else throw new Exception("TransformData는 DataComponent를 가지고 있어야 합니다.");
        ComponentManager.AddComponent(hierarchyName, this);
    }

    /// LocalPosition을 반환합니다.
    public float3 GetLocalPosition()
    {
        return localPosition.value;
    }

    /// LocalPosition을 변경합니다.
    public void SetLocalPosition(in float3 value, bool notifyChanged = true)
    {
        var changed_temp = localPosition.isChanged;
        localPosition.value = value;
        if(!notifyChanged) localPosition.isChanged = changed_temp;
    }

    /// LocalPosition을 변경합니다.
    public void SetLocalPosition(in float2 value, bool notifyChanged = true)
    {
        var changed_temp = localPosition.isChanged;
        var pos = localPosition.value;
        pos.x = value.x;
        pos.y = value.y;
        localPosition.value = pos;
        if(!notifyChanged) localPosition.isChanged = changed_temp;
    }

    /// LocalRotation을 반환합니다.
    public float GetLocalRotation()
    {
        return localRotation.value;
    }

    /// LocalRotation을 변경합니다.
    public void SetLocalRotation(in float value, bool notifyChanged = true)
    {
        var changed_temp = localPosition.isChanged;
        localRotation.value = value;
        if(!notifyChanged) localRotation.isChanged = changed_temp;
    }

    /// LocalScale을 반환합니다.
    public float3 GetLocalScale()
    {
        return localScale.value;
    }

    /// LocalScale을 변경합니다.
    public void SetLocalScale(in float3 value)
    {
        localScale.value = value;
    }

    /// LocalScale을 변경합니다.
    public void SetLocalScale(in float2 value)
    {
        var scale = localScale.value;
        scale.x = value.x;
        scale.y = value.y;
        localScale.value = scale;
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
        return localPosition.isChanged || localRotation.isChanged || localScale.isChanged;
    }

    public void Sync()
    {
        if(localPosition.isChanged) 
        {
            tr.localPosition = localPosition.value;
            localPosition.isChanged = false; 
        }

        if(localRotation.isChanged) 
        {
            tr.localEulerAngles = new Vector3(0,0,localRotation.value);
            localRotation.isChanged = false; 
        }

        if(localScale.isChanged) 
        { 
            tr.localScale = localScale.value;
            localScale.isChanged = false; 
        }
    }

    public void ReadTransform(Transform tr)
    {
        if(!localPosition.isChanged) { SetLocalPosition(tr.localPosition, false); }
        if(!localRotation.isChanged) { SetLocalRotation(tr.localEulerAngles.z, false); }
    }
}