using UnityEngine;


/// TrailData는 TrailRenderer에 대한 정보를 담고 있습니다.
public class TrailData : DefaultData
{
    private TrailRenderer trailRenderer;
    private ValueFlag<bool> emit;
    private ValueFlag<Color> startColor;
    private ValueFlag<Color> endColor;
    private ValueFlag<float> startWidth;
    private ValueFlag<float> endWidth;


    public override void OnInit()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        emit = new (trailRenderer.emitting);
        startColor = new (trailRenderer.startColor);
        endColor = new (trailRenderer.endColor);
        startWidth = new (trailRenderer.startWidth);
        endWidth = new (trailRenderer.endWidth);
        ComponentManager.AddComponent(hierarchyName, this);
    }



    public void SetEmit(bool value)
    {
        emit.value = value;
    }

    public bool GetEmit()
    {
        return emit.value;
    }

    public void SetStartColor(Color value)
    {
        startColor.value = value;
    }

    public Color GetStartColor()
    {
        return startColor.value;
    }

    public void SetEndColor(Color value)
    {
        endColor.value = value;
    }

    public Color GetEndColor()
    {
        return endColor.value;
    }

    public void SetStartWidth(float value)
    {
        startWidth.value = value;
    }

    public float GetStartWidth()
    {
        return startWidth.value;
    }

    public void SetEndWidth(float value)
    {
        endWidth.value = value;
    }

    public float GetEndWidth()
    {
        return endWidth.value;
    }


    public bool IsChanged()
    {
        return emit.isChanged || startColor.isChanged || endColor.isChanged || startWidth.isChanged || endWidth.isChanged;
    }

    public void Sync()
    {
        if(emit.isChanged)
        {
            trailRenderer.emitting = emit.value;
            if(!emit.value) trailRenderer.Clear();
            emit.isChanged = false;
        }

        if(startColor.isChanged)
        {
            trailRenderer.startColor = startColor.value;
            startColor.isChanged = false;
        }

        if(endColor.isChanged)
        {
            trailRenderer.endColor = endColor.value;
            endColor.isChanged = false;
        }

        if(startWidth.isChanged)
        {
            trailRenderer.startWidth = startWidth.value;
            startWidth.isChanged = false;
        }

        if(endWidth.isChanged)
        {
            trailRenderer.endWidth = endWidth.value;
            endWidth.isChanged = false;
        }
    }
}
