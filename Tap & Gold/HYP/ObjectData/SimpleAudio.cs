using UnityEngine;

[RequireComponent(typeof(GameObjectData), typeof(EventData), typeof(AudioData))]
[RequireComponent(typeof(AudioSource))]

public class SimpleAudio : ObjectData
{
    public GameObjectData gameObjectData { get; private set; }
    public AudioData audioData { get; private set; }
    public EventData eventData { get; private set; }


    protected override void OnInit()
    {
        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        audioData = ComponentManager.GetComponent<AudioData>(hierarchyName);
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
        return gameObjectData.IsChanged() ||
                audioData.IsChanged();
    }

    private void Sync()
    {
        audioData.Sync();
        gameObjectData.Sync();
    }
}