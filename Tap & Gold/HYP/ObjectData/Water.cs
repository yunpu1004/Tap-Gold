using System;
using Unity.Mathematics;
using UnityEngine;
using static Unity.Mathematics.math;


#if UNITY_EDITOR

using UnityEditor;
 
#endif

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(BoxCollider2D))]
[RequireComponent(typeof(GameObjectData), typeof(EventData), typeof(TransformData))]
[RequireComponent(typeof(ShaderData), typeof(MeshData), typeof(ColliderData))]
[ExecuteInEditMode]
public class Water : ObjectData
{
    [Range(50, 200)] public int quality_Serialize;
    [Range(0, 100)] public float width_Serialize;
    [Range(0, 100)] public float height_Serialize;
    [Range(0.01f, 10)] public float density_Serialize;
    [Range(0.3f, 1f)] public float waveDecay_Serialize;

    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public Transform tr;
    public BoxCollider2D col;

    public int quality {get; private set;}
    private ValueFlag<float> density;
    private ValueFlag<float> waveDecay;
    private float[] velocity;

    public GameObjectData gameObjectData { get; private set; }
    public TransformData transformData { get; private set; }
    public ColliderData colliderData { get; private set; }
    public ShaderData shaderData { get; private set; }
    public MeshData meshData { get; private set; }
    public EventData eventData { get; private set; }

    protected override void OnInit()
    {
        transform.localScale = Vector3.one;
        col.isTrigger = true;
        col.size = new Vector2(width_Serialize, height_Serialize);

        quality = quality_Serialize;
        density = new (density_Serialize);
        waveDecay = new (waveDecay_Serialize);
        velocity = new float[quality + 1];

        for(int i = 0; i <= quality; i++)
        {
            velocity[i] = 0;
        }

        gameObjectData = ComponentManager.GetComponent<GameObjectData>(hierarchyName);
        transformData = ComponentManager.GetComponent<TransformData>(hierarchyName);
        colliderData = ComponentManager.GetComponent<ColliderData>(hierarchyName);
        shaderData = ComponentManager.GetComponent<ShaderData>(hierarchyName);
        meshData = ComponentManager.GetComponent<MeshData>(hierarchyName);
        eventData = ComponentManager.GetComponent<EventData>(hierarchyName);
        ComponentManager.AddComponent(hierarchyName, this);
        
    }


    [LateInitialize]
    public static void WaterLateInitialize()
    {
        var waterComponents = GameObject.FindObjectsOfType<Water>(true);
        foreach (var waterComponent in waterComponents)
        {
            var physicsGroup = GameObjectUtil.GetComponentInParent<PhysicsGroup>(waterComponent.gameObject, true);
            var physicsGroupEventData = physicsGroup.GetComponent<EventData>();
            physicsGroupEventData.AddDataUpdateEvent(waterComponent.BuoyancyEvent);
        }
    }


    public void BuoyancyEvent()
    { 
        int len = colliderData.GetCollisionCount(); 
        for (int i = 0; i < len; i++)
        { 
            var physicsData = colliderData.GetCollisionData(i);
            if(physicsData.otherIsTrigger || physicsData.collisionType == CollisionType.Enter) continue;
            var otherName = physicsData.otherName;
            TransformData other_TransformData = ComponentManager.GetComponent<TransformData>(otherName);
            ColliderData other_ColliderData = ComponentManager.GetComponent<ColliderData>(otherName);
            RigidbodyData other_RigidbodyData = ComponentManager.GetComponent<RigidbodyData>(otherName);

            var posMax = other_TransformData.GetLocalPosition().y + other_ColliderData.GetColliderSize().y / 2;
            var posMin = other_TransformData.GetLocalPosition().y - other_ColliderData.GetColliderSize().y / 2;
            var sinkedPortion = clamp(unlerp(posMin, posMax, physicsData.pointMax.y), 0, 1);
            var vel = other_RigidbodyData.GetVelocity() * pow(0.9f, GetDensity()) + float2(0, GetDensity() / 4) * sinkedPortion;
            other_RigidbodyData.SetVelocity(vel);
        }
    }


