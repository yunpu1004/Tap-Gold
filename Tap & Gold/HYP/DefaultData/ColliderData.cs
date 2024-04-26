using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static Unity.Mathematics.math;


/// ColliderData는 BoxCollider2D Component에 대한 정보를 담고 있습니다.
public class ColliderData : DefaultData
{
    private BoxCollider2D col;
    public bool isTrigger { get; private set; }

    private ValueFlag<float2> colliderSize;
    private List<CollisionData> collisionDataList;


    public override void OnInit()
    {
        col = GetComponent<BoxCollider2D>();
        isTrigger = col.isTrigger;
        colliderSize = new (col.size);
        collisionDataList = new ();
        ComponentManager.AddComponent(hierarchyName, this);
    }


    /// 이번 프레임에 일어난 충돌의 횟수를 반환합니다.
    public int GetCollisionCount()
    {
        return collisionDataList.Count;
    }
    
    /// 이번 프레임의 충돌 정보를 담고있는 CollisionData 배열을 반환합니다.
    public CollisionData[] GetCollisionDatas()
    {
        return collisionDataList.ToArray();
    }

    /// 이번 프레임의 충돌 정보를 담고있는 CollisionData를 반환합니다.
    public CollisionData GetCollisionData(int index)
    {
        if (index < 0 || index >= collisionDataList.Count) Debug.Log($"접근하려는 CollisionData의 인덱스가 잘못되었습니다. (충돌 횟수 : {collisionDataList.Count}, 인덱스 : {index})");
        return collisionDataList[index];
    }

    /// 이번 프레임의 충돌 정보를 담고있는 CollisionData 객체를 추가합니다.
    public void AddPhysicsData(in CollisionData collisionData)
    {
        collisionDataList.Add(collisionData);
    }

    /// Collider의 크기를 반환합니다.
    public float2 GetColliderSize()
    {
        return colliderSize.value;
    }

    /// Collider의 크기를 변경합니다.
    public void SetColliderSize(in float2 value)
    {
        colliderSize.value = (max(value, 0.01f));
    }

    public bool IsChanged()
    {
        return colliderSize.isChanged || collisionDataList.Count > 0;
    }


    public void Sync()
    {
        if(colliderSize.isChanged) 
        {
            col.size = colliderSize.value;
            colliderSize.isChanged = false;
        }
        collisionDataList.Clear();
    }
}



public struct CollisionData
{
    /// 충돌한 상대 입니다.
    public readonly string otherName;

    /// 충돌한 상대의 질량입니다.
    /// 충돌한 상대가 트리거라면 질량은 0입니다.
    public readonly float otherMass;

    /// 충돌한 상대속도입니다.
    public readonly float2 relativeVelocity;

    /// 충돌한 평균위치입니다.
    public readonly float2 point;

    /// 충돌한 최소위치입니다.
    public readonly float2 pointMin;

    /// 충돌한 최대위치입니다.
    public readonly float2 pointMax;

    /// 질량에 속도를 곱한 값입니다.
    public readonly float2 force;

    /// 충돌한 상대 Entity가 Trigger인지 여부입니다.
    public readonly bool otherIsTrigger;

    /// 충돌이 발생한 타입입니다.
    public readonly CollisionType collisionType;


    public CollisionData(Collision2D collision, CollisionType type)
    {
        var otherColliderName = GameObjectUtil.GetHierarchyName(collision.gameObject);
        var colliderName = GameObjectUtil.GetHierarchyName(collision.otherCollider.gameObject);

        ColliderData colliderData = ComponentManager.GetComponent<ColliderData>(colliderName);
        TransformData transformData = ComponentManager.GetComponent<TransformData>(colliderName);
        RigidbodyData rigidbodyData = ComponentManager.GetComponent<RigidbodyData>(colliderName);
        ColliderData otherColliderData = ComponentManager.GetComponent<ColliderData>(otherColliderName);
        TransformData otherTransformData = ComponentManager.GetComponent<TransformData>(otherColliderName);
        RigidbodyData otherRigidbodyData = ComponentManager.GetComponent<RigidbodyData>(otherColliderName);

        var colliderMin = transformData.GetLocalPosition().xy - colliderData.GetColliderSize() / 2;
        var colliderMax = transformData.GetLocalPosition().xy + colliderData.GetColliderSize() / 2;
        var otherColliderMin = otherTransformData.GetLocalPosition().xy - otherColliderData.GetColliderSize() / 2;
        var otherColliderMax = otherTransformData.GetLocalPosition().xy + otherColliderData.GetColliderSize() / 2;

        pointMin = math.max(colliderMin, otherColliderMin);
        pointMax = math.min(colliderMax, otherColliderMax);
        point = (pointMin + pointMax) / 2;
        collisionType = type;

        otherName = otherColliderName;
        otherIsTrigger = otherColliderData.isTrigger;
        otherMass = (otherIsTrigger)?0 :otherRigidbodyData.GetMass();
        relativeVelocity = half2(otherRigidbodyData.GetVelocity() - rigidbodyData.GetVelocity());
        force = relativeVelocity * otherMass;
    }

    public CollisionData(Collider2D otherCol, Transform self, CollisionType type)
    {
        var otherColliderName = GameObjectUtil.GetHierarchyName(otherCol.gameObject);
        var colliderName = GameObjectUtil.GetHierarchyName(self.gameObject);
        
        ColliderData colliderData = ComponentManager.GetComponent<ColliderData>(colliderName);
        TransformData transformData = ComponentManager.GetComponent<TransformData>(colliderName);
        ColliderData otherColliderData = ComponentManager.GetComponent<ColliderData>(otherColliderName);
        TransformData otherTransformData = ComponentManager.GetComponent<TransformData>(otherColliderName);

        var colliderMin = transformData.GetLocalPosition().xy - colliderData.GetColliderSize() / 2;
        var colliderMax = transformData.GetLocalPosition().xy + colliderData.GetColliderSize() / 2;
        var otherColliderMin = otherTransformData.GetLocalPosition().xy - otherColliderData.GetColliderSize() / 2;
        var otherColliderMax = otherTransformData.GetLocalPosition().xy + otherColliderData.GetColliderSize() / 2;

        otherName = otherColliderName;
        pointMin = math.max(colliderMin, otherColliderMin);
        pointMax = math.min(colliderMax, otherColliderMax);
        point = (pointMin + pointMax) / 2;
        collisionType = type;
        otherIsTrigger = otherColliderData.isTrigger;

        if(otherIsTrigger){ // 상대가 트리거일때
            RigidbodyData rigidbodyData = ComponentManager.GetComponent<RigidbodyData>(colliderName);
            otherMass = 0;
            relativeVelocity = -rigidbodyData.GetVelocity();
            force = half2(0);
        }
        else{ // 내가 트리거일때
            RigidbodyData otherRigidbodyData = ComponentManager.GetComponent<RigidbodyData>(otherColliderName);
            otherMass = otherRigidbodyData.GetMass();
            relativeVelocity = otherRigidbodyData.GetVelocity();
            force = half2(0);
        }
    } 
}

public enum CollisionType
{
    Enter,
    Stay,
    Exit
}