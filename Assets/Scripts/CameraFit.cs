using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFit : MonoBehaviour
{
    [Header("화면에 보여줄 맵의 가로 너비")]
    public float targetWidth = 7f; // 현재 인스펙터 설정값 7 유지

    [Header("맵의 중앙 위치")]
    public Vector3 mapCenter = new Vector3(0, 0, -10);

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        ApplyCameraFit();
    }

    void ApplyCameraFit()
    {
        if (cam == null) return;

        // 1. 카메라 위치 고정
        transform.position = mapCenter;

        // 2. 카메라가 사용하는 실제 화면 비율 계산
        // Viewport Rect의 H(높이)와 W(너비)를 반영해야 합니다.
        float viewportAspect = (Screen.width * cam.rect.width) / (Screen.height * cam.rect.height);

        // 3. 맵의 가로를 Viewport 너비에 맞춤
        float requiredSize = (targetWidth / viewportAspect) / 2f;

        if (cam.orthographicSize != requiredSize)
        {
            cam.orthographicSize = requiredSize;
        }
    }
}
