using System;
using UnityEngine;

[RequireComponent(typeof(GameObjectData), typeof(EventData))]
public class FilterGroup : ObjectData
{
    public GameObjectData gameObjectData { get; private set; }
    public EventData eventData { get; private set; }


    protected override void OnInit()
    {
        if(transform.position != Vector3.zero) throw new Exception("FilterEntityGroup의 위치는 (0, 0, 0)이어야 합니다.");
        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        eventData = ComponentManager.GetComponent<EventData>(hierarchyName);
        ComponentManager.AddComponent(hierarchyName, this);
    }

    protected override void DataUpdate()
    {
        eventData.InvokeDataUpdateEvent();
        if(IsSyncRequired()) UpdateManager.AddMainThreadQueue(Sync);
    }


    private bool IsSyncRequired()
    {
        return gameObjectData.IsChanged();
    }


    private void Sync()
    {
        gameObjectData.Sync();
    }
}