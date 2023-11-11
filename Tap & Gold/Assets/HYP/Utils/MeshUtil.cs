using UnityEngine;

public class MeshUtil
{
    // Create a circle mesh with the given vertex count and radius.
    // Becareful the direction of the vertices.
    public static Mesh CreateCircleMesh(int vertexCount, float radius)
    {
        var mesh = new Mesh();
        var vertices = new Vector3[vertexCount + 1];
        var triangles = new int[vertexCount * 3];
        var uv = new Vector2[vertexCount + 1];

        vertices[0] = Vector3.zero;
        uv[0] = Vector2.zero;

        for (int i = 0; i < vertexCount; i++)
        {
            var angle = Mathf.Deg2Rad * (360f / vertexCount * i);
            vertices[i + 1] = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            uv[i + 1] = new Vector2(vertices[i + 1].x, vertices[i + 1].y);

            if (i < vertexCount - 1)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 2;
                triangles[i * 3 + 2] = i + 1;
            }
            else
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = 1;
                triangles[i * 3 + 2] = i + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        return mesh;

    }
}
