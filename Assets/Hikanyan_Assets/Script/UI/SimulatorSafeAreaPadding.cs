using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[ExecuteAlways]
public class SimulatorSafeAreaPadding : MonoBehaviour
{

    private DeviceOrientation _postOrientation;
    void Update()
    {
        if (Input.deviceOrientation != DeviceOrientation.Unknown && _postOrientation == Input.deviceOrientation)
            return;

        _postOrientation = Input.deviceOrientation;

        var rect = GetComponent<RectTransform>();
        var area = Screen.safeArea;
        var resolition = Screen.currentResolution;

        rect.sizeDelta = Vector2.zero;
        rect.anchorMax = new Vector2(area.xMax / resolition.width, area.yMax / resolition.height);
        rect.anchorMin = new Vector2(area.xMin / resolition.width, area.yMin / resolition.height);
    }
}
