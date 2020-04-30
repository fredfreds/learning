using UnityEngine;
using UnityEngine.Rendering;

public class AddaptivePerformance : MonoBehaviour
{
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        // When the Menu starts, set the rendering to target 20fps
        OnDemandRendering.renderFrameInterval = 3;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) || (Input.touchCount > 0))
        {
            // If the mouse button or touch detected render at 60 FPS (every frame).
            OnDemandRendering.renderFrameInterval = 1;
        }
        else
        {
            // If there is no mouse and no touch input then we can go back to 20 FPS (every 3 frames).
            OnDemandRendering.renderFrameInterval = 3;
        }
    }
}
