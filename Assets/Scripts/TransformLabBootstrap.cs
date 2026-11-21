using UnityEngine;

// Builds the skeleton automatically when any scene starts: grid, orbit camera, tabbed side panel.
// No scene editing is needed; just keep this folder under Assets/ and press Play.
public static class TransformLabBootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Init()
    {
        var cam = Camera.main;
        if (cam == null)
        {
            var camGo = new GameObject("Main Camera");
            camGo.tag = "MainCamera";
            cam = camGo.AddComponent<Camera>();
        }
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.12f, 0.12f, 0.14f);
        if (cam.GetComponent<OrbitCamera>() == null)
            cam.gameObject.AddComponent<OrbitCamera>();

        var root = new GameObject("Transform Lab");
        GridDrawer.Build(root.transform);
        TabUI.Create(root.transform);
    }
}
