using UnityEngine;

public class FullScreenQuad : MonoBehaviour
{
    public void GenerateQuad()
    {
        var transform = GetComponent<Transform>();
        var meshRenderer = GetComponent<MeshRenderer>();
        var meshFilter = GetComponent<MeshFilter>();
        var resolution = ResolutionUtil.GetResolution();

        var xMax = resolution.x / resolution.y;
        var xMin = -xMax;
        var yMax = 1;
        var yMin = -1;


        // create a quad mesh using xMin, xMax, yMin, yMax
        var vertices = new Vector3[4];
        vertices[0] = new Vector3(xMin, yMin, 0);
        vertices[1] = new Vector3(xMax, yMin, 0);
        vertices[2] = new Vector3(xMax, yMax, 0);
        vertices[3] = new Vector3(xMin, yMax, 0);

        var uvs = new Vector2[4];
        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(1, 0);
        uvs[2] = new Vector2(1, 1);
        uvs[3] = new Vector2(0, 1);

        var triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        var mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;
    }
}
