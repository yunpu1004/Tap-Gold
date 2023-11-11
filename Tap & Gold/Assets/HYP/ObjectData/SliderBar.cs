using UnityEngine;
using UnityEngine.UI;
using static Unity.Mathematics.math;


[RequireComponent(typeof(AnchorFitter), typeof(RectTransform))]
[RequireComponent(typeof(GameObjectData), typeof(EventData))]
[RequireComponent(typeof(RectTransformData))]
public class SliderBar : ObjectData
{
    public RectTransform rectTransform;
    public Image barImage;
    public Image handleImage;
    public RectTransform barRect;
    public RectTransform handle;
    
    [SerializeField, HideInInspector] private float barValue_Serialized;

    private ValueFlag<float> barValue;
    public GameObjectData gameObjectData { get; private set; }
    public RectTransformData rectTransformData { get; private set; }
    public EventData eventData { get; private set; }


    protected override void OnInit()
    {
        if(barImage.raycastTarget) throw new System.Exception("슬라이더바의 자식 오브젝트는 레이캐스트를 받지 않도록 설정해야 합니다.");
        if(handleImage.raycastTarget) throw new System.Exception("슬라이더바의 자식 오브젝트는 레이캐스트를 받지 않도록 설정해야 합니다.");

        this.barValue = new (barValue_Serialized);
        this.gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        this.rectTransformData = ComponentManager.GetComponent<RectTransformData>(hierarchyName);
        this.eventData = ComponentManager.GetComponent<EventData>(hierarchyName);
        eventData.AddTouchDataEvent(SliderBarInteractionEvent);
        ComponentManager.AddComponent<SliderBar>(hierarchyName, this);
    }


    public void ApplyValue(float value)
    {
        Vector2 anchorMin = Vector2.zero;
        Vector2 anchorMax = float2(value, 1);
        barRect.anchorMin = anchorMin;
        barRect.anchorMax = anchorMax;
        barRect.offsetMin = Vector2.zero;
        barRect.offsetMax = Vector2.zero;

        var handleSize = handle.anchorMax.x - handle.anchorMin.x;
        var handlePos = value - handleSize / 2;
        handle.anchorMin = float2(handlePos, 0);
        handle.anchorMax = float2(handlePos + handleSize, 1);
        handle.offsetMin = Vector2.zero;
        handle.offsetMax = Vector2.zero;
    }


    private void SliderBarInteractionEvent(){
        var screenPosX = TouchData.pointerScreenPos.x;
        var anchorMinX = rectTransformData.GetAnchorMin().x;
        var anchorMaxX = rectTransformData.GetAnchorMax().x;
        var newValue = clamp(unlerp(anchorMinX, anchorMaxX, screenPosX), 0 ,1);
        barValue_Serialized = newValue;
        ApplyValue(barValue_Serialized);
    }


    protected override void DataUpdate()
    {
        eventData.InvokeDataUpdateEvent();
        if(IsSyncRequired()) UpdateManager.AddMainThreadQueue(Sync);
    }

    private bool IsSyncRequired()
    {
        return gameObjectData.IsChanged() || rectTransformData.IsChanged() || barValue.isChanged;
    }

    private void Sync()
    {
        gameObjectData.Sync();
        rectTransformData.Sync();
        if(barValue.isChanged)
        {
            ApplyValue(barValue.value);
            barValue.isChanged = false;
        }
    }
}