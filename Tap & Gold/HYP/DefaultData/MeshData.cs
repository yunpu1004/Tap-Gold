using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;
using static Unity.Mathematics.math;


/// MeshData는 MeshFilter Component에 대한 정보를 담고 있습니다.
public class MeshData : DefaultData
{
    private MeshFilter meshFilter;
    public int count => vertices.value.Length;

    private ValueFlag<float2[]> vertices;

    public override void OnInit()
    {
        meshFilter = GetComponent<MeshFilter>();
        var list = ListPool<Vector3>.Get();
        list.Clear();
        meshFilter.sharedMesh.GetVertices(list);
        
        var vertices_temp = new float2[list.Count];
        for(int i = 0; i < count; i++)
        {
            vertices_temp[i] = (float2(list[i].x, list[i].y));
        }
        vertices = new (vertices_temp);

        ListPool<Vector3>.Release(list);
        ComponentManager.AddComponent(hierarchyName, this);
    }



    /// 버텍스의 좌표를 반환합니다.
    public float2[] GetVertices()
    {
        return vertices.value;
    }

    /// 버텍스의 좌표를 변경합니다.
    public void SetVertices(in float2[] value)
    {
        vertices.value = value;
    }


    public bool IsChanged()
    {
        return vertices.isChanged;
    }


    public void Sync()
    {
        if(vertices.isChanged)
        {
            var fixedList = vertices.value;
            var list = ListPool<Vector3>.Get();
            list.Clear();
            for(int i = 0; i < count; i++)
            {
                list.Add(float3(fixedList[i], 0));
            }

            var mesh = meshFilter.sharedMesh;
            mesh.SetVertices(list);
            ListPool<Vector3>.Release(list);

            vertices.isChanged = false;
        }
    }
}