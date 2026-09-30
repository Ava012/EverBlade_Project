using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MiniMapArrowMesh : MonoBehaviour
{
    [Range(0.5f, 10f)]
    public float arrowSize = 2f;

    void OnEnable()
    {
        CreateArrow();
    }

    void OnValidate()
    {
        CreateArrow();
    }

    void CreateArrow()
    {
        Mesh mesh = new Mesh();
        mesh.name = "Minimap Player Arrow";

        Vector3[] vertices =
        {
            new Vector3(0f, 0f, 1.2f) * arrowSize,       // tip
            new Vector3(-0.7f, 0f, -0.2f) * arrowSize,  // left
            new Vector3(-0.25f, 0f, -0.2f) * arrowSize,
            new Vector3(-0.25f, 0f, -1f) * arrowSize,
            new Vector3(0.25f, 0f, -1f) * arrowSize,
            new Vector3(0.25f, 0f, -0.2f) * arrowSize,
            new Vector3(0.7f, 0f, -0.2f) * arrowSize
        };

        // Reversed winding so arrow faces UP toward minimap camera
        int[] triangles =
        {
            0, 2, 1,
            0, 5, 2,
            0, 6, 5,
            2, 4, 3,
            2, 5, 4
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}