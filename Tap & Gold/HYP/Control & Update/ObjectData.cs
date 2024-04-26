using System;
using System.Collections.Generic;
using UnityEngine;



/// ObjectData는 HYP/ObjectData 폴더 안에 있는 모든 컴포넌트 클래스의 추상 클래스입니다.
/// 이 추상 클래스를 상속받아서, 특정 오브젝트의 여러 DefaultData 로 접근하는 컴포넌트를 만듭니다.
/// hierarchyDepth 가 같은 오브젝트는 UpdateManager 에서 병렬적으로 실행됩니다.
public abstract class ObjectData : MonoBehaviour
{
    public string hierarchyName { get; private set; }
    public int hierarchyDepth { get; private set; } = -1;
    

    /// 게임 실행시 InitializeManager 에서 호출되는 메소드로 아래와 같은 작업을 실행합니다.
    /// 1. 이 컴포넌트를 보유한 모든 객체를 찾습니다.
    /// 2. 모든 객체의 계층 구조를 숫자로 표현합니다.
    /// 3. 같은 계층의 오브젝트끼리 병렬처리가 가능하도록, UpdateManager.dataUpdateDict 에 DataUpdate 를 추가합니다.
    public static void Init()
	{
        if(!Application.isPlaying) return;
        if(!InitializerManager.IsInit()) return;
        var dataComponents = GameObject.FindObjectsOfType<ObjectData>(true);
        foreach(var dataComponent in dataComponents)
        {
            var hierarchyName = GameObjectUtil.GetHierarchyName(dataComponent.gameObject);
            dataComponent.hierarchyName = hierarchyName;
            dataComponent.hierarchyDepth = dataComponent.SetHierarchyDepth(dataComponent.transform);
            dataComponent.OnInit();
            if(!UpdateManager.dataUpdateDict.ContainsKey(dataComponent.hierarchyDepth))
            {
                UpdateManager.dataUpdateDict.Add(dataComponent.hierarchyDepth, new List<Action>());
            }
            UpdateManager.dataUpdateDict[dataComponent.hierarchyDepth].Add(dataComponent.DataUpdate);
        }
    }

    protected abstract void OnInit();
    protected abstract void DataUpdate();


    /// 이 메소드는 hierarchyDepth 를 계산하기 위해 호출됩니다.
    /// 0부터 시작해서, A/B/C 의 계층을 가지는 오브젝트 C 의 Depth 는 2가 됩니다.
    private int SetHierarchyDepth(Transform transform)
    {
        while(transform.parent != null)
        {
            transform = transform.parent;
            var parentDataComponent = transform.GetComponent<ObjectData>();
            if(parentDataComponent != null)
            {
                if(parentDataComponent.hierarchyDepth == -1)
                {
                    parentDataComponent.hierarchyDepth = SetHierarchyDepth(transform);
                    return parentDataComponent.hierarchyDepth + 1;
                }
                else
                {
                    return parentDataComponent.hierarchyDepth + 1;
                }
            }
        }
        return 0;
    }



    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        var otherColliderName = GameObjectUtil.GetHierarchyName(collision2D.gameObject);
        if(ComponentManager.GetComponent<ColliderData>(hierarchyName) && ComponentManager.GetComponent<ColliderData>(otherColliderName))
        {
            var colliderData = ComponentManager.GetComponent<ColliderData>(hierarchyName);
            var physicsData = new CollisionData(collision2D, CollisionType.Enter);
            colliderData.AddPhysicsData(physicsData);
        }
    }

    private void OnCollisionStay2D(Collision2D collision2D)
    {
        var otherColliderName = GameObjectUtil.GetHierarchyName(collision2D.gameObject);
        if(ComponentManager.GetComponent<ColliderData>(hierarchyName) && ComponentManager.GetComponent<ColliderData>(otherColliderName))
        {
            var colliderData = ComponentManager.GetComponent<ColliderData>(hierarchyName);
            var physicsData = new CollisionData(collision2D, CollisionType.Stay);
            colliderData.AddPhysicsData(physicsData);
        }
    }

    private void OnCollisionExit2D(Collision2D collision2D)
    {
        var otherColliderName = GameObjectUtil.GetHierarchyName(collision2D.gameObject);
        if(ComponentManager.GetComponent<ColliderData>(hierarchyName) && ComponentManager.GetComponent<ColliderData>(otherColliderName))
        {
            var colliderData = ComponentManager.GetComponent<ColliderData>(hierarchyName);
            var physicsData = new CollisionData(collision2D, CollisionType.Exit);
            colliderData.AddPhysicsData(physicsData);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherColliderName = GameObjectUtil.GetHierarchyName(other.gameObject);
        if(ComponentManager.GetComponent<ColliderData>(hierarchyName) && ComponentManager.GetComponent<ColliderData>(otherColliderName))
        {
            var colliderData = ComponentManager.GetComponent<ColliderData>(hierarchyName);
            var physicsData = new CollisionData(other, transform, CollisionType.Enter);
            colliderData.AddPhysicsData(physicsData);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var otherColliderName = GameObjectUtil.GetHierarchyName(other.gameObject);
        if(ComponentManager.GetComponent<ColliderData>(hierarchyName) && ComponentManager.GetComponent<ColliderData>(otherColliderName))
        {
            var colliderData = ComponentManager.GetComponent<ColliderData>(hierarchyName);
            var physicsData = new CollisionData(other, transform, CollisionType.Stay);
            colliderData.AddPhysicsData(physicsData);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var otherColliderName = GameObjectUtil.GetHierarchyName(other.gameObject);
        if(ComponentManager.GetComponent<ColliderData>(hierarchyName) && ComponentManager.GetComponent<ColliderData>(otherColliderName))
        {
            var colliderData = ComponentManager.GetComponent<ColliderData>(hierarchyName);
            var physicsData = new CollisionData(other, transform, CollisionType.Exit);
            colliderData.AddPhysicsData(physicsData);
        }
    }
}