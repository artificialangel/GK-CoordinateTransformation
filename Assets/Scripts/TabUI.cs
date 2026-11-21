using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Side panel with five tabs. Each tab has an empty content area (only a title for now).
public class TabUI : MonoBehaviour
{
    static readonly string[] TabNames =
    {
        "Coordinate System", "Position", "Scale", "Hierarchy", "Rotation"
    };

    static readonly Color PanelColor = new Color(0.1f, 0.1f, 0.12f, 0.92f);
    static readonly Color TabIdle = new Color(0.2f, 0.2f, 0.24f, 1f);
    static readonly Color TabActive = new Color(0.25f, 0.45f, 0.85f, 1f);

    const float PanelWidth = 420f;
    const float TabBarHeight = 56f;

    Font font;
    GameObject[] contents;
    Image[] tabImages;

    public static TabUI Create(Transform parent)
    {
        var go = new GameObject("Tab UI");
        go.transform.SetParent(parent, false);
        var ui = go.AddComponent<TabUI>();
        ui.Build();
        return ui;
    }

    void Build()
    {
        font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        if (EventSystem.current == null)
        {
            var es = new GameObject("EventSystem");
            es.transform.SetParent(transform, false);
            es.AddComponent<EventSystem>();
            es.AddComponent<StandaloneInputModule>();
        }

        var canvasGo = new GameObject("Canvas");
        canvasGo.transform.SetParent(transform, false);
        var canvas = canvasGo.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasGo.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 0.5f;
        canvasGo.AddComponent<GraphicRaycaster>();

        // Side panel, docked to the right edge, full height.
        var panel = NewRect("Side Panel", canvasGo.transform);
        panel.anchorMin = new Vector2(1, 0);
        panel.anchorMax = new Vector2(1, 1);
        panel.pivot = new Vector2(1, 0.5f);
        panel.sizeDelta = new Vector2(PanelWidth, 0);
        panel.anchoredPosition = Vector2.zero;
        panel.gameObject.AddComponent<Image>().color = PanelColor;

        // Tab bar.
        var bar = NewRect("Tab Bar", panel);
        bar.anchorMin = new Vector2(0, 1);
        bar.anchorMax = new Vector2(1, 1);
        bar.pivot = new Vector2(0.5f, 1);
        bar.sizeDelta = new Vector2(0, TabBarHeight);
        bar.anchoredPosition = Vector2.zero;

        contents = new GameObject[TabNames.Length];
        tabImages = new Image[TabNames.Length];
        float w = 1f / TabNames.Length;

        for (int i = 0; i < TabNames.Length; i++)
        {
            int index = i; // captured per button

            var btn = NewRect("Tab " + TabNames[i], bar);
            btn.anchorMin = new Vector2(w * i, 0);
            btn.anchorMax = new Vector2(w * (i + 1), 1);
            btn.offsetMin = new Vector2(1, 0);
            btn.offsetMax = new Vector2(-1, 0);
            var img = btn.gameObject.AddComponent<Image>();
            tabImages[i] = img;
            var button = btn.gameObject.AddComponent<Button>();
            button.targetGraphic = img;
            button.onClick.AddListener(() => Select(index));
            NewText("Label", btn, TabNames[i], 14, TextAnchor.MiddleCenter, true);

            // Content area for this tab (empty for now, only the title).
            var content = NewRect("Content " + TabNames[i], panel);
            content.anchorMin = Vector2.zero;
            content.anchorMax = Vector2.one;
            content.offsetMin = new Vector2(16, 16);
            content.offsetMax = new Vector2(-16, -(TabBarHeight + 16));
            var title = NewText("Title", content, TabNames[i], 24, TextAnchor.UpperLeft, false);
            title.rectTransform.anchorMin = Vector2.zero;
            title.rectTransform.anchorMax = Vector2.one;
            title.rectTransform.offsetMin = Vector2.zero;
            title.rectTransform.offsetMax = Vector2.zero;
            contents[i] = content.gameObject;
        }

        Select(0);
    }

    void Select(int index)
    {
        for (int i = 0; i < contents.Length; i++)
        {
            contents[i].SetActive(i == index);
            tabImages[i].color = i == index ? TabActive : TabIdle;
        }
    }

    static RectTransform NewRect(string name, Transform parent)
    {
        var go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent, false);
        return go.GetComponent<RectTransform>();
    }

    Text NewText(string name, Transform parent, string text, int size, TextAnchor anchor, bool stretch)
    {
        var rt = NewRect(name, parent);
        if (stretch)
        {
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = new Vector2(2, 2);
            rt.offsetMax = new Vector2(-2, -2);
        }
        var t = rt.gameObject.AddComponent<Text>();
        t.font = font;
        t.text = text;
        t.fontSize = size;
        t.alignment = anchor;
        t.color = Color.white;
        t.horizontalOverflow = HorizontalWrapMode.Wrap;
        t.verticalOverflow = VerticalWrapMode.Overflow;
        t.raycastTarget = false;
        return t;
    }
}
