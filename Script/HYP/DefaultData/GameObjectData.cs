using UnityEngine;



/// GameObjectData는 GameObject에 대한 정보를 담고 있습니다.
public class GameObjectData : DefaultData
{
    private GameObject go;
    public int layer { get; private set; }
    private ValueFlag<bool> activated;


    public override void OnInit()
    {
        go = gameObject;
        layer = gameObject.layer;
        activated = new (gameObject.activeSelf);
        ComponentManager.AddComponent(hierarchyName, this);
    }


    /// 게임오브젝트 활성화 상태를 반환합니다.
    public bool GetEnabled()
    {
        return activated.value;
    }

    /// 게임오브젝트 활성화 상태를 변경합니다.
    public void SetEnabled(bool value)
    {
        activated.value = value;
    }


    public bool IsChanged()
    {
        return activated.isChanged;
    }


    public void Sync()
    {
        if(activated.isChanged)
        {
            go.SetActive(activated.value);
            activated.isChanged = false;
        }
    }
}
