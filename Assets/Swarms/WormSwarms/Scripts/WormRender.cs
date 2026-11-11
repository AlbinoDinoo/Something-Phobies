using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class WormRender : MonoBehaviour
{
    [Header("Referances")]
    public WormSimulation Simulation;


    [Header("Mesh")]
    public float Radius = 0.025f;
    public int Segments = 8;
    public int Smoothness = 4;

    public float TailThickness = 0.4f;

    private Mesh WormMesh;
    private MeshFilter MeshFilter;

    private Vector3[] Vertices;
    private int[] Triangles;
    
    private int LastPointCount;

    void Awake()
    {
        MeshFilter = GetComponent<MeshFilter>();

        WormMesh = new Mesh();
        WormMesh.name = "Procedural worm animation";
        WormMesh.MarkDynamic();

        MeshFilter.sharedMesh = WormMesh;
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.sharedMaterial = renderer.sharedMaterial;
    }

    private void LateUpdate()
    {
        if (Simulation == null)
        {
            return;
        }

        if (Simulation.PointCount < 2)
        {
            return;
        }

        if (LastPointCount != Simulation.PointCount)
        {
            CreateMesh();
        }

        UpdateMesh();
    }

    private void CreateMesh()
    {
        LastPointCount = Simulation.PointCount;

        int PointCount = (LastPointCount - 1) * Mathf.Max(1, Smoothness) + 1;
        int Sides = Mathf.Max(3, Segments);

        Vertices = new Vector3[PointCount * Sides];
        Triangles = new int[(PointCount - 1) * Sides * 6];

        int triangleIndex = 0;

        for (int i = 0; i < PointCount - 1; i++)
        {
            for (int j = 0; j < Sides; j++)
            {
                int next = (j + 1) % Sides;

                int a = i * Sides + j;
                int b = i * Sides + next;
                int c = (i + 1) * Sides + j;
                int d = (i + 1) * Sides + next;

                Triangles[triangleIndex++] = a;
                Triangles[triangleIndex++] = c;
                Triangles[triangleIndex++] = b;

                Triangles[triangleIndex++] = b;
                Triangles[triangleIndex++] = c;
                Triangles[triangleIndex++] = d;
            }
        }

        WormMesh.Clear();
        WormMesh.vertices = Vertices;
        WormMesh.triangles = Triangles;
    }


    //Help Of AI
   private void UpdateMesh()
{
    int OriginalPointCount = Simulation.PointCount;
    int Sides = Mathf.Max(3, Segments);
    int Steps = Mathf.Max(1, Smoothness);

    int SmoothPointCount = (OriginalPointCount - 1) * Steps + 1;

    for (int i = 0; i < OriginalPointCount - 1; i++)
    {
        Vector3 P0 = Simulation.Pos[Mathf.Max(i - 1, 0)];
        Vector3 P1 = Simulation.Pos[i];
        Vector3 P2 = Simulation.Pos[i + 1];
        Vector3 P3 = Simulation.Pos[Mathf.Min(i + 2, OriginalPointCount - 1)];

        for (int step = 0; step < Steps; step++)
        {
            float t = step / (float)Steps;

            Vector3 Position = 0.5f * (
                (2f * P1) +
                (-P0 + P2) * t +
                (2f * P0 - 5f * P1 + 4f * P2 - P3) * t * t +
                (-P0 + 3f * P1 - 3f * P2 + P3) * t * t * t
            );

            int SmoothIndex = i * Steps + step;

            Vector3 Tangent;

            float NextT = Mathf.Min(t + 0.01f, 1f);

            Vector3 NextPosition = 0.5f * (
                (2f * P1) +
                (-P0 + P2) * NextT +
                (2f * P0 - 5f * P1 + 4f * P2 - P3) * NextT * NextT +
                (-P0 + 3f * P1 - 3f * P2 + P3) * NextT * NextT * NextT
            );

            Tangent = (NextPosition - Position).normalized;

            if (Tangent.sqrMagnitude < 0.001f)
                Tangent = (P2 - P1).normalized;

            Vector3 ReferenceUp = Vector3.up;

            if (Mathf.Abs(Vector3.Dot(Tangent, ReferenceUp)) > 0.95f)
                ReferenceUp = Vector3.forward;

            Vector3 Normal = Vector3.Cross(Tangent, ReferenceUp).normalized;
            Vector3 Binormal = Vector3.Cross(Tangent, Normal).normalized;

            for (int j = 0; j < Sides; j++)
            {
                float Angle = j * Mathf.PI * 2f / Sides;

                float BodyProgress = SmoothIndex / (float)(SmoothPointCount - 1);

                float Taper = BodyProgress * BodyProgress;
                float CurrentRadius = Radius * Mathf.Lerp(1f, TailThickness, Taper);    

                Vector3 Offset =
                    Normal * Mathf.Cos(Angle) * CurrentRadius +
                    Binormal * Mathf.Sin(Angle) * CurrentRadius;

                Vertices[SmoothIndex * Sides + j] =
                    transform.InverseTransformPoint(Position + Offset);
            }
        }
    }

    // Add the final simulation point.
    int LastIndex = SmoothPointCount - 1;
    Vector3 LastPosition = Simulation.Pos[OriginalPointCount - 1];

    float LastRadius = Radius * TailThickness;
    Vector3 LastTangent =
        (LastPosition - Simulation.Pos[OriginalPointCount - 2]).normalized;

    Vector3 LastUp = Vector3.up;

    if (Mathf.Abs(Vector3.Dot(LastTangent, LastUp)) > 0.95f)
        LastUp = Vector3.forward;

    Vector3 LastNormal = Vector3.Cross(LastTangent, LastUp).normalized;
    Vector3 LastBinormal = Vector3.Cross(LastTangent, LastNormal).normalized;

    for (int j = 0; j < Sides; j++)
    {
        float Angle = j * Mathf.PI * 2f / Sides;

        Vector3 Offset =
            LastNormal * Mathf.Cos(Angle) * LastRadius +
            LastBinormal * Mathf.Sin(Angle) * LastRadius;
        Vertices[LastIndex * Sides + j] =
            transform.InverseTransformPoint(LastPosition + Offset);
    }

    WormMesh.vertices = Vertices;
    WormMesh.RecalculateNormals();
    WormMesh.RecalculateBounds();
}
}
