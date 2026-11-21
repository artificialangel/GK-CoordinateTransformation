using UnityEngine;

// Draws a 3D grid on the XZ plane (Unity's "ground") plus colored X, Y, Z axes.
public static class GridDrawer
{
    public static void Build(Transform parent, int halfSize = 10)
    {
        var mat = new Material(Shader.Find("Sprites/Default"));
        var gridColor = new Color(1f, 1f, 1f, 0.18f);

        for (int i = -halfSize; i <= halfSize; i++)
        {
            if (i == 0) continue; // the axes are drawn over the center lines
            Line(parent, mat, new Vector3(-halfSize, 0, i), new Vector3(halfSize, 0, i), gridColor, 0.02f, "Grid Z=" + i);
            Line(parent, mat, new Vector3(i, 0, -halfSize), new Vector3(i, 0, halfSize), gridColor, 0.02f, "Grid X=" + i);
        }

        Line(parent, mat, new Vector3(-halfSize, 0, 0), new Vector3(halfSize, 0, 0), new Color(0.9f, 0.25f, 0.25f), 0.06f, "Axis X");
        Line(parent, mat, new Vector3(0, 0, 0), new Vector3(0, halfSize, 0), new Color(0.3f, 0.85f, 0.35f), 0.06f, "Axis Y");
        Line(parent, mat, new Vector3(0, 0, -halfSize), new Vector3(0, 0, halfSize), new Color(0.3f, 0.5f, 0.95f), 0.06f, "Axis Z");
    }

    static void Line(Transform parent, Material mat, Vector3 a, Vector3 b, Color color, float width, string name)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);
        var lr = go.AddComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.material = mat;
        lr.positionCount = 2;
        lr.SetPosition(0, a);
        lr.SetPosition(1, b);
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = width;
        lr.endWidth = width;
    }
}
