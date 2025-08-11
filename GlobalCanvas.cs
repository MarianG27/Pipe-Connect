using UnityEngine;
using UnityEngine.UI;

public class GlobalCanvas : MonoBehaviour
{
    private static GlobalCanvas instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Evit? dublarea la schimbarea scenei
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // P?streaz? canvas-ul peste scene

        // Asigur?-te c? e scalabil pe orice rezolu?ie
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            CanvasScaler scaler = GetComponent<CanvasScaler>();
            if (scaler == null)
            {
                scaler = gameObject.AddComponent<CanvasScaler>();
            }

            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080); // Po?i schimba rezolu?ia de baz?
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;
        }
    }
}
