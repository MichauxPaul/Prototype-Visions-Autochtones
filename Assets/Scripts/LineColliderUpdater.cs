using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class LineColliderUpdater : MonoBehaviour
{
    private LineRenderer line;
    private EdgeCollider2D edge;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        edge = GetComponent<EdgeCollider2D>();

        edge.edgeRadius = 0.2f; // 👈 plus facile à cliquer
    }

    void Update()
    {
        int count = line.positionCount;

        Vector2[] points = new Vector2[count];

        for (int i = 0; i < count; i++)
        {
            // 🔥 conversion WORLD → LOCAL
            Vector3 worldPos = line.GetPosition(i);
            Vector3 localPos = transform.InverseTransformPoint(worldPos);

            points[i] = new Vector2(localPos.x, localPos.y);
        }

        edge.points = points;
    }
}