using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Toggle))]
[RequireComponent(typeof(GameObjectData), typeof(EventData))]
[RequireComponent(typeof(RectTransformData), typeof(SpriteData))]
public class SimpleToggle : ObjectData
{
    private Toggle toggle;
    private ValueFlag<bool> toggleValue;
    public GameObjectData gameObjectData { get; private set; }
    public RectTransformData rectTransformData { get; private set; }
    public SpriteData spriteData { get; private set; }
    public EventData eventData { get; private set; }


    protected override void OnInit()
    {
        toggle = GetComponent<Toggle>();
        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        rectTransformData = ComponentManager.GetComponent<RectTransformData>(hierarchyName);
        eventData = ComponentManager.GetComponent<EventData>(hierarchyName);
        ComponentManager.AddComponent(hierarchyName, this);
    }

    /// Toggle의 값을 반환합니다.
    public bool GetToggleValue()
    {
        return toggleValue.value;
    }

    /// Toggle의 값을 변경합니다.
    public void SetToggleValue(bool value)
    {
        toggleValue.value = value;
    }

    /// Toggle의 값을 변경합니다.
    public void SetToggleValueWithoutInvoke(bool value)
    {
        toggleValue.value = value;
        toggleValue.isChanged = false;
    }


    protected override void DataUpdate()
    {
        eventData.InvokeDataUpdateEvent();
        if(IsSyncRequired()) UpdateManager.AddMainThreadQueue(Sync);
    }

    private bool IsSyncRequired()
    {
        return toggleValue.isChanged || rectTransformData.IsChanged() || gameObjectData.IsChanged();
    }

    private void Sync()
    {
        if(toggleValue.isChanged)
        {
            toggle.isOn = GetToggleValue();
            toggleValue.isChanged = false;
        }

        rectTransformData.Sync();
        gameObjectData.Sync();
    }
}