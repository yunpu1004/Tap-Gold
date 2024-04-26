using UnityEngine;



/// ObjectData는 HYP/DefaultData 폴더 안에 있는 모든 컴포넌트 클래스의 추상 클래스입니다.
/// 이 추상 클래스를 상속받은 컴포넌트는 Transform 과 같은 유니티의 기본 컴포넌트의 정보를 옮겨 담습니다.
/// 또한 병렬적으로 Transform 과 같은 기본 컴포넌트의 정보를 업데이트할 수 있습니다.
public abstract class DefaultData : MonoBehaviour
{
    public string hierarchyName { get; private set; }
    
    public static void Init()
	{
        if(!Application.isPlaying) return;
        if(!InitializerManager.IsInit()) return;
        var defaultDataComponents = GameObject.FindObjectsOfType<DefaultData>(true);
        foreach(var component in defaultDataComponents)
        {
            component.hierarchyName = GameObjectUtil.GetHierarchyName(component.gameObject);
            component.OnInit();
        }
    }

    public abstract void OnInit();
}
