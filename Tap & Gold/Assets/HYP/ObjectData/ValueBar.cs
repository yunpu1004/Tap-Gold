using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Mathematics.math;



[RequireComponent(typeof(AnchorFitter), typeof(Image))]
[RequireComponent(typeof(GameObjectData), typeof(EventData))]
[RequireComponent(typeof(RectTransformData))]
public class ValueBar : ObjectData
{
    public RectTransform barRect;
    public TextMeshProUGUI text;
    [SerializeField, HideInInspector] private float barValue_Serialize;
    [SerializeField, HideInInspector] private DecimalType decimalType_Serialize;
    private ValueFlag<float> barValue;
    public GameObjectData gameObjectData { get; private set; }
    public RectTransformData rectTransformData { get; private set; }
    public EventData eventData { get; private set; }
    


    protected override void OnInit()
    {
        barValue = new (barValue_Serialize);
        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        rectTransformData = ComponentManager.GetComponent<RectTransformData>(hierarchyName);
        eventData = ComponentManager.GetComponent<EventData>(hierarchyName);
        ComponentManager.AddComponent(hierarchyName, this);
    }

    public void _ApplyValueFromEditor(float value, DecimalType decimalType)
    {
        float clampedValue = clamp(value, 0, 1);
        barValue_Serialize = clampedValue;
        decimalType_Serialize = decimalType;
        _ApplyValue(barValue_Serialize, decimalType_Serialize);
    }

    private void _ApplyValue(float value, DecimalType decimalType)
    {
        float clampedValue = clamp(value, 0, 1);
        Vector2 anchorMin = Vector2.zero;
        Vector2 anchorMax = float2(clampedValue, 1);
        barRect.anchorMin = anchorMin;
        barRect.anchorMax = anchorMax;
        barRect.offsetMin = Vector2.zero;
        barRect.offsetMax = Vector2.zero;

        if(decimalType == DecimalType.Point)
        {
            /// 소숫점 3자리까지 표시
            text.text = $"{clampedValue:0.###}";
        }
        else if(decimalType == DecimalType.Percent)
        {
            /// xx.x% 표시
            text.text = $"{clampedValue * 100:0.#}%";
        }
        else if(decimalType == DecimalType.None)
        {
            text.text = "";
        }
    }


    /// ValueBar의 값을 반환합니다. (0~1)
    public float GetBarValue()
    {
        return barValue.value;
    }


    ///  ValueBar의 값을 변경합니다. (0~1)
    public void SetBarValue(float value)
    {
        float clampedValue = clamp(value, 0, 1);
        barValue.value = clampedValue;
    }


    protected override void DataUpdate()
    {
        eventData.InvokeDataUpdateEvent();
        if(IsSyncRequired()) UpdateManager.AddMainThreadQueue(Sync);
    }


    private bool IsSyncRequired()
    {
        return barValue.isChanged || gameObjectData.IsChanged() || rectTransformData.IsChanged();
    }
    

    private void Sync()
    {
        if(barValue.isChanged) 
        {
            _ApplyValue(barValue.value, decimalType_Serialize);
            barValue.isChanged = false;
        }
        gameObjectData.Sync();
        rectTransformData.Sync();
    }
}