    /// Water의 Density를 반환합니다.
    public float GetDensity()
    {
        return density.value;
    }

    /// Water의 Density를 변경합니다.
    public void SetDensity(float value)
    {
        density.value = value;
    }

    /// Water의 WaveDecay를 반환합니다.
    public float GetWaveDecay()
    {
        return waveDecay.value;
    }

    /// Water의 WaveDecay를 변경합니다.
    public void SetWaveDecay(float value)
    {
        waveDecay.value = value;
    }


    protected override void DataUpdate()
    {
        CheckCollisionEnter();
        if(IsWaveSimulationNeeded())
        {
            SimulateTension();
            SimulateVelocity();
        }

        eventData.InvokeDataUpdateEvent();
        if(IsSyncRequired()) UpdateManager.AddMainThreadQueue(Sync);
    }


    private void CheckCollisionEnter()
    {
        var physicsDatas = colliderData.GetCollisionDatas();
        var colliderSize = colliderData.GetColliderSize();
        var localPosition = transformData.GetLocalPosition();
        int len = physicsDatas.Length;
        float interval = colliderSize.x / quality;
        float xMin = localPosition.x - colliderSize.x / 2;
        for (int i = 0; i < len; i++)
        {
            var physicsData = physicsDatas[i];
            int xPosIndex = (int)((physicsData.point.x - xMin) / interval);
            float forceY = physicsData.relativeVelocity.y * physicsData.otherMass;
            if(abs(forceY) < 1f) continue;
            if(physicsData.collisionType == CollisionType.Enter)
            {
                for(int j = xPosIndex - 10; j <= xPosIndex + 10; j++)
                {
                    if(j >= 0 && j < quality)
                    {
                        velocity[j] =  velocity[j] + forceY * 0.02f;
                    }
                }
            }
        }
    }
    
    private bool IsWaveSimulationNeeded()
    {
        for (int i = 0; i < quality; i++)
        {
            if(velocity[i] != 0) return true;
        }
        return false;
    }
    
    private void SimulateTension()
    {
        int len = quality + 1;
        var vertices = meshData.GetVertices();

        for(int i = 0; i < len; i++)
        {
            float yPos = vertices[i*2].y;
            float vel = velocity[i];

            for (int j = 1; j <= 4; j++)
            {
                int idx = i;
                idx += (j % 2 == 1) ? -j : j;
                if(idx >= 0 && idx < len)
                {
                    float yPos2 = vertices[idx*2].y;
                    float vel2 = velocity[idx];
                    float yPosDiff = yPos - yPos2;
                    float tensionVel = yPosDiff / 8;
                    float tensionPos = yPosDiff / 16;

                    vel -= tensionVel;
                    velocity[i] = vel;
                    
                    vel2 += tensionVel;
                    velocity[idx] = vel2;

                    yPos -= tensionPos;
                    vertices[i*2] = float2(vertices[i*2].x, yPos);

                    yPos2 += tensionPos;
                    vertices[idx*2] = float2(vertices[idx*2].x, yPos2);
                }
            }
        }

        meshData.SetVertices(vertices);
    }

    private void SimulateVelocity()
    {
        var colliderSize = colliderData.GetColliderSize();
        float maxY = colliderSize.y / 2;
        int len = quality + 1;
        float decayFactor = 1 - waveDecay.value / 10;
        var vertices = meshData.GetVertices();
        
        for(int i = 0; i < len; i++)
        {
            float yPos = vertices[i * 2].y;
            float vel = velocity[i];

            if((abs(vel) + abs(yPos - maxY)) < 0.01f)
            {
                yPos = maxY;
                vel = 0;
            }else
            {
                float springForce = (yPos - maxY) / 100;
                vel -= springForce;
                vel *= decayFactor;
                yPos += vel;
            }

            vertices[i*2] = float2(vertices[i*2].x, yPos);
            velocity[i] = vel;
        }

        meshData.SetVertices(vertices);
    }


    private bool IsSyncRequired()
    {
        return transformData.IsChanged() || meshData.IsChanged() || colliderData.IsChanged() || shaderData.IsChanged() || gameObjectData.IsChanged();
    }

