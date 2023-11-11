using static Unity.Mathematics.math;
using UnityEngine;

[RequireComponent(typeof(GameObjectData), typeof(EventData))]
[RequireComponent(typeof(TransformData))]
public class SimpleTouchEffect : ObjectData
{
    private ValueFlag<float> progress;
    public GameObjectData gameObjectData { get; private set; }
    public TransformData transformData { get; private set; }
    public EventData eventData { get; private set; }


    protected override void OnInit()
    {
        progress = new ValueFlag<float>(0);
        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        transformData = ComponentManager.GetComponent<TransformData>(hierarchyName);
        eventData = ComponentManager.GetComponent<EventData>(hierarchyName);
        ComponentManager.AddComponent(hierarchyName, this);
    }


    protected override void DataUpdate()
    {
        var inputType = TouchData.touchType;

        if(inputType != TouchData.TouchType.None)
        {
            transformData.SetLocalPosition(TouchData.pointerWorldPos);

            if(inputType == TouchData.TouchType.Down)
            {
                progress.value = 1/16f;
                gameObjectData.SetEnabled(true);
            }
            else if(inputType == TouchData.TouchType.Up)
            {
                progress.value = 0;
                gameObjectData.SetEnabled(false);
            }
            else if(progress.value > 0 && progress.value < 1)
            {
                progress.value = min(progress.value + 1/8f, 1);
            }

            if(progress.isChanged)
            {
                transformData.SetLocalScale(lerp(float3(0.002f), float3(0.005f), progress.value));
                progress.isChanged = false;
            }
        }

        eventData.InvokeDataUpdateEvent();
        if(IsSyncRequired()) UpdateManager.AddMainThreadQueue(Sync);
    }


    private bool IsSyncRequired()
    {
        return gameObjectData.IsChanged() || transformData.IsChanged();
    }


    private void Sync()
    {
        gameObjectData.Sync();
        transformData.Sync();   
    }
}