    private void Sync()
    {
        density.isChanged = false;
        waveDecay.isChanged = false;

        transformData.Sync();
        meshData.Sync();
        colliderData.Sync();
        shaderData.Sync();
        gameObjectData.Sync();
    }


 
#if UNITY_EDITOR

    private bool isValid = true;
    private void OnValidate() 
    {
        isValid = false;
    }

    void Update()
    {
        if(!Application.isPlaying && !isValid)
        {
            isValid = true;

            if(width_Serialize <= 0 || height_Serialize <= 0 || quality_Serialize < 1) return;
            if(gameObject == null) return;
            if(PrefabUtility.IsPartOfPrefabAsset(gameObject)) return;
            if(meshRenderer.sharedMaterial == null) return;

            meshFilter.mesh = new Mesh();
            meshFilter.sharedMesh.vertices = GetVertices();
            meshFilter.sharedMesh.triangles = GetTris();
            meshFilter.sharedMesh.uv = GetUV(); 

            meshFilter.sharedMesh.RecalculateUVDistributionMetrics();
            meshFilter.sharedMesh.RecalculateBounds();

            SetMaterialProperty();

            col.isTrigger = true;
            col.size = new Vector2(width_Serialize, height_Serialize);
        }

    }


    public void SetMaterialProperty()
    {
        if(width_Serialize <= 0 || height_Serialize <= 0 || quality_Serialize < 1) return;
        if(gameObject == null) return;
        if(PrefabUtility.IsPartOfPrefabAsset(gameObject)) return;
        if(meshFilter.sharedMesh == null) return;
        if(meshRenderer.sharedMaterial == null) return;

        Camera mainCamera = Camera.main;
        tr.localScale = Vector3.one;
        var position = tr.position;
        var extends = new Vector3(width_Serialize / 2, height_Serialize / 2, 0);
        Vector2 min = position - extends;
        Vector2 max = position + extends;

        min = mainCamera.WorldToViewportPoint(min);
        max = mainCamera.WorldToViewportPoint(max);

        var normalizedSize = max - min;
        meshRenderer.sharedMaterial.SetVector("NormalizedSize", normalizedSize);
        meshRenderer.sharedMaterial.SetFloat("NormalizedSurface", max.y);
    }


    private int[] GetTris()
    {
        int[] result = new int[quality_Serialize * 6];
        for(int i = 0; i < quality_Serialize; i++)
        {
            result[i * 6] = i * 2;
            result[i * 6 + 1] = i * 2 + 2;
            result[i * 6 + 2] = i * 2 + 1;
            result[i * 6 + 3] = i * 2 + 2;
            result[i * 6 + 4] = i * 2 + 3;
            result[i * 6 + 5] = i * 2 + 1;
        }
        return result;
    }

    private Vector2[] GetUV()
    {
        var vertices = meshFilter.sharedMesh.vertices;
        var uvs = new Vector2[vertices.Length];
        var size = new Vector2(width_Serialize, height_Serialize);
        var half = new Vector2(0.5f, 0.5f);

        for (int i = 0; i < vertices.Length; i++)
        {
            uvs[i] = vertices[i] / size + half;
        }

        return uvs;
    }

    private Vector3[] GetVertices()
    {
        float2 start_top = float2(-width_Serialize / 2, height_Serialize / 2);
        float2 start_bottom = float2(-width_Serialize / 2, -height_Serialize / 2);
        float2 stop_top = float2(width_Serialize / 2, height_Serialize / 2);
        float2 stop_bottom = float2(width_Serialize / 2, -height_Serialize / 2);
        var vertex_top = ArrayUtil.RangeFromStartToEnd(start_top, stop_top, quality_Serialize + 1);
        var vertex_bottom = ArrayUtil.RangeFromStartToEnd(start_bottom, stop_bottom, quality_Serialize + 1);

        Vector3[] result = new Vector3[(quality_Serialize + 1) * 2];
        for(int i = 0; i < quality_Serialize + 1; i++)
        {
            result[i * 2] = float3(vertex_top[i], 0);
            result[i * 2 + 1] = float3(vertex_bottom[i], 0);
        }
        return result;
    }

#endif
}